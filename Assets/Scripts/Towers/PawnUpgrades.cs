using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public enum PawnUpgradeType {upgrade0_0, upgrade0_1, upgrade0_2, upgrade1_0, upgrade1_1, upgrade1_2};

public interface PawnUpgradeInterface{
    bool IsBuyable(List<PawnUpgrade> upgrades);
    void Apply(PawnProperties p, ShotProperties s);
}

public abstract class PawnUpgrade : PawnUpgradeInterface{
    public int cost {set; get;}
    public PawnUpgradeType type {set; get;}

    public virtual bool IsBuyable(List<PawnUpgrade> upgrades){
        return true;
    }

    public virtual void Apply(PawnProperties p, ShotProperties s){      
        return;
    }

    public virtual bool HasUpgrade(PawnUpgradeType upgradeType, List<PawnUpgrade> upgrades){
        foreach(PawnUpgrade up in upgrades){
            if(up.type == upgradeType){
                return true;
            }
        }
        return false;
    }
}




// 0
public class pawnUpgrade0_0 : PawnUpgrade{
    public pawnUpgrade0_0(){
        cost = 30;
        type = PawnUpgradeType.upgrade0_0;
    }
    public override bool IsBuyable(List<PawnUpgrade> upgrades){
        return !HasUpgrade(PawnUpgradeType.upgrade1_0, upgrades);
    }
    public override void Apply(PawnProperties p, ShotProperties s){      
        p.Cooldown = 0.85f;
        return;
    }
}

public class pawnUpgrade0_1 : PawnUpgrade{
    public pawnUpgrade0_1(){
        cost = 50;
        type = PawnUpgradeType.upgrade0_1;
    }
    public override bool IsBuyable(List<PawnUpgrade> upgrades){
        return HasUpgrade(PawnUpgradeType.upgrade0_0, upgrades);
    }

    public override void Apply(PawnProperties p, ShotProperties s){      
        p.Range = 3;
        return;
    }
}

public class pawnUpgrade0_2 : PawnUpgrade{
    public pawnUpgrade0_2(){
        cost = 70;
        type = PawnUpgradeType.upgrade0_2;
    }
    public override bool IsBuyable(List<PawnUpgrade> upgrades){
        return HasUpgrade(PawnUpgradeType.upgrade0_1, upgrades);
    }

    public override void Apply(PawnProperties p, ShotProperties s){      
        s.Damage = 2;
        return;
    }
}

// 1
public class pawnUpgrade1_0 : PawnUpgrade{
    public pawnUpgrade1_0(){
        cost = 50;
        type = PawnUpgradeType.upgrade1_0;
    }

    public override bool IsBuyable(List<PawnUpgrade> upgrades){
        return !HasUpgrade(PawnUpgradeType.upgrade0_0, upgrades);
    }

    public override void Apply(PawnProperties p, ShotProperties s){      
        s.HomingShot = true;
        return;
    }
}

public class pawnUpgrade1_1 : PawnUpgrade{
    public pawnUpgrade1_1(){
        cost = 50;
        type = PawnUpgradeType.upgrade1_1;
    }
    public override bool IsBuyable(List<PawnUpgrade> upgrades){
        return HasUpgrade(PawnUpgradeType.upgrade1_0, upgrades);
    }

    public override void Apply(PawnProperties p, ShotProperties s){      
        s.Damage = 2;
        return;
    }
}
public class pawnUpgrade1_2 : PawnUpgrade{
    public pawnUpgrade1_2(){
        cost = 50;
        type = PawnUpgradeType.upgrade1_2;
    }

    public override bool IsBuyable(List<PawnUpgrade> upgrades){
        return HasUpgrade(PawnUpgradeType.upgrade1_1, upgrades);
    }

    public override void Apply(PawnProperties p, ShotProperties s){      
        s.Damage = 2;
        return;
    }
}    

