using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwimmingController : MonoBehaviour
{
    public GameObject player;
    public GameObject swimSprite;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI swimmingText;
    public GameObject popUpBerendam;

    public PlayerData playerData;
    public MissionManager missionManager;
    public UICoinDisplay coindisplay;

    private bool isSwimming = false;
    private float swimDuration = 30f;
    private float currentSwimTime;

    void Start()
    {
        swimSprite.SetActive(false);
        countdownText.gameObject.SetActive(false);
        swimmingText.gameObject.SetActive(false); // Nonaktifkan teks berendam pada awalnya
        popUpBerendam.SetActive(false);

    }

    public void StartBerendam()
    {
        if (!isSwimming && playerData.coins >= 10)
        {
            playerData.coins -= 10; // Mengurangkan koin saat berenang
            PlayerPrefs.SetInt("PlayerCoins", playerData.coins);
            StartSwimming();
        }
        else
        {
            Debug.Log("Koin tidak cukup");
        }
    }

    
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.B) && !isSwimming)
        //{
            //StartSwimming();
        //}

        if (isSwimming)
        {
            currentSwimTime -= Time.deltaTime;
            countdownText.text = Mathf.CeilToInt(currentSwimTime).ToString();

            if (currentSwimTime <= 0)
            {
                EndSwimming();
            }
        }
    }

    void StartSwimming()
    {
        isSwimming = true;
        player.SetActive(false);
        swimSprite.SetActive(true);
        currentSwimTime = swimDuration;

        // Mengatur posisi berendam (misalnya tengah kolam)
        swimSprite.transform.position = new Vector3(7.57f, -1.85f, 0f);

        // Aktifkan countdownText dan swimmingText
        countdownText.gameObject.SetActive(true);
        swimmingText.gameObject.SetActive(true);

        // Mulai countdown
        InvokeRepeating("UpdateCountdownText", 0f, 1f);
        
    }

    void EndSwimming()
    {
        isSwimming = false;
        swimSprite.SetActive(false);
        player.SetActive(true);

        // Reset posisi player ke posisi awal sebelum berendam
        player.transform.localPosition = Vector3.zero;

        // Nonaktifkan countdownText dan swimmingText
        countdownText.gameObject.SetActive(false);
        swimmingText.gameObject.SetActive(false);

        // Hentikan countdown
        StopCoroutine(UpdateCountdownTextCoroutine());

        // Hentikan countdown
        CancelInvoke("UpdateCountdownText");

        // Debug log
        Debug.Log("Berendam selesai!");

        // Menandai bahwa misi berendam telah selesai
        missionManager.SetMisiBerendamCompleted();
        // Aktifkan popUpBerendam selama 3 detik
        StartCoroutine(ShowPopUpBerendam());
    }

    void UpdateCountdownText()
    {
        countdownText.text = Mathf.CeilToInt(currentSwimTime).ToString();
    }

    IEnumerator UpdateCountdownTextCoroutine()
    {
        while (currentSwimTime > 0)
        {
            countdownText.text = Mathf.CeilToInt(currentSwimTime).ToString();
            yield return new WaitForSeconds(1f);
            currentSwimTime -= 1f;
        }

        EndSwimming();
    }

    IEnumerator ShowPopUpBerendam()
    {
        popUpBerendam.SetActive(true);
        yield return new WaitForSeconds(3f);
        popUpBerendam.SetActive(false);
    }
}
