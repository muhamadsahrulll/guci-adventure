using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Cinemachine;

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
        // Join atau buat ruangan dengan nama "Room" dan maksimal 2 pemain
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 2 }, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        string selectedCharacterName = PlayerPrefs.GetString("SelectedCharacter");
        GameObject playerPrefab = Resources.Load<GameObject>(selectedCharacterName);

        if (playerPrefab != null)
        {
            // Instantiate player prefab
            GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);

            // Set Cinemachine target
            SetCinemachineTarget(player.transform);
        }
        else
        {
            Debug.LogError("Selected character prefab not found");
        }

        // Update player names in UI
        uiManager.UpdatePlayerNames();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        // Update player names in UI
        uiManager.UpdatePlayerNames();
    }

    private void SetCinemachineTarget(Transform target)
    {
        CinemachineVirtualCamera virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

        if (virtualCamera != null)
        {
            virtualCamera.Follow = target;
            // virtualCamera.LookAt = target; // Uncomment this if you want the camera to look at the target
        }
        else
        {
            Debug.LogError("Cinemachine Virtual Camera not found");
        }
    }
}
