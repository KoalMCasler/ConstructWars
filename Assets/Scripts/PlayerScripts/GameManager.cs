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
    public GameObject playerArt;
    
    public enum GameState{ MainMenu, Gameplay, Paused, Options, MatchLoss, MatchWin}
    [Header("Game States")]
    public GameState gameState;

    void Start()
    {
        gameState = GameState.MainMenu;
        MainMenu();
    }

    public void ChangeGameState()
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
        Cursor.visible = true;
        player.GetComponent<PlayerController>().Crosshair.SetActive(false);
        player.GetComponent<PlayerController>().CalculateStats();
        TogglePlayerSprite(false);
    }
    void Gameplay()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        player.GetComponent<PlayerController>().Crosshair.SetActive(true);
        uIManager.SetHUDActive();
        player.GetComponent<PlayerController>().CalculateStats();
        TogglePlayerSprite(true);
    }
    void Paused()
    {
        Time.timeScale = 0f;
        Cursor.visible = true;
        player.GetComponent<PlayerController>().Crosshair.SetActive(false);
        uIManager.SetPauseMenuActive();
    }
    void Options()
    {
        Cursor.visible = true;
        player.GetComponent<PlayerController>().Crosshair.SetActive(false);
        uIManager.SetOptionsActive();
    }
    void MatchLoss()
    {

    }
    void MatchWin()
    {

    }
    public void TogglePlayerSprite(bool desiredState)
    {
        playerArt = GameObject.FindWithTag("Body");
        if(desiredState == true)
        {
            playerArt.GetComponent<SpriteRenderer>().enabled = true;
        }
        if(desiredState == false)
        {
            playerArt.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
