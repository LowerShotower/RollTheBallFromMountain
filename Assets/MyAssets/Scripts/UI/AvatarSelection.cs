using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class AvatarSelection : MonoBehaviour {

    public int avatarIndex;
    public List<GameObject> avatars;

    void Awake()
    {

    }

    public void Start()
    {
        avatars = new List<GameObject>();
        avatarIndex = 0;

        foreach (Transform t in transform)
        {
            avatars.Add(t.gameObject);
        }


    }
}
