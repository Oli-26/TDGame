using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    int roundNumber = 1;
    public GameObject path;

    bool roundStarted = false;

    List<(int, GameObject)> enemyList;
    List<GameObject> aliveEnemyList = new List<GameObject>();


    public GameObject pawnPrefab;
    public GameObject knightPrefab;

    private int roundTick = 0;
    private int numberSpawned = 0;

    void FixedUpdate()
    {
       if(roundStarted){
           
           while(numberSpawned < enemyList.Count && checkIfSpawn()){
               Debug.Log("Spawned at: " + roundTick.ToString());

               numberSpawned++;
           }
       } 

       if(roundStarted && aliveEnemyList.Count == 0 && numberSpawned == enemyList.Count){
           roundStarted = false;
           roundNumber++;
       }

       roundTick++;
    }

    public void StartNextRound(){
        if(roundStarted){
            return;
        }
        Debug.Log("Starting round");
        SetEnemiesForRound(roundNumber);
        aliveEnemyList.Clear();
        roundStarted = true;
        numberSpawned = 0;
        roundTick = 0;
    }

    void SetEnemiesForRound(int round){
        enemyList = GetComponent<RoundGenerator>().readRoundFromFile(roundNumber);
        Debug.Log("Spawn times:\n");
        foreach( var (num,_) in enemyList){
            Debug.Log(num.ToString());
        }
    }

    public void RemoveEnemyFromAliveList(GameObject g){
        aliveEnemyList.Remove(g);
    }


    bool checkIfSpawn(){
        int checkTick;
        GameObject enemy;
        Debug.Log(enemyList[numberSpawned]);
        (checkTick,  enemy) = enemyList[numberSpawned];
        if(checkTick == roundTick){
            aliveEnemyList.Add(Instantiate(enemy, new Vector3(-100f,-100f,0f), Quaternion.identity));
            return true;
        }
        return false;
    }

}
