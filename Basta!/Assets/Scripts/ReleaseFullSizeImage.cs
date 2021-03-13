using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseFullSizeImage : MonoBehaviour
{
    public GameObject imageFullSize;
   public void Release()
    {
        PoolManager.ReleaseObject(imageFullSize);

        imageFullSize.GetComponentsInChildren<Animator>()[0].runtimeAnimatorController = null;
    }
}
