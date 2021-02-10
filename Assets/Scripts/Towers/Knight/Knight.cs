﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : BaseTower
{
    protected new KnightProperties properties = new KnightProperties(1.5f, 2.5f, 150);
    protected new SplitShotProperties shotProperties = new SplitShotProperties(6f, 3f, 1, true ,2);

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

        SplitShot shot = Instantiate(shotPrefab, transform.position, Quaternion.identity).GetComponent<SplitShot>();
        shot.setSplitProperties(shotProperties);
        shot.SetTarget(target);
        return true;
    }

    protected override List<TowerUpgrade> PossibleUpgrades() { 
        return KnightUpgrades.AllUpgrades();
    }

    public void upgrade(TowerUpgrade upgrade) {
        if (!upgrades.Contains(upgrade)) {
            upgrades.Add(upgrade);
            upgrade.Apply(properties, shotProperties);
        }
    }


}

public class KnightProperties : TowerProperties{
    public KnightProperties(float cooldown, float range, int worth) : base(cooldown, range, worth){
    }
}
