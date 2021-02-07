using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class PawnUpgrade : TowerUpgrade {

    private static readonly object padlock = new object();
}


public class pawnUpgrade0_0 : PawnUpgrade{
    private static PawnUpgrade instance = null;
    private pawnUpgrade0_0(){
        name = "Upgrade 0-0";
        cost = 30;
        track = 0;
        level = 0;
    }

    public static PawnUpgrade GetInstance() {
        if (instance == null) {
            instance = new pawnUpgrade0_0();
        }
        return instance;
    }

    public override void Apply(TowerProperties p, ShotProperties s){      
        p.Cooldown = 0.85f;
        return;
    }
}

public class pawnUpgrade0_1 : PawnUpgrade{
    private static PawnUpgrade instance = null;
    public static PawnUpgrade GetInstance() {
        if (instance == null) {
            instance = new pawnUpgrade0_1();
        }
        return instance;
    }
    private pawnUpgrade0_1(){
        name = "Upgrade 0-1";
        cost = 50;
        track = 0;
        level = 1;
    }

    public override void Apply(TowerProperties p, ShotProperties s){      
        p.Range = 3;
        return;
    }
}

public class pawnUpgrade0_2 : PawnUpgrade{
    private static PawnUpgrade instance = null;
    public static PawnUpgrade GetInstance() {
        if (instance == null) {
            instance = new pawnUpgrade0_2();
        }
        return instance;
    }
    private pawnUpgrade0_2(){
        name = "Upgrade 0-2";
        cost = 70;
        track = 0;
        level = 2;
    }

    public override void Apply(TowerProperties p, ShotProperties s){      
        s.Damage = 2;
        return;
    }
}

// 1
public class pawnUpgrade1_0 : PawnUpgrade{
    private static PawnUpgrade instance = null;

    public static PawnUpgrade GetInstance() {
        if (instance == null) {
            instance = new pawnUpgrade1_0();
        }
        return instance;
    }
    private pawnUpgrade1_0(){
        name = "Upgrade 1-0";
        cost = 50;
        track = 1;
        level = 0;
    }

    public override void Apply(TowerProperties p, ShotProperties s){      
        p.Range += 1;
        return;
    }
}

public class pawnUpgrade1_1 : PawnUpgrade {
    private static PawnUpgrade instance = null;
    public static PawnUpgrade GetInstance() {
        if (instance == null) {
            instance = new pawnUpgrade1_1();
        }
        return instance;
    }
    private pawnUpgrade1_1(){
        name = "Upgrade 1-1";
        cost = 50;
        track = 1;
        level = 1;
    }

    public override void Apply(TowerProperties p, ShotProperties s){      
        s.HomingShot = true;
        return;
    }
}

public class pawnUpgrade1_2 : PawnUpgrade{
    
    private static PawnUpgrade instance = null;

    public static PawnUpgrade GetInstance() {
        if (instance == null) {
            instance = new pawnUpgrade1_2();
        }
        return instance;
    }
    private pawnUpgrade1_2(){
        name = "Upgrade 1-2";
        cost = 50;
        track = 1;
        level = 2;
    }

    public override void Apply(TowerProperties p, ShotProperties s){      
        p.Range += 1;
        return;
    }
}    

public class pawnUpgrade2_0 : PawnUpgrade{
    private static PawnUpgrade instance = null;
    public static PawnUpgrade GetInstance() {
        if (instance == null) {
            instance = new pawnUpgrade2_0();
        }
        return instance;
    }
    private pawnUpgrade2_0(){
        name = "Upgrade 2-0";
        cost = 50;
        track = 2;
        level = 0;
    }

    public override void Apply(TowerProperties p, ShotProperties s){      
        p.Range += 1;
        return;
    }
}

