using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TabsButtonBehavior : MonoBehaviour
{
    [SerializeField] GameObject[] underline;
    [SerializeField] TextMeshProUGUI[] text;
    [SerializeField] GameObject[] shopTabs;
    // Start is called before the first frame update
    private void Start()
    {
        ChooseBallsTab();
    }
    public void ChooseBallsTab()
    {
        reset();
        shopTabs[0].SetActive(true);
        underline[0].SetActive(true);
        text[0].color = SpriteHolder.Instance.blue;
    }
    public void ChooseWingsTab()
    {
        reset();
        shopTabs[1].SetActive(true);
        underline[1].SetActive(true);
        text[1].color = SpriteHolder.Instance.blue;
    }
    public void ChooseHoopsTab()
    {
        reset();
        shopTabs[2].SetActive(true);
        underline[2].SetActive(true);
        text[2].color = SpriteHolder.Instance.blue;
    }
    public void ChooseFlamesTab()
    {
        reset();
        shopTabs[3].SetActive(true);
        underline[3].SetActive(true);
        text[3].color = SpriteHolder.Instance.blue;
    }
    private void reset()
    {
        for (int i = 0; i < underline.Length; i++)
        {
            underline[i].SetActive(false);
            text[i].color = Color.black;
            shopTabs[i].SetActive(false);
        }
    }
}
