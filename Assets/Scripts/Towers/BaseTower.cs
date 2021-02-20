using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TargetingMode {First, Strong, Weak, Last};

public abstract class BaseTower : TowerUI
{
    public GameObject shotPrefab;
    
    protected GameObject Target;
    protected bool TargetSet = false;

    protected TargetingMode Mode = TargetingMode.First;
    protected bool Active = false;
    protected float CurrentCooldown;

    protected List<TowerUpgrade> Upgrades { get; set; } = new List<TowerUpgrade>();
    protected abstract TowerProperties TowerProperties { get; set; }
    protected abstract ShotProperties ShotProperties { get; set; }

    protected new virtual void Start()
    {
        base.Start();
    }

    protected new virtual void Update()
    {
        if(CurrentCooldown > 0){
            CurrentCooldown -= TimePassed();
        }
        base.Update();
    }
    
    public void Place(){
        Active = true;
    }

    public TargetingMode GetTargetingMode(){
        return Mode;
    }
    public void SetTargetingMode(TargetingMode m){
        Mode = m;
    }

    protected virtual void Retarget(float range){
          switch(Mode){
                case TargetingMode.First:
                    TargetFirst(range);
                    break;
                case TargetingMode.Strong:
                    TargetStrongest(range);
                    break;
                case TargetingMode.Weak:
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
        TargetSet = false;

        for(int i = 0; i<enemies.Length; i++){
            if(Vector3.Distance(enemies[i].transform.position, transform.position) < TowerProperties.Range){
                float lengthTraveled = enemies[i].GetComponent<BaseEnemy>().GetDistanceTraveled();
                if(lengthTraveled > maxDistanceTraveled){
                    Target = enemies[i];
                    maxDistanceTraveled = lengthTraveled;
                    TargetSet = true;
                }
            }
        }   
    }

    protected virtual void TargetStrongest(float range){
        GameObject[] enemies = control.GetComponent<RoundManager>().GetAliveEnemies();
        float highestTier = 0f;
        float maxDistanceTraveled = 0f;
        TargetSet = false;

        for(int i = 0; i<enemies.Length; i++){
            if(Vector3.Distance(enemies[i].transform.position, transform.position) < range){
                float enemyTier = enemies[i].GetComponent<BaseEnemy>().Tier;
                float lengthTraveled = enemies[i].GetComponent<BaseEnemy>().GetDistanceTraveled();
                if(enemyTier > highestTier && maxDistanceTraveled < lengthTraveled){
                    Target = enemies[i];
                    highestTier = enemyTier;
                    maxDistanceTraveled = lengthTraveled;
                    TargetSet = true;
                }
            }
        }  
    }

    protected virtual void TargetWeakest(float range){
        GameObject[] enemies = control.GetComponent<RoundManager>().GetAliveEnemies();
        float lowestTier = 0f;
        float maxDistanceTraveled = 0f;
        TargetSet = false;

        for(int i = 0; i<enemies.Length; i++){
            if(Vector3.Distance(enemies[i].transform.position, transform.position) < range){
                float enemyTier = enemies[i].GetComponent<BaseEnemy>().Tier;
                float lengthTraveled = enemies[i].GetComponent<BaseEnemy>().GetDistanceTraveled();
                if(enemyTier < lowestTier && maxDistanceTraveled < lengthTraveled){
                    Target = enemies[i];
                    lowestTier = enemyTier;
                    maxDistanceTraveled = lengthTraveled;
                    TargetSet = true;
                }
            }
        }  
    }

    protected virtual void TargetLast(float range){
        GameObject[] enemies = control.GetComponent<RoundManager>().GetAliveEnemies();
        float leastDistanceTraveled = 1000f;
        TargetSet = false;

        for(int i = 0; i<enemies.Length; i++){
            if(Vector3.Distance(enemies[i].transform.position, transform.position) < TowerProperties.Range){
                float lengthTraveled = enemies[i].GetComponent<BaseEnemy>().GetDistanceTraveled();
                if(lengthTraveled < leastDistanceTraveled){
                    Target = enemies[i];
                    leastDistanceTraveled = lengthTraveled;
                    TargetSet = true;
                }
            }
        }   
    }

    protected virtual bool Attack(){
        Debug.Log(TowerProperties.Range);
        Retarget(TowerProperties.Range);
        if(!TargetSet)
            return false;
        CurrentCooldown = TowerProperties.Cooldown;    

        ShotBasic shot = Instantiate(shotPrefab, transform.position, Quaternion.identity).GetComponent<ShotBasic>();
        shot.setProperties(ShotProperties);
        shot.SetTarget(Target);
        return true;
    }

    protected virtual List<TowerUpgrade> PossibleUpgrades() { 
        Debug.Log("WARNING: PossibleUpgrades called for BaseTower.");
        return new List<TowerUpgrade>();
    }

    public List<TowerUpgrade> GetBuyableUpgrades() {
        List<TowerUpgrade> buyableUpgrades = PossibleUpgrades().FindAll(u => !Upgrades.Contains(u)).FindAll(u => u.IsBuyable(Upgrades));
        return buyableUpgrades;        
    }

    public void BuyUpgrade(TowerUpgrade upgrade)
    {
        if (!GetBuyableUpgrades().Exists(u => u == upgrade)) return;
        Upgrades.Add(upgrade);
        upgrade.Apply(TowerProperties, ShotProperties);
        VisualUpdatesForUpgrades();
    }

    public void VisualUpdatesForUpgrades(){
        ResizeRangeIndicator(TowerProperties.Range);
    }

    public void IncreaseWorth(int amount){
        TowerProperties.Worth += amount;
    }

    public int GetTowerWorth(){
        return (int)TowerProperties.Worth;
    }

    public void SellTower(){
        Destroy(gameObject);
    }
}

public class TowerProperties{
    public float Cooldown {get; set;}
    public float Range {get; set;}
    public float Worth {get; set;}

    public TowerProperties(float cooldown, float range, int worth){
        Cooldown = cooldown;
        Range = range;
        Worth = worth;
    }
}
