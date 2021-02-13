using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : BaseTower
{
    protected override TowerProperties TowerProperties { get; set; } = new PawnProperties(1f, 2f, 50);
    protected override ShotProperties ShotProperties { get; set; } = new ShotProperties(5f, 1f, 1, false);

    new void Start()
    {
        base.Start();
        Tower = this;

        ResizeRangeIndicator(TowerProperties.Range);
    }

    protected override void Update()
    {
        base.Update(); 
        if(CurrentCooldown <= 0 && Active){
            Attack();

            
        }
    }
    protected override bool Attack(){
        Retarget(TowerProperties.Range);
        if(!TargetSet)
            return false;
        CurrentCooldown = TowerProperties.Cooldown;    

        ShotBasic shot = Instantiate(shotPrefab, transform.position, Quaternion.identity).GetComponent<ShotBasic>();
        shot.setProperties(ShotProperties);
        shot.SetTarget(Target);
        return true;
    }

    protected override List<TowerUpgrade> PossibleUpgrades() { 
        return PawnUpgrades.AllUpgrades();
    }

}

public class PawnProperties : TowerProperties {
    public PawnProperties(float cooldown, float range, int worth) : base(cooldown, range, worth) {
    }
}
