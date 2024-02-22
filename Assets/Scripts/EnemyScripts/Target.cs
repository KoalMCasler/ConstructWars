using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    void OnDisable()
    {
        player.GetComponent<UIManager>().UpdateKillCount();
    }
}
