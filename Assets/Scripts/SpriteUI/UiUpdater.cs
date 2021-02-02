using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiUpdater : MonoBehaviour
{
    public GameObject MoneyText;
    public GameObject HealthText;

    private Stats stats;

    public void Awake(){
        stats = GetComponent<Stats>();
        UpdateMoney();
    }
    public void UpdateMoney(){
        MoneyText.GetComponent<TextMesh>().text = "$" + stats.money.ToString();
    }

    public void UpdateHealth() {
        HealthText.GetComponent<TextMesh>().text = stats.hp.ToString() + "HP";


    }
}
