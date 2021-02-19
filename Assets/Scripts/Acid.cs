using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : TimeEffected
{
    public float DamagePerSeconds = 0f;
    public float MaxDamage = 30f;
    public bool Slow = false;
    public float SlowPercent = 0f;
    public float SlowTime = 0f;
    void OnCollisionStay2D(Collision2D col){
        if(MaxDamage <= 0){
            Destroy(gameObject);
        }
        if(col.gameObject.tag == "Enemy"){
            col.gameObject.GetComponent<BaseEnemy>().AcidDamage(DamagePerSeconds*TimePassed(), Slow, SlowTime, SlowPercent);
            MaxDamage -= DamagePerSeconds*TimePassed();
        }
    }
}
