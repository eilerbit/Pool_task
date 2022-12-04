using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class PuckSpawner : Spawner, IPuckSpawner
{
    public PuckSpawner(Puck puckPrefab, Vector3 spawnPoint, IManager manager)
    {
        this.puckPrefab = puckPrefab;
                
        this.spawnPoint = new Vector3(spawnPoint.x, spawnPoint.y + SpawnOffsetY, spawnPoint.z);

        this.manager = manager;

        Spawn();
    }
        
    private const float SpawnOffsetY = 0.08f;

    private Puck puckPrefab;

    private Vector3 spawnPoint;

    private IManager manager;
        

    public override void Spawn()
    {
        Unit = Object.Instantiate(puckPrefab.gameObject, spawnPoint, Quaternion.Euler(90, 0, 0));

        Unit.GetComponent<Puck>().Initialize(this);        
    }

    public void OnUnitPositionRestore()
    {
        manager.Restart();
    }

    public void OnUnitTookDamage()
    {
        manager.OnPlayerLostHitpoint();
        manager.Restart();
    }
}
