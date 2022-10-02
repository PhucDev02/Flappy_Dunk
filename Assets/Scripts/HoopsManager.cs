using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopsManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static HoopsManager Instance;
    [SerializeField] List<GameObject> hoops;
    public Quaternion[] angles;
    public Vector3[] scales;
    int hoopsCount;
    float distance;
    private void Awake()
    {
        Instance = this;
        distance = 5 - 1.23f; //check in inspector
        hoops = new List<GameObject>();
        hoopsCount = transform.childCount;
        for (int i = 0; i < hoopsCount; i++)
        {
            hoops.Add(transform.GetChild(i).gameObject);
        }
    }
    public void NewGame()
    {
        for (int i = 0; i < hoopsCount; i++)
        {
            hoops[i].SetActive(false);
        }
        hoops[0].SetActive(true);
        hoops[0].GetComponent<HoopController>().ActiveColor();
        hoops[0].transform.position = new Vector3(1.23f, 0.27f);
        
        hoops.Add(hoops[0]);
        hoops.RemoveAt(0);

        hoops[0].SetActive(true);//adjust on body
        hoops[0].GetComponent<HoopController>().DeactiveColor();
        hoops[0].transform.position = new Vector3(1.23f + distance, RandomHeight()); //adjust on holder
                                                                                     //hoops[0].transform.GetChild(0).position = new Vector3(1.23f + distance, RandomHeight(), 0);
        hoops.Add(hoops[0]);
        hoops.RemoveAt(0);
    }
    public GameObject GetHoop()
    {
        for (int i = 0; i < hoopsCount; i++)
        {
            if (!hoops[i].activeInHierarchy)
            {
                hoops.Add(hoops[i]);
                hoops.RemoveAt(0);
                hoops[hoopsCount - 2].GetComponent<HoopController>().ActiveColor();
                return hoops[hoopsCount - 1];
            }
        }
        return null;
    }
    public float RandomHeight()
    {
        return Random.Range(-2f, 2f);
    }
    public float getHorizontalPosition()
    {
        return hoops[hoops.Count - 2].transform.position.x + distance;
    }
    public float getHorizontalLastHoop()
    {
        for (int i = 0; i < hoopsCount; i++)
        {
            if (hoops[i].activeInHierarchy)
            {
                return hoops[i].transform.position.x - distance;
            }
        }
        return 0;
    }
    
    public void FadeAllHoops(float endValue, float time)
    {
        foreach (GameObject obj in hoops)
        {
            obj.GetComponent<HoopController>().Fade(endValue, time);
        }
    }
}
