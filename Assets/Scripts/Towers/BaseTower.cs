using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    // Start is called before the first frame update
    protected float shotCooldown = 1f;
    protected float currentCooldown;
    public float range = 1f;

    protected bool active = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(currentCooldown > 0){
            currentCooldown -= Time.deltaTime;
        }
    }
    
    public void Place(){
        active = true;
    }
}
