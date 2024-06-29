using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public UIMultiplayer uiManager;

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
        Debug.Log("Selected character name: " + selectedCharacterName);
        GameObject playerPrefab = Resources.Load<GameObject>(selectedCharacterName);

        if (playerPrefab != null)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
            Debug.Log("Instantiated character: " + playerPrefab.name);
        }
        else
        {
            Debug.LogError("Selected character prefab not found: " + selectedCharacterName);
        }

        uiManager.UpdatePlayerNames();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        uiManager.UpdatePlayerNames();
    }
}
