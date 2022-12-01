using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : PickUP
{

    public Transform bulletCloneTemplate;


  
    TimerScript gunTimer;

    private float BulletTime = 5f;
    private float gunCoolDown = 0.5f;
    private float bulletDamage = 25f;

    // Start is called before the first frame update
    void Start()
    {
        gunTimer = gameObject.AddComponent<TimerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case PickUpItemStates.Waiting:

                break;

            case PickUpItemStates.Held:

                break;

            case PickUpItemStates.DoYourThing:
                if (gunTimer.RemainingTime <= 0)
                {
                    fireBullet();
                    gunTimer.setCooldown(gunCoolDown);
                }


                break;


        }


    }

    private void fireBullet()
    { Ray bulletRay = new Ray(transform.position, transform.forward);
        RaycastHit info;

        if (Physics.Raycast(bulletRay,out info, 500f))
        {
            Health objectHit = info.transform.GetComponent<Health>();
            if (objectHit != null)
            {
                objectHit.Take_Damage(bulletDamage);
            }
        }
    }

    public void GunFire()
    {
        currentState = PickUpItemStates.DoYourThing;

    }


}
