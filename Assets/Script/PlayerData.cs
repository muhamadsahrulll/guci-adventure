using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Game/Player Data")]
public class PlayerData : ScriptableObject
{
    private static PlayerData instance;
    public int coins; // Properti untuk menyimpan jumlah koin pemain
    public int woodCollected; // Properti untuk menyimpan jumlah kayu yang dikumpulkan oleh pemain


    // Event yang akan dipanggil ketika nilai koin berubah
    public delegate void CoinChangedDelegate();
    public CoinChangedDelegate OnCoinChanged;

    // Event yang akan dipanggil ketika nilai kayu berubah
    public delegate void WoodChangedDelegate();
    public WoodChangedDelegate OnWoodChanged;

    public static PlayerData Instance
    {
        get
        {
            if (instance == null)
            {
                // Create an instance if it doesn't exist
                instance = CreateInstance<PlayerData>();
            }
            return instance;
        }
    }
    // Fungsi untuk menambah koin
    public void AddCoins(int amount)
    {
        coins += amount;
        PlayerPrefs.SetInt("PlayerCoins", coins);

        // Panggil event ketika nilai koin berubah
        OnCoinChanged?.Invoke();
    }

    public bool IsFruitMissionCompleted()
    {
        return PlayerPrefs.GetInt("IsFruitMissionCompleted", 0) == 1;
    }

    // Fungsi untuk menambah kayu
    public void AddWood(int amount)
    {
        woodCollected += amount;
        PlayerPrefs.SetInt("WoodCollected", woodCollected);

        // Panggil event ketika nilai kayu berubah
        OnWoodChanged?.Invoke();
    }

}
