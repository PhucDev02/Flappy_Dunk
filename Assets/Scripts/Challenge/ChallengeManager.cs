using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ChallengeManager : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject initObject, tmpObject;
    [SerializeField] Transform content;
    public static ChallengeManager Instance;
    [SerializeField] Image progressFill;
    [SerializeField] TextMeshProUGUI progressText;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        initObject = content.GetChild(0).gameObject;
        foreach (Challenge challenge in GameManager.Instance.challenges)
        {
            tmpObject = Instantiate(initObject, content);
            tmpObject.GetComponent<ChallengeDisplay>().challenge = challenge;
        }
        Destroy(initObject);
        UpdateStatus();
    }
    public void UpdateStatus()
    {
        int count = content.childCount;
        GameManager.Instance.UpdateStatus();
        progressFill.fillAmount = (float)GameManager.Instance.AmountChallengeComplete / GameManager.Instance.challenges.Length;
        progressFill.color = SpriteHolder.Instance.colorByPercent(progressFill.fillAmount);
        progressText.text = GameManager.Instance.AmountChallengeComplete + "/" + GameManager.Instance.challenges.Length;
        for (int i=0;i<count;i++)
        {
            content.GetChild(i).GetComponent<ChallengeDisplay>().UpdateStatus();
        }
    }
}
