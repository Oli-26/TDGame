using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    float chance = 0.4f;
    public void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "shot" && transform.parent.gameObject.GetComponent<QueenEnemy>().PulseUp()){
            col.gameObject.GetComponent<ShotBasic>().Reverse(chance);
        }
    }
}
