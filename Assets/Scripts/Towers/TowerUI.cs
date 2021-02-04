using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TowerUI : TimeEffected
{
    public string TowerName;
    protected GameObject control;

    protected void Start()
    {
        GetRangeIndicator();
        control = GameObject.Find("Control");
    }

    protected void Update()
    {
        OnHover();
    }

    GameObject rangeIndicator;

    void GetRangeIndicator(){
        rangeIndicator = transform.GetChild(0).gameObject;
    }

    void OnHover(){
        if(CheckIfMouseInRange()){
            rangeIndicator.SetActive(true);
            if(Input.GetMouseButtonDown(0)){
                OpenTowerMenuOnClick();
            }
        }else{
            rangeIndicator.SetActive(false);
        }
    }

    public bool CheckIfMouseInRange(){
        if(PositionIsInsideGameObject(GetMouseToWorld(), gameObject)){
            return true;
        }
        return false;
    }


    Vector3 GetMouseToWorld(){
        Vector3 pointTo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(pointTo.x, pointTo.y, 0);
    }


    bool PositionIsInsideGameObject(Vector3 pos, GameObject obj){
        if(obj.GetComponent<Collider2D>().bounds.Contains(pos)){
            return true;
        }
        return false;
    }

    void OpenTowerMenuOnClick(){
        control.GetComponent<UiContoller>().towerSelectionPanel.SetActive(true);
        control.GetComponent<UiContoller>().towerSelectionPanel.GetComponent<TowerSelection>().Populate(TowerName);
    }


}
