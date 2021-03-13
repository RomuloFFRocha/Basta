using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class PrintTrigger : MonoBehaviour
{
    public GameObject printNotificationPrefab;
    public Sprite printSprite;
    public RectTransform popUpTransform;
    public float animationSpeed;

    public UnityEvent eventsToTrigger;

    private GameObject printNotificationObject;
    public static PrintComponents printComponents;
    
    public void TriggerPrint()
    {
        printNotificationObject = PoolManager.SpawnObject(printNotificationPrefab);
        printNotificationObject.GetComponent<RectTransform>().SetParent(popUpTransform);

        printComponents = printNotificationObject.GetComponent<PrintComponents>();

        printComponents.printPreview.sprite = printSprite;

        printComponents.shareButton.onClick.AddListener(delegate { StartCoroutine(VanishAway()); });
    }

    public IEnumerator VanishAway()
    {
        printComponents.animator.Play("Play");
        eventsToTrigger.Invoke();

        yield return new WaitForSeconds(animationSpeed);

        PoolManager.ReleaseObject(printNotificationObject);
    }
}
