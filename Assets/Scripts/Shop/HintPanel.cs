using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintPanel : MonoBehaviour
{
    [SerializeField] GameObject[] previews;
    public static HintPanel Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void OpenHintPanel(string type)
    {
        gameObject.SetActive(true);
        reset();
        if (type == "Ball")
            previews[0].SetActive(true);
        if (type == "Wing") 
            previews[1].SetActive(true);
        if (type == "Hoop")
            previews[2].SetActive(true);
        if (type == "Flame")
            previews[3].SetActive(true);
    }
    private void reset()
    {
        foreach(GameObject obj in previews)
        {
            obj.SetActive(false);
        }
    }
}
