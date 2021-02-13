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
        name = "Upgrade 0-0",
        description = "15% base cooldown reduction",
        cost = 30,
        track = 0,
        level = 0,
        effect = (p, s) => { p.Cooldown -= 0.15f; }
    };

    public static KnightUpgrade knightUpgrade0_1 = new KnightUpgrade {
        name = "Upgrade 0-1",
        description = "50% base range increase",
        cost = 50,
        track = 0,
        level = 1,
        effect = (p, s) => { p.Range += 1; }
    };

    public static KnightUpgrade knightUpgrade0_2 = new KnightUpgrade {
        name = "Upgrade 0-2",
        description = "2x base damage",
        cost = 70,
        track = 0,
        level = 2,
        effect = (p, s) => { s.Damage += 1; }
    };

    public static KnightUpgrade knightUpgrade1_0 = new KnightUpgrade {
        name = "Upgrade 1-0",
        description = "50% base range increase",
        cost = 50,
        track = 1,
        level = 0,
        effect = (p, s) => { p.Range += 1; }
    };

    public static KnightUpgrade knightUpgrade1_1 = new KnightUpgrade {
        name = "Upgrade 1-1",
        description = "Shots home in on target",
        cost = 50,
        track = 1,
        level = 1,
        effect = (p, s) => { s.HomingShot = true; }
    };

    public static KnightUpgrade knightUpgrade1_2 = new KnightUpgrade {
        name = "Upgrade 1-2",
        description = "50% base range increase",
        cost = 50,
        track = 1,
        level = 2,
        effect = (p, s) => { p.Range += 1; }
    };

    public static KnightUpgrade knightUpgrade2_0 = new KnightUpgrade {
        name = "Upgrade 2-0",
        description = "50% base range increase",
        cost = 50,
        track = 2,
        level = 0,
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
        return list;
    }

}


