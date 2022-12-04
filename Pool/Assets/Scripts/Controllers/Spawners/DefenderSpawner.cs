using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawner : Spawner
{
    public DefenderSpawner(DefenderFactory factory)
    {
        this.factory = factory;                        

        defenders = new List<GameObject>();
    }

    private DefenderFactory factory;

    private DefenderData defenderData;        

    private List<GameObject> defenders;
        
    public override void Spawn()
    {
        for (int i = 0; i < defenderData.DefenderQuantity; i++)
        {
            Unit = factory.Get(defenderData.DefenderType[i]).gameObject;

            Unit.transform.position = defenderData.SpawnPoints[i].position;
            Unit.transform.rotation = Quaternion.Euler(defenderData.Rotation[i]);

            Unit.GetComponent<DefenderSystem>().Initialize(defenderData.StartPoints[i], defenderData.EndPoints[i], defenderData.DefenderSpeed);

            defenders.Add(Unit);
        }
    }
    
    public void DestroyAllDefenders()
    {
        for (int i = 0; i < defenders.Count; i++)
        {
            Object.Destroy(defenders[i]);
        }
    }

    public void UpdateData(ref DefenderData defenderData)
    {
        this.defenderData = defenderData;
    }
}
