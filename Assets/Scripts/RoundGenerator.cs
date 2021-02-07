using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Exception = System.Exception;
public class RoundGenerator : MonoBehaviour
{

    public GameObject pawnPrefab;
    public GameObject knightPrefab;
    public GameObject bishopPrefab;
    public GameObject castlePrefab;
    public GameObject queenPrefab;

    public List<(int, GameObject)> readRoundFromFile(int round){
        string tempPath = Application.persistentDataPath + "/rounds/round" + (round.ToString()) +".txt";
        Debug.Log(tempPath);
        FileInfo t = new FileInfo(tempPath); 
        try{
            if(t.Exists) 
            { 
                var _reader = t.OpenText(); 
                string text = _reader.ReadToEnd();
                string[] lines = text.Split('\n');

                
                List<(int, GameObject)> returnList = new List<(int, GameObject)>();
                foreach(string line in lines){
                    string[] values = line.Split(' ');
                    int spawnAtTick = 6*int.Parse(values[0]);
                    GameObject enemyToSpawn = prefabValueLookUp(int.Parse(values[1]));
                    if(values.Length > 2){
                        int amount = int.Parse(values[2]);
                        int spawnSeperator = 1;
                        if(values.Length > 3){
                            spawnSeperator = int.Parse(values[3]);
                        }
                            for(int i = 0; i < amount; i++){
                                (int, GameObject) pair = (spawnAtTick+i*spawnSeperator*6, enemyToSpawn);
                                returnList.Add(pair);
                            }
                    }else{
                        (int, GameObject) pair = (spawnAtTick, enemyToSpawn);
                        returnList.Add(pair);
                    }
                }
                _reader.Close();
                return returnList;
            }
        }catch(Exception e){
            Debug.Log(e);
        }
        
        return new List<(int, GameObject)>();
    }

    public GameObject prefabValueLookUp(int num){
        switch(num){
            case 1:
                return pawnPrefab;
            case 2:
                return knightPrefab;
            case 3:
                return bishopPrefab;
            case 4:
                return castlePrefab;
            case 5:
                return queenPrefab;
            default:
                return pawnPrefab;
        }
    }
}
