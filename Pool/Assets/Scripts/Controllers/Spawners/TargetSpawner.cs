using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : Spawner, ITargetSpawner
{
    public TargetSpawner(TargetFactory factory, IManager manager)
    {
        this.factory = factory;
                
        this.manager = manager;
    }
        
    private TargetFactory factory;

    private TargetData targetData;
    
    private IManager manager;

    private int targetQuantity;


    public override void Spawn()
    {
        for (int i = 0; i < targetData.TargetQuantity; i++)
        {
            Unit = factory.Get(targetData.TargetType[i]).gameObject;

            Unit.transform.position = targetData.SpawnPoints[i].position;

            Unit.GetComponent<Target>().Initialize(this);
        } 
    }

    public void OnUnitDestroy()
    {        
        targetQuantity--;

        if (targetQuantity > 0) return;

        else manager.StartNextLevel();
    }

    public void  UpdateData(ref TargetData targetData)
    {
        this.targetData = targetData;
        
        targetQuantity = targetData.TargetQuantity;
    }
}
