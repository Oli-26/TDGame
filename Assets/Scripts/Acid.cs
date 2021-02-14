using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : TimeEffected
{
    public float DamagePerSeconds = 0f;
    public float MaxDamage = 30f;
    void OnCollisionStay2D(Collision2D col){
        if(MaxDamage <= 0){
            Destroy(gameObject);
        }
        if(col.gameObject.tag == "Enemy"){
            col.gameObject.GetComponent<BaseEnemy>().AcidDamage(DamagePerSeconds*TimePassed());
            MaxDamage -= DamagePerSeconds*TimePassed();
        }
    }
}
