using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puck : MonoBehaviour, IRestorable, IMovable, IPausable
{   
    [SerializeField]
    [Tooltip("Puck's minimal speed after wall reflection")]
    private float minVelocity;

    private Vector3 lastFrameVelocity;
    private Vector3 cachedVelocity;
    private Vector3 initialPostion;

    private IPuckSpawner spawner;

    private Rigidbody myRigidbody;
    
    private bool speedHasBeenSet;

    private bool paused;

    private bool velocityWasCached;

    public void Initialize(IPuckSpawner spawner)
    {     
        this.spawner = spawner;

        PauseController.Instance.RegisterPauseClient(this);

        initialPostion = transform.position;

        myRigidbody = GetComponent<Rigidbody>();        
    } 

    private void Update()
    {
        if (paused)
        {
            if (velocityWasCached) return;

            velocityWasCached = true;

            cachedVelocity = myRigidbody.velocity;
            myRigidbody.velocity = Vector3.zero;

            return;
        }
        
        else if (velocityWasCached)
        {
            myRigidbody.velocity = cachedVelocity;
            velocityWasCached = false;
        }
            

        lastFrameVelocity = myRigidbody.velocity;

        CheckStop();
    }

    private void OnCollisionEnter(Collision collision)
    {        
        IBounce bounce = collision.collider.GetComponent<IBounce>();               

        if (bounce != null) Bounce(collision.contacts[0].normal);
    }
       
    public void RestorePosition(bool tookDamage)
    { 
        speedHasBeenSet = false;

        transform.position = initialPostion;

        myRigidbody.velocity = Vector3.zero;

        if (tookDamage) spawner.OnUnitTookDamage();
        else spawner.OnUnitPositionRestore();
    }

    public void Launch(Vector3 initialVelocity, float arrowLenth)
    {
        myRigidbody.velocity = initialVelocity * arrowLenth;
        minVelocity = initialVelocity.magnitude;
        speedHasBeenSet = true;
    }

    public void Pause(bool paused)
    {
        this.paused = paused;
    }

    private void CheckStop()
    {
        if (speedHasBeenSet && lastFrameVelocity.z == 0 && lastFrameVelocity.x == 0)
        {
            RestorePosition(tookDamage: false);
        }
    }    

    private void Bounce(Vector3 collisionNormal)
    {        
        var speed = lastFrameVelocity.magnitude;
        var direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);
                
        myRigidbody.velocity = direction * Mathf.Max(speed, minVelocity);
    }        
}
