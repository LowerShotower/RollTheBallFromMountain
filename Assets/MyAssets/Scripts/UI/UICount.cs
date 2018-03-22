using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UICount : MonoBehaviour {

    public Text uICounter;

    public Animator ballCountAnimator;

    void OnEnable ()
    {
        EventManager.StartListening("AddScore", OnAddScore);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
        
	}

    void OnAddScore()
    {
        uICounter.text = DataManager.instance.Score.ToString();
        ballCountAnimator.SetTrigger("countBouncing");
    }
}
