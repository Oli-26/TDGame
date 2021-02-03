using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : TimeEffected
{
    protected GameObject lastFlag;
    protected GameObject nextFlag;
    protected bool nextFlagExists = false;
    protected bool initalFlagSet = false;

    protected Path pathScript;

    protected float speed = 1f;

    public float health = 1f;
    protected int moneyDropped = 1;
    protected int damageDealt = 1; 

    protected GameObject control;

    protected float distanceTraveled = 0f;

    bool ScheduledForDeath = false;

    protected virtual void Start()
    {
        pathScript = GameObject.FindWithTag("Path").GetComponent<Path>();
        control = GameObject.Find("Control");
    }

    protected virtual void Update()
    {
        if(!initalFlagSet){
            SetLastFlag(pathScript.GetInitalFlag(out bool flagExists));
            if(flagExists){
                initalFlagSet = true;
                transform.position = lastFlag.transform.position;
            }   
        }
        Move();
    }

    public void SetLastFlag(GameObject flag){
        lastFlag = flag;
        GameObject maybeNextFlag = pathScript.GetNextFlag(lastFlag, out bool flagExists);
        nextFlagExists = flagExists;
        if(flagExists){
            nextFlagExists = true;
            nextFlag = maybeNextFlag;
        }
    }

    public bool CheckIfFinished() {
        if(!nextFlagExists){ 
            control.GetComponent<Stats>().DealDamage(damageDealt);
            Die();
        }
        return !nextFlagExists;
    } 

    public virtual void Move(){
        CheckIfFinished();

        moveBetween(transform.position, nextFlag.transform.position, 1f);
        CheckAndChangeDirection();
    }

    protected void CheckAndChangeDirection(){
        if(Vector3.Distance(transform.position, nextFlag.transform.position) < 0.04f){
            SetLastFlag(nextFlag);
        }
    }

    protected void moveBetween(Vector3 from, Vector3 to, float multi){
        Vector3 changeVector = new Vector3(to.x-from.x, to.y-from.y, 0);
        changeVector = Vector3.Normalize(changeVector)*TimePassed()*speed*multi;
        Vector3 oldPos = transform.position;
        BaseMove(changeVector);
        distanceTraveled += Vector3.Distance(oldPos, transform.position);
    }


    public virtual void TakeDamage(float d){
        health -= d;
        CheckDead();
    }

    protected virtual void CheckDead(){
        if(health <= 0){
            control.GetComponent<Stats>().GainMoney(moneyDropped);
            Die();
        }
    }

    public void OverRideInitalisationWithNewSpawn(Vector3 pos, GameObject flag, float distanceMoved){
        pathScript = GameObject.FindWithTag("Path").GetComponent<Path>();
        initalFlagSet = true;
        transform.position = pos;
        distanceTraveled = distanceMoved;
        SetLastFlag(flag);
    }

    public float GetDistanceTraveled(){
        return distanceTraveled;
    }

    private void Die() {
        control.GetComponent<RoundManager>().RemoveEnemyFromAliveList(gameObject);
        ScheduledForDeath = true;
        Destroy(gameObject);
    }

    public bool IsScheduledForDeath(){
        return ScheduledForDeath;
    }
}
