using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]                           // prefs for shakin
public class Parametrs : System.Object   
{
    public float duration;
    public float magnitude;
   
}
public class CameraController : MonoBehaviour {

    // to cases of shacking
    public Parametrs EnemyClash;
    public Parametrs HeroDeath;

 
   public  IEnumerator Shake(Parametrs parametrs)
    {
       Vector3 camStartPos = Camera.main.transform.position;
       float elapsed = 0.0f;
        while (elapsed<parametrs.duration)
        {
            elapsed += Time.deltaTime;

            float percentComplete = elapsed/parametrs.duration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            float x = 2 * Random.value - 1.0f;
            float y = 2 * Random.value - 1.0f;

            x *= parametrs.magnitude * damper;
            y *= parametrs.magnitude * damper;

            Camera.main.transform.position = new Vector3(x, y, camStartPos.z);
            yield return null;
        }
        Camera.main.transform.position = camStartPos;
    }
    
}
