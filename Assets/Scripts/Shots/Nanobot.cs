using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class Nanobot : ShotBasic
{
    float Cooldown = 0f;
    
    float JumpRange = 3.5f;

    float __timeUntilNewHoverPoint = 1f;
    const float __timeBetweenHoverPoints = 0.5f;
    Vector3 positionOffset = new Vector3(0f, 0f, 0f);

    public GameObject explosion;

    

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
            __timeUntilNewHoverPoint = ((NanoBotProperties)properties).CooldownTime;
            positionOffset = new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f), 0f);
        }

        if(Cooldown <= 0){
           AttackTarget();
           Cooldown = ((NanoBotProperties)properties).CooldownTime;
        }

        LifeTime -= TimePassed();
        if(LifeTime < 0){
            Destroy(gameObject);
            
            if(((NanoBotProperties)properties).InternalTampering && properties.IsFirstInstance){
                Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 0.3f);
                List<GameObject> enemies = FindAllEnemiesInArea(transform.position, 1f);
                enemies.ForEach(enemy => enemy.GetComponent<BaseEnemy>().TakeDamage(5f));
            }
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


    public void setProperties(NanoBotProperties p){
        this.properties = NanoBotProperties.Duplicate(p);
    }

    public override void OnCollisionEnter2D(Collision2D col){
    }
}

public class NanoBotProperties : ShotProperties {
    public float LifeTime {get; set;} = 12f;
    public float CooldownTime {get; set;} = 1f;
    public bool InternalTampering {get; set;} = false;
    public NanoBotProperties(float speed, float damage, int damageInstances, bool homingShot) : base(speed, damage, damageInstances, homingShot){ }
    public NanoBotProperties() : base() {}

    public static NanoBotProperties Duplicate(NanoBotProperties properties){
            NanoBotProperties newProperties = new NanoBotProperties();
            // Base
            newProperties.Speed = properties.Speed;
            newProperties.Damage = properties.Damage;
            newProperties.DamageInstances = properties.DamageInstances;
            newProperties.HomingShot = properties.HomingShot;

            // Upgrade
            newProperties.LifeTime = properties.LifeTime;
            newProperties.CooldownTime = properties.CooldownTime;
            newProperties.InternalTampering = properties.InternalTampering;

            //Intrernal
            newProperties.IsFirstInstance = properties.IsFirstInstance;

            return newProperties;
        }
}