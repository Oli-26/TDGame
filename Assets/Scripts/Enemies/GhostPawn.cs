using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPawn : BaseEnemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        speed =4f;
        health = 0.1f;
        moneyDropped = 0;
        damageDealt = 0; 
    }

}
