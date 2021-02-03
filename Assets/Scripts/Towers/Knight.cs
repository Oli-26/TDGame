using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : BaseTower
{
    float timeUntilNextDoubleAttack = 0f;
    const float doubleAttackCooldown = 1.5f;

    void Start()
    {
        base.Start();
        shotCooldown = 1.5f;
        damage = 2f;
        shotSpeed = 5f;
    }

    protected override void Update()
    {
        base.Update(); 
        timeUntilNextDoubleAttack -= TimePassed();

        if(currentCooldown <= 0 && active){
            if(Attack()){
                if(timeUntilNextDoubleAttack <= 0){
                    //Debug.Log("Double attacking!");
                    //currentCooldown = 0.1f;
                    //timeUntilNextDoubleAttack = doubleAttackCooldown;
                }
            }
        }
    }
}
