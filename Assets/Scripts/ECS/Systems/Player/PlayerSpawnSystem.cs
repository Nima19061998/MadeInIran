using Unity.Collections;
using Unity.Entities;
using Unity.NetCode;
using Unity.Transforms;

[WorldSystemFilter(WorldSystemFilterFlags.ServerSimulation)]
public partial struct PlayerSpawnSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<PlayerSpawner>();
    }

    public void OnUpdate(ref SystemState state)
    {
        var prefab = SystemAPI.GetSingleton<PlayerSpawner>().Prefab;

        var commandBuffer = new EntityCommandBuffer(Allocator.Temp);

        // Spawn a player for every new connection that doesn't have a player yet
        foreach (var (connection, entity) in
                 SystemAPI.Query<RefRO<NetworkId>>()
                          .WithNone<NetworkStreamInGame>()
                          .WithEntityAccess())
        {
            // Mark connection as in-game
            commandBuffer.AddComponent<NetworkStreamInGame>(entity);

            // Spawn the player
            var player = commandBuffer.Instantiate(prefab);

            // Set the owner of this player
            commandBuffer.SetComponent(player, new GhostOwner
            {
                NetworkId = connection.ValueRO.Value
            });

            // Optional: Set starting position
            commandBuffer.SetComponent(player, LocalTransform.FromPosition(0, 1, 0));

            // Link the player to the connection so it gets destroyed on disconnect
            commandBuffer.AppendToBuffer(entity, new LinkedEntityGroup { Value = player });
        }

        commandBuffer.Playback(state.EntityManager);
        commandBuffer.Dispose();
    }
}