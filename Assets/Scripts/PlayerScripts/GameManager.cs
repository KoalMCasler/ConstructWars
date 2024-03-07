using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Object Referances")]
    public UIManager uIManager;
    public LevelManager levelManager;
    public PlayerController playerController;

    public GameObject player;
    public GameObject spawnPoint;
    public GameObject playerArt;
    
    public enum GameState{ MainMenu, Gameplay, Paused, Options, MatchLoss, MatchWin}
    [Header("Game States")]
    public GameState gameState;

    void Start()
    {
        gameState = GameState.MainMenu;
    }

    void Update()
    {
        switch(gameState)
        {
            case GameState.MainMenu: MainMenu(); break;
            case GameState.Gameplay: Gameplay(); break;
            case GameState.Paused: Paused(); break;
            case GameState.Options: Options(); break;
            case GameState.MatchLoss: MatchLoss(); break;
            case GameState.MatchWin: MatchWin(); break;
        }
    }

    void MainMenu()
    {
        uIManager.SetMainMenuActive();
        player.SetActive(false);
    }
    void Gameplay()
    {
        uIManager.SetHUDActive();
        player.SetActive(true);
    }
    void Paused()
    {
        uIManager.SetPauseMenuActive();
    }
    void Options()
    {
        uIManager.SetOptionsActive();
    }
    void MatchLoss()
    {

    }
    void MatchWin()
    {

    }
}
