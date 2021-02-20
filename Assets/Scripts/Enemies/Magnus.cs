using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Magnus : BaseEnemy
{
    
    protected override void Start()
    {
        base.Start();
        speed = 0.25f;
        health = 500;
        moneyDropped = 100;
        damageDealt = 100;
        Tier = 10;
    }

    protected override void Update()
    {
        transform.Rotate(Vector3.back*2);
        base.Update();
    }
    
    protected override void CheckDead(){
        if(health <= 0){
            for(int i = 0; i < 25; i++)
            {
                GameObject newEnemy = control.GetComponent<RoundManager>().CreateEnemy((int) Random.Range(1, 5));
                control.GetComponent<RoundManager>().AddEnemyToAliveList(newEnemy);
                Vector3 offset = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0);
                newEnemy.GetComponent<BaseEnemy>().OverRideInitalisationWithNewSpawn(gameObject.transform.position + offset, lastFlag, GetDistanceTraveled());
            }
            control.GetComponent<Stats>().GainMoney(moneyDropped);
            control.GetComponent<RoundManager>().RemoveEnemyFromAliveList(gameObject);
            Destroy(gameObject);
        }
    }

}