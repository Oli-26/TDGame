using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : TowerUI
{
    protected float shotCooldown = 1f;
    protected float currentCooldown;

    protected GameObject target;
    protected bool targetSet = false;
    public GameObject shotPrefab;
    
    protected bool active = false;

    protected ShotProperties shotProperties = new ShotProperties(5f, 1f, 1f, 1);

    protected virtual void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(currentCooldown > 0){
            currentCooldown -= TimePassed();
        }
        base.Update();
    }
    
    public void Place(){
        active = true;
    }


    protected virtual void Retarget(float range){
        GameObject[] enemies = control.GetComponent<RoundManager>().GetAliveEnemies();
        float maxDistanceTraveled = 0f;
        targetSet = false;

        for(int i = 0; i<enemies.Length; i++){
            if(Vector3.Distance(enemies[i].transform.position, transform.position) < range){
                float lengthTraveled = enemies[i].GetComponent<BaseEnemy>().GetDistanceTraveled();
                if(lengthTraveled > maxDistanceTraveled){
                    target = enemies[i];
                    maxDistanceTraveled = lengthTraveled;
                    targetSet = true;
                }
            }
        }     
    }


    protected virtual bool Attack(){
        

        Retarget(shotProperties.getRange());
        if(!targetSet)
            return false;
        currentCooldown = shotCooldown;    

        ShotBasic shot = Instantiate(shotPrefab, transform.position, Quaternion.identity).GetComponent<ShotBasic>();
        shot.SetProperties(shotProperties);
        return true;
    }


    
    

}
