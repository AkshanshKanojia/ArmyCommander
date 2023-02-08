using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LevelEditor;

public class PlayerTroopsHolder_VS : MonoBehaviour
{
    public List<UpgradeOrderOfTroopsHolder> upgradeOrderList = new List<UpgradeOrderOfTroopsHolder>();

    [SerializeField]
    int currentLevel = 0;
    [SerializeField]
    int currentNumberOfTroopsPlayerCanHave;
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

    [SerializeField]
    GridManager gridManager;

    //PlayerInventory_VS playerInventoryComponent;
    CollectableUpdate collectableUpdateComponent;
    int currentNumberOfTroopsPlayerHas = 0;

    //public delegate void CreateTroop(bool canCreate);
    //public event CreateTroop setCanCreateTroops;

    // Start is called before the first frame update

    private void Awake()
    {
        if(gridManager == null)
        {
            Debug.Log("Grid Manager Refrence is Missing");
        }
        SetCurrentLevel(currentLevel);
    }
    void Start()
    {
        currentTimeToWait = timeToWait;

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
            if(collectableUpdateComponent != null && currentLevel < upgradeOrderList.Count)
            {
                currentTimeToWait -= Time.deltaTime;
                if(collectableUpdateComponent.GetGold() > 0 && currentTimeToWait < 0)
                {
                    //playerInventoryComponent.useCurrentPoints();
                    collectableUpdateComponent.RemoveGold();
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
            currentNumberOfTroopsPlayerCanHave = upgradeOrderList[level].currentNumberOfTroops;
            pointsRequireToIncreaseNumberOfTroops = upgradeOrderList[level].pointsRequireToUpgrade;
            gridManager.OnNumberOfTroopsChanged(newNoOfTroops: currentNumberOfTroopsPlayerCanHave, horizontalNoOfTroops: 5);
        }
    }

    public bool doesPlayerHaveFullTroops()
    {
        return currentNumberOfTroopsPlayerHas < currentNumberOfTroopsPlayerCanHave;

    }

    public bool doesPlayerHaveNoTroops()
    {
        return currentNumberOfTroopsPlayerHas <= 0;
    }

    public bool OnTroopGenerated()
    {
        currentNumberOfTroopsPlayerHas++;
        return doesPlayerHaveFullTroops();
    }

    public void OnTroopAttackCalled()
    {
        currentNumberOfTroopsPlayerHas--;
        //return doesPlayerHaveNoTroops();
    }
}

[Serializable]
public class UpgradeOrderOfTroopsHolder
{
    public int currentNumberOfTroops;
    public int pointsRequireToUpgrade;
}
