using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampBuilder : MonoBehaviour
{
    public GameObject campPrefab; // Seret prefab tenda ke sini
    public PlayerData playerData; // Referensi ke PlayerData script
    public MissionManager missionManager; // Referensi ke MissionManager script
    public int buildCost = 100; // Biaya membangun tenda

    private void Start()
    {
        // Pastikan objek tenda tidak aktif saat awal game
        campPrefab.SetActive(false);
    }

    public void BuildCamp()
    {
        // Periksa apakah pemain memiliki cukup koin untuk membangun tenda
        if (playerData.coins >= buildCost)
        {
            // Kurangi koin pemain
            playerData.AddCoins(-buildCost);

            // Aktifkan objek tenda di posisi pemain
            campPrefab.SetActive(true);
            missionManager.SetCampBuilt(); // Panggil metode untuk memberi tahu MissionManager bahwa tenda telah dibangun
        }
        else
        {
            Debug.Log("Tidak cukup koin untuk membangun tenda!");
        }
    }
}
