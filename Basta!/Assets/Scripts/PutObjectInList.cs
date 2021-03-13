using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PutObjectInList : MonoBehaviour
{
    [SerializeField]
    int numberOfObjectsRequired;
  
    public List<GameObject> requiredObjects;

    public List<GameObject> rightObjects;

    public UnityEvent onSolve;

    public UnityEvent onFail;

    private void Start()
    {
        requiredObjects = new List<GameObject>();
    }

    private void Update()
    {

    }
   public  void SolvePuzzleList()
    {
        if (requiredObjects.Count == numberOfObjectsRequired)
        {
            bool tempSolved = true;
            
            for (int i = 0; i < rightObjects.Count; i++)
            {
                if(!requiredObjects.Contains(rightObjects[i]))
                {
                    tempSolved = false;
                }   
            }

            if (tempSolved)
            {
                onSolve.Invoke();
            }
            else
            {
                requiredObjects.Clear();
                onFail.Invoke();
            }
        }
        else
        {
            requiredObjects.Clear();
            onFail.Invoke();
        } 
            
    }

    public void AddToList(GameObject target)
    {
        if(!requiredObjects.Contains(target))
        {
            requiredObjects.Add(target);
        }
           
    }

    public void RemoveFromList(GameObject target)
    {
        if(requiredObjects.Contains(target))
        {
            requiredObjects.Remove(target);
        }
        
    }
    
}
