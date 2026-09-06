using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;
using Unity.Transforms;

[UpdateInGroup(typeof(PredictedSimulationSystemGroup))]
public partial struct PlayerMovementSystem : ISystem
{
    private const float Speed = 6f;

    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        foreach (var (transform, input) in
                 SystemAPI.Query<RefRW<LocalTransform>, RefRO<PlayerInput>>()
                          .WithAll<Simulate>())
        {
            float2 move = input.ValueRO.Move;

            if (math.lengthsq(move) < 0.0001f)
                continue;

            float3 direction = new float3(move.x, 0f, move.y);
            transform.ValueRW.Position += direction * Speed * deltaTime;
        }
    }
}