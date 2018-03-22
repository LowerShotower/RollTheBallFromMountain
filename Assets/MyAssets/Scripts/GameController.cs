using UnityEngine;
using System.Collections;


public enum GamePhase { InitGamePlay, GamePlaying, None, EndGamePlay, StartingCondition, Pause }

[System.Serializable]                              // used to set enemy instantiation options
public class SpawnEnemy : System.Object
{
    public Transform enemy;                         // which enemy we should generate
    public float weight;                            // weight
}



[System.Serializable]                   // ------------------------wave perfs---------------------
public class Wave
{
    public string name;
    //public WaveNumber waveNumber;
    public float timeDelay;
    [Range(0.1f,3.0f)]
    public float coeff;
    //public MinMax  repeatTime;
    public SpawnEnemy[] spawnEnemies;

    [System.NonSerialized]public float currRepeatTime;  // used for instantiate through it. Set 
    
    [System.NonSerialized]public int enemyToInst = 0;

}

public class TimerClass                                 // ------------------Timer classs------------------
{
    public bool isTimerRunnig = false;
    float timeElapsed =0.0f;
    float currentTime = 0.0f;
    float lastTime = 0.0f;


    public void UpdateTimer()
    {
        if (isTimerRunnig)
        {
            timeElapsed = Mathf.Abs(Time.time - lastTime);
            currentTime += timeElapsed;
            lastTime = Time.time;
        }
    }

    public void StartTimer()
    {
        if (!isTimerRunnig)
        {
            lastTime = Time.time;
        }
    }
    public void StopTimer()
    {
        isTimerRunnig = false;
    }
    public void ResetTimer()
    {
        timeElapsed = 0.0f;
        currentTime = 0.0f;
        lastTime = Time.time;
    }

    public float GetTime()
    {
        return (float)currentTime;
    }

}                                                       // end of timer class


public class GameController : MonoBehaviour {           // -------GameController------------


    //[Space(10, order = 0)]
    [Range(0, 30)]
    public  float speed;

    public Transform[] heroes;
    
    public Transform[] spawnPoints;

    public int[] coinsNeededToGetAchive;

    private bool isInwoked;
    private float worldWaveTimerTicks;

    public static GamePhase gamePhase;

    public static Vector2 worldVel;    // Linier Vel
    public static float worldAngVel; //Angular Vel

    public Wave[] waves;

    [System.NonSerialized]
     Wave currWave;
    Transform currEnemy;
    float checkNextWaveTime; // summ of all previous wave's time intervals changin(increasing)during gameplay by adding timeInterval of previous wave

    TimerClass enemyTimer;
    int i; // it is used as number of current wave. starting number equal to -1;

    //public bool isPaused;



    void Awake()
    {
        // create a new timer 
        enemyTimer = new TimerClass();

        worldVel = Ground.worldDir * speed;
        worldAngVel = (speed / Ball.ballRadius) * Mathf.Rad2Deg;

        checkNextWaveTime = 0;
    }

    void Start()
    {
        gamePhase = GamePhase.None;// I don't actually know why i make this chack. And later set in Ball after it'll reach the ending position
    
    }


    void Update()
    {
        
        switch (gamePhase)
        {
            case GamePhase.StartingCondition:
                
                break;

            case GamePhase.InitGamePlay:
                
                i = -1;
                worldWaveTimerTicks = 0;
                checkNextWaveTime = 0;

                break;

            case GamePhase.GamePlaying:
                // Increase world timing (actually used for starting waves) during gameplay
                worldWaveTimerTicks += Time.deltaTime;

                // Increase wave depending on i var. it will be set to null in GamePhase.InitGamePlay. It is choosing of wave
                if (i+1 < waves.Length && worldWaveTimerTicks >= checkNextWaveTime)
                {
                    i++;
                    currWave = waves[i];
                    
                    DataManager.instance.waveNo = i+1;
                    checkNextWaveTime += currWave.timeDelay;
                    EventManager.TriggerEvent("waveIncrease");
                    
                }

                if (currWave != null)
                {
                    enemyTimer.StartTimer();                // set the timer to current time (chacking if the timer is running)
                    
                    if (!enemyTimer.isTimerRunnig)
                    {
                        enemyTimer.isTimerRunnig = true;
                        // chose which enemy to  instantiate 
                        currWave.enemyToInst = Choose(currWave.spawnEnemies);
                        //Debug.Log(currWave.enemyToInst);
                        // Instantiate enemy
                       currEnemy = Instantiate(currWave.spawnEnemies[currWave.enemyToInst].enemy) as Transform;
                       
                        
                        currEnemy.position = spawnPoints[(int)currEnemy.gameObject.GetComponentInChildren<Enemies>().chooseSpawnPoint].position;
                        currEnemy.rotation = Ground.worldRot;
                        currWave.currRepeatTime = currEnemy.GetComponentInChildren<Enemies>().timeBetween * currWave.coeff;
                      
                    }
                    // update timer, so it will continue to count
                    enemyTimer.UpdateTimer();
                    
                    // check if the timer reaches spawn time of next enemy
                    if (enemyTimer.GetTime() > currWave.currRepeatTime)
                    {
                        // Generate time for spawning next enemy
                        
                        enemyTimer.ResetTimer();
                        enemyTimer.isTimerRunnig = false;
                    }
                }
                break;
               
            default:
                break;
        }
    }

    // random choosing enemy depending on weight
    int Choose (SpawnEnemy[] probs)
    {

        float total = 0;

        foreach (SpawnEnemy elem in probs)
        {
            total += elem.weight;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i].weight)
            {
                return i;
            }
            else
            {
                randomPoint -= probs[i].weight;
            }
        }
        return probs.Length - 1;
    }

    // used by startButton in StartOptions script  used for main menu
    public static void SetStartCondition()    
    {
       gamePhase = GamePhase.StartingCondition;
    }


    public void InstantiateHero(int number)
    {

        
        Transform currHero;
        if (ScrollContentView.currentCentralObj > 2)
        {
            currHero = (Transform)Instantiate(heroes[0]);
        }
        else
        {
            currHero = (Transform)Instantiate(heroes[number]);
        }
        currHero.SetParent(GameObject.Find("Rolling Ball").transform,false);
        
        
       
    }
   

    public void Quit()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        
    }
    
}
