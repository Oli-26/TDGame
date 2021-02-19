using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nanobot : ShotBasic
{
    float Cooldown = 0f;
    float CooldownTime = 1f;
    float JumpRange = 3.5f;

    float __timeUntilNewHoverPoint = 0f;
    const float __timeBetweenHoverPoints = 0.5f;
    Vector3 positionOffset = new Vector3(0f, 0f, 0f);

    float LifeTime = 12f;

    new public void Start()
    {
        
    }

    new public void Update()
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

        LifeTime -= TimePassed();
        if(LifeTime < 0){
            Destroy(gameObject);
        }
    }

    public override void Move(){
        Vector3 changeVector;
        if(target == null){
            FindNewTarget();
        }else{
            if(Vector3.Distance(target.transform.position+ positionOffset, transform.position) > 0.05f){
                changeVector = Vector3.Normalize(target.transform.position+positionOffset-transform.position) *TimePassed()*properties.Speed;
                transform.position += changeVector;
            }else{
                transform.position = target.transform.position+positionOffset;
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

    public void setProperties(NanoBotProperties p){

    }
}

public class NanoBotProperties : ShotProperties {
    public NanoBotProperties(float speed, float damage, int damageInstances, bool homingShot) : base(speed, damage, damageInstances, homingShot){ }
    public NanoBotProperties() : base() {}

    new public static NanoBotProperties Duplicate(ShotProperties properties){
            NanoBotProperties newProperties = new NanoBotProperties();
            // Base
            newProperties.Speed = properties.Speed;
            newProperties.Damage = properties.Damage;
            newProperties.DamageInstances = properties.DamageInstances;
            newProperties.HomingShot = properties.HomingShot;

            // Upgrade     
            newProperties.GainsDamageWithRange = properties.GainsDamageWithRange;
            newProperties.CritChance = properties.CritChance;
            newProperties.CritMultiplier = properties.CritMultiplier;

            newProperties.PiercePerfect = properties.PiercePerfect;

            return newProperties;
        }
}