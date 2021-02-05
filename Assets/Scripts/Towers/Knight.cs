using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : BaseTower
{

    protected ShotProperties shotProperties = new SplitShotProperties(5f, 1f, 2f, 1);


    void Start()
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
}
