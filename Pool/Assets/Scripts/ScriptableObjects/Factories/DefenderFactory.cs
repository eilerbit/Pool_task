using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Defender Factory", menuName = "Factories/Defender")]
public class DefenderFactory : GameObjectFactory
{
    [SerializeField] private DefenderSystem horizontalDefenderPrefab;
    [SerializeField] private DefenderSystem verticalDefenderPrefab;

    public virtual DefenderSystem Get(DefenderType type)
    {
        switch (type)
        {
            case DefenderType.Horizontal:
                return CreateGameObjectInstance(horizontalDefenderPrefab);
            case DefenderType.Vertical:
                return CreateGameObjectInstance(verticalDefenderPrefab);
        }

        Debug.LogError($"Defender prefab for: {type} is not found");
        return null;
    }

    
}



