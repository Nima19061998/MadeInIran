using Unity.Entities;
using Unity.NetCode;
using Unity.Mathematics;
using UnityEngine;

[UpdateInGroup(typeof(GhostInputSystemGroup))]
[WorldSystemFilter(WorldSystemFilterFlags.ClientSimulation)]
public partial struct GatherPlayerInputSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<PlayerInput>();
    }

    public void OnUpdate(ref SystemState state)
    {
        float x = 0f;
        float z = 0f;

        if (Input.GetKey(KeyCode.A))
            x -= 1f;

        if (Input.GetKey(KeyCode.D))
            x += 1f;

        if (Input.GetKey(KeyCode.S))
            z -= 1f;

        if (Input.GetKey(KeyCode.W))
            z += 1f;

        int2 move = new int2(
            math.clamp((int)x, -1, 1),
            math.clamp((int)z, -1, 1)
        );

        foreach (var input in
                 SystemAPI.Query<RefRW<PlayerInput>>()
                 .WithAll<GhostOwnerIsLocal>())
        {
            input.ValueRW.move = move;
        }
    }
}