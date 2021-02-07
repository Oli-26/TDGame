using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenEnemy : BaseEnemy
{
   protected override void Start()
    {
        base.Start();
        speed = 1.1f;
        health = 150;
        moneyDropped = 25;
        damageDealt = 20;
    }

    protected override void Update()
    {
        base.Update();
    }
}
