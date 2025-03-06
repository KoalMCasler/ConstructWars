using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using Unity.Netcode;

public class LevelManager : NetworkBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private UIManager uIManager;
    [SerializeField]
    private NetworkManager networkManager;
    public GameObject player;
    public GameObject mainCamera;
    public Collider2D foundBoundingShape;
    public CinemachineConfiner2D confiner2D;
    private GameObject playerCopy;
    private List<GameObject> players;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        uIManager = FindObjectOfType<UIManager>();
    }

    public override void OnNetworkSpawn()
    {
        if(!IsOwner)
        {
            enabled = false;
        }
    }

    public void JoinServer()
    {
        networkManager.NetworkConfig.PlayerPrefab = gameManager.player.transform.parent.gameObject;
        networkManager.StartClient();
        player.GetComponent<PlayerController>().altFire.ArenaStart();
        gameManager.gameState = GameManager.GameState.Gameplay;
        gameManager.ChangeGameState();
    }
    public void LoadThisScene(string sceneName)
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        if(sceneName.StartsWith("Arena"))
        {
            networkManager.NetworkConfig.PlayerPrefab = gameManager.player.transform.parent.gameObject;
            networkManager.StartHost();
            //player.GetComponent<PlayerController>().CalculateStats();
            player.GetComponent<PlayerController>().altFire.ArenaStart();
            gameManager.gameState = GameManager.GameState.Gameplay;
            gameManager.ChangeGameState();
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
        if(player.GetComponent<PlayerController>().origin.originType == "Clockwork")
        {
            player.GetComponent<PlayerController>().altFire.Activate();
        }
    }
}
