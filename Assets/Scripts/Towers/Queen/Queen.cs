using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : BaseTower
{
    protected override TowerProperties TowerProperties { get; set; } = new QueenProperties(2f, 3f, 400);
    protected override ShotProperties ShotProperties { get; set; } = new ShotProperties(3f, 2f, 3, true);

    new void Start()
    {
        base.Start();
        Tower = this;

        ResizeRangeIndicator(TowerProperties.Range);
        Mode = TargetingMode.Strongest;
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


}

public class QueenProperties : TowerProperties{
    public QueenProperties(float cooldown, float range, int worth) : base(cooldown, range, worth){
    }
}
