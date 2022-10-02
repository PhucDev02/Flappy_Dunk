using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHolder : MonoBehaviour
{
    // Start is called before the first frame update
    public Color orange, blue, purple, green, red, yellow, disableColor;
    public Color darkRed, darkOrange;
    public Sprite blackBall, feverWing;
    public Sprite flame;
    public static SpriteHolder Instance;
    public Sprite soundOff, soundOn, vibrationOn, vibrationOff;
    private void Awake()
    {
        Instance = this;
    }
    public Color colorByPercent(float percent)
    {
        if (percent >= 0.8f) return blue;
        if (percent >= 0.6f) return green;
        if (percent >= 0.4f) return yellow;
        if (percent >= 0.2f) return orange;
        if (percent >= 0f) return red;
        return purple;
    }
}
