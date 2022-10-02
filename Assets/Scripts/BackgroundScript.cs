using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject bg1, bg2;
    float distance;
    public static BackgroundScript Instance;
    private void Awake()
    {
        Instance = this;
        distance = bg2.transform.position.x - bg1.transform.position.x;
    }
    void Start()
    {
        reset();
    }
    // Update is called once per frame
    void Update()
    {
        if(getIsGameOver()==false)
        {
            if (bg1.transform.position.x < Camera.main.transform.position.x - 35)
            {
                bg1.transform.Translate(bg2.transform.position.x + distance - bg1.transform.position.x, 0, 0);
            }
            if (bg2.transform.position.x < Camera.main.transform.position.x - 35)
            {
                bg2.transform.Translate(bg1.transform.position.x + distance - bg2.transform.position.x, 0, 0);
            }
        }
    }
    public void reset()
    {
        bg1.transform.localPosition = Vector3.zero;
        bg2.transform.localPosition = bg1.transform.localPosition + new Vector3(distance, 0, 0);
    }
    private bool getIsGameOver()
    {
        if (GameManager.mode == GameMode.Challenge)
        {
            return ChallengeController.Instance.IsGameOver;
        }
        else return GameController.instance.IsGameOver;
    }
}
