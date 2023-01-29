using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableUpdate : MonoBehaviour
{

    // Vector3 currentPos=Vector3.zero, nextPos =Vector3.zero;

    [SerializeField]
    Transform goldTransform, dogTagTransform;
    [SerializeField]
    float sizeToIncrease = 0.05f;


    int collectedGold = 0, collectedDogTag = 0;




    
    // Start is called before the first frame update
    void Start()
    {
        dogTagTransform.gameObject.SetActive(false);
        goldTransform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetGold()
    {
        return collectedGold;
    }
    public int GetDogTag()
    {
        return collectedDogTag;
    }
    public void AddGold()
    {
        if(collectedGold<=0)
        {
            goldTransform.gameObject.SetActive(true);
        }
        collectedGold++;

        ResizeGold();

    }
    public void RemoveGold()
    {
        if(collectedGold<=0)
        {
            goldTransform.gameObject.SetActive(false);
            collectedGold = 0;
            return;
        }
        collectedGold--;
        ResizeGold();
    }

    void ResizeGold()
    {

        //goldTransform.localScale += Vector3.up * sizeToIncrease;
        goldTransform.localScale = new Vector3(goldTransform.localScale.x,
            1 * collectedGold * sizeToIncrease, goldTransform.localScale.z);
        goldTransform.localPosition = transform.localPosition + (Vector3.up * collectedGold * sizeToIncrease / 2) +
            new Vector3(0, dogTagTransform.localScale.y, 0);

       // goldTransform.localPosition = transform.localPosition + (Vector3.up * sizeToIncrease);

    }

    public void AddDogTag()
    {
        if(collectedDogTag <= 0)
        {
            dogTagTransform.gameObject.SetActive(true);
        }
        collectedDogTag++;
        ResizeDogTag();

    }

    public void RemoveDogTag()
    {
        if(collectedDogTag <=0)
        {
            dogTagTransform.gameObject.SetActive(false);
            collectedDogTag = 0;
            return;
        }
        collectedDogTag--;
        ResizeDogTag();
    }

    void ResizeDogTag()
    {
        //dogTagTransform.localScale += Vector3.up * sizeToIncrease;
        //dogTagTransform.localPosition = transform.localPosition + (Vector3.down * sizeToIncrease);

        dogTagTransform.localScale = new Vector3(dogTagTransform.localScale.x,
            1 * sizeToIncrease * collectedDogTag, dogTagTransform.localScale.z);
        print(dogTagTransform.localScale);
        dogTagTransform.localPosition = transform.localPosition + (Vector3.up * collectedDogTag * sizeToIncrease / 2);
        if (collectedGold > 0)
        {
            ResizeGold();
        }
    }


   /* public void PlaceDogTag(Transform other)
    {
        other.localPosition = nextPos;
        currentPos = nextPos;
        nextPos = currentPos + (Vector3.up * 0.45f);
    }*/
}
