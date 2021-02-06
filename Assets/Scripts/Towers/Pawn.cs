using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : BaseTower
{
    new PawnProperties properties = new PawnProperties(1f, 2f);
    List<PawnUpgrade> upgrades = new List<PawnUpgrade>();

    new void Start()
    {
        base.Start();
        shotCooldown = 1f;

        pawnUpgrade1_0 up = new pawnUpgrade1_0();
        up.Apply(properties, shotProperties);


        pawnUpgrade1_1 up2 = new pawnUpgrade1_1();
        up2.Apply(properties, shotProperties);
        

        pawnUpgrade1_2 up3 = new pawnUpgrade1_2();
        up3.Apply(properties, shotProperties);


        ResizeRangeIndicator(properties.Range);
    }

    protected override void Update()
    {
        base.Update(); 
        if(currentCooldown <= 0 && active){
            Attack();

            
        }
    }
    protected override bool Attack(){
        Debug.Log(properties.Range);
        Retarget(properties.Range);
        if(!targetSet)
            return false;
        currentCooldown = properties.Cooldown;    

        ShotBasic shot = Instantiate(shotPrefab, transform.position, Quaternion.identity).GetComponent<ShotBasic>();
        shot.setProperties(shotProperties);
        shot.SetTarget(target);
        return true;
    }


}

public class PawnProperties : TowerProperties{
    public PawnProperties(float cooldown, float range) : base(cooldown, range){
    }
}
