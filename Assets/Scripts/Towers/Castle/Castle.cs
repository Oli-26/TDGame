using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : BaseTower
{
    new CastleProperties properties = new CastleProperties(0.2f, 1.2f, 250);
    //List<PawnUpgrade> upgrades = new List<PawnUpgrade>();

    new void Start()
    {
        base.Start();
        Tower = this;

        ResizeRangeIndicator(properties.Range);
        shotProperties = new ShotProperties(7f, 0.3f, 1, true);
        //mode = TargetingMode.Strongest;
    }

    protected override void Update()
    {
        base.Update(); 
        if(currentCooldown <= 0 && active){
            Attack();
        }
    }
    protected override bool Attack(){
        Retarget(properties.Range);
        if(!targetSet)
            return false;
        currentCooldown = properties.Cooldown;    

        ShotBasic shot = Instantiate(shotPrefab, transform.position, Quaternion.identity).GetComponent<ShotBasic>();
        shot.setProperties(shotProperties);
        shot.SetTarget(target);

        shot = Instantiate(shotPrefab, transform.position + new Vector3(0f, 0.1f, 0f), Quaternion.identity).GetComponent<ShotBasic>();
        shot.setProperties(shotProperties);
        shot.SetTarget(target);

        shot = Instantiate(shotPrefab, transform.position + new Vector3(0f, -0.1f, 0f), Quaternion.identity).GetComponent<ShotBasic>();
        shot.setProperties(shotProperties);
        shot.SetTarget(target);
        return true;
    }


}

public class CastleProperties : TowerProperties{
    public CastleProperties(float cooldown, float range, int worth) : base(cooldown, range, worth){
    }
}
