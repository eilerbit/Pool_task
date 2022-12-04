using System.Collections;
using UnityEngine;

public interface IDraggable
{
    public void Drag(float x, float y) { }

    public void Release() { }

    public Vector3 TargetDirection { get; }

    public float ArrowLenth { get; }
}
