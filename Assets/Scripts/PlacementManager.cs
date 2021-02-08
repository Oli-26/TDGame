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
    public GameObject Tower3;
    public GameObject Tower4;
    public GameObject Tower5;

    const int tower1Cost = 100;
    const int tower2Cost = 300;
    const int tower3Cost = 300;
    const int tower4Cost = 500;
    const int tower5Cost = 800;

    const float xSize = 1f;
    const float ySize = 1f;
    const float xStart = -5.67f;
    const float yStart = 3f;

    void Update()
    {
        if(shortPlacementBlocker > 0f){
            shortPlacementBlocker-= Time.deltaTime;
        }

        if(towerIndicatorExists){
            towerIndicator.transform.position = GetMouseToGrid();         
        }

        if (Input.GetMouseButtonDown(0) && shortPlacementBlocker <= 0 && towerIndicatorExists){
            if(!IsInsideGameObjectWithTag(GetMouseToGrid(), "Tower")){
                towerIndicatorExists = false;
                towerIndicator.GetComponent<BaseTower>().Place();
                towerIndicator.transform.position = GetMouseToGrid();
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

    Vector3 GetMouseToGrid(){
        Vector3 pointTo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float x = pointTo.x;
        float y = pointTo.y;
        x = x - xStart;
        y = y - yStart;
        float gridOffsetX = x/(xSize/2);
        float gridOffsetY = y/(ySize/2);

        int gridOffSetXNearest = (int)Mathf.Floor(gridOffsetX);
        int gridOffsetYNearest = (int)Mathf.Floor(gridOffsetY);

        float finalPositionX = xStart + gridOffSetXNearest*(xSize/2) + xSize/4;
        float finalPositionY = yStart + gridOffsetYNearest*(ySize/2) + ySize/4;
        return new Vector3(finalPositionX, finalPositionY, 0f);
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
                if(GetComponent<Stats>().SpendMoney(tower1Cost)){
                    CreateNewTowerIndicator(Tower1);
                }
                break;
            case 2:
                if(GetComponent<Stats>().SpendMoney(tower2Cost)){
                    CreateNewTowerIndicator(Tower2);
                }
                break;
            case 3:
                if(GetComponent<Stats>().SpendMoney(tower3Cost)){
                    CreateNewTowerIndicator(Tower3);
                }
                break;
            case 4:
                if(GetComponent<Stats>().SpendMoney(tower4Cost)){
                    CreateNewTowerIndicator(Tower4);
                }
                break;
            case 5:
                if(GetComponent<Stats>().SpendMoney(tower5Cost)){
                    CreateNewTowerIndicator(Tower5);
                }
                break;
            default:
                break;
        }
    }
}
