using UnityEngine;
using System.Collections;



public class Ground : MonoBehaviour
{


    public static Vector2 worldDir; // direction of the ground and other objects
    public static Quaternion worldRot; // rotation of grounded objects
    

    private Vector2 myVel;
    private Vector2 myDir;
    private Vector2 myCurrVel;

    private bool isRichedConstantVel;  // still NOT used
    private Vector3 destinationPoint1; // where ground GO should arrive
    private GameController gC;         // private ref to GameController
    private BoxCollider2D childGOBoxC; // private ref to any of child BoxCollider2D
    private Transform myTr;            // chashed ref to Transform
    private Vector3 startMovePos;      // where the ground start to move




    void Awake()
    {
        // cashed ref to transform
        myTr = transform; 
        // get any of child BoxCollider2D
        childGOBoxC = GetComponentInChildren<BoxCollider2D>() as BoxCollider2D;
        // get ref to GameController, later we'll take speed from there
        gC = FindObjectOfType<GameController>() as GameController;
        // Initialize world direction depending on ground position
        worldDir = myDir = myTr.right;
        worldRot = myTr.rotation;

    }

    void Start()
    {
        // set start positioin of moving
        startMovePos = myTr.position;
        // point  that ground should rich
        destinationPoint1 = myTr.position + (Vector3)GameController.worldVel.normalized * childGOBoxC.size.x;

        isRichedConstantVel = false; // NOT used 

    }

    void Update()
    {
        switch (GameController.gamePhase)
        {
            case GamePhase.InitGamePlay:
              
                break;

            case GamePhase.GamePlaying:
                
                // generate moving process of the ground 
                myTr.position = Vector3.MoveTowards(myTr.position, destinationPoint1, gC.speed * Time.deltaTime);
                if (myTr.position == destinationPoint1) { myTr.position = startMovePos; }

                break;

            

            case GamePhase.EndGamePlay:
                break;
            
            default:
                break;
        }
    }
}

