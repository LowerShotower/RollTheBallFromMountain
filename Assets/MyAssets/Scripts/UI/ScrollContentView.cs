using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollContentView : MonoBehaviour {

    // public variables
    public RectTransform panel;
    public RectTransform center;
    public RectTransform[] objs;

    [System.NonSerialized]
    public static int currentCentralObj;

    private float[] distanceRepositions;

    // private variables
    private float[] distancesToCenter; // distance from the bttn to the center of scrollPanel
    
    private bool dragging = false;
    public int betweenBttnDistance; // distance between bttns 
    private float maxDistance;
    private int objsAmount;
    private Vector2 originalSize;
    
    

    void Awake()
    {
        // cash for array of scrolling objects
        objsAmount = objs.Length;

        //set the length of distances's array
        distancesToCenter = new float[objsAmount];
        distanceRepositions = new float[objsAmount];

        // get the distance between bttns
       
        originalSize = objs[1].sizeDelta;
        

        currentCentralObj =  Mathf.RoundToInt( objs.Length / 2);

        // set avatars in the appropriate places
        for (int i = 0; i < objs.Length; i++)
        {
            if (i < currentCentralObj) { objs[i].transform.position = new Vector2((center.transform.position.x - (currentCentralObj-i)*betweenBttnDistance), objs[i].transform.position.y); }
            if (i == currentCentralObj) { objs[i].transform.position = center.transform.position; }
            if (i > currentCentralObj) { objs[i].transform.position = new Vector2((center.transform.position.x - (currentCentralObj-i)*betweenBttnDistance), objs[i].transform.position.y); }
           
        }
        
    }

    void Start()
    {
     
    }

    void Update()
    {
       
       
        for (int i = 0; i < objs.Length; i++)
        {
            distanceRepositions[i] = center.GetComponent<RectTransform>().position.x - objs[i].GetComponent<RectTransform>().position.x;
            distancesToCenter[i] = Mathf.Abs(distanceRepositions[i]);

            // check if we had to relocate the avatar if it reaches the mental border
            if (distanceRepositions[i] > (1+Mathf.RoundToInt( objs.Length / 2))*betweenBttnDistance)
            {
                float curX = objs[i].GetComponent<RectTransform>().position.x;
                float curY = objs[i].GetComponent<RectTransform>().position.y;

                Vector2 newAnchoredPos = new Vector2(curX + (objsAmount * betweenBttnDistance), curY);
                objs[i].GetComponent<RectTransform>().position = newAnchoredPos;
            }

            if (distanceRepositions[i] < -(1+Mathf.RoundToInt( objs.Length / 2))*betweenBttnDistance )
            {
                float curX = objs[i].GetComponent<RectTransform>().position.x;
                float curY = objs[i].GetComponent<RectTransform>().position.y;

                Vector2 newAnchoredPos = new Vector2(curX - objsAmount * betweenBttnDistance, curY);
                objs[i].GetComponent<RectTransform>().position = newAnchoredPos;
            } // end of checking
        }

        // min distance to the center
        float minDistance = Mathf.Min(distancesToCenter);

        for (int a = 0; a < objs.Length; a++)
        {
            if (minDistance == distancesToCenter[a])
            {
                currentCentralObj = a;
            }
        }

       // make scaling
        for (int i = 0; i < objs.Length; i++ )
        {
            objs[i].sizeDelta = originalSize * 1 / (1 + 0.02f * Mathf.Abs(objs[i].GetComponent<RectTransform>().position.x) * Mathf.Abs(objs[i].GetComponent<RectTransform>().position.x));
        }

        // lerping all panel to the center
        if (!dragging)
        {
            LerpToObj( -(int)objs[currentCentralObj].GetComponent<RectTransform>().anchoredPosition.x );

        }

    }

    void LerpToObj(int positionX)
    {
        float newX = Mathf.Lerp(panel.anchoredPosition.x, positionX, Time.deltaTime * 10f);
        Vector2 newPosition = new Vector2(newX, panel.anchoredPosition.y);

        panel.anchoredPosition = newPosition;
    }


    public void StartDrug()
    {
        dragging = true;
    }

    public void EndDrug()
    {
        dragging = false;
    }

	
}
