using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : TimeEffected
{
    int roundNumber = 1;
    public GameObject path;
    bool roundStarted = false;

    List<(int, GameObject)> enemyList;
    List<GameObject> aliveEnemyList = new List<GameObject>();

    private int roundTick = 0;
    private int numberSpawned = 0;

    void FixedUpdate()
    {
       if(roundStarted){
           
           while(numberSpawned < enemyList.Count && checkIfSpawn()){
               numberSpawned++;
           }
       } 

       if(roundStarted && aliveEnemyList.Count == 0 && numberSpawned == enemyList.Count){
           roundStarted = false;
           GetMoneyForRound();
           roundNumber++;
       }

       roundTick = Tick(roundTick);
    }
    
    public void GetMoneyForRound(){
        int moneyGain = 100 + roundNumber*20;

        GetComponent<Stats>().GainMoney(moneyGain);
    }

    public void StartNextRound(){
        if(roundStarted){
            return;
        }
        SetEnemiesForRound(roundNumber);
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
}
