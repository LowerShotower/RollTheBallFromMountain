using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour 
{
    public static bool isDefeated; // used by CheckPoint to start\stop counting set "false" in Awake and true after heating enemy

    public float jumpPower;
    public float mass;

    
    private Animator myAnim;
    private Vector2 myCurrVel;
    private Vector2 myStartVel;
    private Vector2 myStartPos;
    private Transform myTr;
    private CameraController cameraRef;
    private Vector2 normolizedVec;
    Coroutine co1,co2, co3;

    private Vector3 heroStartPosition;  // used to back hero at starting point after reload
    

    void OnEnable()
    {
        EventManager.StartListening("Jump", StartJump);
    }
    
    /*
    void OnDisable()
    {
        EventManager.StopListening("Jump", StartJump);
    }
     */


    void Awake()
    {
        myAnim = GetComponent<Animator>();
       isDefeated = false;
       normolizedVec = new Vector2(0.0f, 1.0f);
        myTr = transform;
        myStartVel = normolizedVec * jumpPower;
        myStartPos = myTr.localPosition+new Vector3(0,0.1f,0);
        cameraRef = GameObject.Find("Main Camera").GetComponent<CameraController>() as CameraController;

        // used to back hero to starting position on the ball after reloading
        heroStartPosition = myStartPos ;
    }

    public void StartJump()
    {

        if (InputController.wasPressedOnce && !InputController.wasPressedTwice)
        {
            co1 = StartCoroutine(Jump());
        }
        if (InputController.wasPressedTwice)
        {
            StopCoroutine(co1);
            co2 = StartCoroutine(Jump());
        }
    }

    public IEnumerator Jump()
    {
        myAnim.SetTrigger("jump");
        myCurrVel = myStartVel;
        
        while (myTr.localPosition.y >= myStartPos.y )
        {
            myTr.localPosition += (Vector3)myCurrVel * Time.deltaTime;
            
            myCurrVel = myCurrVel - normolizedVec * 9.807f * mass * Time.deltaTime;
            if (!isDefeated)
            {
                yield return null;
            }
            else
            {
                yield break;
            }
            
        }
        myTr.localPosition = myStartPos;
        if (myTr.localPosition.y == myStartPos.y)
        {
            InputController.wasPressedOnce = false;
            InputController.wasPressedTwice = false;
        }
    }


   public IEnumerator FinalJump()
    {
        myAnim.SetTrigger("dead");
        myCurrVel = new Vector2(0, 0);
        while (myTr.position.y>-1)
        {
            myTr.position += new Vector3(-5.0f, 2.0f, 0.0f)* 10 * Time.deltaTime +(Vector3)myCurrVel * Time.deltaTime;

            myCurrVel = myCurrVel - normolizedVec * 9.807f * mass * Time.deltaTime;
            
            yield return null;

        }
        GameController.gamePhase = GamePhase.EndGamePlay;
        Invoke("LoadEndMenu", 0.0f);
        //Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "enemy" && !isDefeated)
        {
          
            StartCoroutine(cameraRef.Shake(cameraRef.HeroDeath));
            isDefeated = true;
            InputController.wasPressedOnce = true;
            InputController.wasPressedTwice = true;
            //EventManager.StopListening("Jump", StartJump);
            co3 = StartCoroutine(FinalJump());
            

            
        }
    }

    void LoadEndMenu()
    {
        //then load menu
        ShowPanels.panels.ShowRestartMenu();
        ShowPanels.panels.HideGUI();
        ShowPanels.panels.HidePause();
    }

    void Update()
    {
        switch (GameController.gamePhase)
        {
            case GamePhase.StartingCondition:
                
                
                break;
             
            case GamePhase.InitGamePlay:
                transform.localPosition = myStartPos;
                InputController.wasPressedOnce = false;
                InputController.wasPressedTwice = false;
                isDefeated = false;
                break;
           
        }
    }
}

