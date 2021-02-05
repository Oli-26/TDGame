using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitShot : ShotBasic
{
    public GameObject babyShot;
    List<GameObject> cannotTargetAgain;


    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        base.Update();
    }

    public override void OnCollisionEnter2D(Collision2D col){
       
        SplitShotProperties tempProperties = (SplitShotProperties)properties;
        if(col.gameObject.tag == "Enemy" && properties.DamageInstances >= 1){
            col.gameObject.GetComponent<BaseEnemy>().TakeDamage(properties.Damage);
            cannotTargetAgain = new List<GameObject>();
            for(int i = 0; i < tempProperties.SplitNumber; i++){
                CreateChildShot();
            }

            if (properties.decrementDamageInstances() == 0) {
                Destroy(gameObject);
            }
        }
    }


    void CreateChildShot(){
        GameObject maybeTarget = FindTarget(out bool targetFound);
        
        if(targetFound){
            Vector3 offset = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0);
            GameObject childShot = Instantiate(babyShot, gameObject.transform.position, Quaternion.identity);
            childShot.GetComponent<ShotBasic>().setProperties(new ShotProperties(4f, 1f, properties.Damage/2f, 1));
            childShot.GetComponent<ShotBasic>().SetTarget(maybeTarget);
            childShot.GetComponent<ShotBasic>().SetIgnoreEnemy(target);
        }
        
    }
      
    protected GameObject FindTarget(out bool targetFound){
        targetFound = false;
        GameObject[] enemies = GameObject.Find("Control").GetComponent<RoundManager>().GetAliveEnemies();
        List<GameObject> targets = new List<GameObject>();
        for(int i = 0; i<enemies.Length; i++){
            if(enemies[i].GetInstanceID() != target.GetInstanceID() && Vector3.Distance(enemies[i].transform.position, transform.position) < properties.Range){
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

    public override void Move(){
        Vector3 changeVector;
        if(target != null){
            changeVector = Vector3.Normalize(target.transform.position-transform.position) *TimePassed()*properties.Speed;
        }else{
            changeVector = direction *TimePassed()*properties.Speed;
        }
        
        transform.position += changeVector;
    }

    public void setSplitProperties(SplitShotProperties p) {
        this.properties = new SplitShotProperties(p.Speed, p.Range, p.Damage, p.DamageInstances, p.SplitNumber);
    }

}

public class SplitShotProperties : ShotProperties {
    public int SplitNumber {get; set;}
    public SplitShotProperties(float speed, float range, float damage, int damageInstances, int splitNumber) : base(speed, range, damage, damageInstances) {
        SplitNumber = splitNumber;    
    }

}
