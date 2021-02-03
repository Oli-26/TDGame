﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : BaseTower
{
    void Start()
    {
        base.Start();
        shotCooldown = 1f;
        damage = 1f;
        shotSpeed = 7f;
    }

    protected override void Update()
    {
        base.Update(); 
        if(currentCooldown <= 0 && active){
            Attack();
        }
    }
}
