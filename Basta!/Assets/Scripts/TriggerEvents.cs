using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvents : MonoBehaviour
{
    public UnityEvent[] eventsToTrigger;
    
    public void Trigger(int index)
    {
            eventsToTrigger[index].Invoke();
    }
}
