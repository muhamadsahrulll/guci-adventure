using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneload : MonoBehaviour
{
    public PlayerData playerData;
    public void loadScene (string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void keluar()
    {
        Application.Quit();
    }

    public void add100Coins()
    {
        playerData.coins += 100;
    }
}
