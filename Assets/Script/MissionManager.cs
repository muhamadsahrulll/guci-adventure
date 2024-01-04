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

    [SerializeField] private string misi1 = "Ikan Koi"; // Nama ikan yang diperlukan untuk misi
    private bool isMissionCompleted;

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

    public bool IsMissionCompleted()
    {
        return isMissionCompleted;
    }


    public bool IsMisiKoiCompleted()
    {
        return isMissionCompleted && misi1 == "Ikan Koi";
    }
}
