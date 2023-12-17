using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FishingController : MonoBehaviour
{
    public TextMeshProUGUI fishingText;
    public Image FishImage;
    public TextMeshProUGUI FishName;

    public FishData[] fishDataArray;

    private bool isFishing;
    

    void Update()
    {
        if (isFishing && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(FishingCoroutine());
        }
    }

    private IEnumerator FishingCoroutine()
    {
        fishingText.gameObject.SetActive(true);
        fishingText.text = "Sedang Memancing....";
        yield return new WaitForSeconds(2f);

        FishData caughtFish = GetRandomFish();
        DisplayCaughtFish(caughtFish);

        // Lakukan logika lain jika diperlukan

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
        //FishImage.gameObject.SetActive(true);
        

        // Misalnya, tampilkan gambar pada UI Canvas
        // fishImage.sprite = fishData.fishSprite;
        // fishNameText.text = fishData.fishName;
    }

    public void StartFishing()
    {
        if (!isFishing)
        {
            isFishing = true;
            StartCoroutine(FishingCoroutine());
        }
    }

    public void AmbilIkan()
    {
        // Lakukan logika untuk menangani pengambilan ikan, misalnya, menambahkan ikan ke inventori pemain.
        // ...

        // Setelah melakukan logika ambil ikan, kembalikan ke kondisi awal
        isFishing = false;
        fishingText.text = string.Empty;
        // Sembunyikan UI informasi ikan, atur sesuai kebutuhan Anda.
    }
}
