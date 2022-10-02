using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Hoop", menuName = "Hoop")]
public class Hoop : ScriptableObject
{
    public int ID;
    public Sprite FrontSprite,BackSprite;
    public string Condition;
    public Sprite startEffect;
}
