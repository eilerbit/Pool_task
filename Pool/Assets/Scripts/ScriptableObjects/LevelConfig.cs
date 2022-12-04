using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Config", menuName = "Configs/Levels")]
public class LevelConfig : ScriptableObject
{
    public Level[] Levels;    
}
