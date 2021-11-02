using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    // Start the Game DL
    public void PlayGameDL()
    {
        SceneManager.LoadScene("snakeDL");
    }

    // Start the Game DL
    public void PlayGameDRL()
    {
        SceneManager.LoadScene("snakeDRL");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Onboarding()
    {
        SceneManager.LoadScene("onboard2");
    }

    public void Begin()
    {
        SceneManager.LoadScene("onboard1");
    }


}
