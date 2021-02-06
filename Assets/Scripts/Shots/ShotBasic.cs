using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBasic : TimeEffected
{
    protected GameObject target;
    protected Vector3 direction;
    protected float lifeTime = 4f;
    protected GameObject ignoreEnemy;

    public ShotProperties properties;

    protected bool ignoreEnemySet = false;

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
        Vector3 changeVector;
        if(properties.HomingShot && target != null){
            changeVector = Vector3.Normalize(target.transform.position-transform.position) *TimePassed()*properties.Speed;
        }else{
            changeVector = direction *TimePassed()*properties.Speed;
        }
        
        BaseMove(changeVector);
    }

    public virtual void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "Enemy" && properties.DamageInstances >= 1){
            if(ignoreEnemySet && col.gameObject.GetInstanceID() == ignoreEnemy.GetInstanceID()){
                return;
            }
            if(col.gameObject.GetComponent<BaseEnemy>().IsScheduledForDeath()){
                return;
            }
            col.gameObject.GetComponent<BaseEnemy>().TakeDamage(properties.Damage);
            
            if (properties.decrementDamageInstances() == 0) {
                Destroy(gameObject);
            }
            
        }
    }
    
    public void SetIgnoreEnemy(GameObject enemy){
        ignoreEnemy = enemy;
    }

    public virtual void setProperties(ShotProperties properties) {
        this.properties = new ShotProperties(properties.Speed, properties.Damage, properties.DamageInstances, properties.HomingShot);
    }

    public ShotProperties getProperties() {
        return properties;
    }

}

public class ShotProperties {

        public float Speed {get; set;}
        public float Damage {get; set;}
        public int DamageInstances {get; set;}
        public bool HomingShot {get; set;}

        public ShotProperties(float speed, float damage, int damageInstances, bool homingShot) {
            Speed = speed;
            Damage = damage;
            DamageInstances = damageInstances;
            HomingShot = homingShot;
        }

        public int decrementDamageInstances() {
            return --DamageInstances;
        }

    }
