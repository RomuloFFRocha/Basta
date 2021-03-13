using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] float duration = 1f;
    public RectTransform notificationBar, popUpBar;

    public void ShowNotificationBar()
    {
        notificationBar.DOAnchorPos(new Vector2(0, -250), duration);
    }

    public void HideNotificationBar()
    {
        notificationBar.DOAnchorPos(new Vector2(0, 250), duration);
    }

    public void ShowAnswerButtons(RectTransform panelToMove)
    {
        panelToMove.DOAnchorPos(new Vector2(0, 600), duration);
    }

    public void HideAnswerButtons(RectTransform panelToMove)
    {
        panelToMove.DOAnchorPos(new Vector2(0, 0), duration);
    }
}
