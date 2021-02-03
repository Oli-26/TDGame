using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBasic : MonoBehaviour
{
    protected GameObject target;
    protected Vector3 direction;
    protected float lifeTime = 4f;
    public float speed;
    protected float damage = 0f;

    protected GameObject ignoreEnemy;
    protected bool ignoreEnemySet = false;
    protected int damageInstancesLeft = 1;

    public void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    public void Update()
    {
        Move();
    }

    public void SetTarget(GameObject obj){
        target = obj;
        Vector3 targetPos = target.transform.position;
        direction = Vector3.Normalize(new Vector3(targetPos.x-transform.position.x, targetPos.y-transform.position.y, 0));
    }

    public virtual void Move(){
        Vector3 changeVector = direction *Time.deltaTime*speed;
        transform.position += changeVector;
    }

    public virtual void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "Enemy" && damageInstancesLeft >= 1){
            if(ignoreEnemySet && col.gameObject.GetInstanceID() == ignoreEnemy.GetInstanceID()){
                return;
            }
            if(col.gameObject.GetComponent<BaseEnemy>().IsScheduledForDeath()){
                return;
            }
            col.gameObject.GetComponent<BaseEnemy>().TakeDamage(damage);
            damageInstancesLeft--;
            Destroy(gameObject);
            
        }
    }

    public void SetSpeed(float sp){
        speed = sp;
    }

    public void SetDamage(float d){
        damage = d;
    }
    
    public void SetIgnoreEnemy(GameObject enemy){
        ignoreEnemy = enemy;
    }
}
