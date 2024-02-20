using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Slider healthBar;
    public GameObject player;
    public TextMeshProUGUI killCountObject;
    public int killCount;
    private string killCountText;
    public int totalEnemies;
    public Slider shotCoolDownSlider;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        healthBar.maxValue = player.GetComponent<PlayerController>().ReturnMaxHP();
        healthBar.value = player.GetComponent<PlayerController>().ReturnCurrentHP();
    }
    void Update()
    {
        healthBar.value = player.GetComponent<PlayerController>().ReturnCurrentHP();
        killCountText = string.Format("Remaining Enemies\n\n{0}/{1} ",killCount,totalEnemies);
        killCountObject.text = killCountText;
    }
    public void UpdateKillCount()
    {
        killCount += 1;
    }
}
