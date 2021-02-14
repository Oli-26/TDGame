using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EventCentral : MonoBehaviour
{
    public static UnityEvent RoundEndEvent = new UnityEvent();
    void Start()
    {
        
    }

    public static void EndRound(){
        RoundEndEvent.Invoke();
    }

  
}
