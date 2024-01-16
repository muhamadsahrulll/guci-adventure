using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FruitCollector : MonoBehaviour
{
    public TextMeshProUGUI fruitCountText;
    public FruitData[] fruits;
    public PlayerData playerData;
    public MissionManager missionManager;
    public int requiredFruitCount = 5;
    public int coinReward = 50;
    public GameObject reward;
    public TextMeshProUGUI rewardTxt;

    private int collectedFruitCount = 0;

    private void Start()
    {
        UpdateFruitCountText();
    }

    private void Update()
    {
        UpdateFruitCountText();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Fruit"))
        {
            Debug.Log("Player touched a fruit!");
            FruitData collectedFruitData = collider.GetComponent<Fruit>().fruitData;

            // Check if the collected fruit exists in the array of fruits
            if (ArrayContainsFruit(collectedFruitData))
            {
                collectedFruitCount++;
                UpdateFruitCountText();

                // Check if the mission is completed
                if (collectedFruitCount >= requiredFruitCount)
                {
                    CompleteMission();
                }

                // Optionally, you can destroy the collected fruit object
                Destroy(collider.gameObject);
                Debug.Log("berhasil mengambil buah" + collectedFruitCount + requiredFruitCount );

                // Memanggil metode AddCollectedFruit dari MissionManager
                missionManager.AddCollectedFruit();
            }
        }
    }

    private bool ArrayContainsFruit(FruitData fruit)
    {
        foreach (FruitData fruitData in fruits)
        {
            if (fruitData == fruit)
            {
                return true;
            }
        }
        return false;
    }

    private void UpdateFruitCountText()
    {
        if (fruitCountText != null)
        {
            if (collectedFruitCount >= requiredFruitCount)
            {
                // Jika misi selesai, tampilkan 5/5
                fruitCountText.text = $"Buah {requiredFruitCount}/{requiredFruitCount}";
            }
            else
            {
                // Jika masih dalam progres misi, tampilkan progres saat ini
                fruitCountText.text = $"Buah {collectedFruitCount}/{requiredFruitCount}";
            }
        }
    }

    private void CompleteMission()
    {
        // Give the player a reward (coins in this case)
        //PlayerData.Instance.AddCoins(coinReward);
        playerData.AddCoins(coinReward);
        reward.gameObject.SetActive(true);
        rewardTxt.text = coinReward + "Coin";

        // Save mission completion status
        PlayerPrefs.SetInt("IsFruitMissionCompleted", 1);

        // Optionally, you can trigger an event or perform other actions
        Debug.Log("Fruit mission completed!");
    }
}
