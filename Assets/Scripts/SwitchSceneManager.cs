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
        SceneManager.LoadScene(SceneManager.GetSceneByName("MainScene").buildIndex);
    }
    
    public void SwitchToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetSceneByName("MainMenu").buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}


