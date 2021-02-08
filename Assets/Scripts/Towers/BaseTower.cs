using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TargetingMode {First, Strongest, Weakest, Last};

public class BaseTower : TowerUI
{
    protected List<TowerUpgrade> upgrades = new List<TowerUpgrade>();
    protected float shotCooldown = 1f;
    protected float currentCooldown;

    protected GameObject target;
    protected bool targetSet = false;
    public GameObject shotPrefab;
    
    protected bool active = false;
    protected TargetingMode mode = TargetingMode.First;

    protected TowerProperties properties = new TowerProperties(1f, 2f);
    protected ShotProperties shotProperties = new ShotProperties(5f, 1f, 1, false);

    protected new virtual void Start()
    {
        base.Start();
    }

    protected new virtual void Update()
    {
        if(currentCooldown > 0){
            currentCooldown -= TimePassed();
        }
        base.Update();
    }
    
    public void Place(){
        active = true;
    }

    public void SetTargetingMode(TargetingMode m){
        mode = m;
    }

    protected virtual void Retarget(float range){
          switch(mode){
                case TargetingMode.First:
                    TargetFirst(range);
                    break;
                case TargetingMode.Strongest:
                    TargetStrongest(range);
                    break;
                case TargetingMode.Weakest:
                    TargetWeakest(range);
                    break;
                case TargetingMode.Last:
                    TargetLast(range);
                    break;
                default:
                    TargetFirst(range);
                    break;
          }
    }

    protected virtual void TargetFirst(float range){
        GameObject[] enemies = control.GetComponent<RoundManager>().GetAliveEnemies();
        float maxDistanceTraveled = 0f;
        targetSet = false;

        for(int i = 0; i<enemies.Length; i++){
            if(Vector3.Distance(enemies[i].transform.position, transform.position) < properties.Range){
                float lengthTraveled = enemies[i].GetComponent<BaseEnemy>().GetDistanceTraveled();
                if(lengthTraveled > maxDistanceTraveled){
                    target = enemies[i];
                    maxDistanceTraveled = lengthTraveled;
                    targetSet = true;
                }
            }
        }   
    }

    protected virtual void TargetStrongest(float range){
        GameObject[] enemies = control.GetComponent<RoundManager>().GetAliveEnemies();
        float highestHealth = 0f;
        float maxDistanceTraveled = 0f;
        targetSet = false;

        for(int i = 0; i<enemies.Length; i++){
            if(Vector3.Distance(enemies[i].transform.position, transform.position) < range){
                float enemyHealth = enemies[i].GetComponent<BaseEnemy>().health;
                float lengthTraveled = enemies[i].GetComponent<BaseEnemy>().GetDistanceTraveled();
                if(enemyHealth > highestHealth && maxDistanceTraveled < lengthTraveled){
                    target = enemies[i];
                    highestHealth = enemyHealth;
                    maxDistanceTraveled = lengthTraveled;
                    targetSet = true;
                }
            }
        }  
    }

    protected virtual void TargetWeakest(float range){
        GameObject[] enemies = control.GetComponent<RoundManager>().GetAliveEnemies();
        float lowestHealth = 100000000f;
        targetSet = false;

        for(int i = 0; i<enemies.Length; i++){
            if(Vector3.Distance(enemies[i].transform.position, transform.position) < range){
                float enemyHealth = enemies[i].GetComponent<BaseEnemy>().health;
                if(enemyHealth < lowestHealth){
                    target = enemies[i];
                    lowestHealth = enemyHealth;
                    targetSet = true;
                }
            }
        }  
    }

    protected virtual void TargetLast(float range){
        GameObject[] enemies = control.GetComponent<RoundManager>().GetAliveEnemies();
        float leastDistanceTraveled = 1000f;
        targetSet = false;

        for(int i = 0; i<enemies.Length; i++){
            if(Vector3.Distance(enemies[i].transform.position, transform.position) < properties.Range){
                float lengthTraveled = enemies[i].GetComponent<BaseEnemy>().GetDistanceTraveled();
                if(lengthTraveled < leastDistanceTraveled){
                    target = enemies[i];
                    leastDistanceTraveled = lengthTraveled;
                    targetSet = true;
                }
            }
        }   
    }

    protected virtual bool Attack(){
        Debug.Log(properties.Range);
        Retarget(properties.Range);
        if(!targetSet)
            return false;
        currentCooldown = properties.Cooldown;    

        ShotBasic shot = Instantiate(shotPrefab, transform.position, Quaternion.identity).GetComponent<ShotBasic>();
        shot.setProperties(shotProperties);
        shot.SetTarget(target);
        return true;
    }

    protected virtual List<TowerUpgrade> PossibleUpgrades() { 
        Debug.Log("WARNING: PossibleUpgrades called for BaseTower.");
        return null;
    }

    public List<TowerUpgrade> GetBuyableUpgrades() {
        List<TowerUpgrade> buyableUpgrades = PossibleUpgrades().FindAll(u => !upgrades.Contains(u)).FindAll(u => u.IsBuyable(upgrades));
        return buyableUpgrades;        
    }

    public void BuyUpgrade(TowerUpgrade upgrade) {
        if (GetBuyableUpgrades().Exists(u => u == upgrade)) {
            upgrades.Add(upgrade);
            upgrade.Apply(properties, shotProperties);
        }
    }
}

public class TowerProperties{
    public float Cooldown {get; set;}
    public float Range {get; set;}

    public TowerProperties(float cooldown, float range){
        Cooldown = cooldown;
        Range = range;
    }
}
