using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    protected float speed = 10f, range = 20, damage = 3.0f;// speed and Bullet Life Time
    float lifeTime;//Bullet Life time

    private void Start()
    {
        lifeTime = range / speed;
        Destroy(gameObject, lifeTime);
    }


    // Update is called once per frame
    void Update()
    {

        transform.position += transform.forward*speed*Time.deltaTime;// Moving Bullet forward\\

    }
}
