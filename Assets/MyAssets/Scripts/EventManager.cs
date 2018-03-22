using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;


// EVENTS

/* 
 * 

 * Jump         // Jump button was pressed
 * Spawn        // Spawn enemy at Spawn Point 
 * 
 */
public class EventManager : MonoBehaviour {

    private static EventManager eventManager;

    private Dictionary<string, UnityEvent> eventDictionary;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType<EventManager>() as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("Here is no EventManager script on any of gameObjects. Add at least one somewhere, please");
                }
                else
                {
                    eventManager.Init();
                }
            }
            return eventManager;
        }
    }

    //Make initialisation of dictionary if here is no inicialized dictionary on eventManager instance 
    void Init ()
    {
        if (instance.eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, UnityEvent>();
        }
    }


    public static void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;

        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }


    public static void StopListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;

        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) 
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }


    public static void TriggerEvent(string eventName)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
        else
        {
            Debug.LogError("There is no " + eventName + "  Event in eventDictionary");
        }
    }






}
