using UnityEngine;
using System.Collections;

public class DataManager : MonoBehaviour {
    


	private  int _score;
	private  int _hightScore;
    public int waveNo;
    private int _coins;
    private int _achiveNo;

    public static DataManager instance;

    void OnEnable()
    {
        // Сида нужно дописать инициализацию монет! при старте игры.
        _coins = 0;
        _score = 0;
        _hightScore = 0;
        _achiveNo = 0;
    }
    void Awake()
    {
      
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {
            instance = this;
           
        }
        else
        {
            Destroy(this.gameObject);
        }
        _score = 0;
        _hightScore = 0;
        
    }


    public int Score
    {
        get
        {
            return _score;

        }
    }

        public int AchiveNo { 
		get{
			return _achiveNo;
		}
	}

	public int HightScore {
		get {
			return _hightScore;
		}
	}
    public int Coins
    {
        get
        {
            return _coins;
        }
    }

	public void AddScore (int aScore)
	{
		_score = _score + aScore;
        EventManager.TriggerEvent("AddScore");
		if(_hightScore<_score)
		{
			SetHightScore (_score);
            
		}


	}
	public void ResetScore ()
	{
		_score = 0;
	}

	public void SetHightScore (int aScore)
	{
		_hightScore = aScore;
	}

    public void AddCoins(int anAmount)
    {
        _coins = _coins + anAmount;
        EventManager.TriggerEvent("coinAdd");
    }

    public void ReduceCoins(int anAmount)
    {
        _coins -= anAmount;
    }

    public void IncreaseAchiveNo(int anAmount)
    {
        if (_achiveNo >= FindObjectOfType<GameController>().coinsNeededToGetAchive.Length - 1)
        { 
            _achiveNo = FindObjectOfType<GameController>().coinsNeededToGetAchive.Length - 1;
        }
        else if (_achiveNo < 0) 
        {
            _achiveNo = 0;
        }
        else
        {
            _achiveNo += anAmount;
        }
    }

    public void DecreaseAchiveNo(int anAmount)
    {
        _achiveNo -= anAmount;
    }

}
