using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
public class TowerSellButton :  Button, IPointerEnterHandler, IPointerExitHandler
{

    private bool mouseIsOver { get; set; }
    new protected void Update()
    {
        base.Update();
    }
    

    public override void clicked(){
        var towerUpgrade = GetSelectedTower();
        
        if(GetSelectedTower() == null)
            return;
        
        int worth = GetSelectedTower().GetTowerWorth();
        GetSelectedTower().SellTower();
        control.GetComponent<Stats>().GainMoney(worth); 
        transform.parent.gameObject.SetActive(false);
        control.GetComponent<UiContoller>().towerSelectionIndicator.SetActive(false);
    }

    private BaseTower GetSelectedTower()
    {
        return control.GetComponent<UiContoller>().SelectedTower;
    }

    public void SetText(string worth){
        transform.GetChild(0).gameObject.GetComponent<Text>().text = "Sell " + worth;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        mouseIsOver = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        mouseIsOver = false;
    }
}
