using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{

    public GameObject FLickerGuy;
    public float FlickerTime;
    private float currentTime;
    void Update()
    {
        FlickerGuy();
        CheckIfAnyPlayerPressA();
    }

    private void FlickerGuy()
    {
        currentTime += Time.deltaTime;
        if(currentTime > FlickerTime)
        {
            currentTime = 0f;
            FLickerGuy.SetActive(!FLickerGuy.activeSelf);
        }
    }

    private void CheckIfAnyPlayerPressA()
    {
        //Check if any player presses a button
        for (int i = 0; i < ReInput.players.AllPlayers.Count; i++)
        {
            if (ReInput.players.AllPlayers[i].GetButtonDown("A"))
                LoadHub();
        }
    }
    private void LoadHub()
    {
        SceneManager.LoadScene("Hub");
    }
}
