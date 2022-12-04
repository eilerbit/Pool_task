using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public abstract class Spawner
{    
    [HideInInspector] public GameObject Unit; 

    public abstract void Spawn();

    void A()
    {
        Level a = new Level();
        a.TargetData = new TargetData();
                
    }
}
