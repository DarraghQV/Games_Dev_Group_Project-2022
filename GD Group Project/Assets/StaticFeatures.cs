using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class StaticFeatures : MonoBehaviour
{

    public static GameObject test;
    public static GameObject cineCameraCloneTemplate;


    // Start is called before the first frame update
    void Start()
    {
        test = Resources.Load<GameObject>("FloatingTextGO");
        cineCameraCloneTemplate = Resources.Load<GameObject>("ThirdPersonCam");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
