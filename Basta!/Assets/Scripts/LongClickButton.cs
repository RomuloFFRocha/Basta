using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LongClickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool pointerDown;
    private float pointerDownTimer;

    [SerializeField]
    private float requiredHoldTimer;

    [SerializeField]
    private Image fillImage;

    public UnityEvent OnLongClick;

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Reset();
    }

    public void FixToggle(bool isOn)
    {
        gameObject.GetComponent<Button>().enabled = !isOn;
    }


    private void Update()
    {
        if(pointerDown)
        {
            pointerDownTimer += Time.deltaTime;

            if (pointerDownTimer >= requiredHoldTimer)
            {
                if (OnLongClick != null)
                    OnLongClick.Invoke();

                Reset();
            }

            fillImage.fillAmount = pointerDownTimer / requiredHoldTimer;
        }
    }

    private void Reset()
    {
        pointerDown = false;
        pointerDownTimer = 0;

        fillImage.fillAmount = pointerDownTimer / requiredHoldTimer;
    }
}
