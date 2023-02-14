using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGDamagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float life = 1;
    [SerializeField]
    int damage = 3;
    void Start()
    {
        Destroy(gameObject, life);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Enemy")
        {
            //Damage Enemy.
            other.GetComponent<EnemyBehaviour_FSM_VS>().GetDamage(damage: damage);
        }
        //Debug.Log(other.name);
        
    }
}
