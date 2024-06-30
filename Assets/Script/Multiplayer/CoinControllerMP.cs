using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinControllerMP : MonoBehaviour
{
    public int coinValue = 10; // Nilai koin yang akan ditambahkan

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Cek apakah yang mendekati adalah pemain A atau B
            bool isPlayerA = collision.gameObject.CompareTag("PlayerA");

            // Tambahkan koin ke data pemain yang sesuai
            PhotonManager.Instance.AddCoinsToPlayer(isPlayerA, coinValue);

            // Hancurkan objek koin setelah ditambahkan koin
            Destroy(gameObject);
        }
    }
}
