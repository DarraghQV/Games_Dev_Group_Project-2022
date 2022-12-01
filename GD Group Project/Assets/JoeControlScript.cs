using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Cinemachine;

public class JoeControlScript : NetworkBehaviour,Health
{
    enum CharacterStates {Grounded, JumpUp, Falling }

    internal Transform myRightHand;

    PickUP rightHand;



    CharacterStates joe_state = CharacterStates.Grounded;
    private Vector3 jumping_velocity;
    float start_jump_velocity = 10;

    private float walking_speed = 2;
    private float running_speed = 4;
    float current_speed = 0;

    Animator joe_animator;
    private float rotation_speed = 180;
    private float gravity = 10;
    private float dirBoost = 2;

    // Start is called before the first frame update
    void Start()
    {
        setupCamera();

        myRightHand = getRightHand();

        joe_animator = GetComponent<Animator>();
        if (joe_animator)
            print("Found");
        else
            print("Animator not found");

    }

    private void setupCamera()
    {

        GameObject newCam =   Instantiate(StaticFeatures.cineCameraCloneTemplate);
        (newCam as Cinemachine.).CinemachineVirtualCamera;

    }

    private Transform getRightHand()
    {
        Transform[] allBones = transform.GetComponentsInChildren<Transform>();
        foreach (Transform bone in allBones)
        {
            if (bone.name == "HoldRight")
            {
                return bone;

            }


        }

        return null;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;

        current_speed = 0;


        switch (joe_state)
        {
            case CharacterStates.Grounded:
                joe_animator.SetBool("isWalking", false);
                joe_animator.SetBool("isWalkingBackwards", false);
                if (shouldWalkForward()) walk_forward();
                if (shouldWalkBackwards()) walk_backwards();
                if (shouldTurnLeft()) turn_left();
                if (shouldTurnRight()) turn_right();
                if (shouldPickUp()) pickUp();
                if (shouldUseRight()) useRight();
                if (shouldFireGun()) FireGun();
                if (shouldJump()) jump();
                transform.position += current_speed * transform.forward * Time.deltaTime;
                break;




            case CharacterStates.JumpUp:
                transform.position += jumping_velocity  * Time.deltaTime ;
                jumping_velocity -= gravity*Vector3.up * Time.deltaTime;
                 
                if (jumping_velocity.y <0f)
                {
                    joe_state = CharacterStates.Falling;
                }
                break;


            case CharacterStates.Falling:


                Collider[] colliding_with = Physics.OverlapBox(transform.position, new Vector3(0.5f, 0.1f, 0.5f));
                foreach (Collider c in colliding_with)
                {   
                    print(c.tag);

                    if (c.tag == "Tile")
                    {
                        joe_animator.SetBool("isLanding", true);
                        joe_animator.SetBool("isJumping", false);
                   
                        joe_state = CharacterStates.Grounded;
                    }

                }

                break;


        }


       
 

  




    }

    private void FireGun()
    {
        throw new NotImplementedException();
    }

    private bool shouldFireGun()
    {
        return Input.GetKeyDown(KeyCode.F);
    }

    private void useRight()
    {
        if (rightHand is BombScript )
        {
            (rightHand as BombScript).BombThrow(transform.forward, 5);
            rightHand = null;
        }

        if (rightHand is GunScript)
        {
            (rightHand as GunScript).GunFire();
        }
       
    }

    private bool shouldUseRight()
    {
        return Input.GetKeyDown(KeyCode.T);
    }

    private void pickUp()
    {
    Collider[] allPossiblePickUps = Physics.OverlapSphere(transform.position, 1f);
    foreach (Collider c in allPossiblePickUps)
        {
            
            PickUP newItem = c.transform.GetComponent<PickUP>();
          
            if (newItem)
            {   if (rightHand == null)
                {

                    joe_animator.SetBool("isPickingUp", true);
                    rightHand = newItem;
                    newItem.latestOwner(this);
                }
            }



          
        }
    
    }

    private bool shouldPickUp()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    private void jump()
    {
        joe_animator.SetBool("isJumping", true);
        joe_animator.SetBool("isLanding", false);
        joe_state = CharacterStates.JumpUp;

        jumping_velocity =  dirBoost *current_speed* transform.forward +  start_jump_velocity* Vector3.up;


    }

    private bool shouldJump()
    {
        return Input.GetKey(KeyCode.Space);
    }

    private void turn_right()
    {
        joe_animator.SetBool("isWalkingBackwards", true);
        transform.Rotate(Vector3.up, rotation_speed * Time.deltaTime);
    }

    private bool shouldTurnRight()
    {
        return Input.GetKey(KeyCode.D);
    }

    private void turn_left()
    {
        joe_animator.SetBool("isWalkingBackwards", true);
        transform.Rotate(Vector3.up, -rotation_speed * Time.deltaTime);
    }

    private bool shouldTurnLeft()
    {
        return Input.GetKey(KeyCode.A);
    }

    private void walk_backwards()
    {
        joe_animator.SetBool("isWalkingBackwards", true);
        current_speed = -walking_speed;

    }

    private bool shouldWalkBackwards()
    {
        return Input.GetKey(KeyCode.S);
    }

    private void walk_forward()
    {
        joe_animator.SetBool("isWalking",true);
        current_speed = walking_speed;
    }

    private  bool shouldWalkForward()
    {
        return Input.GetKey(KeyCode.W);
    }

    public void Take_Damage(float damage)
    {
        throw new NotImplementedException();
    }
}
