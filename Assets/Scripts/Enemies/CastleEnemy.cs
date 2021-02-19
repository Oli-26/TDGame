using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleEnemy : BaseEnemy
{
   
    float damageReduction = 0.8f;
    public GameObject effectField;

    protected override void Start()
    {
        base.Start();
        speed = 1f;
        health = 180;
        moneyDropped = 10;
        damageDealt = 10;
        Tier = 4;
    }

    protected override void Update()
    {
        base.Update();
        if(effects.AbilityBlocked){
            effectField.SetActive(false);
        }else{
            effectField.SetActive(true);
        }

    }

    public float GetDamageReduction(){
        return damageReduction;
    }
}
