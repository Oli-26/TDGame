using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : BaseTower
{
    protected override TowerProperties TowerProperties { get; set; } = new PawnProperties(1f, 2f, 50);
    protected override ShotProperties ShotProperties { get; set; } = new ShotProperties(2f, 1f, 1, false);

    new void Start()
    {
        base.Start();
        Tower = this;
        EventCentral.RoundEndEvent.AddListener(RoundReset);
        ResizeRangeIndicator(TowerProperties.Range);
    }

    protected override void Update()
    {
        base.Update(); 
        if(CurrentCooldown <= 0 && Active){
            Attack();

            
        }
    }
    public void RoundReset(){
        PawnProperties p = (PawnProperties)TowerProperties;
        p.AttackSpeedBonusPercentOfMax = 0;
    }

    protected override bool Attack(){
        PawnProperties p = (PawnProperties)TowerProperties;

        Retarget(TowerProperties.Range);
        if(!TargetSet)
            return false;
        
        CurrentCooldown = TowerProperties.Cooldown;
        // Magic to make the gaining of attack speed to work.    
        CurrentCooldown = p.IncreasingSpeedPerShot ? CurrentCooldown*(1f-p.AttackSpeedBonusMax*(0.01f*p.AttackSpeedBonusPercentOfMax)) : CurrentCooldown;

        float randomNumber = UnityEngine.Random.Range(0,1f);
        float doubleShotChance = p.DoubleShotChance;
        if(randomNumber < doubleShotChance){
            DoubleShoot();
        }else{
            Shoot();
        }
        
        return true;
    }
    protected void DoubleShoot(){
        ShotBasic shot = Instantiate(shotPrefab, transform.position + new Vector3(0f,0.1f, 0f), Quaternion.identity).GetComponent<ShotBasic>();
        shot.setProperties(ShotProperties);
        shot.SetTarget(Target);
        shot = Instantiate(shotPrefab, transform.position + new Vector3(0f, -0.1f, 0f), Quaternion.identity).GetComponent<ShotBasic>();
        shot.setProperties(ShotProperties);
        shot.SetTarget(Target);
        increaseAttackSpeedWithShot();
        increaseAttackSpeedWithShot();
    }

    protected void Shoot(){
        ShotBasic shot = Instantiate(shotPrefab, transform.position, Quaternion.identity).GetComponent<ShotBasic>();
        shot.setProperties(ShotProperties);
        shot.SetTarget(Target);
        increaseAttackSpeedWithShot();
    }

    private void increaseAttackSpeedWithShot(){
        PawnProperties p = (PawnProperties)TowerProperties;
        if(p.IncreasingSpeedPerShot && p.AttackSpeedBonusPercentOfMax < 100){
            p.AttackSpeedBonusPercentOfMax += 2;
        }
    }

    protected override List<TowerUpgrade> PossibleUpgrades() { 
        return PawnUpgrades.AllUpgrades();
    }

}

public class PawnProperties : TowerProperties {
    // Upgrade
    public float DoubleShotChance {get; set;}
    public bool IncreasingSpeedPerShot {get; set;}

    // Internal
    public int AttackSpeedBonusPercentOfMax {get; set;}
    public float AttackSpeedBonusMax {get; set;}

    public PawnProperties(float cooldown, float range, int worth) : base(cooldown, range, worth) {
        DoubleShotChance = 0.0f;
        IncreasingSpeedPerShot = false;
        AttackSpeedBonusPercentOfMax = 0;
        AttackSpeedBonusMax = 0.5f;

    }
}
