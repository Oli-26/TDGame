using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUI : TimeEffected
{
    // Start is called before the first frame update
    protected void Start()
    {
        GetRangeIndicator();
    }

    // Update is called once per frame
    protected void Update()
    {
        SetRangeIndicatorOnHover();
    }

    // Lets do some UI stuff here for now.
    GameObject rangeIndicator;

    void GetRangeIndicator(){
        rangeIndicator = transform.GetChild(0).gameObject;
    }

    void SetRangeIndicatorOnHover(){
        if(CheckIfMouseInRange()){
            rangeIndicator.SetActive(true);
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
}
