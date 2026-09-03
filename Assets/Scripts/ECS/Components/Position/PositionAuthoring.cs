using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PositionAuthoring : MonoBehaviour
{
    public int3 value;

    class Baker : Baker<PositionAuthoring>
    {
        public override void Bake(PositionAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);

            AddComponent(entity, new Position
            {
                value = new int3(authoring.value)
            });
        }
    }
}