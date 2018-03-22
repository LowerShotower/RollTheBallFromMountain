using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

    private static bool isPaused;

    public static Pause instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    public void DoPause()
    {
        isPaused = true;
        Time.timeScale = 0.0f;
        
       
    }


    public void UnPause()
    {
        isPaused = false;
        Time.timeScale = 1.0f;
       
    }

    public static bool CheckPause()
    {
        return isPaused;
    }



}
