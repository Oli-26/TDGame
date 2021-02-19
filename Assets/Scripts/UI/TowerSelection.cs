using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerSelection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool dragging;
    private bool mouseIsOver;
    private Vector3 clickOffset;


    public GameObject Control;
    public GameObject NameTag;

    public GameObject Upgrade0;
    public GameObject Upgrade1;
    public GameObject Upgrade2;

    public GameObject TargetButton;

    public void Update() {
        Populate();
        if(Input.GetMouseButtonDown(0) &&  mouseIsOver && gameObject.activeSelf){
            dragging = true;
            clickOffset = GetMouseToWorld() - transform.position;
        }
        if(Input.GetMouseButtonUp(0)){
            dragging = false;
        }
        if (dragging) {
            transform.position = GetMouseToWorld() - clickOffset;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        mouseIsOver = true;
    }
      
    public void OnPointerExit(PointerEventData eventData) {
        mouseIsOver = false;
    }

    Vector3 GetMouseToWorld(){
        Vector3 pointTo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(pointTo.x, pointTo.y, transform.position.z);
    }

    public void Close(){
        mouseIsOver = false;
        dragging = false;
        gameObject.SetActive(false);
    }

    public void Populate(){
        BaseTower tower = Control.GetComponent<UiContoller>().SelectedTower;
        if (tower == null) return;

        NameTag.GetComponent<Text>().text = tower.TowerName;

        List<TowerUpgrade> buyableUpgrades = tower.Tower.GetBuyableUpgrades();
        Upgrade0.GetComponent<Text>().text = buyableUpgrades.Exists(u => u.track == 0) ? createUpgradeLabel(buyableUpgrades.Find(u => u.track == 0)) : "Maxed!";
        Upgrade1.GetComponent<Text>().text = buyableUpgrades.Exists(u => u.track == 1) ? createUpgradeLabel(buyableUpgrades.Find(u => u.track == 1)) : "Maxed!";
        Upgrade2.GetComponent<Text>().text = buyableUpgrades.Exists(u => u.track == 2) ? createUpgradeLabel(buyableUpgrades.Find(u => u.track == 2)) : "Maxed!";

        TargetButton.GetComponent<TowerTargetButton>().SetText(tower.GetTargetingMode());

    }

    private string createUpgradeLabel(TowerUpgrade upgrade) {
        return upgrade.name + ": $" + upgrade.cost;
    }
}
