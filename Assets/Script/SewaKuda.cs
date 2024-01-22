using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SewaKuda : MonoBehaviour
{
    public GameObject player;
    public GameObject kuda;
    public TextMeshProUGUI timer;
    public GameObject sewaPanel;
    public GameObject tidakCukup;
    public GameObject popupSewa;
    public GameObject belum2;
    public GameObject sudah2;


    public PlayerData playerData;
    public MissionManager missionManager;
    public UICoinDisplay coindisplay;

    private bool isRiding = false;
    private float sewaDurasi = 60f;
    private float currentSewaTime;

    void Start()
    {
        Debug.Log(playerData.coins);
        kuda.SetActive(false);
        timer.gameObject.SetActive(false);
        sewaPanel.gameObject.SetActive(false); // Nonaktifkan teks berendam pada awalnya
        popupSewa.SetActive(false);
        
    }

    

    void RestoreMissionStatus()
    {
        int isMisiSewaKudaCompleted = PlayerPrefs.GetInt("IsMisiSewaKudaCompleted", 0);
        if (isMisiSewaKudaCompleted == 1)
        {
            missionManager.OnMisiSewaKudaCompleted?.Invoke();
            //Debug.Log("Berhasil mendapatkan misi berendam (di awal game)");
            belum2.gameObject.SetActive(false);
            sudah2.gameObject.SetActive(true);
        }
        else
        {
            //Debug.Log("Misi berendam belum berhasil");
            belum2.gameObject.SetActive(true);
            sudah2.gameObject.SetActive(false);
        }
    }

    public void StartSewa()
    {
        if (!isRiding && playerData.coins >= 100)
        {
            playerData.coins -= 100; // Mengurangkan koin saat berenang
            PlayerPrefs.SetInt("PlayerCoins", playerData.coins);
            StartRiding();
        }

        else
        {
            StartCoroutine(tidakCukupKoin(3f));
            Debug.Log("Koin tidak cukup");
        }
    }

    IEnumerator tidakCukupKoin(float seconds)
    {
        tidakCukup.gameObject.SetActive(true);
        yield return new WaitForSeconds(seconds);
        tidakCukup.gameObject.SetActive(false);
    }

    void Update()
    {
        RestoreMissionStatus();
        if (isRiding)
        {
            currentSewaTime -= Time.deltaTime;
            timer.text = Mathf.CeilToInt(currentSewaTime).ToString();

            if (currentSewaTime <= 0)
            {
                EndRiding();
            }
        }
    }

    void StartRiding()
    {
        isRiding = true;
        player.SetActive(false);
        kuda.SetActive(true);
        currentSewaTime = sewaDurasi;

        // Mengatur posisi berendam (misalnya tengah kolam)
        //swimSprite.transform.position = new Vector3(7.57f, -1.85f, 0f);

        // Aktifkan countdownText dan swimmingText
        sewaPanel.gameObject.SetActive(true);
        timer.gameObject.SetActive(true);

        // Mulai countdown
        InvokeRepeating("UpdateCountdownText", 0f, 1f);
    }

    void EndRiding()
    {
        isRiding = false;
        kuda.SetActive(false);
        player.SetActive(true);

        // Reset posisi player ke posisi awal sebelum berendam
        player.transform.localPosition = Vector3.zero;

        // Nonaktifkan countdownText dan swimmingText
        sewaPanel.gameObject.SetActive(false);
        timer.gameObject.SetActive(false);

        // Hentikan countdown
        StopCoroutine(UpdateCountdownTextCoroutine());

        // Hentikan countdown
        CancelInvoke("UpdateCountdownText");

        // Debug log
        Debug.Log("Berendam selesai!");

        // Menandai bahwa misi berendam telah selesai
        missionManager.SetMisiSewaKudaCompleted();
        // Aktifkan popUpBerendam selama 3 detik
        StartCoroutine(ShowPopUpBerendam());
    }

    void UpdateCountdownText()
    {
        timer.text = Mathf.CeilToInt(currentSewaTime).ToString();
    }

    IEnumerator UpdateCountdownTextCoroutine()
    {
        while (currentSewaTime > 0)
        {
            timer.text = Mathf.CeilToInt(currentSewaTime).ToString();
            yield return new WaitForSeconds(1f);
            currentSewaTime -= 1f;
        }

        EndRiding();
    }

    IEnumerator ShowPopUpBerendam()
    {
        popupSewa.SetActive(true);
        yield return new WaitForSeconds(3f);
        popupSewa.SetActive(false);
    }

}
