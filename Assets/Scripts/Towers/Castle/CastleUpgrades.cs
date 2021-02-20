using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CastleUpgrade : TowerUpgrade
{
    public Action<CastleProperties, ShotProperties> effect { get; set; }


    public override void Apply(TowerProperties p, ShotProperties s)
    {
        effect(p as CastleProperties, s);
    }
}
public class CastleUpgrades {

    public static CastleUpgrade castleUpgrade0_0 = new CastleUpgrade {
        name = "And Another?",
        description = "Add a extra shot to each attack",
        cost = 250,
        track = 0,
        level = 0,
        effect = (p, s) => { p.ShotCount += 1; }
    };

    public static CastleUpgrade castleUpgrade0_1 = new CastleUpgrade {
        name = "Heavy fuse",
        description = "Add 1 damage to each shot",
        cost = 350,
        track = 0,
        level = 1,
        effect = (p, s) => { s.Damage += 1; }
    };

    public static CastleUpgrade castleUpgrade0_2 = new CastleUpgrade {
        name = "TODO",
        description = "",
        cost = 80000000,
        track = 0,
        level = 2,
        effect = (p, s) => {  }
    };

    public static CastleUpgrade castleUpgrade1_0 = new CastleUpgrade {
        name = "TODO",
        description = "",
        cost = 250,
        track = 1,
        level = 0,
        effect = (p, s) => { }
    };

    public static CastleUpgrade castleUpgrade1_1 = new CastleUpgrade {
        name = "TODO",
        description = "",
        cost = 80000000,
        track = 1,
        level = 1,
        effect = (p, s) => {  }
    };

    public static CastleUpgrade castleUpgrade1_2 = new CastleUpgrade {
        name = "TODO",
        description = "",
        cost = 80000000,
        track = 1,
        level = 2,
        effect = (p, s) => {  }
    };

    public static CastleUpgrade castleUpgrade2_0 = new CastleUpgrade {
        name = "TODO",
        description = "",
        cost = 80000000,
        track = 2,
        level = 0,
        effect = (p, s) => {  }
    };


    public static CastleUpgrade castleUpgrade2_1 = new CastleUpgrade {
        name = "TODO",
        description = "",
        cost = 80000000,
        track = 2,
        level = 1,
        effect = (p, s) => { }
    };

    public static CastleUpgrade castleUpgrade2_2 = new CastleUpgrade {
        name = "TODO",
        description = "",
        cost = 80000000,
        track = 2,
        level = 2,
        effect = (p, s) => { }
    };
    public static List<TowerUpgrade> AllUpgrades() {
        List<TowerUpgrade> list = new List<TowerUpgrade>();
        list.Add(castleUpgrade0_0);
        list.Add(castleUpgrade0_1);
        list.Add(castleUpgrade0_2);
        list.Add(castleUpgrade1_0);
        list.Add(castleUpgrade1_1);
        list.Add(castleUpgrade1_2);
        list.Add(castleUpgrade2_0);
        list.Add(castleUpgrade2_1);
        list.Add(castleUpgrade2_2);
        return list;
    }

}


