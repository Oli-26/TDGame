using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoundManager : TimeEffected
{
    int roundNumber = 1;
    public GameObject path;
    public GameObject ghostPawn;
    bool roundStarted = false;

    List<(int, GameObject)> enemyList;
    List<GameObject> aliveEnemyList = new List<GameObject>();

    private int roundTick = 0;
    private int numberSpawned = 0;


    void Start(){
        InvokeRepeating("spawnGhostPawn", 2f, 8f);
    }

    void FixedUpdate()
    {
       if(roundStarted){
           
           while(numberSpawned < enemyList.Count && checkIfSpawn()){
               numberSpawned++;
           }
       } 

       if(roundStarted && aliveEnemyList.Count == 0 && numberSpawned == enemyList.Count){
           roundStarted = false;
           EventCentral.EndRound();
           GetMoneyForRound();
           roundNumber++;
       }

       roundTick = Tick(roundTick);
    }
    
    public void GetMoneyForRound(){
        int moneyGain = 100 + roundNumber;

        GetComponent<Stats>().GainMoney(moneyGain);
    }

    public void StartNextRound(){
        if(roundStarted){
            return;
        }
        SetEnemiesForRound(roundNumber);
        Debug.Log("New round: " + roundNumber);
        aliveEnemyList.Clear();
        roundStarted = true;
        numberSpawned = 0;
        roundTick = 0;
    }

    void SetEnemiesForRound(int round){
        enemyList = GetComponent<RoundGenerator>().readRoundFromFile(round);
        sortBySpawnTime();
    }

    public void RemoveEnemyFromAliveList(GameObject g){
        aliveEnemyList.Remove(g);
    }

    public void AddEnemyToAliveList(GameObject g){
        aliveEnemyList.Add(g);
    }


    bool checkIfSpawn(){
        int checkTick;
        GameObject enemy;
        (checkTick,  enemy) = enemyList[numberSpawned];
        if(checkTick <= roundTick){
            aliveEnemyList.Add(CreateEnemy(enemy));
            return true;
        }
        return false;
    }

    public GameObject CreateEnemy(GameObject enemy){
        return Instantiate(enemy, new Vector3(-100f,-100f,0f), Quaternion.identity);
    }

    public GameObject CreateEnemy(int enemyNum){
        GameObject enemy = GetComponent<RoundGenerator>().prefabValueLookUp(enemyNum);
        return Instantiate(enemy, new Vector3(-100f,-100f,0f), Quaternion.identity);
    }

    public GameObject[] GetAliveEnemies(){
        return aliveEnemyList.ToArray();
    }

    public void sortBySpawnTime(){
        enemyList.Sort((a, b) => a.Item1.CompareTo(b.Item1));
        //foreach(var (a,b) in enemyList){
            //Debug.Log(a);
        //}
    }
    public void spawnGhostPawn(){
        if(!roundStarted){
            Instantiate(ghostPawn, new Vector3(-100f,-100f,0f), Quaternion.identity);
        }
    }
}
