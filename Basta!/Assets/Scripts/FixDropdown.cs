using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixDropdown : MonoBehaviour
{
    public List<GameObject> objects;
    
   public void HandleInputData(int value)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (i == value)
            {
                objects[i].SetActive(true);
            }
            
            if (i != value)
            {
                objects[i].SetActive(false);
            }
            
        }
    }
         
    
}
