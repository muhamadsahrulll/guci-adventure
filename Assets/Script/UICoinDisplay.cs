using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICoinDisplay : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public PlayerData playerData;
    public GameObject Selesaikan;

    private void OnEnable()
    {
        // Daftarkan fungsi UpdateCoinUI sebagai listener pada event OnCoinChanged
        if (playerData != null)
        {
            playerData.OnCoinChanged += UpdateCoinUI;
        }
    }

    private void OnDisable()
    {
        // Hapus fungsi UpdateCoinUI dari daftar listener pada event OnCoinChanged
        if (playerData != null)
        {
            playerData.OnCoinChanged -= UpdateCoinUI;
        }
    }

    private void Start()
    {
        UpdateCoinUI();
        Selesaikan.gameObject.SetActive(true);
    }

    private void Update()
    {
        UpdateCoinUI();
    }

    private void UpdateCoinUI()
    {
        if (coinText != null && playerData != null)
        {
            coinText.text = "" + playerData.coins.ToString();
        }
    }
}
