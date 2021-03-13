using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class NotificationTrigger : MonoBehaviour
{
    public Notification notification;

    [SerializeField] private GameObject modelNotification;
    [SerializeField] private RectTransform notificationConteiner;
    [SerializeField] private RectTransform popUpTransform;
    [SerializeField] private float popUpTime = 3f;
    public static GameObject notificationObject;
    public static NotificationComponents notificationComponents;
    [SerializeField] private AudioClip notificationSfx;
    public float volume;
    private Coroutine popUpCoroutine;
    [SerializeField] private UIManager uiManager;
    private bool alreadyTriggered = false;

    public UnityEvent eventsToTrigger;

    public void TriggerNotification()
    {
        if (notificationObject == null)
        {
            notificationObject = PoolManager.SpawnObject(modelNotification);
            notificationObject.GetComponent<RectTransform>().SetParent(notificationConteiner);
            notificationObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            notificationComponents = notificationObject.GetComponent<NotificationComponents>();
        }

        ResetButton();

        if (!alreadyTriggered)
        {
            notificationComponents.descriptionText.text = notification.notificationTitle;
            notificationComponents.titleText.text = notification.notificationDescription;
            notificationComponents.iconImage.sprite = notification.notificationIcon;
            notificationComponents.notificationButton.onClick.AddListener(delegate { eventsToTrigger.Invoke(); });
            notificationComponents.notificationButton.onClick.AddListener(delegate { CloseNotificationPopUp(); });

            AudioManager.PlaySFX(notificationSfx, volume);

            if (Settings.vibrate)
                Handheld.Vibrate();

            if (popUpCoroutine != null)
                StopCoroutine(popUpCoroutine);

            popUpCoroutine = StartCoroutine(ShowNotificationPopUp());

            alreadyTriggered = true;
        }
    }

    private IEnumerator ShowNotificationPopUp()
    {
        notificationObject.GetComponent<RectTransform>().SetParent(popUpTransform);

        yield return new WaitForSeconds(popUpTime);

        popUpTransform.DOAnchorPos(new Vector2(0, 250), popUpTime/2);

        yield return new WaitForSeconds(popUpTime/2);

        notificationObject.GetComponent<RectTransform>().SetParent(notificationConteiner);

        popUpTransform.DOAnchorPos(new Vector2(0, -250), 0);
    }

    private void ResetButton()
    {
        notificationComponents.notificationButton.onClick.RemoveAllListeners();
    }

    private void CloseNotificationPopUp()
    {
        uiManager.HideNotificationBar();
    }

    public void HigherSound(float sound)
    {
        sound = volume;
    }
}
