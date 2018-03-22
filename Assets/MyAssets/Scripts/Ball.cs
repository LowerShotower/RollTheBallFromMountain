using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    public static float ballRadius; // radius of the ball
    public float ballRollingDistance; // distance which ball is ralling

    private Vector3 destinationPoint1;   // points where ball should moving
    private Vector3 destinationPoint2;   // points where ball should moving
    private Vector3 ballStartPosition;   // where the ball should appear at the first beginning of level. it is set in Start

    private GameController gC; // ref to Game controller 
    [System.NonSerialized] public GameObject rollingElement; // the art gameobject that rotates on actual ball
    private GameObject ball;      // ball art gameobject

    


    void Awake()
    {
        // get ref to game controller
        gC = FindObjectOfType<GameController>() as GameController;
        // get ref to art gameobject of the ball to set rotation for it
        rollingElement = GameObject.Find("Dots");
        
        //get ref to boll art gameobject to get radius from it
        ball = GameObject.Find("Ball");
        // ball radius is
        ballRadius = ball.GetComponent<CircleCollider2D>().radius;

        


    }

    void Start()
    {
        
        ballStartPosition = transform.position;
        // where the ball should arrive at the very start of the game
        destinationPoint1 = transform.position - (Vector3)GameController.worldVel.normalized * ballRollingDistance;
        // and where the ball shold arrive at the end of the game  
        destinationPoint2 = transform.position - 3*(Vector3)GameController.worldVel.normalized * ballRollingDistance; 
    }


    
    void Update()
    {
        switch (GameController.gamePhase)
        {
            case GamePhase.StartingCondition:
                
                // destroy all existing enemies  in scene
                Enemies[] enemies = FindObjectsOfType<Enemies>();
                for (int i = 0; i < enemies.Length; i++)
                {
                    Destroy(enemies[i].gameObject);
                }

                //set the starting position after loading reloading level
                transform.position = ballStartPosition;

                GameController.gamePhase = GamePhase.InitGamePlay;
                break;

            case GamePhase.InitGamePlay:
                
                //rotate dots
                rollingElement.transform.Rotate(0.0f, 0.0f, GameController.worldAngVel*Time.deltaTime,Space.World);

               // move the ball to destination1 
               transform.position = Vector3.MoveTowards(transform.position, destinationPoint1, gC.speed* Time.deltaTime);
               
                // till it reaches the destination point
               if (transform.position == destinationPoint1)
               {
                   //then change phase to the next one 
                   GameController.gamePhase = GamePhase.GamePlaying;
               }
                
                break;


            case GamePhase.GamePlaying:
                //rotate dots
                rollingElement.transform.Rotate(0.0f, 0.0f, GameController.worldAngVel * Time.deltaTime, Space.World);
                break;


            case GamePhase.EndGamePlay:

                //rotate dots
                rollingElement.transform.Rotate(0.0f, 0.0f, GameController.worldAngVel * Time.deltaTime, Space.World);

                // move the ball to destination2 
                transform.position = Vector3.MoveTowards(transform.position, destinationPoint2 , gC.speed * Time.fixedDeltaTime);
                // till it reaches the destination point
                if (transform.position == destinationPoint2)
                {
                    
                   
                    //and change phase to None, just for shure that nothing will happen f.e. throwing enemies, etc =)
                    GameController.gamePhase = GamePhase.None;
                }

                break;

            default:
                break;
        }
    }

    public void MoveTo(Vector3 destinationPoint)
    {
                
    }
}
