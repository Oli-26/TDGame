using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int Money = 50;
    
    public void GainMoney(int m){
        Money += m;
        GetComponent<UiUpdater>().UpdateMoney();
    }

    public bool SpendMoney(int amount){
        if(Money >= amount){
            Money -= amount;  
            GetComponent<UiUpdater>().UpdateMoney();
            return true;
        }else{
            return false;
        }

    }
}
