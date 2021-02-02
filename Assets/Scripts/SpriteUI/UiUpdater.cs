using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiUpdater : MonoBehaviour
{
    public GameObject MoneyText;
    public void Awake(){
        UpdateMoney();
    }
    public void UpdateMoney(){
        MoneyText.GetComponent<TextMesh>().text = "$" + GetComponent<Stats>().Money.ToString();
    }
}
