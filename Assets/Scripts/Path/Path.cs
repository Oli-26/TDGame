using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    GameObject[] flags;

    public GameObject EasyPath;
    public GameObject MediumPath;
    public GameObject HardPath;
    
    private GameObject spawnedPath;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GameInfo.difficulty);
        switch(GameInfo.difficulty){
            case "Easy":
                spawnedPath = Instantiate(EasyPath, new Vector3(0f,0f,0f), Quaternion.identity);
                break;
            case "Medium":
                spawnedPath = Instantiate(MediumPath, new Vector3(0f,0f,0f), Quaternion.identity);
                break;
            case "Hard":
                spawnedPath = Instantiate(HardPath, new Vector3(0f,0f,0f), Quaternion.identity);
                break;
            case "Random":
            default:
                spawnedPath = GetComponent<PathGenerator>().CreatePath(25);
                break;
        }

        spawnedPath.transform.parent = gameObject.transform;
        List<GameObject> tempList = new List<GameObject>();
        foreach (Transform child in spawnedPath.transform){
            tempList.Add(child.gameObject);
            Debug.Log("Added child");
        }
        flags = tempList.ToArray();
        LinkFlags();
    }


    GameObject[] GetAllChildrenWithTag(string tag){
        int numberOfChildren = gameObject.transform.childCount;
        List<GameObject> matches = new List<GameObject>();
        for(int n = 0; n < numberOfChildren; n++){
            GameObject nthChild = gameObject.transform.GetChild(n).gameObject;
            if(nthChild.transform.tag == tag && nthChild.activeSelf){
                matches.Add(nthChild);
            }
        }
        return matches.ToArray();
    }




    public GameObject GetNextFlag(GameObject flag, out bool flagExists){
        for(int i = 0; i< flags.Length; i++){
            if(flag == flags[i] && i != flags.Length-1){
                flagExists = true;
                return flags[i+1];
            }
        }
        flagExists = false;
        return null;
    }

    public GameObject GetInitalFlag(out bool flagExists){
        if(flags.Length == 0){
            flagExists = false;
            return null;
        }
        flagExists = true;
        return flags[0];
    }


    public void LinkFlags(){
        for(int i = 0; i < flags.Length-1; i++){
            var line = new GameObject();
            line.transform.position = new Vector3(0f,0f,-9f);
            var lineComponent = line.AddComponent<LineRenderer>();

            lineComponent.SetPosition(0, flags[i].transform.position);
            lineComponent.SetPosition(1, flags[i+1].transform.position);
            lineComponent.endWidth = 0.04f;
            lineComponent.startWidth = 0.04f;
            lineComponent.material = new Material(Shader.Find("Sprites/Default"));
            lineComponent.startColor = Color.white;
            lineComponent.endColor = Color.white;
            line.transform.parent = gameObject.transform;
            line.name = "drawnline";

        }   
    }
}
