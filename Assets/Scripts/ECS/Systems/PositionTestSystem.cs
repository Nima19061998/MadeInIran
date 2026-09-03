using Unity.Burst;
using Unity.Entities;
using Unity.NetCode;

[BurstCompile]
[WorldSystemFilter(WorldSystemFilterFlags.ServerSimulation)]
public partial struct PositionTestSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        new MovePositionJob().ScheduleParallel();
    }

    [BurstCompile]
    public partial struct MovePositionJob : IJobEntity
    {
        private void Execute(ref Position position)
        {
            position.value.x += 1;
        }
    }
}