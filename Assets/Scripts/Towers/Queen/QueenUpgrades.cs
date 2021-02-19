using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QueenUpgrade : TowerUpgrade
{
    public Action<QueenProperties, NanoBotProperties> effect { get; set; }


    public override void Apply(TowerProperties p, ShotProperties s)
    {
        effect(p as QueenProperties, s as NanoBotProperties);
    }
}
public class QueenUpgrades {

    public static QueenUpgrade queenUpgrade0_0 = new QueenUpgrade {
        name = "TODO",
        description = "",
        cost = 80000000,
        track = 0,
        level = 0,
        effect = (p, s) => {  }
    };

    public static QueenUpgrade queenUpgrade0_1 = new QueenUpgrade {
        name = "TODO",
        description = "",
        cost = 80000000,
        track = 0,
        level = 1,
        effect = (p, s) => {  }
    };

    public static QueenUpgrade queenUpgrade0_2 = new QueenUpgrade {
        name = "TODO",
        description = "",
        cost = 80000000,
        track = 0,
        level = 2,
        effect = (p, s) => {  }
    };

    public static QueenUpgrade queenUpgrade1_0 = new QueenUpgrade {
        name = "TODO",
        description = "",
        cost = 250,
        track = 1,
        level = 0,
        effect = (p, s) => { }
    };

    public static QueenUpgrade queenUpgrade1_1 = new QueenUpgrade {
        name = "TODO",
        description = "",
        cost = 80000000,
        track = 1,
        level = 1,
        effect = (p, s) => {  }
    };

    public static QueenUpgrade queenUpgrade1_2 = new QueenUpgrade {
        name = "TODO",
        description = "",
        cost = 80000000,
        track = 1,
        level = 2,
        effect = (p, s) => {  }
    };

    public static QueenUpgrade queenUpgrade2_0 = new QueenUpgrade {
        name = "TODO",
        description = "",
        cost = 80000000,
        track = 2,
        level = 0,
        effect = (p, s) => {  }
    };


    public static QueenUpgrade queenUpgrade2_1 = new QueenUpgrade {
        name = "TODO",
        description = "",
        cost = 80000000,
        track = 2,
        level = 1,
        effect = (p, s) => { }
    };

    public static QueenUpgrade queenUpgrade2_2 = new QueenUpgrade {
        name = "TODO",
        description = "",
        cost = 80000000,
        track = 2,
        level = 2,
        effect = (p, s) => { }
    };
    public static List<TowerUpgrade> AllUpgrades() {
        List<TowerUpgrade> list = new List<TowerUpgrade>();
        list.Add(queenUpgrade0_0);
        list.Add(queenUpgrade0_1);
        list.Add(queenUpgrade0_2);
        list.Add(queenUpgrade1_0);
        list.Add(queenUpgrade1_1);
        list.Add(queenUpgrade1_2);
        list.Add(queenUpgrade2_0);
        list.Add(queenUpgrade2_1);
        list.Add(queenUpgrade2_2);
        return list;
    }

}


