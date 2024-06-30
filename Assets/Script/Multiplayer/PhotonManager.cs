using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public UIMultiplayer uiManager;

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
}
