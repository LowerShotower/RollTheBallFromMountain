using UnityEngine;
using System.Collections;

public enum EnemyPhase { start, middle, end} // phases for enemy

public enum SpawnPoints:int { first, second }
public class Enemies : MonoBehaviour {

    
    private CameraController cameraC;

    public Vector2 minMaxDistanceBetween;
    public Vector2 minMaxRelSpeed;
    public SpawnPoints chooseSpawnPoint;
   
    
    public int namberOfCicles;                   // How much cicles enemy will stay on the ball;  STILL DID NOT USED
    public float throwPower;                     // how strong the throw is  (while unstucking from the ball)

    public bool isCatchable;

    private Animator myAnim;
    private Quaternion myStartRot;                 // rot of current enemy
    private EnemyPhase enemyPhase;              
    private float thirdOfHight;                // 0.25 of enemy's height. used while stucking
    private float stuckTime;                    // time enemy stucked in the ball during numberOfCicles times
    private Vector2 myVertVel;                  // velocity of folling
    private Vector2 myStartVel;                 // starting vel in the end phase
    private Vector2 normolizedVec;              // used in calculations of falling speed
    public Transform spawnPoint;

    private GameController gC;                  // used for taking world speed  (float)
    private Transform myTr;                     
    private Ball ball;                          // used for calc of stucking

    
     float relSpeedValue;                           // it is randomly generated speed in range of min max
    
     float distanceValue;
    [System.NonSerialized]
    public float timeBetween;

    



     
    void Awake()
    {
        spawnPoint = GameObject.Find("GameCore").GetComponent<GameController>().spawnPoints[(int)chooseSpawnPoint];
        cameraC = GameObject.Find("Main Camera").GetComponent<CameraController>() as CameraController;
        
        myAnim = GetComponent<Animator>();
        gC = FindObjectOfType<GameController>() as GameController;
        enemyPhase = EnemyPhase.start;         // first phase, till enemy will rich the ball 
        myTr = transform;
       // myTr.position = spawnPoint.position;

       // transform.rotation = Ground.worldRot;          // set direction of current enemy at the beggining of movement
        ball = FindObjectOfType<Ball>();
        thirdOfHight = GetComponent<Collider2D>().bounds.size.y / 2; // calculate third part of the enemy

        stuckTime = namberOfCicles * (2 * Mathf.PI * Ball.ballRadius / gC.speed);        // calculate stuck time in the ball

        myStartVel = myTr.up + myTr.right ;  // in the end phase it is direction where enemy will be thrown in the world space
        myVertVel = new Vector2(0, 0);    // starting speed of falling 
        normolizedVec = new Vector2(0.0f, 1.0f);

        relSpeedValue = Random.Range(minMaxRelSpeed.x, minMaxRelSpeed.y); //generate te current enemy's speed
        distanceValue = Random.Range(minMaxDistanceBetween.x, minMaxDistanceBetween.y);
        timeBetween = distanceValue / gC.speed + relSpeedValue;

        //myTr.position = spawnPoint.position;
    }


    void Update()
    {
        
        switch (enemyPhase)
        {
            case EnemyPhase.start:

                
                // move enemy to the ball
                myTr.Translate(myTr.right * (gC.speed + relSpeedValue) * Time.deltaTime, Space.World);

               // check if enemy isCatchable (not a bird) and catch him if the enemy riches the ball
                if (isCatchable && myTr.position.x >= ball.transform.position.x+2)
                {
                    // and if it's true, set ball as enemy's parent 
                    myTr.SetParent(ball.rollingElement.transform);

                    // and set the position at the distance equal center of the ball minus radius and fourth part of the enemy
                    myTr.position = ball.rollingElement.transform.position - myTr.up * (Ball.ballRadius + thirdOfHight); 

                    // activate shaking
                    StartCoroutine(cameraC.Shake(cameraC.EnemyClash));

                    // then change enemyPhase to next one
                    enemyPhase = EnemyPhase.middle;

                }
                break;

            case EnemyPhase.middle:
                myAnim.SetBool("middle", true);
                // after enemy was glued earliar it is staying on the ball during stuck time
                Invoke("ChangeEnemyPhaseToEnd", stuckTime);
              
                break;

            case EnemyPhase.end:
                myAnim.SetBool("end", true);

                myTr.Translate(( (Vector3) myStartVel * throwPower + (Vector3) myVertVel*1.5f ) * Time.deltaTime, Space.World);    
                myVertVel = myVertVel - normolizedVec * 9.807f  * Time.deltaTime;
                myTr.Rotate(0, 0, GameController.worldAngVel * Time.deltaTime, Space.Self);
                Invoke("Destroy",3.0f);

                break;

            default:
                break;
        }
    }


    public void ChangeEnemyPhaseToEnd()
    {
        enemyPhase = EnemyPhase.end;
        myTr.parent = null;
      
    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }
}
