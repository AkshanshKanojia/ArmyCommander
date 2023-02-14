using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyOrPlayerBulletScript_VS : BulletScript
{
    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.tag == "Enemy")
    //    {
    //        //Damage Enemy
    //        other.GetComponent<EnemyBehaviour_FSM_VS>().GetDamage(damage: damage);
    //        Destroy(gameObject);
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            collision.collider.GetComponent<EnemyBehaviour_FSM_VS>().GetDamage(damage: damage);
            Destroy(gameObject);
        }
    }
}
