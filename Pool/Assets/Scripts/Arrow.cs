using System;
using System.Collections;
using UnityEngine;


public class Arrow : MonoBehaviour, IDraggable
{
    [SerializeField] private Transform pointerTransform; 
    [SerializeField] private float minX = -1.15f, maxX = 1.15f;
    [SerializeField] private float minZ = -8.5f, maxZ = -6.5f;

    [SerializeField] private float speedModifier = 0.01f;

    public Vector3 TargetDirection { get; private set; }

    public float ArrowLenth { get; private set; }

    private SpriteRenderer spriteRenderer;

    private Vector3 initialPosition;
    private Vector3 startPosition;
    private Vector3 dragPosition;        

    private float factor = 0.075f;

    public void Initialize(Vector3 spawnPointPosition)
    {
        initialPosition = transform.position;

        startPosition = spawnPointPosition;
        
        spriteRenderer = GetComponent<SpriteRenderer>();

        HideArrow();
    }

    public void Drag(float inputX, float inputZ)
    {   
        float xDrag = Mathf.Clamp(pointerTransform.position.x + inputX * speedModifier, minX, maxX);
        float zDrag = Mathf.Clamp(pointerTransform.position.z + inputZ * speedModifier, minZ, maxZ);
                
        dragPosition = new Vector3(xDrag, pointerTransform.position.y, zDrag);

        pointerTransform.position = dragPosition;
                        
        TargetDirection = dragPosition - startPosition;        

        DisplayArrow();
        SetArrowPosition();
    }        

    public void Release()
    {
        HideArrow();
        transform.position = initialPosition;
    }    

    private void DisplayArrow()
    {
        spriteRenderer.enabled = true;
    }

    private void HideArrow()
    {
        spriteRenderer.enabled = false;
    }
       
    private void SetArrowPosition()
    {
        var direction = dragPosition - startPosition;
        var middlePosition = direction / 2.0f + startPosition;

        transform.position = new Vector3(middlePosition.x, middlePosition.y + 0.2f, middlePosition.z);
        transform.rotation = Quaternion.FromToRotation(Vector3.forward, direction);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + 90, transform.rotation.eulerAngles.y - 90, transform.rotation.eulerAngles.z);

        Vector3 scale = transform.localScale;
        scale.x = direction.magnitude * factor;
        transform.localScale = scale;

        ArrowLenth = 3 * direction.magnitude;
    }
}
