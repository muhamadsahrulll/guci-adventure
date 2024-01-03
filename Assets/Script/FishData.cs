using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fish Data", menuName = "Game/Fish Data")]
public class FishData : ScriptableObject
{
    public string fishName;
    public Sprite fishSprite;
    public float chancePercentage;
    public int rewardCoins; // Properti untuk menyimpan jumlah koin yang diberikan
}
