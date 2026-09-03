using Unity.Entities;
using Unity.NetCode;
using Unity.Mathematics;

[UpdateInGroup(typeof(PredictedSimulationSystemGroup))]
[WorldSystemFilter(WorldSystemFilterFlags.ServerSimulation)]
public partial struct PlayerMovementSystem : ISystem
{
    private const int SPEED_CM_PER_SECOND = 300;

    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        foreach (var (position, input) in
                 SystemAPI.Query<
                     RefRW<Position>,
                     RefRO<PlayerInput>>())
        {
            int2 move = input.ValueRO.move;

            if (move.x == 0 && move.y == 0)
                continue;

            int delta = (int)(SPEED_CM_PER_SECOND * deltaTime);

            position.ValueRW.value.x += move.x * delta;
            position.ValueRW.value.z += move.y * delta;
        }
    }
}