using UnityEngine;
using System.Collections;

public class BackGround : MonoBehaviour {

    public float parallaxSpeed;


    private Vector3 destinationPoint1;
    private Transform myTr;
    private Vector3 startMovePos;
    private BoxCollider2D childGOBoxC;
    

    void Awake()
    {
        myTr = transform;
        startMovePos = myTr.position;
        childGOBoxC = GetComponentInChildren<BoxCollider2D>() as BoxCollider2D;
        destinationPoint1 = myTr.position + Vector3.right * childGOBoxC.size.x;
    }

    void Update()
    {
        switch (GameController.gamePhase)
        {
            case GamePhase.InitGamePlay:

                break;

            case GamePhase.GamePlaying:

                // generate moving process of the ground 
                myTr.position = Vector3.MoveTowards(myTr.position, destinationPoint1, parallaxSpeed * Time.deltaTime);
                if (myTr.position == destinationPoint1) { myTr.position = startMovePos; }

                break;


            default:
                break;
        }
    }
}
