using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : BaseTower
{
    new PawnProperties properties = new PawnProperties(1f, 2f);

    protected override List<TowerUpgrade> PossibleUpgrades() {
        Debug.Log("initialisingUpgradeList");
        List<TowerUpgrade> list = new List<TowerUpgrade>();
        list.Add(pawnUpgrade0_0.GetInstance());
        list.Add(pawnUpgrade0_1.GetInstance());
        list.Add(pawnUpgrade0_2.GetInstance());
        list.Add(pawnUpgrade1_0.GetInstance());
        list.Add(pawnUpgrade1_1.GetInstance());
        list.Add(pawnUpgrade1_2.GetInstance());
        list.Add(pawnUpgrade2_0.GetInstance());
        return list;
    }

    new void Start()
    {
        base.Start();
        Tower = this;

        pawnUpgrade1_0 up = (pawnUpgrade1_0) pawnUpgrade1_0.GetInstance();
        up.Apply(properties, shotProperties);

        pawnUpgrade1_1 up2 = (pawnUpgrade1_1) pawnUpgrade1_1.GetInstance();
        up2.Apply(properties, shotProperties);
        

        pawnUpgrade1_2 up3 = (pawnUpgrade1_2) pawnUpgrade1_2.GetInstance();
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
        Retarget(properties.Range);
        if(!targetSet)
            return false;
        currentCooldown = properties.Cooldown;    

        ShotBasic shot = Instantiate(shotPrefab, transform.position, Quaternion.identity).GetComponent<ShotBasic>();
        shot.setProperties(shotProperties);
        shot.SetTarget(target);
        return true;
    }

    public void upgrade(PawnUpgrade upgrade) {
        if (!upgrades.Contains(upgrade)) {
            upgrades.Add(upgrade);
            upgrade.Apply(properties, shotProperties);
        }
    }


}

public class PawnProperties : TowerProperties {
    public PawnProperties(float cooldown, float range) : base(cooldown, range) {
    }
}
