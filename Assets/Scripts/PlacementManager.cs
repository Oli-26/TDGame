using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    bool towerIndicatorExists = false;
    GameObject towerIndicator;
    float shortPlacementBlocker = 0f;

    public GameObject Tower1;
    public GameObject Tower2;

    void Update()
    {
        if(shortPlacementBlocker > 0f){
            shortPlacementBlocker-= Time.deltaTime;
        }

        if(towerIndicatorExists){
            towerIndicator.transform.position = GetMouseToWorld();         
        }

        if (Input.GetMouseButtonDown(0) && shortPlacementBlocker <= 0 && towerIndicatorExists){
            if(!IsInsideGameObjectWithTag(GetMouseToWorld(), "Tower")){
                towerIndicatorExists = false;
                towerIndicator.GetComponent<BaseTower>().Place();
            }else{
                Debug.Log("Cannot place tower on a tower");
            }
        }
    }

    void CreateNewTowerIndicator(GameObject Tower){
        if(towerIndicatorExists){
            Debug.Log("Cannot create a tower because one already exists!");
            return;
        }
        towerIndicator = Instantiate(Tower, transform.position, Quaternion.identity);
        towerIndicatorExists = true;
        shortPlacementBlocker = 0.1f;
    }


    Vector3 GetMouseToWorld(){
        Vector3 pointTo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(pointTo.x, pointTo.y, 0);
    }


    bool IsInsideGameObjectWithTag(Vector3 pos, string tag){
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        foreach(GameObject obj in objects){
            if(obj.GetInstanceID() != towerIndicator.GetInstanceID() && obj.GetComponent<Collider2D>().bounds.Contains(pos)){
                return true;
            }
        }
        return false;
    }




    public void CreateTower(int type){
        switch(type){
            case 1:
                if(GetComponent<Stats>().SpendMoney(25)){
                    CreateNewTowerIndicator(Tower1);
                }
                break;
            case 2:
                if(GetComponent<Stats>().SpendMoney(100)){
                    CreateNewTowerIndicator(Tower1);
                }
                break;
            default:
                break;
        }
    }
}
