using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class TowerUpgrade { 
    public int cost {set; get;}
    public string name {set; get;}
    public int track {set; get;}
    public int level {set; get;}

    public virtual bool IsBuyable(List<TowerUpgrade> upgrades) {
        if (level > 0) {
            return upgrades.Exists(u => u.track == track && u.level == level-1);
        }
        return true;
    }

    public virtual void Apply(TowerProperties p, ShotProperties s) {
        Debug.Log("Apply not overridden in class: " + this.GetType());
    }

    protected virtual bool HasUpgrade(TowerUpgrade upgrade, List<TowerUpgrade> upgrades) {
        return upgrades.Contains(upgrade);
    }
}
