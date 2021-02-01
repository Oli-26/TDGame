using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBasic : MonoBehaviour
{
    Vector3 target;
    Vector3 direction;
    float lifeTime = 4f;
    public float speed;
    float damage = 0f;

    int damageInstancesLeft = 1;
    void Start()
    {
        Destroy(gameObject, lifeTime);
        speed = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetTarget(GameObject obj){
        target = obj.transform.position;
        direction = Vector3.Normalize(new Vector3(target.x-transform.position.x, target.y-transform.position.y, 0));
    }

    public virtual void Move(){
        Vector3 changeVector = direction *Time.deltaTime*speed;
        transform.position += changeVector;
    }

    public void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "Enemy" && damageInstancesLeft >= 1){
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
}
