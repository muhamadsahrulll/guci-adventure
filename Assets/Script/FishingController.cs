using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FishingController : MonoBehaviour
{
    public TextMeshProUGUI fishingText;
    public GameObject panelIkan;
    public Image FishImage;
    public TextMeshProUGUI FishName;
    public TextMeshProUGUI rewardInfo;

    public FishData[] fishDataArray;
    public PlayerData playerData;
    public MissionManager missionManager;


    private bool isFishing;

    void Start()
    {
        RestoreMissionStatus();
    }

    void Update()
    {
        

        
    }

    private void RestoreMissionStatus()
    {
        int isMissionCompleted = PlayerPrefs.GetInt("IsMissionCompleted", 0);
        if (isMissionCompleted == 1)
        {
            missionManager.OnMissionCompleted?.Invoke("Koi");
            if (missionManager.IsMisiKoiCompleted())
            {
                Debug.Log("Berhasil mendapatkan ikan Koi (di awal game)");
            }
        }
        else
        {
            Debug.Log("Misi ikan Koi belum berhasil");
        }
    }


    private IEnumerator FishingCoroutine()
    {
        
        fishingText.gameObject.SetActive(true);
        fishingText.text = "Sedang Memancing....";
        yield return new WaitForSeconds(2f);

        FishData caughtFish = GetRandomFish();
        DisplayCaughtFish(caughtFish);

        playerData.AddCoins(caughtFish.rewardCoins); // Menambah koin sesuai reward ikan

        // Pemeriksaan apakah ikan yang ditangkap pemain adalah ikan yang diperlukan untuk misi
        missionManager.CheckMissionCompletion(caughtFish);

        fishingText.text = string.Empty;
        isFishing = false;
    }

    private FishData GetRandomFish()
    {
        float totalPercentage = 0f;
        foreach (FishData fishData in fishDataArray)
        {
            totalPercentage += fishData.chancePercentage;
        }

        float randomValue = Random.Range(0f, totalPercentage);
        float cumulativePercentage = 0f;

        foreach (FishData fishData in fishDataArray)
        {
            cumulativePercentage += fishData.chancePercentage;
            if (randomValue <= cumulativePercentage)
            {
                return fishData;
            }
        }

        // Jika terjadi kesalahan atau peluang tidak sesuai, kembalikan ikan pertama
        return fishDataArray[0];
    }

    private void DisplayCaughtFish(FishData fishData)
    {
        // Tampilkan gambar dan nama ikan, lakukan sesuai kebutuhan Anda
        Debug.Log("Mendapatkan: " + fishData.fishName);

        //showIkan
        panelIkan.gameObject.SetActive(true);
        FishName.text = fishData.fishName;
        FishImage.sprite = fishData.fishSprite;

        

        // Memperbarui UI Text jumlah koin
        rewardInfo.text = "Dapat " + fishData.rewardCoins;
        Debug.Log(rewardInfo.text = "Dapat coin" + fishData.rewardCoins);

        // Misalnya, tampilkan gambar pada UI Canvas
        // fishImage.sprite = fishData.fishSprite;
        // fishNameText.text = fishData.fishName;
    }

    public void StartFishing()
    {
        if (!isFishing && playerData.coins >= 10)
        {
            playerData.coins -= 10; // Mengurangkan koin saat memancing
            PlayerPrefs.SetInt("PlayerCoins", playerData.coins);
            isFishing = true;
            StartCoroutine(FishingCoroutine());
        }
        else
        {
            Debug.Log("Koin tidak cukup");
        }
    }

    public void AmbilIkan()
    {
        // Lakukan logika untuk menangani pengambilan ikan, misalnya, menambahkan ikan ke inventori pemain.
        // ...

        // Setelah melakukan logika ambil ikan, kembalikan ke kondisi awal
        isFishing = false;

        panelIkan.gameObject.SetActive(false);
        // Sembunyikan UI informasi ikan, atur sesuai kebutuhan Anda.

        // Simpan ikan ke dalam inventori
        //SaveToInventory(caughtFish);
        
    }

   
}
