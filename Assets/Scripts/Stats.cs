using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int money = 50;
    public int hp = 100;

    private UiUpdater uiUpdater;

    public void Awake() {
         uiUpdater = GetComponent<UiUpdater>();
    }

    
    public void GainMoney(int m){
        money += m;
        uiUpdater.UpdateMoney();
    }

    public bool SpendMoney(int amount){
        if(money >= amount){
            money -= amount;  
            uiUpdater.UpdateMoney();
            return true;
        }else{
            return false;
        }

    }

    public void DealDamage(int damage) {
        if ((hp - damage) < 0) {
            // TODO: DIE HERE
            hp = 0;
        } else {
            hp -= damage;
        }
        uiUpdater.UpdateHealth();
    }
}
