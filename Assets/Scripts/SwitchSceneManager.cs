using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneManager : MonoBehaviour
{
    [SerializeField] private string sceneName;
    
    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public void SwitchToGameScene()
    {
        SceneManager.LoadScene("MainScene");
    }
    
    public void SwitchToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}


