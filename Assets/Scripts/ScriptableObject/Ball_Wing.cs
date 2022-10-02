using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName="Ball_Wing",menuName ="Ball_Wing")]
public class Ball_Wing : ScriptableObject
{
    public int ID;
    public Sprite Sprite;
    public string Condition,Type;
    // Start is called before the first frame update
}
