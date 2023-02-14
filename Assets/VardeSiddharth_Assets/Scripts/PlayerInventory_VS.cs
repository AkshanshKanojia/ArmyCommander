using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory_VS : MonoBehaviour
{
    [SerializeField]
    int currentPoints;

    [SerializeField]
    float health = 100;
    [SerializeField]
    float maxHealth = 100;
    [SerializeField]
    float timeToRegainHelth = 3;
    [SerializeField]
    float healthRegainRatePerSecond = 20f;

    float timeRemainingToRegainHelth = 0;

    public delegate void playerAttackDeligate();
    public event playerAttackDeligate OnPlayerAttckDelegateEvent;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        gainHealth();
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
        timeRemainingToRegainHelth = 0;
        if(health <= 0)
        {
            Debug.LogError("Game Over");
        }
    }

    public void gainHealth()
    {
        timeRemainingToRegainHelth += Time.deltaTime;

        if(timeRemainingToRegainHelth >= timeToRegainHelth)
        {
            health += (healthRegainRatePerSecond * Time.deltaTime);
            if(health >= maxHealth)
            {
                health = maxHealth;
            }
            timeRemainingToRegainHelth = timeToRegainHelth;
        }
    }
}
