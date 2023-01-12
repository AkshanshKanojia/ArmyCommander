using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWinCondition : MonoBehaviour
{
    [SerializeField]
    WinningCondition_ScriptableObject currentWinCondition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnemyKilled()
    {
        currentWinCondition.ReduceEnemies();

        if(currentWinCondition.numberOfEnemiesToKill <= 0)
        {
            //activate the logic for player to win
        }
    }
}
