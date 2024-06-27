using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class UIMultiplayer : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public Button startButton;
    public GameObject multiplayerNama;

    private void Start()
    {
        //startButton.onClick.AddListener(OnStartButtonClicked);
        multiplayerNama.SetActive(true);
    }

    public void OnStartButtonClicked()
    {
        string playerName = nameInputField.text;

        if (!string.IsNullOrEmpty(playerName))
        {
            PhotonNetwork.NickName = playerName;
            PhotonNetwork.ConnectUsingSettings();
            multiplayerNama.SetActive(false);
        }
        else
        {
            Debug.LogError("Player name is empty");
        }
    }
}
