using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : BaseTower
{
    new PawnProperties properties = new PawnProperties(1f);
    List<PawnUpgrade> upgrades = new List<PawnUpgrade>();

    new void Start()
    {
        base.Start();
        shotCooldown = 1f;
    }

    protected override void Update()
    {
        base.Update(); 
        if(currentCooldown <= 0 && active){
            Attack();

            pawnUpgrade0_0 up = new pawnUpgrade0_0();
            up.Apply(properties, shotProperties);

            pawnUpgrade0_1 up2 = new pawnUpgrade0_1();
            up2.Apply(properties, shotProperties);

            pawnUpgrade0_2 up3 = new pawnUpgrade0_2();
            up3.Apply(properties, shotProperties);
        }
    }



}

public class PawnProperties : TowerProperties{
    public PawnProperties(float cooldown) : base(cooldown){

    }
}
