using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerTroopsHolder_VS : MonoBehaviour
{
    public List<UpgradeOrderOfTroopsHolder> upgradeOrderList = new List<UpgradeOrderOfTroopsHolder>();

    [SerializeField]
    int currentLevel = 0;
    [SerializeField]
    int currentNumberOfTroops;
    //[SerializeField]
    //int maxNumberOfTroops = 30;
    //[SerializeField]
    //int increaseAmountOfTroopsBy = 5;
    [SerializeField]
    int pointsRequireToIncreaseNumberOfTroops;

    [SerializeField]
    float timeToWait = 0.2f;
    [SerializeField]
    float currentTimeToWait;

    PlayerInventory_VS playerInventoryComponent;
    // Start is called before the first frame update
    void Start()
    {
        currentTimeToWait = timeToWait;

        SetCurrentLevel(currentLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerInventoryComponent = other.GetComponent<PlayerInventory_VS>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if(playerInventoryComponent != null && currentLevel < upgradeOrderList.Count)
            {
                currentTimeToWait -= Time.deltaTime;
                if(playerInventoryComponent.GetCurrentPoints() > 0 && currentTimeToWait < 0)
                {
                    playerInventoryComponent.useCurrentPoints();
                    if (pointsRequireToIncreaseNumberOfTroops > 0)
                    {
                        pointsRequireToIncreaseNumberOfTroops--;
                    }
                    currentTimeToWait = timeToWait;
                    if(pointsRequireToIncreaseNumberOfTroops <= 0)
                    {
                        if(currentLevel < upgradeOrderList.Count)
                        {
                            currentLevel++;
                            SetCurrentLevel(currentLevel);
                        }
                    }

                }
            }
        }
    }

    void SetCurrentLevel(int level)
    {
        if (level < upgradeOrderList.Count)
        {
            currentNumberOfTroops = upgradeOrderList[level].currentNumberOfTroops;
            pointsRequireToIncreaseNumberOfTroops = upgradeOrderList[level].pointsRequireToUpgrade;
        }
    }
}

[Serializable]
public class UpgradeOrderOfTroopsHolder
{
    public int currentNumberOfTroops;
    public int pointsRequireToUpgrade;
}
