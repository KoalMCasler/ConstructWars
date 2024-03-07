using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
     public GameManager gameManager;
    public GameObject player;
    public GameObject mainCamera;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    public void LoadThisScene(string sceneName)
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        if(sceneName.StartsWith("Arena"))
        {
            gameManager.gameState = GameManager.GameState.Gameplay;
        }
        if(sceneName == "MainMenu")
        {
            gameManager.gameState = GameManager.GameState.MainMenu;
        }
        SceneManager.LoadScene(sceneName);
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
        mainCamera.GetComponent<CameraFollow>().target = player.transform;
        player.transform.position = GameObject.FindWithTag("Spawn").transform.position;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
