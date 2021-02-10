using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : BaseTower
{
    new PawnProperties properties = new PawnProperties(1f, 2f, 50);

    new void Start()
    {
        base.Start();
        Tower = this;

        ResizeRangeIndicator(properties.Range);
    }

    protected override void Update()
    {
        base.Update(); 
        if(currentCooldown <= 0 && active){
            Attack();

            
        }
    }
    protected override bool Attack(){
        Retarget(properties.Range);
        if(!targetSet)
            return false;
        currentCooldown = properties.Cooldown;    

        ShotBasic shot = Instantiate(shotPrefab, transform.position, Quaternion.identity).GetComponent<ShotBasic>();
        shot.setProperties(shotProperties);
        shot.SetTarget(target);
        return true;
    }

    public void upgrade(TowerUpgrade upgrade) {
        if (!upgrades.Contains(upgrade)) {
            upgrades.Add(upgrade);
            upgrade.Apply(properties, shotProperties);
        }
    }

    protected override List<TowerUpgrade> PossibleUpgrades() { 
        return PawnUpgrades.AllUpgrades();
    }

}

public class PawnProperties : TowerProperties {
    public PawnProperties(float cooldown, float range, int worth) : base(cooldown, range, worth) {
    }
}
