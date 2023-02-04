using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack_VS : MonoBehaviour
{
    [SerializeField]
    float timeTowait = 1;

    float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = timeTowait;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            currentTime -= Time.deltaTime;
            if (currentTime < timeTowait)
            {
                //Attack
                other.GetComponent<PlayerInventory_VS>().OnPlayerAttckEventCalled();
                currentTime = timeTowait;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            currentTime = timeTowait;
        }
    }
}
