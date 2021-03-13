using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeObjectStatic : MonoBehaviour
{
    private MakeObjectStatic instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }
}
