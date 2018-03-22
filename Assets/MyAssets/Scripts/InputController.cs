using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

    public static bool wasPressedOnce;
    public static bool wasPressedTwice;
    Touch currentTouch;
    public bool tap;

    void Awake()
    {
        wasPressedOnce = false;
        wasPressedTwice = false;
    }

    void Update ()
    {
        #if UNITY_STANDALONE || UNITY_EDITOR
        switch (GameController.gamePhase)
        {
            case GamePhase.GamePlaying:
                if (Input.GetKeyDown(KeyCode.Space))
                {

                    if (wasPressedOnce == false)
                    {
                        wasPressedOnce = true;
                        wasPressedTwice = false;
                        EventManager.TriggerEvent("Jump");

                    }
                    else if (wasPressedOnce == true && wasPressedTwice == false)
                    {
                        wasPressedTwice = true;
                        EventManager.TriggerEvent("Jump");

                    }
                }
                break;

            default:
                break;

        }   
        #endif
        #if UNITY_ANDROID
        switch (GameController.gamePhase)  
        {
            case GamePhase.GamePlaying: 
		    
                if (Input.touchCount==1)
                {
                    currentTouch = Input.GetTouch (0);
			    
                    if (tap == false && currentTouch.phase == TouchPhase.Began ) 
                    {
                        tap = true;
                        if (wasPressedOnce == false)
                        {
                            wasPressedOnce = true;
                            wasPressedTwice = false;
                            EventManager.TriggerEvent("Jump");

                        }
                        else if (wasPressedOnce == true && wasPressedTwice == false)
                        {
                            wasPressedTwice = true;
                            EventManager.TriggerEvent("Jump");
                        }
			        }
                
                    if (currentTouch.phase == TouchPhase.Ended ) {
				        tap = false;
			        }
		    }
            break;

            default:
                break;
        }   
        #endif
    }
}
