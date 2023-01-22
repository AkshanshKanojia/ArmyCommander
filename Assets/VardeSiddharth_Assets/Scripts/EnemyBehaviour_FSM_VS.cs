using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyBehaviurStates
{
    Formation,
    Fight,
    Deth
}

public class EnemyBehaviour_FSM_VS : MonoBehaviour
{
    int health = 100;
    Transform targetForEnemy;
    EnemyBehaviurStates enemyBehaviourState = new EnemyBehaviurStates();

    // Start is called before the first frame update
    void Start()
    {
        enemyBehaviourState = EnemyBehaviurStates.Formation;
    }

    // Update is called once per frame
    void Update()
    {
        switch(enemyBehaviourState)
        {
            case EnemyBehaviurStates.Formation:
                break;
            case EnemyBehaviurStates.Fight:
                break;
            case EnemyBehaviurStates.Deth:
                break;
        }
    }

    void OnFormationState()
    {
        if(health <= 0)
        {
            enemyBehaviourState = EnemyBehaviurStates.Deth;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Change the tag to the tag that you have decided to put on group of player Ally Troops
        //if(other.tag == "PlayerTroopTag")
        //{
        //    targetForEnemy = other.transform;
        //}
        //else if(other.tag == "Player")
        //{
        //    targetForEnemy = other.transform;
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            targetForEnemy = null;
            enemyBehaviourState = EnemyBehaviurStates.Formation;
        }
        //else if(other.tag == "")
    }

    void OnFightState()
    {

    }
}
