using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory_VS : MonoBehaviour
{
    [SerializeField]
    int currentPoints;

    [SerializeField]
    float health = 100;

    public delegate void playerAttackDeligate();
    public event playerAttackDeligate OnPlayerAttckDelegateEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerAttckEventCalled()
    {
        if(OnPlayerAttckDelegateEvent != null)
        {
            OnPlayerAttckDelegateEvent();
        }
    }

    public int GetCurrentPoints()
    {
        return currentPoints;
    }

    public void useCurrentPoints()
    {
        currentPoints--;
    }

    public void GetDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Debug.LogError("Game Over");
        }
    }
}
