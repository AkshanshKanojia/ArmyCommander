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
                OnFormationState();
                break;
            case EnemyBehaviurStates.Fight:
                OnFightState();
                break;
            case EnemyBehaviurStates.Deth:
                OnDeathState();
                break;
        }
    }

    void OnFormationState()
    {
        if(health <= 0)
        {
            enemyBehaviourState = EnemyBehaviurStates.Deth;
            return;
        }

        if(targetForEnemy != null)
        {
            enemyBehaviourState = EnemyBehaviurStates.Fight;
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Change the tag to the tag that you have decided to put on group of player Ally Troops
        Debug.Log(other.name);

        if (other.tag == "Ally")
        {
            targetForEnemy = other.transform.parent;
            Debug.Log(targetForEnemy.name);

        }
        else if (other.tag == "Player" && targetForEnemy == null)
        {
            targetForEnemy = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player" && targetForEnemy.tag == "Player")
        {
            targetForEnemy = null;
        }
        
    }

    void OnFightState()
    {
        if(targetForEnemy == null)
        {
            enemyBehaviourState = EnemyBehaviurStates.Formation;
            return;
        }
        
        if(health <= 0)
        {
            enemyBehaviourState = EnemyBehaviurStates.Deth;
            return;
        }

        // shoot towards player
    }

    void OnDeathState()
    {
        //spawn gold
        //reduce the no. of enemies to kill from Check win condition script

        Destroy(gameObject);
    }
}
