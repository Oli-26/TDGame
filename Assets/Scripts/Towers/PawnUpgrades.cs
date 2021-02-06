using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public interface PawnUpgrade{
    bool IsBuyable(List<PawnUpgrade> upgrades);
    void Apply(PawnProperties p, ShotProperties s);
}

// 0
public class pawnUpgrade0_0 : PawnUpgrade{
    public bool IsBuyable(List<PawnUpgrade> upgrades){
        return true;
    }

    public void Apply(PawnProperties p, ShotProperties s){      
        p.Cooldown = 0.85f;
        return;
    }
}

public class pawnUpgrade0_1{
    public bool IsBuyable(List<PawnUpgrade> upgrades){
        return true;
    }

    public void Apply(PawnProperties p, ShotProperties s){      
        s.Damage = 2;
        return;
    }
}

public class pawnUpgrade0_2{
    public bool IsBuyable(List<PawnUpgrade> upgrades){
        return true;
    }

    public void Apply(PawnProperties p, ShotProperties s){      
        s.Range = 3;
        return;
    }
}

// 1
public class pawnUpgrade1_0{

}
public class pawnUpgrade1_1{

}
public class pawnUpgrade1_2{

}    

