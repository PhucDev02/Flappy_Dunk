using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "Challenge", menuName = "Challenge")]
public class Challenge : ScriptableObject
{
    public GameObject levelPrefab;
    public string Condition;
    public int ID;
}
