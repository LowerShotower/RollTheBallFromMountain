using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

    int currEnemy;

    void OnTriggerEnter2D(Collider2D other )
    {
        
        if ( other.tag == "enemy" && currEnemy != other.gameObject.GetInstanceID() && !Hero.isDefeated)
        {
            DataManager.instance.AddScore(1);
            if (DataManager.instance.Score % 2 == 0)
            {
                DataManager.instance.AddCoins(1);
            }
            currEnemy = other.gameObject.GetInstanceID();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (currEnemy == other.gameObject.GetInstanceID())
        {
            currEnemy = 0;
        }
    }
}

