using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : BaseTower
{
    protected override TowerProperties TowerProperties { get; set; } = new CastleProperties(0.2f, 1.2f, 250);
    protected override ShotProperties ShotProperties { get; set; } = new ShotProperties(7f, 0.3f, 1, true);

    new void Start()
    {
        base.Start();
        Tower = this;

        ResizeRangeIndicator(TowerProperties.Range);
        //mode = TargetingMode.Strongest;
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

        shot = Instantiate(shotPrefab, transform.position + new Vector3(0f, 0.1f, 0f), Quaternion.identity).GetComponent<ShotBasic>();
        shot.setProperties(ShotProperties);
        shot.SetTarget(Target);

        shot = Instantiate(shotPrefab, transform.position + new Vector3(0f, -0.1f, 0f), Quaternion.identity).GetComponent<ShotBasic>();
        shot.setProperties(ShotProperties);
        shot.SetTarget(Target);
        return true;
    }


}

public class CastleProperties : TowerProperties{
    public CastleProperties(float cooldown, float range, int worth) : base(cooldown, range, worth){
    }
}
