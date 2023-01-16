using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory_VS : MonoBehaviour
{
    [SerializeField]
    int currentPoints;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetCurrentPoints()
    {
        return currentPoints;
    }

    public void useCurrentPoints()
    {
        currentPoints--;
    }
}
