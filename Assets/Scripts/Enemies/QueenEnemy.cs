using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenEnemy : BaseEnemy
{
   public bool pulseMode = true;
   protected override void Start()
    {
        base.Start();
        speed = 1.1f;
        health = 200;
        moneyDropped = 25;
        damageDealt = 20;
    }

    protected override void Update()
    {
        base.Update();
        
        if(blockedAbilityTime >= 0){
            pulseMode = false;
            blockedAbilityTime -= TimePassed();
        }else{
            pulseMode = true;
        }
    }

    public bool PulseUp(){
        return pulseMode;
    }
    
}
