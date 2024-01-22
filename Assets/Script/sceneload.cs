using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneload : MonoBehaviour
{
    public PlayerData playerData;
    private bool coinsAdded = false;
    public MissionManager missionManager;
    //public FishingController fishingController;
    public void loadScene (string scene)
    {
        SavePlayerPrefs();
        SceneManager.LoadScene(scene);
    }

    public void keluar()
    {
        SavePlayerPrefs();
        Application.Quit();
    }

    public void add100Coins()
    {
        if (!coinsAdded)  // Periksa apakah koin belum ditambahkan
        {
            playerData.coins += 100;
            coinsAdded = true;  // Setel variabel untuk menandakan bahwa koin telah ditambahkan
        }
    }

    private void SavePlayerPrefs()
    {
        // Simpan state terakhir ke PlayerPrefs
        PlayerPrefs.SetInt("PlayerCoins", playerData.coins);
        PlayerPrefs.SetInt("WoodCollected", playerData.woodCollected);
        PlayerPrefs.SetInt("IsFruitMissionCompleted", playerData.IsFruitMissionCompleted() ? 1 : 0);
        PlayerPrefs.SetInt("IsMissionCompleted", missionManager.IsMissionCompleted() ? 1 : 0);
        PlayerPrefs.SetInt("IsMisiBerendamCompleted", missionManager.IsMisiBerendamCompleted() ? 1 : 0);
        PlayerPrefs.SetInt("IsMisiSewaKudaCompleted", missionManager.IsMisiSewaKudaCompleted() ? 1 : 0);

        // Simpan status camp dan campfire jika diperlukan
        PlayerPrefs.SetInt("IsCampBuilt", missionManager.IsCampBuilt() ? 1 : 0);
        PlayerPrefs.SetInt("IsCampfireBuilt", missionManager.IsCampfireBuilt() ? 1 : 0);
    }
}
