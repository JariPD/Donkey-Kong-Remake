using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
   public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("you quit");
    }

    public void RestartGame()
    {
        if (true)
        {
            SceneManager.LoadScene("MainScene");
        }
    }

}
