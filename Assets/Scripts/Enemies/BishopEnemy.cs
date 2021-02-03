using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopEnemy : BaseEnemy
{
    int splitCount = 6;

   protected override void Start()
    {
        base.Start();
        speed = 0.8f;
        health = 14f;
        moneyDropped = 3;
    }

    protected override void Update()
    {
        base.Update();   
    }

    public override void Move(){
        CheckIfFinished();

        moveBetween(transform.position, nextFlag.transform.position, 1f);
        CheckAndChangeDirection();
    }

    protected override void CheckDead(){
        if(health <= 0){
            for(int i = 0; i < splitCount; i++){
                GameObject newPawn = control.GetComponent<RoundManager>().CreateEnemy(1);
                control.GetComponent<RoundManager>().AddEnemyToAliveList(newPawn);
                Vector3 offset = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0);
                newPawn.GetComponent<BaseEnemy>().OverRideInitalisationWithNewSpawn(gameObject.transform.position + offset, lastFlag, GetDistanceTraveled());
            }
            control.GetComponent<Stats>().GainMoney(moneyDropped);
            control.GetComponent<RoundManager>().RemoveEnemyFromAliveList(gameObject);
            Destroy(gameObject);
        }
    }

}
