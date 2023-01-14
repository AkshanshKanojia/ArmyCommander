using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogTagPlacement : MonoBehaviour
{

    Vector3 currentPos=Vector3.zero, nextPos =Vector3.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceDogTag(Transform other)
    {
        other.localPosition = nextPos;
        currentPos = nextPos;
        nextPos = currentPos + (Vector3.up * 0.45f);
    }
}
