using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class UIMultiplayer : MonoBehaviourPunCallbacks
{
    public TMP_InputField nameInputField;
    public Button startButton;
    public GameObject multiplayerNama;
    public TextMeshProUGUI player1NameText;  // Referensi ke TextMeshPro untuk menampilkan nama pemain 1
    public TextMeshProUGUI player2NameText;  // Referensi ke TextMeshPro untuk menampilkan nama pemain 2
    public TextMeshProUGUI coinsText;  // Referensi ke TextMeshPro untuk menampilkan jumlah koin
    [Header("tanam wortel")]
    public TextMeshProUGUI timerText;  // Referensi ke TextMeshPro untuk menampilkan timer penanaman
    public GameObject timerobjek;
    public GameObject Wortel;

    [Header("tanam jagung")]
    public TextMeshProUGUI cornTimerText;  // Referensi ke TextMeshPro untuk menampilkan timer penanaman jagung
    public GameObject cornTimerobjek;
    public GameObject Corn;

    [Header("Pemilihan karakter")]
    // Tambahkan referensi untuk tombol pemilihan karakter
    public Button playerAButton;
    public Button playerBButton;

    // Tambahkan referensi untuk prefab karakter
    public GameObject playerAPrefab;
    public GameObject playerBPrefab;

    private GameObject selectedCharacterPrefab;
    private bool isPlayerA;

    private void Start()
    {
        multiplayerNama.SetActive(true);
    }

    // Ubah metode SelectCharacter menjadi publik agar dapat diatur di Inspector
    public void SelectCharacterA()
    {
        selectedCharacterPrefab = playerAPrefab;
        isPlayerA = true;
        Debug.Log(playerAPrefab.name + " selected as Character A");
    }

    public void SelectCharacterB()
    {
        selectedCharacterPrefab = playerBPrefab;
        isPlayerA = false;
        Debug.Log(playerBPrefab.name + " selected as Character B");
    }

    // Metode yang dipanggil saat tombol Start diklik
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
        }
        else
        {
            Debug.LogError("Player name is empty");
        }
    }

    // Metode untuk memperbarui tampilan koin berdasarkan pemain lokal
    public void UpdateCoins()
    {
        int playerACoins = PlayerDataMultiplayer.Instance.playerACoins;
        int playerBCoins = PlayerDataMultiplayer.Instance.playerBCoins;

        if (isPlayerA)
        {
            coinsText.text = $"{playerACoins}";
        }
        else
        {
            coinsText.text = $"{playerBCoins}";
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

    // Metode yang dipanggil saat tombol Plant Carrot diklik
    public void OnPlantCarrotButtonClicked()
    {
        PhotonManager.Instance.PlantCarrot(isPlayerA);
    }

    // Metode yang dipanggil saat tombol Plant Corn diklik
    public void OnPlantCornButtonClicked()
    {
        PhotonManager.Instance.PlantCorn(isPlayerA);
    }

    // Metode untuk memulai timer penanaman wortel
    public void StartPlantingTimer(bool isPlayerA)
    {
        StartCoroutine(PlantingCoroutine(isPlayerA));
    }

    private IEnumerator PlantingCoroutine(bool isPlayerA)
    {
        float plantingTime = 30f; // 30 detik
        timerobjek.SetActive(true);
        while (plantingTime > 0)
        {
            timerText.text = "Menanam: " + plantingTime.ToString("F0") + "Detik";
            yield return new WaitForSeconds(1f);
            plantingTime--;
        }
        timerText.text = "";

        Debug.Log("Player " + (isPlayerA ? "A" : "B") + " menanam wortel sudah selesai");
        timerobjek.SetActive(false);
        Wortel.SetActive(true);
    }

    // Metode untuk memulai timer penanaman jagung
    public void StartPlantingCornTimer(bool isPlayerA)
    {
        StartCoroutine(PlantingCornCoroutine(isPlayerA));
    }

    private IEnumerator PlantingCornCoroutine(bool isPlayerA)
    {
        float plantingTime = 30f; // 30 detik
        cornTimerobjek.SetActive(true);
        while (plantingTime > 0)
        {
            cornTimerText.text = "Menanam: " + plantingTime.ToString("F0") + "Detik";
            yield return new WaitForSeconds(1f);
            plantingTime--;
        }
        cornTimerText.text = "";

        Debug.Log("Player " + (isPlayerA ? "A" : "B") + " menanam jagung sudah selesai");
        cornTimerobjek.SetActive(false);
        Corn.SetActive(true);
    }
}
