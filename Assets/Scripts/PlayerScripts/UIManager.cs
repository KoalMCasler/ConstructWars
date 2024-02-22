using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Player Info")]
    public Slider healthBar;
    public GameObject player;
    public TextMeshProUGUI killCountObject;
    public int killCount;
    private string killCountText;
    public int totalEnemies;
    public Slider shotCoolDownSlider;
    public InputActionAsset inputAction;
    public bool isPaused;
    public int menuSceneBuildIndex;
    [Header("Active spell Components")]
    public GameObject activeSpell; // used for transform and name
    public GameObject nextSpell; // ^
    public GameObject prevSpell; // ^
    public TextMeshProUGUI activeSpellTextObject;
    public TextMeshProUGUI nextSpellTextObject;
    public TextMeshProUGUI prevSpellTextObject;
    [Header("Object References")]
    public GameObject HUD;
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject mainPauseMenu;
    void Start()
    {
        isPaused = false;
        killCount = totalEnemies;
        player = GameObject.FindWithTag("Player");
        healthBar.maxValue = player.GetComponent<PlayerController>().ReturnMaxHP();
        healthBar.value = player.GetComponent<PlayerController>().ReturnCurrentHP();
        shotCoolDownSlider.maxValue = player.GetComponent<PlayerController>().ReturneShotDelay();
    }
    void Update()
    {
        TogglePauseGame();
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
        activeSpellTextObject.text = activeSpell.GetComponent<Spell>().spellName;
        nextSpellTextObject.text = nextSpell.GetComponent<Spell>().spellName;
        prevSpellTextObject.text = prevSpell.GetComponent<Spell>().spellName;
    }
    void OnPause()
    {
        if(isPaused)
        {
            isPaused = false;
            optionsMenu.SetActive(false);
            mainPauseMenu.SetActive(true);
        }
        else
        {
            isPaused = true;
            mainPauseMenu.SetActive(true);
            optionsMenu.SetActive(false);
        }
    }
    void TogglePauseGame()
    {
        if(isPaused)
        {
            Time.timeScale = 0f;
            HUD.SetActive(false);
            pauseMenu.SetActive(true);
        }
        if(!isPaused)
        {
            Time.timeScale = 1f;
            HUD.SetActive(true);
            pauseMenu.SetActive(false);
        }
    }
    public void ResumeGame()
    {
        isPaused = false;
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(menuSceneBuildIndex);
    }
    public void QuitGame()
    {
        //Debug line to test quit function in editor
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
