using System;
using UnityEngine;

[Serializable]
public struct TargetData
{
    public int TargetQuantity;

    public TargetType[] TargetType;

    public Transform[] SpawnPoints;
}