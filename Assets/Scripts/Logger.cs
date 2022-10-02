using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool enable = false;
    public static void Log(string message)
    {
        if (enable)
            Debug.Log(message);
    }
    public static void Warning(string message)
    {
        if (enable)
            Debug.Log(message);
    }
}
