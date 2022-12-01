using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUP : MonoBehaviour
{





    internal enum PickUpItemStates { Waiting, Held, Thrown, Landed, DoYourThing }
    internal PickUpItemStates currentState = PickUpItemStates.Waiting;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    internal void latestOwner(JoeControlScript joe)
    {
        currentState = PickUpItemStates.Held;
        transform.parent = joe.myRightHand;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
}
