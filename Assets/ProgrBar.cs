using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProgrBar : MonoBehaviour {

    public Image fillingImage;
    public Text coinsCount;
    public Button ruletkaBttn;
    public Button playRuletkaBttn;

    private Animator ruletkaAnim;

    private int maxCoins;
    private float currCoins;
    private float totalFilling;
    private float currFilling;
    private bool isFilled;
    

    void OnEnable()
    {
        
       //Ref to animator of Ruletka
        ruletkaAnim = ruletkaBttn.GetComponent<Animator>();

        //Starting conditions ( filling var, EditorFillinfVar,  currCoins and maxCoins and ruletkaBttn condition);
        currFilling = 0;
        fillingImage.fillAmount = 0;
        currCoins = DataManager.instance.Coins;
        maxCoins = FindObjectOfType<GameController>().coinsNeededToGetAchive[DataManager.instance.AchiveNo];
        ruletkaBttn.interactable = false;
        
        StartCoroutine(BarFilling());

        
    }


    //Coroutine of filling
    public IEnumerator BarFilling()
    {
        // gate to wich we should obtain
       
        if (currCoins >= maxCoins) { totalFilling = 1; }
        else{ totalFilling = currCoins / maxCoins;}
        
        
        //Text of actual amount
        coinsCount.text = Mathf.RoundToInt(currFilling / totalFilling * currCoins) + " / " + maxCoins; ;
        
        if (currCoins >=maxCoins)
        {
            playRuletkaBttn.interactable = true;   
        }
        else
        {
            playRuletkaBttn.interactable = false;
            //ruletkaBttn.interactable = false;
        }
        //remain before start
        yield return new WaitForSeconds(0.5f);

        //filling
        do
        {
            //filling
            currFilling = Mathf.MoveTowards(currFilling, totalFilling, 2 * Time.deltaTime);
            fillingImage.fillAmount = currFilling;
            //updating text
            coinsCount.text = Mathf.RoundToInt(currFilling / totalFilling * currCoins) + " / " + maxCoins;
            yield return null;
        } while (currFilling != totalFilling);

        isFilled = true; // в принципе этот параметр не нужен (хотел использовать для активации рулетки) но добавил в итоге активациюв корутин
        
        //check if we should set ruletka bttn interactable
        if (currCoins >= maxCoins)
        {
            ruletkaBttn.interactable = true;
        }
        else 
        {
            ruletkaBttn.interactable = false;
        }
    }

    public void OnPressPlayRuletkaBttn()
    {
        if (currCoins >= maxCoins)
        {
            DataManager.instance.ReduceCoins(maxCoins);
            DataManager.instance.IncreaseAchiveNo(1);
        }
            currCoins = DataManager.instance.Coins;
            
            maxCoins = FindObjectOfType<GameController>().coinsNeededToGetAchive[DataManager.instance.AchiveNo];
            StartCoroutine(BarFilling());
        }

}
