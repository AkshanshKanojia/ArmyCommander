using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWinCondition_VS : MonoBehaviour
{
    public static CheckWinCondition_VS checkWinCondition_VS_instance;

    private void Awake()
    {
        if (checkWinCondition_VS_instance == null)
        {
            checkWinCondition_VS_instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
        numberOfEnemiesToKill = GameObject.FindGameObjectsWithTag("Enemy").Length;
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
