using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : BaseTower
{
    protected new TowerProperties properties = new TowerProperties(1f, 3f);
    protected new SplitShotProperties shotProperties = new SplitShotProperties(6f, 2f, 1, true ,2);

    new void Start()
    {
        base.Start();
        shotCooldown = 1.5f;
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
        currentCooldown = shotCooldown;    

        SplitShot shot = Instantiate(shotPrefab, transform.position, Quaternion.identity).GetComponent<SplitShot>();
        shot.setSplitProperties(shotProperties);
        shot.SetTarget(target);
        return true;
    }
}
