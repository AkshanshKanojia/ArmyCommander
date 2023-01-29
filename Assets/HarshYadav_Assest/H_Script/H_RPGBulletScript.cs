using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_RPGBulletScript : BulletScript
{
    [SerializeField]
    GameObject objToSpawn;// An object will spawn when RPG gets destoryed 
    private void OnDestroy()
    {
        Instantiate(objToSpawn, transform.position, transform.rotation);
    }



}
