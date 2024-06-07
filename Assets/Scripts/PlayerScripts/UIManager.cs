using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [Header("Managers")]
    public GameManager gameManager;
    public LevelManager levelManager;
    [Header("Player Info")]
    public GameObject player;
    public bool isPaused;
    public Slider healthBar;
    public TextMeshProUGUI killCountObject;
    public int killCount;
    private string killCountText;
    public int totalEnemies;
    public Slider shotCoolDownSlider;
    public InputActionAsset inputAction;
    [Header("Active spell Components")]
    public GameObject activeSpell; // used for transform and name
    public GameObject nextSpell; // ^
    public GameObject prevSpell; // ^
    public TextMeshProUGUI activeSpellTextObject;
    public TextMeshProUGUI nextSpellTextObject;
    public TextMeshProUGUI prevSpellTextObject;
    [Header("Object References")]
    public GameObject HUD;
    public GameObject OnPlayerHUD;
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject mainMenu;
    public GameObject shop;
    public GameObject workshop;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        killCount = totalEnemies;
        healthBar.maxValue = player.GetComponent<PlayerController>().ReturnMaxHP();
        healthBar.value = player.GetComponent<PlayerController>().ReturnCurrentHP();
        shotCoolDownSlider.maxValue = player.GetComponent<PlayerController>().ReturneShotDelay();
    }
    void Update()
    {
        UpdateActiveSpell();
        shotCoolDownSlider.maxValue = player.GetComponent<PlayerController>().ReturneShotDelay();
        healthBar.value = player.GetComponent<PlayerController>().ReturnCurrentHP();
        killCountText = string.Format("Remaining Enemies\n\n{0}/{1} ",killCount,totalEnemies);
        killCountObject.text = killCountText;
        shotCoolDownSlider.value = player.GetComponent<PlayerController>().ReturneShotTimer();
    }
    public void UpdateKillCount()
    {
        killCount -= 1;
    }
    void UpdateActiveSpell()
    {
        int nextSpellIndex = player.GetComponent<PlayerController>().spellIndex + 1;
        int prevSpellIndex = player.GetComponent<PlayerController>().spellIndex - 1;
        if(prevSpellIndex < 0)
        {
            prevSpellIndex = 2;
        }
        if(nextSpellIndex > 2)
        {
            nextSpellIndex = 0;
        }
        activeSpell = player.GetComponent<PlayerController>().spellArray[player.GetComponent<PlayerController>().spellIndex];
        nextSpell = player.GetComponent<PlayerController>().spellArray[nextSpellIndex];
        prevSpell = player.GetComponent<PlayerController>().spellArray[prevSpellIndex];
        activeSpellTextObject.text = activeSpell.GetComponent<SpellBase>().spell.spellName;
        nextSpellTextObject.text = nextSpell.GetComponent<SpellBase>().spell.spellName;
        prevSpellTextObject.text = prevSpell.GetComponent<SpellBase>().spell.spellName;
    }
    public void ResumeGame()
    {
        gameManager.gameState = GameManager.GameState.Gameplay;
        gameManager.ChangeGameState();
    }
    public void BackToMenu()
    {
        
    }
    public void QuitGame()
    {
        //Debug line to test quit function in editor
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
    public void SetMainMenuActive()
    {
        ResetMenus();
        isPaused = false;
        mainMenu.SetActive(true);
    }
    public void SetHUDActive()
    {
        ResetMenus();
        isPaused = false;
        HUD.SetActive(true);
        OnPlayerHUD.SetActive(true);
    }
    public void SetOptionsActive()
    {
        ResetMenus();
        optionsMenu.SetActive(true);
    }
    public void SetPauseMenuActive()
    {
        ResetMenus();
        isPaused = true;
        pauseMenu.SetActive(true);
    }
    void ResetMenus()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        HUD.SetActive(false);
        pauseMenu.SetActive(false);
        OnPlayerHUD.SetActive(false);
        workshop.SetActive(false);
    }
    public void BackFromOptions()
    {
        if(isPaused)
        {
            gameManager.gameState = GameManager.GameState.Paused;
            gameManager.ChangeGameState();

        }
        if(!isPaused)
        {
            gameManager.gameState = GameManager.GameState.MainMenu;
            gameManager.ChangeGameState();
        }
    }
    public void OpenOptionsMenu()
    {
        gameManager.gameState = GameManager.GameState.Options;
        gameManager.ChangeGameState();
    }

    public void SetWorkshopActive()
    {
        ResetMenus();
        workshop.SetActive(true);
    }

}
