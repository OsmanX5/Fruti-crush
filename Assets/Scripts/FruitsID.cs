using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct fruit
{
    public string name;
    public Sprite fruitSprite;
}
public class FruitsID :MonoBehaviour
{
    
    public fruit[] fruits;
}
