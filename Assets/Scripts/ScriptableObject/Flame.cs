using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Flame", menuName = "Flame")]
public class Flame : ScriptableObject
{
    public int ID;
    public string Condition;
    public Color Color;
}
