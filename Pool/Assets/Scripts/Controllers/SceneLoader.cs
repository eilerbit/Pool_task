using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader 
{        
    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");       
    }

    public void OpenMainMenu()
    {        
        SceneManager.LoadScene("MainMenu");
    }    

    public void Exit()
    {
        Application.Quit();
    }    
}
