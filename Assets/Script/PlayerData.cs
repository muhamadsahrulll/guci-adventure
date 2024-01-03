using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Game/Player Data")]
public class PlayerData : ScriptableObject
{
    public int coins; // Properti untuk menyimpan jumlah koin pemain

    // Event yang akan dipanggil ketika nilai koin berubah
    public delegate void CoinChangedDelegate();
    public CoinChangedDelegate OnCoinChanged;
    // Fungsi untuk menambah koin
    public void AddCoins(int amount)
    {
        coins += amount;
        PlayerPrefs.SetInt("PlayerCoins", coins);

        // Panggil event ketika nilai koin berubah
        OnCoinChanged?.Invoke();
    }
}
