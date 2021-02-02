﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    protected GameObject lastFlag;
    protected GameObject nextFlag;
    protected bool nextFlagExists = false;
    protected bool initalFlagSet = false;

    protected Path pathScript;

    protected float speed = 1f;

    public float health = 1f;
    protected int value = 1;

    protected GameObject control;

    protected float distanceTraveled = 0f;

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
        if(flagExists){
            nextFlagExists = true;
            nextFlag = maybeNextFlag;
        }
    }

    public virtual void Move(){
        if(!nextFlagExists){
            return;
        };
        moveBetween(transform.position, nextFlag.transform.position, 1f);
        IfHitSetNewFlag();
    }

    protected void IfHitSetNewFlag(){
        if(Vector3.Distance(transform.position, nextFlag.transform.position) < 0.04f){
            SetLastFlag(nextFlag);
        }
    }

    protected void moveBetween(Vector3 from, Vector3 to, float multi){
        Vector3 changeVector = new Vector3(to.x-from.x, to.y-from.y, 0);
        changeVector = Vector3.Normalize(changeVector)*Time.deltaTime*speed*multi;
        transform.position += changeVector;
        distanceTraveled += Vector3.Distance(new Vector3(0f,0f,0f), changeVector);
    }


    public virtual void TakeDamage(float d){
        health -= d;
        CheckDead();
    }

    protected virtual void CheckDead(){
        if(health <= 0){
            control.GetComponent<Stats>().GainMoney(value);
            control.GetComponent<RoundManager>().RemoveEnemyFromAliveList(gameObject);
            Destroy(gameObject);
        }
    }

    public void OverRideInitalisationWithNewSpawn(Vector3 pos, GameObject flag){
        pathScript = GameObject.FindWithTag("Path").GetComponent<Path>();
        initalFlagSet = true;
        transform.position = pos;
        SetLastFlag(flag);
    }

    public float GetDistanceTraveled(){
        return distanceTraveled;
    }

}
