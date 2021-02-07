﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : BaseTower
{
    new QueenProperties properties = new QueenProperties(3f, 3f);
    //List<PawnUpgrade> upgrades = new List<PawnUpgrade>();

    new void Start()
    {
        base.Start();
        Tower = this;

        ResizeRangeIndicator(properties.Range);
        shotProperties = new ShotProperties(2f, 1f, 3, true);
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

public class QueenProperties : TowerProperties{
    public QueenProperties(float cooldown, float range) : base(cooldown, range){
    }
}
