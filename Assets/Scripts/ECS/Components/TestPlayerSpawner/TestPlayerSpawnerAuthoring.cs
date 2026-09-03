using Unity.Entities;
using UnityEngine;

public class TestPlayerSpawnerAuthoring : MonoBehaviour
{
    public GameObject prefab;

    class Baker : Baker<TestPlayerSpawnerAuthoring>
    {
        public override void Bake(TestPlayerSpawnerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);

            AddComponent(entity, new TestPlayerSpawner
            {
                Prefab = GetEntity(
                    authoring.prefab,
                    TransformUsageFlags.Dynamic)
            });
        }
    }
}