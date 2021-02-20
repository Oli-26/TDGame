using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : BaseTower
{
    protected override TowerProperties TowerProperties { get; set; } = new QueenProperties(3f, 3f, 400);
    protected override ShotProperties ShotProperties { get; set; } = new NanoBotProperties(1.5f, 2f, 3, true);

    new void Start()
    {
        base.Start();
        Tower = this;

        ResizeRangeIndicator(TowerProperties.Range);
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

        Nanobot shot = Instantiate(shotPrefab, transform.position, Quaternion.identity).GetComponent<Nanobot>();
        shot.setProperties(ShotProperties as NanoBotProperties);
        shot.SetTarget(Target);
        return true;
    }

    protected override List<TowerUpgrade> PossibleUpgrades() { 
        return QueenUpgrades.AllUpgrades();
    }

}

public class QueenProperties : TowerProperties{
    public QueenProperties(float cooldown, float range, int worth) : base(cooldown, range, worth){
    }
}
