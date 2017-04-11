using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour {
    public static LaserController instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public static void StopLaser()
    {
        instance.gameObject.SetActive(false);
    }
}
