using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgradeButton : Button {

    public int upgrade = 0;

    public override void clicked(){
        Debug.Log(upgrade + " clicked");
        BaseTower tower = control.GetComponent<UiContoller>().SelectedTower;
        List<TowerUpgrade> upgrades = tower.GetBuyableUpgrades();
        if (control.GetComponent<Stats>().SpendMoney(upgrades.Find(u => u.track == upgrade).cost)) {
            tower.BuyUpgrade(upgrades.Find(u => u.track == upgrade));
        }
    }
}
