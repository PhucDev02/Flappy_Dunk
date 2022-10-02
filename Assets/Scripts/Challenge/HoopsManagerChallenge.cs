using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopsManagerChallenge : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<HoopController> hoops;
    public static HoopsManagerChallenge Instance;
    int hoopsCount;
    void Awake()
    {
        Instance = this;
        hoops = new List<HoopController>();
        hoopsCount = transform.childCount-2;
        for (int i = 0; i < hoopsCount; i++)
        {
            hoops.Add(transform.GetChild(i).GetComponent<HoopController>());
        }
    }
    private void Start()
    {
        FadeAllHoop(1,0.01f);
    }
    public void FadeAllHoop(float endValue,float time)
    {
        foreach (HoopController hoop in hoops)
        {
            hoop.Fade(endValue, time);
        }
    }
    public float getHorizontalLastHoop()
    {
        for (int i = 0; i < hoopsCount; i++)
        {
            if (hoops[i].gameObject.activeInHierarchy)
            {
                return hoops[i].transform.position.x - 4f;
            }
        }
        return 4f;
    }
}
