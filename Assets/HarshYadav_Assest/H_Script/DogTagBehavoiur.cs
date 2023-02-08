using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogTagBehavoiur : MonoBehaviour
{
     
    public Transform target;
    
    [SerializeField]
    float speed;
    bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && isMoving)
        {

            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, target.position)<=0.2f)
            {
                /* isMoving = false;
                 transform.parent = target;
                 target.GetComponent<DogTagPlacement>().PlaceDogTag(transform);*/
                if(transform.tag=="Gold")
                {
                    target.GetComponent<CollectableUpdate>().AddGold();
                }
                else if(transform.tag == "dogTag")
                {
                    target.GetComponent<CollectableUpdate>().AddDogTag();
                }
                Destroy(gameObject);
            }
        }

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            target = other.transform.GetChild(0);
            isMoving = true;
            print(other.transform.GetChild(0).name);
            Destroy(GetComponent<Rigidbody>());
            //Destroy(GetComponent<BoxCollider>());
            BoxCollider[] boxColliders = GetComponents<BoxCollider>();
            for(int i = 0; i < boxColliders.Length; i++)
            {
                if(boxColliders[i] != this)
                {
                    Destroy(boxColliders[i]);
                }
            }
        }
    }
        





        
 
   



}
