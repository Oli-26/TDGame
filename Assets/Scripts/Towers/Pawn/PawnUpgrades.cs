using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PawnUpgrade : TowerUpgrade
{
    public Action<PawnProperties, ShotProperties> effect { get; set; }


    public override void Apply(TowerProperties p, ShotProperties s)
    {
        PawnProperties props = p as PawnProperties;
        Debug.Log("Tower Properties: " + p.GetType() + " " + (props==null) + " " + this.name + " ShotProperties: " + s.GetType() );
        effect(p as PawnProperties, s);
    }
}
public class PawnUpgrades {
    public static PawnUpgrade pawnUpgrade0_0 = new PawnUpgrade {
        name = "Liquid cooling",
        description = "30% shot cooldown reduction",
        cost = 80,
        track = 0,
        level = 0,
        effect = (p, s) => { p.Cooldown -= 0.3f; }
    };

    public static PawnUpgrade pawnUpgrade0_1 = new PawnUpgrade {
        name = "Misfiring",
        description = "40% chance to doubleshot",
        cost = 130,
        track = 0,
        level = 1,
        effect = (p, s) => { p.DoubleShotChance = 0.4f; }
    };

    public static PawnUpgrade pawnUpgrade0_2 = new PawnUpgrade {
        name = "Super charge",
        description = "Increase attack speed by 2% per shot for the round, upto 50%",
        cost = 250,
        track = 0,
        level = 2,
        effect = (p, s) => { p.IncreasingSpeedPerShot = true;}
    };

    public static PawnUpgrade pawnUpgrade1_0 = new PawnUpgrade {
        name = "Extended scope",
        description = "50% base range increase",
        cost = 60,
        track = 1,
        level = 0,
        effect = (p, s) => { p.Range += 1; }
    };

    public static PawnUpgrade pawnUpgrade1_1 = new PawnUpgrade {
        name = "Targeted shots",
        description = "Shots home in on target",
        cost = 80,
        track = 1,
        level = 1,
        effect = (p, s) => { s.HomingShot = true; }
    };

    public static PawnUpgrade pawnUpgrade1_2 = new PawnUpgrade {
        name = "Tumbling shots",
        description = "Shot damage increases with range upto +3",
        cost = 250,
        track = 1,
        level = 2,
        effect = (p, s) => { s.GainsDamageWithRange = true; }
    };

    public static PawnUpgrade pawnUpgrade2_0 = new PawnUpgrade {
        name = "Power shot",
        description = "Doubles shot damage (+1)",
        cost = 150,
        track = 2,
        level = 0,
        effect = (p, s) => { s.Damage += 1; }
    };

    public static PawnUpgrade pawnUpgrade2_1 = new PawnUpgrade {
        name = "Critial hit",
        description = "10% chance damage to critial hit for 5X",
        cost = 220,
        track = 2,
        level = 1,
        effect = (p, s) => { s.CritChance = 0.1f; s.CritMultiplier = 5f; }
    };

    public static PawnUpgrade pawnUpgrade2_2 = new PawnUpgrade {
        name = "Weapon charging",
        description = "Increases crit chance to 40% while increasing shot cooldown by 20%",
        cost = 500,
        track = 2,
        level = 2,
        effect = (p, s) => { p.Cooldown += 0.2f; s.CritChance = 0.4f; }
    };

    public static List<TowerUpgrade> AllUpgrades() {
        List<TowerUpgrade> list = new List<TowerUpgrade>();
        list.Add(pawnUpgrade0_0);
        list.Add(pawnUpgrade0_1);
        list.Add(pawnUpgrade0_2);
        list.Add(pawnUpgrade1_0);
        list.Add(pawnUpgrade1_1);
        list.Add(pawnUpgrade1_2);
        list.Add(pawnUpgrade2_0);
        list.Add(pawnUpgrade2_1);
        list.Add(pawnUpgrade2_2);
        return list;
    }

}


