using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot_VS : MonoBehaviour
{
    [SerializeField]
    GunScript playerGun;
    [SerializeField]
    float playerShootrange = 5;

    Transform enemyToShoot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyToShoot != null)
        {
            transform.parent.GetComponent<PlayerController_VS>().SetShouldRotate(false);
            transform.parent.rotation = Quaternion.LookRotation(enemyToShoot.position - transform.position);
            //transform.rotation = Quaternion.LookRotation(enemyToShoot.position - transform.position);
            //transform.localRotation = Quaternion.Euler(0, (enemyToShoot.position - transform.position).y, 0);
            playerGun.StartShoot();
        }
        else
        {
            transform.parent.GetComponent<PlayerController_VS>().SetShouldRotate(true);
            //transform.rotation = Quaternion.LookRotation(transform.parent.forward);
            playerGun.StopShoot();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ally")
        {
            other.GetComponent<AllyBehaviour_FSM_VS>().SetIsBuffed(isBuffed: true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy" && enemyToShoot == null && ((transform.position - other.transform.position).magnitude < playerShootrange))
        {
            enemyToShoot = other.transform;
            Debug.Log("Start Shooting");
            //playerGun.StartShoot();
        }

        //if (other.tag == "Ally")
        //{
        //    other.GetComponent<AllyBehaviour_FSM_VS>().SetIsBuffed(isBuffed: true);
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ally")
        {
            other.GetComponent<AllyBehaviour_FSM_VS>().SetIsBuffed(isBuffed: false);
        }

        if (other.tag == "Enemy")
        {
            enemyToShoot = null;
            //transform.rotation = Quaternion.Euler(Vector3.zero);
            //playerGun.StopShoot();
        }
    }
}
