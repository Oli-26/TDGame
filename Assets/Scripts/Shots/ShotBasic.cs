using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBasic : TimeEffected
{
    protected GameObject target;
    protected Vector3 direction;
    protected float lifeTime = 4f;
    protected GameObject ignoreEnemy;

    public ShotProperties properties;

    protected bool reverseDirection = false;
    protected bool ignoreEnemySet = false;


    private float explodeOutwardsTime = 0.15f;
    private Vector3 outwardsVector;

    public void Start()
    {
        Destroy(gameObject, lifeTime);
        outwardsVector = Vector3.Normalize(new Vector3(Random.Range(-1,1), Random.Range(-1,1), 0));
    }

    public void Update()
    {
        Move();
    }

    public void SetTarget(GameObject obj){
        if(obj == null)
            return;
        target = obj;
        Vector3 targetPos = target.transform.position;
        direction = Vector3.Normalize(new Vector3(targetPos.x-transform.position.x, targetPos.y-transform.position.y, 0));
    }

    public virtual void Move(){
        Vector3 changeVector;
        if(properties.HomingShot && target != null){
            changeVector = Vector3.Normalize(target.transform.position-transform.position) *TimePassed()*properties.Speed;
        }else{
            changeVector = direction *TimePassed()*properties.Speed;
        }
        changeVector = reverseDirection ? -changeVector : changeVector;
        

        // Make split shot look cooler with exploding outwards property
        if(properties.ExplodeOutwards){
            if(explodeOutwardsTime >= 0){
                explodeOutwardsTime -= TimePassed();
                changeVector = outwardsVector *TimePassed()*properties.Speed*2.5f;
            }else{
                SetTarget(target);
            }
            
        }
        transform.position += changeVector;
        
        gainDamageFromRange(Vector3.Distance(new Vector3(0,0,0), changeVector));
    }


    protected void gainDamageFromRange(float distance){
        if(properties.GainsDamageWithRange && properties.RangeBonusDamage < 3f){
            properties.RangeBonusDamage += 1f*(distance);
        }
    }

    protected void ReduceDamage(GameObject g){
        GameObject castleEnemy = g.transform.parent.gameObject;
        CastleEnemy CastleScript = castleEnemy.GetComponent<CastleEnemy>();
        properties.reduceDamage(CastleScript.GetDamageReduction());
    }

    public virtual void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "DamageReduction"){
            ReduceDamage(col.gameObject);
        }
        if(col.gameObject.tag == "Enemy" && properties.DamageInstances >= 1){
            if(ignoreEnemySet && col.gameObject.GetInstanceID() == ignoreEnemy.GetInstanceID()){
                return;
            }
            if(col.gameObject.GetComponent<BaseEnemy>().IsScheduledForDeath()){
                return;
            }
            float bonusAdjustedDamage = properties.Damage + (properties.GainsDamageWithRange ? properties.RangeBonusDamage : 0f);
            float randomNumber = UnityEngine.Random.Range(0f,1f);
            float critAdjustedDamage = randomNumber < properties.CritChance ? bonusAdjustedDamage* properties.CritMultiplier : bonusAdjustedDamage;
            col.gameObject.GetComponent<BaseEnemy>().TakeDamage(critAdjustedDamage);
            
            if (properties.decrementDamageInstances() == 0) {
                Destroy(gameObject);
            }
            
        }
    }
    
    public void SetIgnoreEnemy(GameObject enemy){
        ignoreEnemy = enemy;
    }

    public virtual void setProperties(ShotProperties properties) {
        this.properties = ShotProperties.Duplicate(properties);
    }

    public ShotProperties getProperties() {
        return properties;
    } 

    public void Reverse(float chance){
        if(reverseDirection)
            return;
        float rand = Random.Range(0, 1f);
        if(rand < chance){
            reverseDirection = true;
            properties.Speed = properties.Speed*1.4f;
        }
        
    }

}

public class ShotProperties {

        // Default Properties
        public float Speed {get; set;}
        public float Damage {get; set;}
        public int DamageInstances {get; set;}
        public bool HomingShot {get; set;}

        // Upgraded properties
        public bool GainsDamageWithRange {get; set;}
        public float CritChance = 0f;
        public float CritMultiplier = 0f;

        //Internal Propeties
        public bool DamageReduced {get; set;}
        public float RangeBonusDamage {get; set;}
        public bool ExplodeOutwards {get; set;}

        public ShotProperties(){
            RangeBonusDamage = 0f;
            DamageReduced = false;
        }
        public ShotProperties(float speed, float damage, int damageInstances, bool homingShot) {
            Speed = speed;
            Damage = damage;
            DamageInstances = damageInstances;
            HomingShot = homingShot;
            DamageReduced = false;         
            RangeBonusDamage = 0f;
            ExplodeOutwards = false;       
        }

        public int decrementDamageInstances() {
            return --DamageInstances;
        }

        public void reduceDamage(float reduceAmount){
            if(DamageReduced){
                return;
            }else{
                Damage = Damage * (1f-reduceAmount);
                DamageReduced = true;
            }
        }


        public static ShotProperties Duplicate(ShotProperties properties){
            ShotProperties newProperties = new ShotProperties();
            // Base
            newProperties.Speed = properties.Speed;
            newProperties.Damage = properties.Damage;
            newProperties.DamageInstances = properties.DamageInstances;
            newProperties.HomingShot = properties.HomingShot;

            // Upgrade     
            newProperties.GainsDamageWithRange = properties.GainsDamageWithRange;
            newProperties.CritChance = properties.CritChance;
            newProperties.CritMultiplier = properties.CritMultiplier;

            return newProperties;
        }
    }
