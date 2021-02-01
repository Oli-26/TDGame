using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Exception = System.Exception;
public class RoundGenerator : MonoBehaviour
{

    public GameObject pawnPrefab;
    public GameObject knightPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
                    string[] valueDouble = line.Split(' ');
                    int spawnAtTick = 6*int.Parse(valueDouble[0]);
                    GameObject enemyToSpawn = prefabValueLookUp(int.Parse(valueDouble[1]));
                    (int, GameObject) pair = (spawnAtTick, enemyToSpawn);
                    returnList.Add(pair);
                }
                _reader.Close();
                return returnList;
            }
        }catch(Exception e){
            Debug.Log(e);
        }
        
        return new List<(int, GameObject)>();
    }

    GameObject prefabValueLookUp(int num){
        switch(num){
            case 1:
                return pawnPrefab;
            case 2:
                return knightPrefab;
            default:
                return pawnPrefab;
        }
    }
}
