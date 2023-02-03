using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript_VS : BulletScript
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //Damage the player
            other.GetComponent<PlayerInventory_VS>().GetDamage(damage: damage);
            Destroy(gameObject);
        }
        else if(other.tag == "Ally")
        {
            //Damage Ally
            other.GetComponent<AllyBehaviour_FSM_VS>().GetDamage(damage: damage);
            Destroy(gameObject);
        }
    }
}
