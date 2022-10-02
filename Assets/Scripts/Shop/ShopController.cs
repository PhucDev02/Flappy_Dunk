using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopController : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject initObject, tmpObject;
    [SerializeField] Transform ballsContent, wingsContent, hoopsContent, flamesContent;
    [SerializeField] Image progressBar;
    [SerializeField] TextMeshProUGUI progressText;
    public static ShopController Instance;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        InitBallsShop();
        InitWingsShop();
        InitHoopsShop();
        InitFlamesShop();
        progressBar.fillAmount = (float)GameManager.Instance.AmountItemUnlocked / GameManager.Instance.AmountItem;
        progressBar.color = SpriteHolder.Instance.colorByPercent(progressBar.fillAmount);
        progressText.text = GameManager.Instance.AmountItemUnlocked +"/"+ GameManager.Instance.AmountItem;
    }
    public void DeactiveCheckIcon(string type,int id)
    {
        if (type == "Ball")
            ballsContent.GetChild(id).GetChild(2).gameObject.SetActive(false);
        if (type == "Wing")
            wingsContent.GetChild(id).GetChild(2).gameObject.SetActive(false);
        if (type == "Hoop")
            hoopsContent.GetChild(id).GetChild(3).gameObject.SetActive(false);
        if (type == "Flame")
            flamesContent.GetChild(id).GetChild(2).gameObject.SetActive(false);

    }
    void InitBallsShop()
    {
        initObject = ballsContent.GetChild(0).gameObject;
        foreach (Ball_Wing ball in GameManager.Instance.balls)
        {
            tmpObject = Instantiate(initObject, ballsContent);
            tmpObject.GetComponent<BallWingDisplay>().Init(ball);
        }
        Destroy(initObject);
    }
    void InitWingsShop()
    {
        initObject = wingsContent.GetChild(0).gameObject;
        foreach (Ball_Wing wing in GameManager.Instance.wings)
        {
            tmpObject = Instantiate(initObject, wingsContent);
            tmpObject.GetComponent<BallWingDisplay>().Init(wing);
        }
        Destroy(initObject);
    }
    void InitHoopsShop()
    {
        initObject = hoopsContent.GetChild(0).gameObject;
        foreach (Hoop hoop in GameManager.Instance.hoops)
        {
            tmpObject = Instantiate(initObject, hoopsContent);
            tmpObject.GetComponent<HoopDisplay>().Init(hoop);
        }
        Destroy(initObject);
    }
    void InitFlamesShop()
    {
        initObject = flamesContent.GetChild(0).gameObject;
        foreach (Flame flame in GameManager.Instance.flames)
        {
            tmpObject = Instantiate(initObject, flamesContent);
            tmpObject.GetComponent<FlameDisplay>().Init(flame);
        }
        Destroy(initObject);
    }
}
