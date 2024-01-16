using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampBuilder : MonoBehaviour
{
    public GameObject campPrefab; // Seret prefab tenda ke sini
    public PlayerData playerData; // Referensi ke PlayerData script
    public MissionManager missionManager; // Referensi ke MissionManager script
    public int buildCost = 100; // Biaya membangun tenda
    public GameObject tidakCukup;
    public GameObject sudahTenda;
    public GameObject belumTenda;

    private void Start()
    {
        // Pastikan objek tenda tidak aktif saat awal game
        //campPrefab.SetActive(false);
        missionManager.campbuilder = FindObjectOfType<CampBuilder>();
        missionManager.LoadMissionStatus();

        
    }

    public void BuildCamp()
    {
        // Periksa apakah pemain memiliki cukup koin untuk membangun tenda
        if (playerData.coins >= buildCost)
        {
            // Kurangi koin pemain
            playerData.AddCoins(-buildCost);

            // Aktifkan objek tenda di posisi pemain
            campPrefab.gameObject.SetActive(true);
            missionManager.SetCampBuilt(); // Panggil metode untuk memberi tahu MissionManager bahwa tenda telah dibangun
            sudahTenda.gameObject.SetActive(true);
            belumTenda.gameObject.SetActive(false);

        }
        else
        {
            Debug.Log("Tidak cukup koin untuk membangun tenda!");
            StartCoroutine(tidakCukupKoin(2f));
        }
    }

    IEnumerator tidakCukupKoin(float seconds)
    {
        tidakCukup.gameObject.SetActive(true);
        yield return new WaitForSeconds(seconds);
        tidakCukup.gameObject.SetActive(false);
    }
}
