using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private ITargetSpawner spawner;

    public void Initialize(ITargetSpawner spawner)
    {
        this.spawner = spawner;
    }

    private void OnCollisionEnter(Collision collisionObject)
    {
        IRestorable restorable = collisionObject.collider.GetComponent<IRestorable>();

        if (restorable != null)
        {
            restorable.RestorePosition(tookDamage: false);
            spawner.OnUnitDestroy();
            Destroy(gameObject);
        }
    }
}
