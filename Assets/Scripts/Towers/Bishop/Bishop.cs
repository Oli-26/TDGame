﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : BaseTower
{
    new BishopProperties properties = new BishopProperties(2f, 2.5f);
    //List<PawnUpgrade> upgrades = new List<PawnUpgrade>();

    new void Start()
    {
        base.Start();

        ResizeRangeIndicator(properties.Range);
        shotProperties = new ShotProperties(12f, 7f, 3, true);
        mode = TargetingMode.Strongest;
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


}

public class BishopProperties : TowerProperties{
    public BishopProperties(float cooldown, float range) : base(cooldown, range){
    }
}
