using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public void GoMainMenuScene()
    {
       SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
    }

}
