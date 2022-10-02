using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewSkinPopup : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] previews;
    [SerializeField] TextMeshProUGUI condition;
    [SerializeField] Image ballPreview, wingPreview, backHoopPreview, frontHoopPreview, flamePreview;
    private void resetPreview()
    {
        foreach (GameObject preview in previews)
        {
            preview.SetActive(false);
        }
    }
    public void Init(string type, int ID)
    {
        resetPreview();
        if (type == "Ball")
        {
            previews[0].SetActive(true);
            condition.text = GameManager.Instance.balls[ID].Condition;
            ballPreview.sprite = GameManager.Instance.balls[ID].Sprite;
        }
        if (type == "Wing")
        {
            previews[1].SetActive(true);
            condition.text = GameManager.Instance.wings[ID].Condition;
            wingPreview.sprite = GameManager.Instance.wings[ID].Sprite;
        }
        if (type == "Hoop")
        {
            previews[2].SetActive(true);
            condition.text = GameManager.Instance.hoops[ID].Condition;
            frontHoopPreview.sprite = GameManager.Instance.hoops[ID].FrontSprite;
            backHoopPreview.sprite = GameManager.Instance.hoops[ID].BackSprite;
        }
        if (type == "Flame")
        {
            previews[3].SetActive(true);
            condition.text = GameManager.Instance.flames[ID].Condition;
            flamePreview.sprite = SpriteHolder.Instance.flame;
            flamePreview.color = GameManager.Instance.flames[ID].Color;
        }
    }
    public void DestroyPanel()
    {
        Destroy(gameObject);
    }
}
