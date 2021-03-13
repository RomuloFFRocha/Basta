using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PrintPhoto : MonoBehaviour
{
    [SerializeField]
    private GameObject grid;

    [SerializeField]
    private RectTransform content;
    
    [Space]
    [SerializeField]
    private Vector2 anchorPosLimit;
    
    [SerializeField]
    private float maxDistance;

    [Space]
    public AudioClip printSound;

    [Space]
    public UnityEvent onEndPrint;

    [Space]
    public UnityEvent onLockGrid;

    bool locked = false;
    bool wasPrinted;

    void Update()
    {
        Debug.Log("X " + content.anchoredPosition.x);
        Debug.Log("Y " + content.anchoredPosition.y);

        if(locked)
            content.anchoredPosition = anchorPosLimit;


        if (content.anchoredPosition.x >= anchorPosLimit.x - maxDistance && content.anchoredPosition.x <= anchorPosLimit.x + maxDistance && content.anchoredPosition.y >= anchorPosLimit.y - maxDistance && content.anchoredPosition.y <= anchorPosLimit.y + maxDistance)
        {
            locked = true;

            onLockGrid.Invoke();

            if (!wasPrinted)
                grid.SetActive(true);  
        }
        else
        {
            grid.SetActive(false);
        }
    }

    public void Print()
    {
        onEndPrint.Invoke();
        
        if(Settings.vibrate)
            Handheld.Vibrate();

        AudioManager.PlaySFX(printSound);
        
        wasPrinted = true;

    }

}
