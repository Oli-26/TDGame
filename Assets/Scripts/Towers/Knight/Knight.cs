using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : BaseTower
{
    protected override TowerProperties TowerProperties { get; set; } = new KnightProperties(3f, 2.5f, 150);
    protected override ShotProperties ShotProperties { get; set; } = new SplitShotProperties(3f, 3f, 1, true , 3);

    new void Start()
    {
        base.Start();
        Tower = this;

    }

    protected override void Update()
    {
        base.Update(); 

        if(CurrentCooldown <= 0 && Active){
            Attack();
        }
    }

    protected override bool Attack(){
        Retarget(TowerProperties.Range);
        if(!TargetSet)
            return false;
        CurrentCooldown = TowerProperties.Cooldown;   

        SplitShot shot = Instantiate(shotPrefab, transform.position, Quaternion.identity).GetComponent<SplitShot>();
        shot.setSplitProperties(ShotProperties as SplitShotProperties);
        shot.SetTarget(Target);
        return true;
    }
    
    protected override List<TowerUpgrade> PossibleUpgrades() { 
        return KnightUpgrades.AllUpgrades();
    }
}

public class KnightProperties : TowerProperties{
    public KnightProperties(float cooldown, float range, int worth) : base(cooldown, range, worth){
    }
}
