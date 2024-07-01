using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public UIMultiplayer uiManager;
    public GameObject tidakcukup;
    public GameObject TendaA;
    public GameObject TendaB;


    private static PhotonManager instance;
    public static PhotonManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (uiManager == null)
        {
            uiManager = FindObjectOfType<UIMultiplayer>();
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 2 }, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        string selectedCharacterName = PlayerPrefs.GetString("SelectedCharacter");
        GameObject playerPrefab = Resources.Load<GameObject>(selectedCharacterName);

        if (playerPrefab != null)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Selected character prefab not found");
        }

        uiManager.UpdatePlayerNames();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        uiManager.UpdatePlayerNames();
    }

    public void AddCoinsToPlayer(bool isPlayerA, int amount)
    {
        PlayerDataMultiplayer playerData = PlayerDataMultiplayer.Instance;
        if (isPlayerA)
        {
            playerData.AddPlayerACoins(amount);
        }
        else
        {
            playerData.AddPlayerBCoins(amount);
        }
        uiManager.UpdateCoins();
    }

    // Metode untuk membangun tenda
    [PunRPC]
    public void BuildTent(bool isPlayerA)
    {
        PlayerDataMultiplayer playerData = PlayerDataMultiplayer.Instance;
        if (isPlayerA && playerData.playerACoins >= 20)
        {
            TendaA.SetActive(true);
            playerData.AddPlayerACoins(-20);
            Debug.Log("Player A sudah membeli tenda");
        }
        else if (!isPlayerA && playerData.playerBCoins >= 20)
        {
            TendaB.SetActive(true);
            playerData.AddPlayerBCoins(-20);
            Debug.Log("Player B sudah membeli tenda");
        }
        else
        {
            StartCoroutine(tidakCukupKoin(2f));
            Debug.Log("Koin tidak cukup untuk membeli tenda");
        }
        uiManager.UpdateCoins();
    }

    // Metode untuk menanam wortel
    [PunRPC]
    public void PlantCarrot(bool isPlayerA)
    {
        PlayerDataMultiplayer playerData = PlayerDataMultiplayer.Instance;
        if (isPlayerA && playerData.playerACoins >= 20)
        {
            playerData.AddPlayerACoins(-20);
            Debug.Log("Player A mulai menanam wortel");
            uiManager.StartPlantingTimer(isPlayerA);
        }
        else if (!isPlayerA && playerData.playerBCoins >= 20)
        {
            playerData.AddPlayerBCoins(-20);
            Debug.Log("Player B mulai menanam wortel");
            uiManager.StartPlantingTimer(isPlayerA);
        }
        else
        {
            StartCoroutine(tidakCukupKoin(2f));
            Debug.Log("Koin tidak cukup untuk menanam wortel");
        }
        uiManager.UpdateCoins();
    }

    // Metode untuk menanam jagung
    [PunRPC]
    public void PlantCorn(bool isPlayerA)
    {
        PlayerDataMultiplayer playerData = PlayerDataMultiplayer.Instance;
        if (isPlayerA && playerData.playerACoins >= 20)
        {
            playerData.AddPlayerACoins(-20);
            Debug.Log("Player A mulai menanam jagung");
            uiManager.StartPlantingCornTimer(isPlayerA);
        }
        else if (!isPlayerA && playerData.playerBCoins >= 20)
        {
            playerData.AddPlayerBCoins(-20);
            Debug.Log("Player B mulai menanam jagung");
            uiManager.StartPlantingCornTimer(isPlayerA);
        }
        else
        {
            StartCoroutine(tidakCukupKoin(2f));
            Debug.Log("Koin tidak cukup untuk menanam jagung");
        }
        uiManager.UpdateCoins();
    }

    IEnumerator tidakCukupKoin(float seconds)
    {
        tidakcukup.gameObject.SetActive(true);
        yield return new WaitForSeconds(seconds);
        tidakcukup.gameObject.SetActive(false);
    }

    [PunRPC]
    public void StartBath(bool isPlayerA)
    {
        PlayerDataMultiplayer playerData = PlayerDataMultiplayer.Instance;
        if (isPlayerA && playerData.playerACoins >= 20)
        {
            playerData.AddPlayerACoins(-20);
            Debug.Log("Player A mulai berendam");
            uiManager.playerAPrefab.SetActive(false);
            uiManager.StartBathTimer(isPlayerA);
        }
        else if (!isPlayerA && playerData.playerBCoins >= 20)
        {
            playerData.AddPlayerBCoins(-20);
            Debug.Log("Player B mulai berendam");
            uiManager.playerBPrefab.SetActive(false);
            uiManager.StartBathTimer(isPlayerA);
        }
        else
        {
            StartCoroutine(tidakCukupKoin(2f));
            Debug.Log("Koin tidak cukup untuk berendam");
        }
        uiManager.UpdateCoins();
    }
}
