using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TowerUpgrade { 

    public int cost {set; get;}
    public string name {set; get;}
    public string description {set; get;}
    public int track {set; get;}
    public int level {set; get;}
    public Action<TowerProperties, ShotProperties> effect {set; get;}

    public virtual bool IsBuyable(List<TowerUpgrade> upgrades) {
        if (level > 0) {
            return upgrades.Exists(u => u.track == track && u.level == level-1);
        }
        return true;
    }

    public virtual void Apply(TowerProperties p, ShotProperties s) {
        effect(p, s);
    }

    protected virtual bool HasUpgrade(TowerUpgrade upgrade, List<TowerUpgrade> upgrades) {
        return upgrades.Contains(upgrade);
    }
}
