using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MoveObjectUI : MonoBehaviour
{
    public RectTransform grid;
    public int thisObjectCorrectIndex;
    public float time = 0.5f;
    public Animator animator;

    private RectTransform thisObject;
    private int thisEntryInTheList;

    public static List<bool> positionList = new List<bool>();

    public static bool isOneObjetctSelected = false;

    public static bool winned = false;

    private Vector2 inicialTouchPos;
    private Vector2 finalTouchPos;

    public static RectTransform firstObjSelected;
    public static RectTransform lastObjSelected;
    public static int firstObjSelectedIndex;
    public static int lastObjSelectedIndex;

    public UnityEvent[] eventsToTrigger;
    

    private void Start()
    {
        winned = false;
        thisObject = GetComponent<RectTransform>();
        thisObject.SetSiblingIndex(Random.Range(0, grid.childCount));
        positionList.Add(CheckPosition());
        thisEntryInTheList = positionList.Count - 1;
    }

    private void FixedUpdate()
    {
        positionList[thisEntryInTheList] = CheckPosition();

        if (CheckWin() && !winned)
        {
            StartCoroutine(TimeToFinish());

            winned = true;
        }
    }

    public void SelectObject(RectTransform objectSelected)
    {
        if (objectSelected == firstObjSelected)
        {
            firstObjSelected = null;

            animator.Play("Stop");

            isOneObjetctSelected = false;

            return;
        }
        else
        {
            if (isOneObjetctSelected)
            {
                lastObjSelected = objectSelected;

                lastObjSelectedIndex = lastObjSelected.GetSiblingIndex();

                Swap();

                return;
            }

        }

        firstObjSelected = objectSelected;

        firstObjSelectedIndex = firstObjSelected.GetSiblingIndex();

        animator.Play("Play");

        isOneObjetctSelected = true;
    }

    private void Swap()
    {
        if (Mathf.Abs(lastObjSelectedIndex - firstObjSelectedIndex) == 1 || Mathf.Abs(lastObjSelectedIndex - firstObjSelectedIndex) == Mathf.Sqrt(grid.childCount))
        {
            if (CanMove())
            {
                firstObjSelected.SetSiblingIndex(lastObjSelectedIndex);

                lastObjSelected.SetSiblingIndex(firstObjSelectedIndex);
            }
        }

        firstObjSelected.gameObject.GetComponent<Animator>().Play("Stop");

        firstObjSelected = null;

        isOneObjetctSelected = false;

        lastObjSelected = null;
    }

    private bool CanMove()
    {
        if (
            (firstObjSelectedIndex % Mathf.Sqrt(grid.childCount) == 0 || lastObjSelectedIndex % Mathf.Sqrt(grid.childCount) == 0) &&

            ((firstObjSelectedIndex + 1) % Mathf.Sqrt(grid.childCount) == 0 || (lastObjSelectedIndex + 1) % Mathf.Sqrt(grid.childCount) == 0) &&

            (Mathf.Abs(lastObjSelectedIndex - firstObjSelectedIndex) == 1)
           )
        {
            return false;
        }

        return true;
    }

    private bool CheckPosition()
    {
        if (thisObject.GetSiblingIndex() == thisObjectCorrectIndex)
            return true;
        else
            return false;
    }

    private bool CheckWin()
    {
        return positionList.Where(l => !l).Count() == 0;
    }

    private IEnumerator TimeToFinish()
    {
        eventsToTrigger[0].Invoke();

        yield return new WaitForSeconds(time);

        positionList.Clear();
        eventsToTrigger[1].Invoke();
    }
}
