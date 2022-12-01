using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLock: MonoBehaviour
{
    public Transform orientation;
    public Transform player;
    public Transform basic_rig;
    public Rigidbody rb;

    public float rotationSpeed;
    
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        // rotate player object
        
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDir != Vector3.zero)
                basic_rig.forward = Vector3.Slerp(basic_rig.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        

        if (Input.GetKey(KeyCode.O))
            Cursor.lockState = CursorLockMode.Locked;

        if (Input.GetKey(KeyCode.I))
            Cursor.lockState = CursorLockMode.None;
    }
}

