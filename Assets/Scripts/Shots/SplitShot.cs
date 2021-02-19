using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitShot : ShotBasic
{
    public GameObject babyShot;
    public GameObject explosionPNG;
    public GameObject AcidPool;

    List<GameObject> cannotTargetAgain;

    // Until we put it inside splitshotproperties
    private float splitRange = 3f;

    new void Start()
    {
        base.Start();
        
    }

    new void Update()
    {
        base.Update();
    }

    public override void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "DamageReduction"){
            ReduceDamage(col.gameObject);
        }
        SplitShotProperties tempProperties = (SplitShotProperties)properties;
        if(col.gameObject.tag == "Enemy" && properties.DamageInstances >= 1){
            col.gameObject.GetComponent<BaseEnemy>().TakeDamage(properties.Damage);

            // Explosive Shots
            if(tempProperties.ExplosiveShots && tempProperties.IsFirstInstance){
                List<GameObject> enemies = FindAllEnemiesInArea(transform.position, 1f);
                
                //Destroy(Instantiate(explosionPNG, col.transform.position, Quaternion.identity), 0.25f);
                foreach (var enemy in enemies)
                {
                    enemy.transform.position += new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f), 0f);
                    enemy.GetComponent<BaseEnemy>().Stun(tempProperties.StunLength, tempProperties.StunBonusDamage, tempProperties.StunBonusDamageMultiplier);
                    if(tempProperties.AbilityStripping){
                        enemy.GetComponent<BaseEnemy>().BlockAbility(tempProperties.StrippingTime);
                    }
                }
            }

            // Acid pools
            if(tempProperties.CreateAcidPool){
                GameObject acid = Instantiate(AcidPool, col.transform.position, Quaternion.identity);
                Destroy(acid, 4f/TimeEffect(1f));
                acid.GetComponent<Acid>().DamagePerSeconds = tempProperties.Damage * tempProperties.AcidPoolDamageMultiplier;
                acid.GetComponent<Acid>().MaxDamage = tempProperties.AcidPoolMaxDamage;
                acid.GetComponent<Acid>().Slow = tempProperties.AcidSlow;
                acid.GetComponent<Acid>().SlowPercent = tempProperties.AcidSlowPercent;
                acid.GetComponent<Acid>().SlowTime = tempProperties.AcidSlowTime;
            }


            // Spawn Children
            cannotTargetAgain = new List<GameObject>();
            for(int i = 0; i < tempProperties.SplitNumber; i++){
                CreateChildShot();
            }

            if (properties.decrementDamageInstances() == 0) {
                Destroy(gameObject);
            }
        }
    }

    List<GameObject> FindAllEnemiesInArea(Vector3 point, float diameter){
        GameObject[] enemies = GameObject.Find("Control").GetComponent<RoundManager>().GetAliveEnemies();
        List<GameObject> targets = new List<GameObject>();
        for(int i = 0; i<enemies.Length; i++){
            if(Vector3.Distance(enemies[i].transform.position, transform.position) < diameter){
                targets.Add(enemies[i]);
            }
        }
        return targets;
    }


    void CreateChildShot(){
        GameObject maybeTarget = FindTarget(out bool targetFound);
        SplitShotProperties p = (SplitShotProperties)properties;
        
            if(p.SplitRecurrence && p.IsFirstInstance && targetFound){
                GameObject childShot = Instantiate(gameObject, gameObject.transform.position, Quaternion.identity);
                //p.IsFirstInstance = false;
                childShot.GetComponent<SplitShot>().setSplitProperties(p);
                ((SplitShotProperties)childShot.GetComponent<SplitShot>().properties).IsFirstInstance = false;
                childShot.GetComponent<SplitShot>().SetTarget(maybeTarget);
                childShot.GetComponent<SplitShot>().SetIgnoreEnemy(target);
            }else{
                Vector3 offset = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0);
                GameObject childShot = Instantiate(babyShot, gameObject.transform.position, Quaternion.identity);
                childShot.GetComponent<ShotBasic>().setProperties(new ShotProperties(2f, properties.Damage*((SplitShotProperties)properties).SplitDamage, 1, false));
                childShot.GetComponent<ShotBasic>().properties.ExplodeOutwards = true;
                childShot.GetComponent<ShotBasic>().SetTarget(targetFound ? maybeTarget : null);
                childShot.GetComponent<ShotBasic>().SetIgnoreEnemy(target);
            }
        
    }
      
    protected GameObject FindTarget(out bool targetFound){
        targetFound = false;
        GameObject[] enemies = GameObject.Find("Control").GetComponent<RoundManager>().GetAliveEnemies();
        List<GameObject> targets = new List<GameObject>();
        for(int i = 0; i<enemies.Length; i++){
            if(enemies[i].GetInstanceID() != target.GetInstanceID() && Vector3.Distance(enemies[i].transform.position, transform.position) < splitRange){
                if(!cannotTargetAgain.Contains(enemies[i])){
                    targets.Add(enemies[i]);
                    targetFound = true;
                }
            }
        }
        if(targetFound){
            int randNumber = Random.Range(0, targets.Count);
            cannotTargetAgain.Add(targets[randNumber]);
            return targets[randNumber]; 
        }else{
            return null;
        }
    }


    public void setSplitProperties(SplitShotProperties p) {
        this.properties = SplitShotProperties.Duplicate(p);
    }

}

