using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fruit Data", menuName = "Game/Fruit Data")]
public class FruitData : ScriptableObject
{
    public string fruitName;
    public Sprite fruitSprite;
}
