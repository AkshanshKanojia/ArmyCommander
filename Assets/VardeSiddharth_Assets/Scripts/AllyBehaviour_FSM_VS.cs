using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using LevelEditor;


public enum AllyStates
{
    Upgrade,
    Formation,
    MoveTowardsEnemy,
    Fight,
    Death,
    Win
};

public class AllyBehaviour_FSM_VS : MonoBehaviour
{
    AllyStates allyState = new AllyStates();

    Transform targetTransform;
    Transform enemyToShootTowards;
    NavMeshAgent navMeshAgent;
    //Vector3 targetPosition;
    [SerializeField]
    float health = 100;
    [SerializeField]
    float range = 5;

    public static int indexToAsign = 0;
    public int index = -1;

    [SerializeField]
    int activeGunIndex = 0;
    GunScript gunScript;
    PlayerInventory_VS player;

    [SerializeField]
    GameObject dogTagPrefabToSpawn;

    private void Awake()
    {
        //index = indexToAsign - 1;
        gunScript = transform.GetChild(activeGunIndex).GetComponent<GunScript>();
        gunScript.gameObject.SetActive(true);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory_VS>();
        
        //player.OnPlayerAttckDelegateEvent += StartAttack;
    }

    // Start is called before the first frame update
    void Start()
    {
        index = -1;
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
            case AllyStates.Death:
                OnDethState();
                break;
            case AllyStates.Win:
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
            //Debug.Log("Moving");
            //when reached 
            if (Vector3.Distance(transform.position, targetTransform.position) < 1)
            {
                //upgrade the wepon
                gunScript.StopShoot();
                gunScript.gameObject.SetActive(false);
                activeGunIndex++;
                gunScript = transform.GetChild(activeGunIndex).GetComponent<GunScript>();
                gunScript.gameObject.SetActive(true);
                targetTransform = null;
                //Debug.Log("Formation state start");
                
                allyState = AllyStates.Formation;
            }
            //Access the shooting script and upgrade the wepon of ally
            //change target transform = null;
            //change the allyState to AllyStates.Formation
        }
    }

    void OnFormationState()
    {
        if(index < 0)
        {
            indexToAsign++;
            index = indexToAsign - 1;
            player.OnPlayerAttckDelegateEvent += StartAttack;

        }

        if(targetTransform == null)
        {
            //try to get the position to stand in a grid
            targetTransform = transform.parent; //GameObject.FindGameObjectWithTag("PlayerGroup").transform;
            //Debug.Log(targetTransform.name);
        }

        if(targetTransform != null)
        {
            // move towards target transform
            navMeshAgent.destination = targetTransform.GetComponent<GridManager>().gridVertices[index];
            //when reached stop on the position and subscribe to the attackCommandEvent in player
            // and wait for it to be triggered
            // set the target transform to null
            // when triggered change allyState to Move towards Enemy

            if(Input.GetKeyDown(KeyCode.Space))
            {
                StartAttack();
            }
        }
    }

    /*
     * This function sets the conditions of Attacking 
     * and by that it makes player troop ready to attack
     */
    void StartAttack()
    {
        targetTransform = null;
        index = -1;
        indexToAsign = 0;
        player.OnPlayerAttckDelegateEvent -= StartAttack;
        FindObjectOfType<PlayerTroopsHolder_VS>().OnTroopAttackCalled();
        allyState = AllyStates.MoveTowardsEnemy;
    }

    void OnMoveTowardsEnemyState()
    {
        gunScript.StopShoot();
        if(targetTransform == null)
        {
            // try to get the position of the enemy group that is nearest
            targetTransform = GetTheNearestEnemyGroup();
            if(targetTransform == null)
            {
                allyState = AllyStates.Win;
                return;
            }
        }

        if(targetTransform != null)
        {
            // move towards enemy group
            // when reached at a certain distance stop
            // change allyState to Fight
            navMeshAgent.destination = targetTransform.position;
            navMeshAgent.stoppingDistance = range;

            if(Vector3.Distance(transform.position, targetTransform.position) < range)
            {
                allyState = AllyStates.Fight;
                return;
            }
        }

        if(health <= 0)
        {
            allyState = AllyStates.Death;
            return;
        }
    }


    /*
     * This is the function that finds the nearest group of enemies from 
     * player grid / Player Ally Group GameOn=bject
     */
    Transform GetTheNearestEnemyGroup()
    {
        GameObject[] enemiesTransforms = GameObject.FindGameObjectsWithTag("EnemyGroup");
        Transform nearestEnemyGroup = null;
        if (enemiesTransforms.Length > 0)
        {
            nearestEnemyGroup = enemiesTransforms[0].transform;
        }
        for (int i = 0; i < enemiesTransforms.Length; i++)
        {
            float nearestDistance = Vector3.Distance(transform.parent.position, nearestEnemyGroup.position);
            float currentDistance = Vector3.Distance(transform.parent.position, enemiesTransforms[i].transform.position);
            if (currentDistance < nearestDistance)
            {
                nearestEnemyGroup = enemiesTransforms[i].transform;
            }
        }
        return nearestEnemyGroup;
    }

    void OnFightState()
    {
        if(targetTransform == null)
        {
            gunScript.StopShoot();
            allyState = AllyStates.MoveTowardsEnemy;
            Debug.Log("Fight to movetowards");
            return;
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
                    Destroy(targetTransform.gameObject);
                    allyState = AllyStates.MoveTowardsEnemy;
                    return;
                }
            }

            if(enemyToShootTowards != null)
            {
                //Rotate towards enemy target then shoot
                //shoot towards enemy
                transform.rotation = Quaternion.LookRotation(enemyToShootTowards.position - transform.position);
                //shoot towards enemy
                gunScript.StartShoot();
            }
        }

        if(health <= 0)
        {
            allyState = AllyStates.Death;
            return;
        }
    }

    void OnDethState()
    {
        // spawn Dogtag
        if(dogTagPrefabToSpawn != null)
        {
            Instantiate(dogTagPrefabToSpawn, transform.position + Vector3.up, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Dog tag prefab to spawn is null");
        }

        //  Destroy gameObject
        player.OnPlayerAttckDelegateEvent -= StartAttack;
        gunScript.StopShoot();
        //FindObjectOfType<PlayerTroopsHolder_VS>().OnTroopDied();
        Destroy(gameObject);
    }


    public void GetDamage(float damage)
    {
        health -= damage;
    }
}
