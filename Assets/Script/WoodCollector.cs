using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WoodCollector : MonoBehaviour
{
    public int woodValue = 1; // Jumlah kayu yang diberikan saat dikumpulkan
    public PlayerData playerData;
    public TextMeshProUGUI woodCountText; // Referensi ke UI TextMeshProUGUI
    public CampfireBuilder campfire;


    private void Start()
    {
        woodCountText.text = $"Kayu {playerData.woodCollected}/4";
        if (playerData.woodCollected == 4)
        {
            campfire.campfirePrefab.SetActive(true);
            campfire.tanahfire.SetActive(false);
        }

    }

    private void Update()
    {
        woodCountText.text = $"Kayu {playerData.woodCollected}/4";
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            // Pemain menyentuh kayu bakar, tambahkan kayu ke PlayerData
            playerData.AddWood(woodValue);

            // Hancurkan objek kayu bakar
            Destroy(gameObject);

            // Update teks UI TextMeshPro
            UpdateWoodCountText();


        }
    }

    private void UpdateWoodCountText()
    {
        if (woodCountText != null && playerData != null)
        {
            woodCountText.text = $"Kayu {playerData.woodCollected}/4";
        }
    }
}
