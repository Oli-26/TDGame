using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightEnemy : BaseEnemy
{
    float boostCooldown = 2.5f;
    float nextBoost = 2f;
    float boostRemaining = 0f;
    float boostTime = 1f;
    bool boosting = false;

    protected override void Start()
    {
        base.Start();
        speed = 1f;
        health = 5.5f;
        moneyDropped = 2;
        damageDealt = 2;
        Tier = 2;
    }

    protected override void Update()
    {
        base.Update();
        HandleBoosting();
    }

    public override void Move(){
        CheckIfFinished();

        float movementMultiplier = boosting ? 3f : 1f;
        moveBetween(transform.position, nextFlag.transform.position, movementMultiplier);
        CheckAndChangeDirection();
    }

    void HandleBoosting(){
        if(nextBoost > 0 && boosting == false){
            nextBoost-= TimePassed();
        }
        if(boosting && boostRemaining <= 0){
            nextBoost = boostCooldown;
            boosting = false;
        }
        if(nextBoost <= 0 && boosting == false){
            boostRemaining = boostTime;
            boosting = true;
        }
        if(boostRemaining >= 0 && boosting){
            boostRemaining -= TimePassed();
        }
        
    }

  
}
