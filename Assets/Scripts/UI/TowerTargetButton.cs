using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
public class TowerTargetButton :  Button, IPointerEnterHandler, IPointerExitHandler
{
    private bool mouseIsOver { get; set; }
    private TargetingMode[] targetModeArray= {TargetingMode.First, TargetingMode.Last, TargetingMode.Strong, TargetingMode.Weak};
    private int currentModeIndex = 0;

    new protected void Update()
    {
        base.Update();
    }
    

    public override void clicked(){
        var towerUpgrade = GetSelectedTower();
        currentModeIndex++;
        if(currentModeIndex > targetModeArray.Length-1){
            currentModeIndex = 0;
        }

        if(GetSelectedTower() == null)
            return;
        
        GetSelectedTower().SetTargetingMode(targetModeArray[currentModeIndex]);
        transform.GetChild(0).gameObject.GetComponent<Text>().text = targetModeArray[currentModeIndex].ToString();

        
    }

    private BaseTower GetSelectedTower()
    {
        return control.GetComponent<UiContoller>().SelectedTower;
    }

    public void SetText(TargetingMode mode){
        transform.GetChild(0).gameObject.GetComponent<Text>().text = mode.ToString();
        currentModeIndex = Array.FindIndex(targetModeArray,  w => w == mode);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        mouseIsOver = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        mouseIsOver = false;
    }
}
