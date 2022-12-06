using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform basicRig;
    public Transform NetworkJoe;
    public Rigidbody rb;

    public float rotationSpeed;



  

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
     

        // rotate orientation
        Vector3 viewDir = basicRig.position - new Vector3(transform.position.x, basicRig.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        if (Input.GetKey(KeyCode.O))
            Cursor.lockState = CursorLockMode.Locked;

        if (Input.GetKey(KeyCode.I))
            Cursor.lockState = CursorLockMode.None;

        // rotate basicRig object
      
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDir != Vector3.zero)
                NetworkJoe.forward = Vector3.Slerp(NetworkJoe.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);

      
    }

}
