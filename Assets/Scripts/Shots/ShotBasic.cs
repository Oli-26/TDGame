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


    private float explodeOutwardsTime = 0.2f;
    private Vector3 outwardsVector;

    public void Start()
    {
        Destroy(gameObject, lifeTime);
        outwardsVector = Vector3.Normalize(new Vector3(Random.Range(-1f,1f), Random.Range(-1f,1f), 0));
    }

    public void Update()
    {
        Move();
    }

    public bool SetTarget(GameObject obj){
        if(obj == null)
            return false;
        target = obj;
        Vector3 targetPos = target.transform.position;
        direction = Vector3.Normalize(new Vector3(targetPos.x-transform.position.x, targetPos.y-transform.position.y, 0));
        outwardsVector = direction;
        return true;
    }

    public virtual void Move(){
        Vector3 changeVector;

        if(properties.ExplodeOutwards){
            if(explodeOutwardsTime >= 0){
                explodeOutwardsTime -= TimePassed();
                changeVector = outwardsVector *TimePassed()*properties.Speed*1.2f;
            }else{
                if(!SetTarget(target)){
                    explodeOutwardsTime = 15f;
                }else{
                    properties.ExplodeOutwards = false;
                }
                return;
            }
            
        }else{
            if(properties.HomingShot && target != null){
                changeVector = Vector3.Normalize(target.transform.position-transform.position) *TimePassed()*properties.Speed;
            }else{
                if(target != null){
                    changeVector = (Vector3.Normalize(target.transform.position-transform.position)*0.3f + direction*0.7f) *TimePassed()*properties.Speed;
                }else{
                    changeVector = direction *TimePassed()*properties.Speed;
                }
                
            }
            changeVector = reverseDirection ? -changeVector : changeVector;
        }
        
        transform.position += changeVector;
        gainDamageFromRange(Vector3.Distance(new Vector3(0,0,0), changeVector));
    }


    protected void gainDamageFromRange(float distance){
        if(properties.GainsDamageWithRange && properties.RangeBonusDamage < 3f){
            properties.RangeBonusDamage += 1f*(distance);
        }
    }

    public void ReduceDamage(float reduction){
        properties.reduceDamage(reduction);
    }

    public virtual void OnCollisionEnter2D(Collision2D col){
        Debug.Log("Colliding with " + col.gameObject.name + "   " + col.gameObject.tag);
        
        if(col.gameObject.tag == "Enemy" && properties.DamageInstances >= 1){
            if(ignoreEnemySet && col.gameObject.GetInstanceID() == ignoreEnemy.GetInstanceID()){
                return;
            }
            if(col.gameObject.GetComponent<BaseEnemy>().IsScheduledForDeath()){
                return;
            }
            if(properties.Precision){
                if(col.gameObject != target){
                    return;
                }
            }
            if(properties.ExplodeOutwards){
                return;
            }
            float bonusAdjustedDamage = properties.Damage + (properties.GainsDamageWithRange ? properties.RangeBonusDamage : 0f);
            float randomNumber = UnityEngine.Random.Range(0f,1f);
            float critAdjustedDamage = randomNumber < properties.CritChance ? bonusAdjustedDamage* properties.CritMultiplier : bonusAdjustedDamage;
            col.gameObject.GetComponent<BaseEnemy>().TakeDamage(critAdjustedDamage);
            
            if (properties.decrementDamageInstances() == 0) {
                Destroy(gameObject);
            }else{
                if(properties.PiercePerfect){
                    properties.Damage += properties.Damage*0.2f;
                }
                properties.HomingShot = false;
            }
            
        }
    }

    public void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "DamageReduction"){
            GameObject castleEnemy = col.transform.parent.gameObject;
            CastleEnemy CastleScript = castleEnemy.GetComponent<CastleEnemy>();
            ReduceDamage(CastleScript.GetDamageReduction());
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

    protected List<GameObject> FindAllEnemiesInArea(Vector3 point, float diameter){
        GameObject[] enemies = GameObject.Find("Control").GetComponent<RoundManager>().GetAliveEnemies();
        List<GameObject> targets = new List<GameObject>();
        for(int i = 0; i<enemies.Length; i++){
            if(Vector3.Distance(enemies[i].transform.position, transform.position) < diameter){
                targets.Add(enemies[i]);
            }
        }
        return targets;
    }


}

public class ShotProperties {

        // Default Properties
        public float Speed {get; set;}
        public float Damage {get; set;}
        public int DamageInstances {get; set;}
        public bool HomingShot {get; set;}

        // Upgraded properties
        // PAWN
        public bool GainsDamageWithRange {get; set;}
        public float CritChance = 0f;
        public float CritMultiplier = 0f;

        // BISHOP
        public bool PiercePerfect {get; set;} = false;
        public bool Precision {get; set;} = false;


        //Internal Propeties
        public bool DamageReduced {get; set;}
        public float RangeBonusDamage {get; set;}
        public bool ExplodeOutwards {get; set;}
        public bool IsFirstInstance {get; set;} = true;

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
            PiercePerfect = false;       
        }

        public int decrementDamageInstances() {
            return --DamageInstances;
        }

        public void reduceDamage(float reduceAmount){
            if(DamageReduced){
                return;
            }else{
                Damage = Damage - reduceAmount;
                if(Damage <= 0f){
                    Damage = 0f;
                }
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

            newProperties.PiercePerfect = properties.PiercePerfect;
            newProperties.Precision = properties.Precision;

            return newProperties;
        }
    }
