using Unity.Entities;
using Unity.NetCode;
using Unity.Mathematics;

public struct PlayerInput : IInputComponentData
{
    public int2 move;
}