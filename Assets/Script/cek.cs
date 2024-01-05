using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cek : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Destroy(collider.gameObject);
            Debug.Log("buah");
        }
    }
}
