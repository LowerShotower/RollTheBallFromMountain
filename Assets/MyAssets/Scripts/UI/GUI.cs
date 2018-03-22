using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUI : MonoBehaviour {

    public Text waveText;
    public Text coinText;
    public Animator myAnim;

    void OnEnable()
    {
        EventManager.StartListening("waveIncrease", OnWaveIncrease);

        coinText.text = DataManager.instance.Coins.ToString();
        EventManager.StartListening("coinAdd", OnCoinAdd);
    }
    void Awake()
    {
        waveText.gameObject.SetActive(false);
    }

    void Update()
    {
       
    }

    public void OnWaveIncrease()
    {
        waveText.text = "Wave " + DataManager.instance.waveNo.ToString();
        
        myAnim.SetTrigger("waveIncrease");
    }

    public void OnCoinAdd()
    {
        coinText.text = DataManager.instance.Coins.ToString();
        myAnim.SetTrigger("coinAdd");
    }
}
