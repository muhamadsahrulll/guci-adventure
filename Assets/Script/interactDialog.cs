using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class interactDialog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDialogue;
    private void OnTriggerEnter2D(Collider2D colider)
    {
        if (colider.CompareTag("Player"))
        {
            textDialogue.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D colider)
    {
        if (colider.CompareTag("Player"))
        {
            textDialogue.gameObject.SetActive(false);
        }
    }

}
