using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : BaseTower
{
    protected override TowerProperties TowerProperties { get; set; } = new BishopProperties(2.5f, 2.5f, 150);
    protected override ShotProperties ShotProperties { get; set; } = new ShotProperties(2f, 3.5f, 1, true);

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

        ShotBasic shot = Instantiate(shotPrefab, transform.position, Quaternion.identity).GetComponent<ShotBasic>();
        shot.setProperties(ShotProperties);
        shot.SetTarget(Target);
        return true;
    }
    protected override List<TowerUpgrade> PossibleUpgrades() { 
        return BishopUpgrades.AllUpgrades();
    }

}

public class BishopProperties : TowerProperties{
    public BishopProperties(float cooldown, float range, int worth) : base(cooldown, range, worth){
    }
}
