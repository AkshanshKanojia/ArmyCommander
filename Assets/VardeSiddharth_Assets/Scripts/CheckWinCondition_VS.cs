using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWinCondition_VS : MonoBehaviour
{
    //[SerializeField]
    //WinningCondition_ScriptableObject currentWinCondition;

    //[SerializeField]
    int numberOfEnemiesToKill;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(gameObject.name);
        //if(numberOfEnemiesToKill == 0)
        //{
        //    Debug.Log("Define The number of enemies to kill");
        //}
        numberOfEnemiesToKill = GameObject.FindGameObjectsWithTag("Enemy").Length + 1;

    }

    // Update is called once per frame
    void Update()
    {
        //Uncomment This code only For testing
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    OnEnemyKilled();
        //}
    }

    public void OnEnemyKilled()
    {
        //currentWinCondition.ReduceEnemies();
        ReduceEnemies();
        if(numberOfEnemiesToKill <= 0)
        {
            //activate the logic for player to win
            BoxCollider flagCollider;
            flagCollider = GameObject.FindGameObjectWithTag("WinFlag").GetComponent<BoxCollider>();
            flagCollider.isTrigger = true;
            Debug.Log("You can Load next level by reaching the flag");
        }
    }

    void ReduceEnemies()
    {
        if(numberOfEnemiesToKill > 0)
        {
            numberOfEnemiesToKill--;
        }
    }
}
