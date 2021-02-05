using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : BaseTower
{

    protected new SplitShotProperties shotProperties = new SplitShotProperties(6f, 3f, 2f, 1, 2);

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
        Retarget(shotProperties.Range);
        if(!targetSet)
            return false;
        currentCooldown = shotCooldown;    

        SplitShot shot = Instantiate(shotPrefab, transform.position, Quaternion.identity).GetComponent<SplitShot>();
        shot.setSplitProperties(shotProperties);
        shot.SetTarget(target);
        return true;
    }
}
