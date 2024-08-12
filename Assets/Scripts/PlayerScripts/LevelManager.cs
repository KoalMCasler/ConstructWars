using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class LevelManager : MonoBehaviour
{
    public GameManager gameManager;
    public UIManager uIManager;
    public GameObject player;
    public GameObject mainCamera;
    public Collider2D foundBoundingShape;
    public CinemachineConfiner2D confiner2D;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        uIManager = FindObjectOfType<UIManager>();
    }
    public void LoadThisScene(string sceneName)
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        if(sceneName.StartsWith("Arena"))
        {
            gameManager.gameState = GameManager.GameState.Gameplay;
            gameManager.ChangeGameState();
            player.GetComponent<PlayerController>().CalculateStats();
            player.GetComponent<PlayerController>().altfire.ArenaStart();
        }
        else if(sceneName == "MainMenu")
        {
            gameManager.gameState = GameManager.GameState.MainMenu;
            gameManager.ChangeGameState();
        }
        SceneManager.LoadScene(sceneName);
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        foundBoundingShape = GameObject.FindWithTag("Confiner").GetComponent<Collider2D>();
        confiner2D.m_BoundingShape2D = foundBoundingShape;
        player.transform.position = GameObject.FindWithTag("Spawn").transform.position;
        SceneManager.sceneLoaded -= OnSceneLoaded;
        uIManager.UpdateHUD();
    }
}
