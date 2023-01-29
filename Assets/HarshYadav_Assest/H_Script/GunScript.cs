using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] 
    Transform bulletSpawnPoint;// spawnPoint of the bullet 

    [SerializeField]
    GameObject bulletPrfb;// bullet prefab

    
   public float fireRate = 1.0f;//FireRate Variable.

    float fireTime, currentTimeToFire;// fire time per sec.&& // tells you the current fire rate



    bool canFire = false;// bool for bullet firing.

    
    
    
    void Start()
    {
        if(fireRate<=0)
        {
            fireRate = 1;
        }
        fireTime = 1 / fireRate;
        currentTimeToFire = 0;
        //StartShoot();
        
    }

    // Update is called once per frame
    void Update()
    {
      
        if(canFire)
        {
            currentTimeToFire += Time.deltaTime;
           
            if(currentTimeToFire>=fireTime)
            {
                Instantiate(bulletPrfb, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                currentTimeToFire = 0;

            }

        }
            
    }
    public void StartShoot()
    {
        canFire = true;

    }
    public void StopShoot()
    {
        canFire = false;
    }
       
        
        
}
           
            

           

           
           
          
        

       


