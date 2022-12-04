using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSystem : MonoBehaviour, IPausable
{
    [SerializeField] private Transform defenderTransform;
        
    private Vector3 startPoint;    
    private Vector3 endPoint;     
    private Vector3 targetPosition;    
        
    private float speed;

    private bool positionHasBeenSwapped;

    private bool paused;

    public void Initialize(Vector3 startPoint, Vector3 endPoint, float speed)
    {  
        this.startPoint = startPoint;
        this.endPoint = endPoint;
        this.speed = speed;

        PauseController.Instance.RegisterPauseClient(this);

        targetPosition = endPoint;
    }    

    private void Update()
    {
        if (paused) return;

        defenderTransform.position = Vector3.MoveTowards(defenderTransform.transform.position, targetPosition, speed * Time.deltaTime);

        if(defenderTransform.position == targetPosition) targetPosition = SwapPosition();
    }

    public void Pause(bool paused)
    {
        this.paused = paused;
    }

    private Vector3 SwapPosition()
    {
        positionHasBeenSwapped = !positionHasBeenSwapped;

        if(positionHasBeenSwapped) return startPoint;
        else return endPoint;
    }    
}
