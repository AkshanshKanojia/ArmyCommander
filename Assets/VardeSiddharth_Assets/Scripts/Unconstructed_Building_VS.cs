using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unconstructed_Building_VS : MonoBehaviour
{
    [SerializeField]
    int requiredCurrencyToBuild = 10;
    [SerializeField]
    GameObject buildingToCreatePrefab;
    [SerializeField]
    Vector3 offsetToCreateBuilding = Vector3.up * 2;
    [SerializeField]
    float timeTowait = 0.3f;

    float currentTimeToWait;

    //[SerializeField]
    //PlayerInventory_VS playerInventoryComponent;
    CollectableUpdate collectableUpdateComponent;



    // Start is called before the first frame update
    void Start()
    {
        currentTimeToWait = timeTowait;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //playerInventoryComponent = other.GetComponent<PlayerInventory_VS>();
            collectableUpdateComponent = other.transform.GetChild(0).GetComponent<CollectableUpdate>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            //remove player points if exists

            if(collectableUpdateComponent != null)
            {
                currentTimeToWait -= Time.deltaTime;
                if (collectableUpdateComponent.GetDogTag() > 0 && currentTimeToWait < 0)
                {
                    //playerInventoryComponent.useCurrentPoints();
                    collectableUpdateComponent.RemoveDogTag();
                    requiredCurrencyToBuild--;
                    currentTimeToWait = timeTowait;

                    if(requiredCurrencyToBuild <= 0)
                    {
                        CreateTheBuilding();
                    }
                }
            }
        }
    }

    void CreateTheBuilding()
    {
        Instantiate(buildingToCreatePrefab, transform.position + offsetToCreateBuilding, Quaternion.identity);

        Destroy(gameObject);
    }
}
