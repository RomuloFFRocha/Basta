using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingMenu : MonoBehaviour
{
    [Header("Space between items")]
    [SerializeField] Vector2 spacing;

    [Space]
    [Header("Main button rotation")]
    [SerializeField] bool hasRotation;
    [SerializeField] float rotationDuration;
    [SerializeField] Ease rotationEase;
    
    [Space]
    [Header("Animations")]
    [SerializeField] float expandDuration;
    [SerializeField] Ease expandEase;
    [SerializeField] float collapseDuration;
    [SerializeField] Ease collapseEase;

    [Space]
    [Header("Fading")]
    [SerializeField] float expandFadeDuration;
    [SerializeField] float collapseFadeDuration;

    Button mainButton;
    SettingMenuItem[] menuItems;
    bool isExpanded = false;

    Vector2 mainButtonPos;
    int itemsCount;

    private void Start()
    {
        itemsCount = transform.childCount - 1;
        menuItems = new SettingMenuItem[itemsCount];
       
        for (int i = 0; i < itemsCount; i++)
        {
            menuItems[i] = transform.GetChild(i + 1).GetComponent<SettingMenuItem>();
            menuItems[i].img.color = new Color(menuItems[i].img.color.r, menuItems[i].img.color.b, menuItems[i].img.color.g, 0);
            
            if(menuItems[i].hasChild)
            {
                menuItems[i].img2.color = new Color(menuItems[i].img2.color.r, menuItems[i].img2.color.g, menuItems[i].img2.color.g, 0);
            }
        }

        mainButton = transform.GetChild(0).GetComponent<Button>();
        mainButton.onClick.AddListener(ToggleMenu);
        mainButton.transform.SetAsLastSibling();

        mainButtonPos = mainButton.transform.position;

        ResetPositions();
    }

    void ResetPositions()
    {
        for (int i = 0; i < itemsCount; i++)
        {
            menuItems[i].trans.position = mainButtonPos;
        }
    }

    void ToggleMenu()
    {
        isExpanded = !isExpanded;

        if(isExpanded)
        {
            for (int i = 0; i < itemsCount; i++)
            {
                menuItems[i].trans.DOMove(mainButtonPos + spacing * (i + 1), expandDuration).SetEase(expandEase);
                menuItems[i].img.DOFade(1f, expandFadeDuration).From(0f);

                if (menuItems[i].hasChild)
                {
                    menuItems[i].img2.DOFade(1f, expandFadeDuration).From(0f);
                }
            }
        }
        else 
        {
            for (int i = 0; i < itemsCount; i++)
            {
                menuItems[i].trans.DOMove(mainButtonPos, collapseDuration).SetEase(collapseEase);
                menuItems[i].img.DOFade(0f, collapseFadeDuration);

                if (menuItems[i].hasChild)
                {
                    menuItems[i].img2.DOFade(0f, collapseFadeDuration);
                }
            }
        }

        if(hasRotation)
        {
            mainButton.transform
            .DORotate(Vector3.forward * 180f, rotationDuration)
            .From(Vector3.zero)
            .SetEase(rotationEase);

        }
    }
}
