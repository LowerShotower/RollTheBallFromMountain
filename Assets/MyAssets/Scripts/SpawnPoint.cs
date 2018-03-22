using UnityEngine;
using System.Collections;


public class SpawnPoint : MonoBehaviour {

    public Transform[] enemies;
    private Transform myTr;

   /* void OnEnable()
    {
        EventManager.StartListening("Spawn", Spawn);
    }*/


    /*void OnDisable()
    {
        EventManager.StopListening("Spawn", Spawn);
    }*/


    void Awake (){
        myTr = transform;
    }


    /*public void Spawn()
    {
       
        int i = Random.Range(0,enemies.Length-1);
        Instantiate(enemies[i], myTr.position, Ground.worldRot);
    }*/

}
