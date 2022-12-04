using System;
using UnityEngine;

[Serializable]
public struct DefenderData
{
    [SerializeField]
    public int DefenderQuantity;

    public float DefenderSpeed;

    public DefenderType[] DefenderType;

    public Transform[] SpawnPoints;

    public Vector3[] StartPoints;
    public Vector3[] EndPoints;

    public Vector3[] Rotation;
}