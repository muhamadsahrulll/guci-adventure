using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireBuilder : MonoBehaviour
{
    public GameObject campfirePrefab; // Seret prefab api unggun ke sini
    public GameObject tanahfire;
    public PlayerData playerData; // Referensi ke PlayerData script
    public MissionManager missionManager; // Referensi ke MissionManager script
    //public int buildCost = 0;
    
    void Start()
    {
        campfirePrefab.SetActive(false);
        tanahfire.SetActive(true);

        if(playerData.woodCollected == 4)
        {
            campfirePrefab.SetActive(true);
            tanahfire.SetActive(false);
        }
    }

    public void BuildCampfire()
    {
        // Periksa apakah pemain sudah membangun tenda
        if (!missionManager.IsCampBuilt())
        {
            Debug.Log("Anda harus membangun tenda terlebih dahulu!");
            return;
        }

        // Periksa apakah pemain memiliki cukup koin untuk membangun api unggun

        // Periksa apakah pemain sudah mengumpulkan kayu yang cukup
        // Periksa apakah pemain sudah mengumpulkan kayu yang cukup
        if (playerData.woodCollected >= 4)
        {
            // Kurangi koin pemain
            //playerData.AddCoins(-buildCost);

            // Aktifkan objek api unggun di posisi pemain
            campfirePrefab.SetActive(true);
            tanahfire.SetActive(false);
            missionManager.SetCampBuilt();// Panggil metode untuk memberi tahu MissionManager bahwa api unggun telah dibangun
        }
        else
        {
            Debug.Log("Anda memerlukan 4 kayu untuk membangun api unggun!");
        }
    }
        
}

