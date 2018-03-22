using UnityEngine;
using System.Collections;

public class MyGizmos : MonoBehaviour {


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, .8f);
    }

}
