using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampBuilder : MonoBehaviour
{
    
    public PlayerData playerData; // Referensi ke PlayerData script
    public MissionManager missionManager; // Referensi ke MissionManager script
    public int buildCost = 100; // Biaya membangun tenda
    public GameObject tidakCukup;
    public GameObject sudahTenda;
    public GameObject belumTenda;
    public GameObject tenda;
    public GameObject tendaBtn;
    public GameObject Tendabangun;

    public void Start()
    {
        // Pastikan objek tenda tidak aktif saat awal game
        //campPrefab.SetActive(false);
        //missionManager.campbuilder = FindObjectOfType<CampBuilder>();
        missionManager.LoadMissionStatus();
        //tenda.SetActive(false);
        
    }
    

    public void BuildCamp()
    {
        if (missionManager.IsCampBuilt())
        {
            Debug.Log("Tenda sudah dibangun!");
            StartCoroutine(sudahTendabangun(2f));
            return; // Keluar dari fungsi jika tenda sudah dibangun
        }

        // Periksa apakah pemain memiliki cukup koin untuk membangun tenda
        if (playerData.coins >= buildCost)
        {
            // Kurangi koin pemain
            playerData.AddCoins(-buildCost);

            // Aktifkan objek tenda di posisi pemain
            tenda.gameObject.SetActive(true);
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

    IEnumerator sudahTendabangun(float seconds)
    {
        Tendabangun.gameObject.SetActive(true);
        yield return new WaitForSeconds(seconds);
        Tendabangun.gameObject.SetActive(false);
    }
}
