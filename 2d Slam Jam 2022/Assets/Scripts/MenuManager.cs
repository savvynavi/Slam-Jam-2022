using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string gameScene = "Fishing Level";
    
    public void StartGame()
    {
        SceneManager.LoadScene(gameScene);
    }
}
