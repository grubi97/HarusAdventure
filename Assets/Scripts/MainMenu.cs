using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);


    }

    public void Level2()
    {
        SceneManager.LoadScene("SecondLevel");


    }

    public void Level3()
    {
        SceneManager.LoadScene("ThirdLevel");


    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
