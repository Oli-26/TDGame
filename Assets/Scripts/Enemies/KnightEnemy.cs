using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightEnemy : BaseEnemy
{
    float boostCooldown = 2f;
    float nextBoost = 2f;
    float boostRemaining = 0f;
    float boostTime = 1f;
    bool boosting = false;

    protected override void Start()
    {
        base.Start();
        speed = 0.9f;
        health = 4f;
        moneyDropped = 2;
    }

    protected override void Update()
    {
        base.Update();
        HandleBoosting();
    }

    public override void Move(){
        CheckIfFinished();

        float movementMultiplier = boosting ? 1.8f : 1f;
        moveBetween(transform.position, nextFlag.transform.position, movementMultiplier);
        CheckAndChangeDirection();
    }

    void HandleBoosting(){
        if(nextBoost > 0 && boostRemaining <= 0){
            nextBoost-= Time.deltaTime;
        }
        if(nextBoost <= 0){
            boostRemaining = boostTime;
            boosting = true;
        }
        if(boostRemaining >= 0){
            boostRemaining -= Time.deltaTime;
        }
        if(nextBoost <= 0 && boostRemaining <= 0){
            nextBoost = boostCooldown;
        }
    }

  
}
