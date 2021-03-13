using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ShowUpImage : MonoBehaviour
{
    public Image foto;
    public GameObject imageFullSize;

    public UnityEvent eventsToTrigger;

    private GameObject imageToShow;
    private ShowUpImageComponents components;

    public void ShowImage()
    {
        imageToShow = PoolManager.SpawnObject(imageFullSize);

        components = imageToShow.GetComponent<ShowUpImageComponents>();

        components.image.sprite = foto.sprite;
        components.image.SetNativeSize();

        if (foto.GetComponent<Animator>() != null)
        {
            components.animator.runtimeAnimatorController = foto.GetComponent<Animator>().runtimeAnimatorController;
        }

        if (components.shareButton != null)
        {
            components.shareButton.onClick.AddListener(delegate { eventsToTrigger.Invoke(); });

            components.shareButton.onClick.AddListener(delegate { ResetButton(); });
        }   
           
    }

    void ResetButton()
    {
        components.shareButton.onClick.RemoveAllListeners();
        PoolManager.ReleaseObject(imageToShow);
    }
}
