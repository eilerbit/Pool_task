using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Target Factory", menuName = "Factories/Taget")]
public class TargetFactory : GameObjectFactory
{
    [SerializeField] private Target rectangularTargetPrefab;
    [SerializeField] private Target triangularTargetPrefab;
    [SerializeField] private Target CylindricTargetPrefab;

    public virtual Target Get(TargetType type)
    {
        switch (type)
        {
            case TargetType.Rectangular:
                return CreateGameObjectInstance(rectangularTargetPrefab);
            case TargetType.Triangular:
                return CreateGameObjectInstance(triangularTargetPrefab);
            case TargetType.Cylindric:
                return CreateGameObjectInstance(CylindricTargetPrefab);
        }

        Debug.LogError($"Target prefab for: {type} is not found");
        return null;
    }
}
