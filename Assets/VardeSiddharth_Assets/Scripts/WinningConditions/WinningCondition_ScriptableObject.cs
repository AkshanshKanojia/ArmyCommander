using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WinningCondition_Level",menuName ="ScriptableObject/WinningCondition")]
public class WinningCondition_ScriptableObject : ScriptableObject
{
    public int numberOfEnemiesToKill;

    public void ReduceEnemies()
    {
        if(numberOfEnemiesToKill > 0)
        {
            numberOfEnemiesToKill--;
        }
    }
}
