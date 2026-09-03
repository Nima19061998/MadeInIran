using Unity.Burst;
using Unity.Entities;
using Unity.NetCode;

[BurstCompile]
[WorldSystemFilter(WorldSystemFilterFlags.ServerSimulation)]
public partial struct TestPlayerSpawnSystem : ISystem
{
    private bool spawned;

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        if (spawned)
            return;

        var spawner =
            SystemAPI.GetSingleton<TestPlayerSpawner>();

        Entity player =
            state.EntityManager.Instantiate(spawner.Prefab);

        state.EntityManager.SetComponentData(
            player,
            new Position
            {
                value = new Unity.Mathematics.int3(0, 0, 0)
            });

        spawned = true;
    }
}