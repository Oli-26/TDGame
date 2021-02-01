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

    void Awake(){
        speed = 1.4f;
        health = 3f;
        value = 4;
        Debug.Log("knight initated");
    }
    protected override void Start()
    {
        base.Start();
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

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

    public override void Move(){
        if(!nextFlagExists){
            Debug.Log("flag not set, unable to move.");
            return;
        }
        Vector3 pos2 = nextFlag.transform.position;
        if(boosting){
            moveBetween(transform.position, pos2, 1.5f);
        }else{
            moveBetween(transform.position, pos2, 1f);
        }
        
        IfHitSetNewFlag();
    }
}
