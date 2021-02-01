using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : BaseTower
{
    GameObject target;
    bool targetSet = false;
    public GameObject shotPrefab;

    float shotSpeed = 5f;
    float damage = 1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update(); 
        if(currentCooldown <= 0 && active){
            Attack();
        }
    }



    void Retarget(){
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i<enemies.Length; i++){
            if(Vector3.Distance(enemies[i].transform.position, transform.position) < range){
                target = enemies[0];
                targetSet = true;
                return;     
            }
        }
        targetSet = false;
        
    }


    void Attack(){
        Retarget();
        if(!targetSet)
            return;
        currentCooldown = shotCooldown;    
        GameObject shot = Instantiate(shotPrefab, transform.position, Quaternion.identity);
        
        shot.GetComponent<ShotBasic>().SetTarget(target);
        shot.GetComponent<ShotBasic>().SetSpeed(shotSpeed);
        shot.GetComponent<ShotBasic>().SetDamage(damage);
    }
}
