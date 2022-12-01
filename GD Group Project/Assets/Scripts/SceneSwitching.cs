using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitching : MonoBehaviour
{
    TimerScript my_timer;
    void Start()
    {
        my_timer = gameObject.AddComponent<TimerScript>();

        my_timer.setCooldown(5);
    }
    public void playLevel()
    {
        SceneManager.LoadScene("SecondScene");
    }

    public void goBack()
    {
        SceneManager.LoadScene("MainScene");
    }

    //To exit when User presses Exit in the menu
    public void exitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting"); //For testing 
    }

    //To exit when User presses *Esc*
    void Update()
    {

        if (my_timer.RemainingTime <= 0)
        {
            SceneManager.LoadScene("RobsTestScene");
        }



            if (Input.GetKey("escape"))
        {
            Application.Quit();
            Debug.Log("Game will exit"); //For testing 

        }

    }
}