public class SplitShotProperties : ShotProperties {
    // Upgrades
    public int SplitNumber {get; set;}
    public float SplitDamage {get; set;}
    public bool SplitRecurrence {get; set;}

    public bool ExplosiveShots {get; set;}
    public bool AbilityStripping {get; set;}
    public bool StunBonusDamage {get; set;}
    public float StunBonusDamageMultiplier {get; set;}

    public bool CreateAcidPool {get; set;}
    public float AcidPoolDamageMultiplier {get; set;}
    public float AcidPoolMaxDamage {get; set;}
    public bool AcidSlow {get; set;}
    public float AcidSlowPercent {get; set;}
    public float AcidSlowTime {get; set;}

    // Internal
    public float StunLength {get; set;}
    public float StrippingTime {get; set;}
    public bool IsFirstInstance {get; set;}

    public SplitShotProperties(float speed, float damage, int damageInstances, bool homingShot, int splitNumber) : base(speed, damage, damageInstances, homingShot) {
        SplitNumber = splitNumber;    
        SplitDamage = 0.5f;
        SplitRecurrence = false;
        ExplosiveShots = false;
        IsFirstInstance = true;

        StunLength = 2f;
        AbilityStripping = false;
        StrippingTime = 4f;
        StunBonusDamage = false;

        CreateAcidPool = false;
        AcidPoolDamageMultiplier = 0.2f;
        AcidPoolMaxDamage = 30f;
        AcidSlow = false;
        AcidSlowPercent = 0f;
        AcidSlowTime = 0f;
        
        
    }
    public SplitShotProperties() : base() {  
    }

    public static SplitShotProperties Duplicate(SplitShotProperties properties){
        SplitShotProperties newProperties = new SplitShotProperties();

        // Base
        newProperties.Speed = properties.Speed;
        newProperties.Damage = properties.Damage;
        newProperties.DamageInstances = properties.DamageInstances;
        newProperties.HomingShot = properties.HomingShot;

        // Upgrade 
        ////path 0
        newProperties.SplitNumber = properties.SplitNumber;
        newProperties.SplitDamage = properties.SplitDamage;
        newProperties.SplitRecurrence = properties.SplitRecurrence;
        ////path1
        newProperties.ExplosiveShots = properties.ExplosiveShots;
        newProperties.StunLength = properties.StunLength;
        newProperties.AbilityStripping = properties.AbilityStripping;
        newProperties.StrippingTime = properties.StrippingTime;
        newProperties.StunBonusDamage = properties.StunBonusDamage;
        newProperties.StunBonusDamageMultiplier = properties.StunBonusDamageMultiplier;

        ////path2
        newProperties.CreateAcidPool = properties.CreateAcidPool;
        newProperties.AcidPoolDamageMultiplier = properties.AcidPoolDamageMultiplier;
        newProperties.AcidPoolMaxDamage = properties.AcidPoolMaxDamage;
        newProperties.AcidSlow = properties.AcidSlow;
        newProperties.AcidSlowPercent = properties.AcidSlowPercent;
        newProperties.AcidSlowTime = properties.AcidSlowTime;

        //Internal
        newProperties.IsFirstInstance = properties.IsFirstInstance;

        return newProperties; 
    }

}
