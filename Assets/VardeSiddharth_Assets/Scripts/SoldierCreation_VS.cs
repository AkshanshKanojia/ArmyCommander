using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierCreation_VS : MonoBehaviour
{
    [SerializeField]
    GameObject allyPrefabRefrence;
    [SerializeField]
    Vector3 instanciatePoint;
    [SerializeField]
    float timeToCreate = 0.5f;

    float currentTime;
    bool canCreate = false;
    PlayerTroopsHolder_VS playerTroopsHolder;
    Transform playerTroopParent;


    private void Awake()
    {
        playerTroopsHolder = FindObjectOfType<PlayerTroopsHolder_VS>().GetComponent<PlayerTroopsHolder_VS>();
        playerTroopParent = GameObject.FindGameObjectWithTag("PlayerGroup").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTime = timeToCreate;

        if(playerTroopsHolder == null)
        {
            Debug.Log("Player Troop holder is null");
        }
        canCreate = playerTroopsHolder.doesPlayerHaveFullTroops();
    }

    // Update is called once per frame
    void Update()
    {
        if(canCreate)
        {
            currentTime -= Time.deltaTime;
            if(currentTime <= 0)
            {
                CreateAllyTroop();
                currentTime = timeToCreate;
            }
        }
        else
        {
            if(currentTime != timeToCreate)
            {
                currentTime = timeToCreate;
            }
        }
    }

    public void CreateAllyTroop()
    {
        canCreate = playerTroopsHolder.OnTroopGenerated();
        if (canCreate)
        {
            Instantiate(allyPrefabRefrence, transform.position + instanciatePoint, Quaternion.identity, playerTroopParent);
        }
    }
}
