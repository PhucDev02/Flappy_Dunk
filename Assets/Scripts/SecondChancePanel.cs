using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class SecondChancePanel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Image fill;
    float time;
    [SerializeField] Color orange;
    [SerializeField] GameObject holder;
    public bool IsActived;
    private void OnEnable()
    {
        reset();
        holder.transform.DOScale(new Vector3(1.1f, 1.1f, 1), 0.5f).SetEase(Ease.InOutSine).OnComplete(() =>
           {
               holder.transform.DOScale(new Vector3(1f, 1f, 1), 0.5f).SetEase(Ease.InOutSine);

           }).SetLoops(-1, LoopType.Yoyo);

        fill.DOColor(Color.yellow, 5.0f / 3).OnComplete(() =>
           {
               fill.DOColor(orange, 5.0f / 3).OnComplete(() =>
               {
                   fill.DOColor(Color.red, 5.0f / 3);
               }
               );
           }
        );
    }
    private void Awake()
    {
        time = 5.0f;
        IsActived = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (IsActived == false)
            time -= Time.deltaTime;
        fill.fillAmount = time / 5.0f;
        if (time < 0 && IsActived == false)
        {
            openGameTab();
            gameObject.SetActive(false);
            IsActived = true;
        }
    }
    public void reset()
    {
        time = 5.0f;
        IsActived = false;
        fill.DOKill();
    }
    void openGameTab()
    {
        if (GameManager.mode == GameMode.Challenge)
        {
            UI_Challenge.Instance.OpenChallenge();
        }
        else UI_Controller.instance.OpenMenu();
    }
}
