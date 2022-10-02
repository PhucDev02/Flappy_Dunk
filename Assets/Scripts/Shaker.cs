using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    public static Shaker Instance;
    bool isShake;
    [SerializeField] float shakeTime;
    [SerializeField] float shakeRange;
    [SerializeField] GameObject ceil, floor;
    [SerializeField] Vector3 ceilPos, floorPos;
    [SerializeField] AnimationCurve curve;
    void Awake()
    {
        Instance = this;
        shakeTime = 0.3f;
        isShake = false;
        //shakeRange = 0.1f;
    }
    public void Shake()
    {
        isShake = true;
    }
    void Update()
    {
        if (isShake == true)
        {
            shakeTime -= Time.deltaTime;
            ceil.transform.localPosition = ceilPos + new Vector3(0, curve.Evaluate(shakeTime));
            floor.transform.localPosition = floorPos + new Vector3(0, curve.Evaluate(shakeTime));
            if (shakeTime <= 0)
            {
                isShake = false;
                shakeTime = 0.3f;
                ceil.transform.localPosition = ceilPos;
                floor.transform.localPosition = floorPos;
            }
        }
    }
}
