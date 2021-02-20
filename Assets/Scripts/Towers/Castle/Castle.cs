using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : BaseTower
{
    protected override TowerProperties TowerProperties { get; set; } = new CastleProperties(0.65f, 1.2f, 250);
    protected override ShotProperties ShotProperties { get; set; } = new ShotProperties(2f, 1f, 1, true);

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
        CastleProperties p = TowerProperties as CastleProperties;
        Retarget(TowerProperties.Range);
        if(!TargetSet)
            return false;
        CurrentCooldown = TowerProperties.Cooldown;    

        for(int i = 0; i < p.ShotCount; i++){
            ShotBasic shot = Instantiate(shotPrefab, transform.position + new Vector3(0f, 0.1f*i, 0f), Quaternion.identity).GetComponent<ShotBasic>();
            shot.setProperties(ShotProperties);
            shot.SetTarget(Target);
        }
        return true;
    }

    protected override List<TowerUpgrade> PossibleUpgrades() { 
        return CastleUpgrades.AllUpgrades();
    }


}

public class CastleProperties : TowerProperties{
    public int ShotCount {get; set;} = 3;
    public CastleProperties(float cooldown, float range, int worth) : base(cooldown, range, worth){
    }
}
