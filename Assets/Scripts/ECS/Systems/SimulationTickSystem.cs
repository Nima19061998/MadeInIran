using Unity.Burst;
using Unity.Entities;

[BurstCompile]
public partial struct SimulationTickSystem : ISystem
{
    private double accumulator;
    private double tickDelta;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        accumulator = 0.0;
        tickDelta = 1.0 / Constants.Simulations.TICK_RATE;

        // Singleton
        state.EntityManager.CreateSingleton(new SimulationTick
        {
            Value = 0
        });
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        accumulator += SystemAPI.Time.DeltaTime;

        while (accumulator >= tickDelta)
        {
            accumulator -= tickDelta;

            ref SimulationTick tick =
                ref SystemAPI.GetSingletonRW<SimulationTick>().ValueRW;

            tick.Value++;
        }
    }
}