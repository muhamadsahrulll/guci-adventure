using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mission Manager", menuName = "Game/Mission Manager")]

public class MissionManager : ScriptableObject
{
    public delegate void MissionCompletedDelegate(string missionName);
    public MissionCompletedDelegate OnMissionCompleted;
    public delegate void MisiKoiCompletedDelegate();
    public MisiKoiCompletedDelegate OnMisiKoiCompleted;
    public delegate void MisiBerendamCompletedDelegate();
    public MisiBerendamCompletedDelegate OnMisiBerendamCompleted;

    [SerializeField] private string misi1 = "Ikan Koi"; // Nama ikan yang diperlukan untuk misi
    private bool isMissionCompleted;
  
    private bool isMisiBerendamCompleted;
    
    public string fruitMissionName = "FruitMission";
    public int requiredFruitCount = 5;
    private int collectedFruitCount = 0;

    private bool isFruitMissionCompleted;
    private bool isCampBuilt;

    private void Start()
    {
        LoadMissionStatus();
    }

    public void CheckFruitMissionCompletion()
    {
        if (!isFruitMissionCompleted && collectedFruitCount >= requiredFruitCount)
        {
            isFruitMissionCompleted = true;
            Debug.Log("Misi buah selesai!");

            // Panggil event ketika misi buah selesai
            OnMissionCompleted?.Invoke(fruitMissionName);

            // Simpan status misi buah ke PlayerPrefs
            PlayerPrefs.SetInt("IsFruitMissionCompleted", isFruitMissionCompleted ? 1 : 0);
            PlayerPrefs.SetInt("CollectedFruitCount", collectedFruitCount);
        }
    }

    // Menambah jumlah buah yang sudah dikumpulkan
    public void AddCollectedFruit()
    {
        collectedFruitCount++;
        Debug.Log("Berhasil mengambil buah, total buah: " + collectedFruitCount);

        // Cek apakah misi buah sudah selesai setelah menambah buah
        CheckFruitMissionCompletion();
    }
    public void CheckMissionCompletion(FishData caughtFish)
    {
        if (!isMissionCompleted && caughtFish != null && caughtFish.fishName == misi1)
        {
            isMissionCompleted = true;
            Debug.Log("Kamu berhasil mendapatkan ikan " + misi1);

            OnMissionCompleted?.Invoke(misi1);

            if (IsMisiKoiCompleted())
            {
                OnMisiKoiCompleted?.Invoke();
            }

            PlayerPrefs.SetInt("IsMissionCompleted", isMissionCompleted ? 1 : 0);
        }
    }

    // Fungsi untuk memuat status misi dari PlayerPrefs
    private void LoadMissionStatus()
    {
        if (PlayerPrefs.HasKey("IsFruitMissionCompleted"))
        {
            isFruitMissionCompleted = PlayerPrefs.GetInt("IsFruitMissionCompleted") == 1;

            if (isFruitMissionCompleted)
            {
                // Jika misi buah sudah selesai, muat jumlah buah yang sudah dikumpulkan
                collectedFruitCount = PlayerPrefs.GetInt("CollectedFruitCount", 0);
                Debug.Log("Load status misi buah, total buah: " + collectedFruitCount);
            }
        }

        if (PlayerPrefs.HasKey("IsCampBuilt"))
        {
            isCampBuilt = PlayerPrefs.GetInt("IsCampBuilt") == 1;

            if (isCampBuilt)
            {
                Debug.Log("Load status tenda: Tenda sudah dibangun!");
                // Tambahkan logika atau pembaruan yang diperlukan ketika tenda sudah dibangun
            }
        }
    }

    public bool IsMissionCompleted()
    {
        return isMissionCompleted;
    }


    public bool IsMisiKoiCompleted()
    {
        return isMissionCompleted && misi1 == "Ikan Koi";
    }

    public void SetMisiBerendamCompleted()
    {
        isMisiBerendamCompleted = true;
        OnMisiBerendamCompleted?.Invoke();
        PlayerPrefs.SetInt("IsMisiBerendamCompleted", isMisiBerendamCompleted ? 1 : 0);
    }

    public bool IsMisiBerendamCompleted()
    {
        return isMisiBerendamCompleted;
    }

    

    public void SetCampBuilt()
    {
        isCampBuilt = true;
        Debug.Log("Tenda telah dibangun!");
        PlayerPrefs.SetInt("IsCampBuilt", isCampBuilt ? 1 : 0);
    }

    public bool IsCampBuilt()
    {
        return isCampBuilt;
    }
}
