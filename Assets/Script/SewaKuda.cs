using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SewaKuda : MonoBehaviour
{
    public TextMeshProUGUI waktuRentalText;
    //public int biayaSewa = 100;
    public float waktuRental = 60f; // Dalam detik

    private bool sedangSewa = false;

    private void Start()
    {
        // Cek status sewa kuda dari PlayerPrefs
        if (PlayerPrefs.HasKey("SewaKudaStatus"))
        {
            // Pemain sedang menyewa kuda, lakukan inisialisasi
            OnSewaKudaButtonPressed();
        }
    }

    public void OnSewaKudaButtonPressed()
    {
        // Cek apakah pemain memiliki cukup koin untuk menyewa kuda
        if (PlayerData.Instance.coins >= 100 && !sedangSewa)
        {
            // Kurangi koin pemain
            PlayerData.Instance.coins -= 100;
            PlayerPrefs.SetInt("PlayerCoins", PlayerData.Instance.coins);

            // Simpan status sewa kuda ke PlayerPrefs
            PlayerPrefs.SetInt("SewaKudaStatus", 1);

            // Set aktivitas game object kuda dan non-aktifkan game object player
            SetAktivitasKuda(true);
            SetAktivitasPlayer(false);

            // Mulai hitung mundur waktu rental
            StartCoroutine(HitungMundurRental());

            // Set status sedang sewa
            sedangSewa = true;

            // Debug status sewa
            Debug.Log("Pemain menyewa kuda.");
        }
        else if (sedangSewa)
        {
            // Tampilkan debug jika pemain sudah menyewa kuda
            Debug.Log("Pemain sudah menyewa kuda.");
        }
        else
        {
            // Tampilkan debug jika koin tidak mencukupi
            Debug.Log("Koin tidak mencukupi untuk menyewa kuda.");
        }
    }

    private IEnumerator HitungMundurRental()
    {
        float waktuSisa = waktuRental;

        while (waktuSisa > 0f)
        {
            waktuRentalText.text = "Waktu Rental: " + Mathf.Ceil(waktuSisa).ToString();
            yield return new WaitForSeconds(1f);
            waktuSisa -= 1f;
        }

        // Waktu rental habis, non-aktifkan kuda dan aktifkan player
        SetAktivitasKuda(false);
        SetAktivitasPlayer(true);

        // Hapus status sewa kuda dari PlayerPrefs
        PlayerPrefs.DeleteKey("SewaKudaStatus");

        // Set status sedang sewa menjadi false
        sedangSewa = false;

        // Debug ketika waktu rental habis
        Debug.Log("Waktu rental kuda habis.");
    }

    private void SetAktivitasKuda(bool aktif)
    {
        // Set aktivitas game object kuda
        // Misalnya, jika kuda diwakili oleh GameObject bernama "Kuda"
        GameObject kuda = GameObject.Find("Kuda");
        if (kuda != null)
        {
            kuda.SetActive(aktif);
        }
    }

    private void SetAktivitasPlayer(bool aktif)
    {
        // Set aktivitas game object player
        // Misalnya, jika player diwakili oleh GameObject bernama "Player"
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            player.SetActive(aktif);
        }
    }

}
