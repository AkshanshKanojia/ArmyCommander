using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum AllyStates
{
    Upgrade,
    Formation,
    MoveTowardsEnemy,
    Fight,
    Deth
};

public class AllyBehaviour_FSM_VS : MonoBehaviour
{
    AllyStates allyState = new AllyStates();

    Transform targetTransform;
    Transform enemyToShootTowards;
    NavMeshAgent navMeshAgent;
    //Vector3 targetPosition;
    int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        allyState = AllyStates.Upgrade;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(allyState)
        {
            case AllyStates.Upgrade:
                OnUpgradeState();
                break;
            case AllyStates.Formation:
                OnFormationState();
                break;
            case AllyStates.MoveTowardsEnemy:
                OnMoveTowardsEnemyState();
                break;
            case AllyStates.Fight:
                OnFightState();
                break;
            case AllyStates.Deth:
                OnDethState();
                break;
        }
    }

    void OnUpgradeState()
    {
        if(targetTransform == null)
        {
            //try to find the upgrade guns matchine gameobject
            targetTransform = GameObject.FindGameObjectWithTag("UpgradeWepon").transform;
            //Debug.Log(targetTransform.position + "  Name " + targetTransform.name);
        }

        if(targetTransform == null)
        {
            allyState = AllyStates.Formation;
            return;
        }
        else
        {
            //go to upgrade guns matchine gameobject
            navMeshAgent.destination = targetTransform.position;
            Debug.Log("Moving");
            //when reached 
            if (Vector3.Distance(transform.position, targetTransform.position) < 1)
            {
                //upgrade the wepon
                targetTransform = null;
                Debug.Log("Formation state start");

                allyState = AllyStates.Formation;
            }
            //Access the shooting script and upgrade the wepon of ally
            //change target transform = null;
            //change the allyState to AllyStates.Formation
        }
    }

    void OnFormationState()
    {
        if(targetTransform == null)
        {
            //try to get the position to stand in a grid
            targetTransform = transform.parent; //GameObject.FindGameObjectWithTag("PlayerGroup").transform;
            Debug.Log(targetTransform.name);
        }

        if(targetTransform != null)
        {
            // move towards target transform
            navMeshAgent.destination = targetTransform.position;
            //when reached stop on the position and subscribe to the attackCommandEvent in player
            // and wait for it to be triggered
            // set the target transform to null
            // when triggered change allyState to Move towards Enemy
        }
    }

    void OnMoveTowardsEnemyState()
    {
        if(targetTransform == null)
        {
            // try to get the position of the enemy group that is nearest
        }

        if(targetTransform != null)
        {
            // move towards enemy group
            // when reached at a certain distance stop
            // change allyState to Fight
        }

        if(health <= 0)
        {
            allyState = AllyStates.Deth;
        }
    }

    void OnFightState()
    {
        if(targetTransform == null)
        {
            // try to get the enemy group to attack that is nearest
        }

        if(targetTransform != null)
        {
            if (enemyToShootTowards == null)
            {
                if (targetTransform.childCount > 0)
                {
                    enemyToShootTowards = targetTransform.GetChild(Random.Range(0, targetTransform.childCount));
                }
                else
                {
                    //Destroy target transform
                    allyState = AllyStates.MoveTowardsEnemy;
                }
            }

            if(enemyToShootTowards != null)
            {
                //shoot towards enemy
            }
        }

        if(health <= 0)
        {
            allyState = AllyStates.Deth;
        }
    }

    void OnDethState()
    {
        // spawn Dogtag
        //  Destroy gameObject
    }

}
