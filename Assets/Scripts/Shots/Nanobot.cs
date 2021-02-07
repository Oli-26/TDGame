using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nanobot : ShotBasic
{
    float Cooldown = 0f;
    float CooldownTime = 0.25f;
    float JumpRange = 1.5f;

    float __timeUntilNewHoverPoint = 0f;
    const float __timeBetweenHoverPoints = 0.2f;
    Vector3 positionOffset = new Vector3(0f, 0f, 0f);

    public void Start()
    {
        lifeTime = 10f;
        Destroy(gameObject, lifeTime);
    }

    public void Update()
    {
        Move();
        Cooldown -= TimePassed();
        __timeUntilNewHoverPoint -= TimePassed();
        if(__timeUntilNewHoverPoint <= 0){
            __timeUntilNewHoverPoint = __timeBetweenHoverPoints;
            positionOffset = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f), 0f);
        }

        if(Cooldown <= 0){
           AttackTarget();
           Cooldown = CooldownTime;
        }
    }

    public override void Move(){
        Vector3 changeVector;
        if(target == null){
            Destroy(gameObject);
        }else{
            if(Vector3.Distance(target.transform.position, transform.position) > 0.05f){
                changeVector = Vector3.Normalize(target.transform.position+positionOffset-transform.position) *TimePassed()*properties.Speed;
                BaseMove(changeVector);
            }
        }
    }

    void AttackTarget(){
        if(target != null && Vector3.Distance(target.transform.position, transform.position) < 0.5f){
            target.GetComponent<BaseEnemy>().TakeDamage(properties.Damage);
            if(target.GetComponent<BaseEnemy>().IsScheduledForDeath()){
                FindNewTarget();
            }
        }
    }

    void FindNewTarget(){
        GameObject[] enemies = GameObject.Find("Control").GetComponent<RoundManager>().GetAliveEnemies();

        for(int i = 0; i<enemies.Length; i++){
            if(Vector3.Distance(enemies[i].transform.position, transform.position) < JumpRange){
                target = enemies[i];
                return;
            }
        }
        Destroy(gameObject);   
    }


    public override void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "DamageReduction"){
            ReduceDamage(col.gameObject);
        }
    }
}
