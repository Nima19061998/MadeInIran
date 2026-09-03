using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;

public struct Position : IComponentData
{
    [GhostField]
    public int3 value;
}