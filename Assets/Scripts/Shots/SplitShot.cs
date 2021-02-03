using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitShot : ShotBasic
{
    public GameObject babyShot;
    float range = 2f;
    int splitNumber = 2;

    List<GameObject> cannotTargetAgain;
    void Start()
    {
        base.Start();
    }

    void Update()
    {
        base.Update();
    }

    public override void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "Enemy" && damageInstancesLeft >= 1){
            col.gameObject.GetComponent<BaseEnemy>().TakeDamage(damage);
            damageInstancesLeft--;
            cannotTargetAgain = new List<GameObject>();
            for(int i = 0; i < splitNumber; i++){
                CreateChildShot();
            }
            Destroy(gameObject);
            
        }
    }


    void CreateChildShot(){
        GameObject maybeTarget = FindTarget(out bool targetFound);
        
        if(targetFound){
            Vector3 offset = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0);
            GameObject childShot = Instantiate(babyShot, gameObject.transform.position, Quaternion.identity);
            childShot.GetComponent<ShotBasic>().SetTarget(maybeTarget);
            childShot.GetComponent<ShotBasic>().SetSpeed(4f);
            childShot.GetComponent<ShotBasic>().SetDamage(damage/2f);
            childShot.GetComponent<ShotBasic>().SetIgnoreEnemy(target);
        }
        
    }
      
    protected GameObject FindTarget(out bool targetFound){
        targetFound = false;
        GameObject[] enemies = GameObject.Find("Control").GetComponent<RoundManager>().GetAliveEnemies();
        List<GameObject> targets = new List<GameObject>();
        for(int i = 0; i<enemies.Length; i++){
            if(enemies[i].GetInstanceID() != target.GetInstanceID() && Vector3.Distance(enemies[i].transform.position, transform.position) < range){
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
            changeVector = Vector3.Normalize(target.transform.position-transform.position) *TimePassed()*speed;
        }else{
            changeVector = direction *TimePassed()*speed;
        }
        
        transform.position += changeVector;
    }

}
