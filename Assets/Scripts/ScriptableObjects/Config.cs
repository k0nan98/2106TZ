using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelConfig", menuName = "LevelConfig", order = 51)]
public class Config : ScriptableObject
{
    [SerializeField]
    public int spawnRadius = 20;
}
