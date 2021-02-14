using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class KnightUpgrade : TowerUpgrade
{
    public Action<KnightProperties, SplitShotProperties> effect { get; set; }


    public override void Apply(TowerProperties p, ShotProperties s)
    {
        effect(p as KnightProperties, s as SplitShotProperties);
    }
}
public class KnightUpgrades {

    public static KnightUpgrade knightUpgrade0_0 = new KnightUpgrade {
        name = "Bonus shots",
        description = "Split shots now split into 4 projectiles",
        cost = 200,
        track = 0,
        level = 0,
        effect = (p, s) => { s.SplitNumber += 2; }
    };

    public static KnightUpgrade knightUpgrade0_1 = new KnightUpgrade {
        name = "Split damage up",
        description = "Split damage up to 80% from 50%",
        cost = 200,
        track = 0,
        level = 1,
        effect = (p, s) => { s.SplitDamage = 0.8f; }
    };

    public static KnightUpgrade knightUpgrade0_2 = new KnightUpgrade {
        name = "Split shots split!",
        description = "Shots now split into shots that split!",
        cost = 800,
        track = 0,
        level = 2,
        effect = (p, s) => { s.SplitRecurrenceNumber += 1; }
    };

    public static KnightUpgrade knightUpgrade1_0 = new KnightUpgrade {
        name = "Shock Shots",
        description = "Shots now knock enemies about and stun them for 1 seconds, but slows your attacks by 50%",
        cost = 250,
        track = 1,
        level = 0,
        effect = (p, s) => { s.ExplosiveShots = true; p.Cooldown += 1.5f;}
    };

    public static KnightUpgrade knightUpgrade1_1 = new KnightUpgrade {
        name = "Ability stripper",
        description = "Strips the abilities of powerful pieces for 2seconds",
        cost = 250,
        track = 1,
        level = 1,
        effect = (p, s) => { s.AbilityStripping = true; }
    };

    public static KnightUpgrade knightUpgrade1_2 = new KnightUpgrade {
        name = "Crippling shrapnel",
        description = "Stunned enemies are slowed for 30% for 10 seconds",
        cost = 5000000,
        track = 1,
        level = 2,
        effect = (p, s) => { Debug.Log("TODO:  Crippling shrapnel "); }
    };

    public static KnightUpgrade knightUpgrade2_0 = new KnightUpgrade {
        name = "Pools of acid",
        description = "First shot spawns a pool of acid that damages for 0.3x tower damage per second. Pools last upto 4 seconds. Upto 30 damage per pool",
        cost = 250,
        track = 2,
        level = 0,
        effect = (p, s) => { s.AcidPoolDamageMultiplier = 0.3f; s.CreateAcidPool = true; }
    };


    public static KnightUpgrade knightUpgrade2_1 = new KnightUpgrade {
        name = "Stronger acid",
        description = "First shot spawns a pool of acid that damages for 0.5x tower damage per second. Pools last upto 4 seconds. Upto 60 damage per pool",
        cost = 400,
        track = 2,
        level = 1,
        effect = (p, s) => { s.AcidPoolDamageMultiplier = 0.5f; s.AcidPoolMaxDamage = 60;}
    };

    public static KnightUpgrade knightUpgrade2_2 = new KnightUpgrade {
        name = "Upgrade 2-0",
        description = "50% base range increase",
        cost = 50000000,
        track = 2,
        level = 2,
        effect = (p, s) => { p.Range += 1; }
    };
    public static List<TowerUpgrade> AllUpgrades() {
        List<TowerUpgrade> list = new List<TowerUpgrade>();
        list.Add(knightUpgrade0_0);
        list.Add(knightUpgrade0_1);
        list.Add(knightUpgrade0_2);
        list.Add(knightUpgrade1_0);
        list.Add(knightUpgrade1_1);
        list.Add(knightUpgrade1_2);
        list.Add(knightUpgrade2_0);
        list.Add(knightUpgrade2_1);
        list.Add(knightUpgrade2_2);
        return list;
    }

}


