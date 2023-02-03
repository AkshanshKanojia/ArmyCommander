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
    float health = 100;
    Transform targetForEnemy;
    EnemyBehaviurStates enemyBehaviourState = new EnemyBehaviurStates();

    [SerializeField]
    int weponIndex = 0;
    GunScript gunScriptOfEnemy;

    // Start is called before the first frame update
    void Start()
    {
        enemyBehaviourState = EnemyBehaviurStates.Formation;
        gunScriptOfEnemy = transform.GetChild(weponIndex).GetComponent<GunScript>();
        gunScriptOfEnemy.gameObject.SetActive(true);
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
        gunScriptOfEnemy.StopShoot();
        if (health <= 0)
        {
            enemyBehaviourState = EnemyBehaviurStates.Deth;
            return;
        }

        if (targetForEnemy != null)
        {
            enemyBehaviourState = EnemyBehaviurStates.Fight;
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Change the tag to the tag that you have decided to put on group of player Ally Troops

        //Debug.Log(other.name);

        if (other.tag == "Ally")
        {
            targetForEnemy = other.transform.parent.GetChild(Random.Range(0, other.transform.parent.childCount));
            //Debug.Log(other.transform.parent.childCount);
            //Debug.Log(targetForEnemy.name);

        }
        else if (other.tag == "Player" && targetForEnemy == null)
        {
            targetForEnemy = other.transform;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Ally" && targetForEnemy == null)
        {
            targetForEnemy = other.transform.parent.GetChild(Random.Range(0, other.transform.parent.childCount));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (targetForEnemy != null)
        {
            if (other.tag == "Player" && targetForEnemy.tag == "Player")
            {
                targetForEnemy = null;
            }
        }
        

        if(other.tag == "Ally")
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

        if (health <= 0)
        {
            enemyBehaviourState = EnemyBehaviurStates.Deth;
            return;
        }

        // shoot towards player
        transform.rotation = Quaternion.LookRotation(targetForEnemy.position - transform.position);
        gunScriptOfEnemy.StartShoot();
    }

    void OnDeathState()
    {
        //spawn gold
        //reduce the no. of enemies to kill from Check win condition script
        CheckWinCondition_VS.checkWinCondition_VS_instance.OnEnemyKilled();
        gunScriptOfEnemy.StopShoot();
        Destroy(gameObject);
    }

    public void GetDamage(float damage)
    {
        health -= damage;
    }

}
