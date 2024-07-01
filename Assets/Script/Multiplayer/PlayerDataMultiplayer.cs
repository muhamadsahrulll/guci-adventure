using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data Multiplayer", menuName = "Game/Player Data Multiplayer")]
public class PlayerDataMultiplayer : ScriptableObject
{
    public int playerACoins; // Koin untuk Player A
    public int playerBCoins; // Koin untuk Player B

    // Event yang akan dipanggil ketika nilai koin berubah
    public delegate void CoinsChangedDelegate();
    public CoinsChangedDelegate OnCoinsChanged;

    // Singleton pattern untuk mengakses instance PlayerDataMultiplayer
    private static PlayerDataMultiplayer instance;
    public static PlayerDataMultiplayer Instance
    {
        get
        {
            if (instance == null)
            {
                // Load dari Resources jika belum ada
                instance = Resources.Load<PlayerDataMultiplayer>("PlayerDataMultiplayer");

                if (instance == null)
                {
                    Debug.LogError("PlayerDataMultiplayer instance not found. Make sure it exists as a ScriptableObject in a Resources folder.");
                }
            }
            return instance;
        }
    }

    // Method untuk menambah koin untuk player A
    public void AddPlayerACoins(int amount)
    {
        playerACoins += amount;
        PlayerPrefs.SetInt("PlayerACoins", playerACoins);

        // Panggil event ketika nilai koin Player A berubah
        OnCoinsChanged?.Invoke();
    }

    // Method untuk menambah koin untuk player B
    public void AddPlayerBCoins(int amount)
    {
        playerBCoins += amount;
        PlayerPrefs.SetInt("PlayerBCoins", playerBCoins);

        // Panggil event ketika nilai koin Player B berubah
        OnCoinsChanged?.Invoke();
    }

    // Method untuk mendapatkan jumlah koin berdasarkan pemain (Player A atau Player B)
    public int GetPlayerCoins(bool isPlayerA)
    {
        return isPlayerA ? playerACoins : playerBCoins;
    }
}
