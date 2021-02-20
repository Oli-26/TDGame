using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BishopUpgrade : TowerUpgrade
{
    public Action<BishopProperties, ShotProperties> effect { get; set; }


    public override void Apply(TowerProperties p, ShotProperties s)
    {
        effect(p as BishopProperties, s);
    }
}
public class BishopUpgrades {

    public static BishopUpgrade bishopUpgrade0_0 = new BishopUpgrade {
        name = "Pierce shots",
        description = "Shots now pierce upto 3 enemies",
        cost = 200,
        track = 0,
        level = 0,
        effect = (p, s) => { s.DamageInstances += 2; }
    };

    public static BishopUpgrade bishopUpgrade0_1 = new BishopUpgrade {
        name = "Pierced perfect",
        description = "Shots gain 20% damage after piercing an enemy",
        cost = 200,
        track = 0,
        level = 1,
        effect = (p, s) => { s.PiercePerfect = true; }
    };

    public static BishopUpgrade bishopUpgrade0_2 = new BishopUpgrade {
        name = "Diamond tipped",
        description = "Shots now pierce upto 6 enemies",
        cost = 700,
        track = 0,
        level = 2,
        effect = (p, s) => { s.DamageInstances += 3; }
    };

    public static BishopUpgrade bishopUpgrade1_0 = new BishopUpgrade {
        name = "World wide",
        description = "Increase range by 2",
        cost = 300,
        track = 1,
        level = 0,
        effect = (p, s) => {p.Range += 2; }
    };

    public static BishopUpgrade bishopUpgrade1_1 = new BishopUpgrade {
        name = "Precision targeting",
        description = "Shots only hit their target (initially)",
        cost = 300,
        track = 1,
        level = 1,
        effect = (p, s) => { s.Precision = true; s.Speed += 1f; }
    };

    public static BishopUpgrade bishopUpgrade1_2 = new BishopUpgrade {
        name = "Aim for the head",
        description = "5% chance damage to critial hit for 9X",
        cost = 500,
        track = 1,
        level = 2,
        effect = (p, s) => { s.CritChance = 0.05f; s.CritMultiplier = 9f; }
    };

    public static BishopUpgrade bishopUpgrade2_0 = new BishopUpgrade {
        name = "Heated Shots",
        description = "Instead shot damage by 3",
        cost = 300,
        track = 2,
        level = 0,
        effect = (p, s) => { s.Damage += 3f; }
    };


    public static BishopUpgrade bishopUpgrade2_1 = new BishopUpgrade {
        name = "Over charging",
        description = "Halfs attack speed, adds 10 damage to shots",
        cost = 800,
        track = 2,
        level = 1,
        effect = (p, s) => { s.Damage += 10f; p.Cooldown += 2.1f; }
    };

    public static BishopUpgrade bishopUpgrade2_2 = new BishopUpgrade {
        name = "TODO",
        description = "",
        cost = 80000000,
        track = 2,
        level = 2,
        effect = (p, s) => { }
    };
    public static List<TowerUpgrade> AllUpgrades() {
        List<TowerUpgrade> list = new List<TowerUpgrade>();
        list.Add(bishopUpgrade0_0);
        list.Add(bishopUpgrade0_1);
        list.Add(bishopUpgrade0_2);
        list.Add(bishopUpgrade1_0);
        list.Add(bishopUpgrade1_1);
        list.Add(bishopUpgrade1_2);
        list.Add(bishopUpgrade2_0);
        list.Add(bishopUpgrade2_1);
        list.Add(bishopUpgrade2_2);
        return list;
    }

}


