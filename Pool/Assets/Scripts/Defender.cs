using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{    
    private void OnCollisionEnter(Collision collisionObject)
    {        
        IRestorable restorable = collisionObject.collider.GetComponent<IRestorable>();

        if (restorable != null) restorable.RestorePosition(tookDamage: true);
    }
}
