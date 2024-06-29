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
    public TextMeshProUGUI player1NameText;  // Referensi ke TextMeshPro untuk menampilkan nama pemain 1
    public TextMeshProUGUI player2NameText;  // Referensi ke TextMeshPro untuk menampilkan nama pemain 2

    // Tambahkan referensi untuk tombol pemilihan karakter
    public Button playerAButton;
    public Button playerBButton;

    // Tambahkan referensi untuk prefab karakter
    public GameObject playerAPrefab;
    public GameObject playerBPrefab;

    private GameObject selectedCharacterPrefab;

    private void Start()
    {
        multiplayerNama.SetActive(true);
        // Menggunakan Inspector untuk menambahkan listener, tidak di sini
    }

    // Ubah metode SelectCharacter menjadi publik agar dapat diatur di Inspector
    public void SelectCharacterA()
    {
        selectedCharacterPrefab = playerAPrefab;
        Debug.Log(playerAPrefab.name + " selected as Character A");
    }

    public void SelectCharacterB()
    {
        selectedCharacterPrefab = playerBPrefab;
        Debug.Log(playerBPrefab.name + " selected as Character B");
    }

    public void OnStartButtonClicked()
    {
        string playerName = nameInputField.text;

        if (selectedCharacterPrefab == null)
        {
            Debug.LogError("No character selected");
            return;
        }

        if (!string.IsNullOrEmpty(playerName))
        {
            PhotonNetwork.NickName = playerName;
            PlayerPrefs.SetString("SelectedCharacter", selectedCharacterPrefab.name);
            PhotonNetwork.ConnectUsingSettings();
            multiplayerNama.SetActive(false);
            Debug.Log("Connecting to Photon with character: " + selectedCharacterPrefab.name);
        }
        else
        {
            Debug.LogError("Player name is empty");
        }
    }

    public void UpdatePlayerNames()
    {
        var players = PhotonNetwork.PlayerList;

        // Set player 1 name
        if (players.Length > 0)
        {
            player1NameText.text = "Player 1: " + players[0].NickName;
        }

        // Set player 2 name
        if (players.Length > 1)
        {
            player2NameText.text = "Player 2: " + players[1].NickName;
        }
    }
}
