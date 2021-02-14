using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleEnemy : BaseEnemy
{
   
    float damageReduction = 0.5f;
    public GameObject effectField;

    protected override void Start()
    {
        base.Start();
        speed = 1f;
        health = 70;
        moneyDropped = 10;
        damageDealt = 10;
    }

    protected override void Update()
    {
        base.Update();
        if(blockedAbilityTime >= 0){
            effectField.SetActive(false);
            blockedAbilityTime -= TimePassed();
        }else{
            effectField.SetActive(true);
        }

    }

    public float GetDamageReduction(){
        return damageReduction;
    }
}
