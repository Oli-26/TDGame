using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenEnemy : BaseEnemy
{
   float pulseCooldown = 5f;
   float nextPulse = 2.5f;
   bool pulseMode = false;
   float pulseTimeLeft = 2.5f;
   float pulseTime = 2.5f;

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
        nextPulse -= TimePassed();
        if(nextPulse < 0){
            pulseMode = true;
            GetComponent<SpriteRenderer>().color = new Color(0.9f, 0.4f, 0.4f);
            nextPulse = pulseCooldown;
            pulseTimeLeft = pulseTime;
        }
        if(pulseMode){
            pulseTimeLeft -= TimePassed();
            if(pulseTimeLeft < 0){
                pulseMode = false;
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
            }
        }
    }

    public bool PulseUp(){
        return pulseMode;
    }
    
}
