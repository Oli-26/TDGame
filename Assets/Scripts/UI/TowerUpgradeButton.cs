using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerUpgradeButton : Button, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject upgradeDescriptionBox;
    public int upgrade = 0;
    private Canvas myCanvas;
    private bool mouseIsOver { get; set; }
    
    void Start()
    {
        myCanvas = GetComponentInParent<Canvas>();
    }

    protected void Update()
    {
        base.Update();
        if (mouseIsOver)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
            upgradeDescriptionBox.transform.position = myCanvas.transform.TransformPoint(pos);
        }
    }
    

    public override void clicked(){
        Debug.Log(upgrade + " clicked");
        var towerUpgrade = GetAssociatedUpgrade();

        if (control.GetComponent<Stats>().SpendMoney(towerUpgrade.cost)) {
            GetSelectedTower().BuyUpgrade(towerUpgrade);
            UpdateDescriptionBox();
        }
    }

    private TowerUpgrade GetAssociatedUpgrade()
    {
        List<TowerUpgrade> upgrades = GetSelectedTower().GetBuyableUpgrades();
        TowerUpgrade towerUpgrade = upgrades.Find(u => u.track == upgrade);
        return towerUpgrade;
    }

    private BaseTower GetSelectedTower()
    {
        return control.GetComponent<UiContoller>().SelectedTower;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        mouseIsOver = true;
        UpdateDescriptionBox();
        upgradeDescriptionBox.SetActive(true);
    }

    private void UpdateDescriptionBox()
    {
        upgradeDescriptionBox.transform.GetChild(0).gameObject.GetComponent<Text>().text =
            GetAssociatedUpgrade().description;
    }

    public void OnPointerExit(PointerEventData eventData) {
        mouseIsOver = false;
        upgradeDescriptionBox.SetActive(false);
    }
}
