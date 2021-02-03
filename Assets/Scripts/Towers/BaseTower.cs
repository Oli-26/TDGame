using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    // Start is called before the first frame update
    protected float shotCooldown = 1f;
    protected float currentCooldown;
    public float range = 1f;


    protected GameObject target;
    protected bool targetSet = false;
    public GameObject shotPrefab;

    public float shotSpeed = 5f;
    public float damage = 1f;

    protected GameObject control;
    protected bool active = false;
    protected virtual void Start()
    {
        control = GameObject.Find("Control");
        GetRangeIndicator();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(currentCooldown > 0){
            currentCooldown -= Time.deltaTime;
        }
        SetRangeIndicatorOnHover();
    }
    
    public void Place(){
        active = true;
    }


    
    protected virtual void Retarget(){
        GameObject[] enemies = control.GetComponent<RoundManager>().GetAliveEnemies();
        float maxDistanceTraveled = 0f;
        targetSet = false;

        for(int i = 0; i<enemies.Length; i++){
            if(Vector3.Distance(enemies[i].transform.position, transform.position) < range){
                float lengthTraveled = enemies[i].GetComponent<BaseEnemy>().GetDistanceTraveled();
                if(lengthTraveled > maxDistanceTraveled){
                    target = enemies[i];
                    maxDistanceTraveled = lengthTraveled;
                    targetSet = true;
                }
            }
        }     
    }


    protected virtual bool Attack(){
        Retarget();
        if(!targetSet)
            return false;
        currentCooldown = shotCooldown;    
        GameObject shot = Instantiate(shotPrefab, transform.position, Quaternion.identity);
        
        shot.GetComponent<ShotBasic>().SetTarget(target);
        shot.GetComponent<ShotBasic>().SetSpeed(shotSpeed);
        shot.GetComponent<ShotBasic>().SetDamage(damage);
        return true;
    }


    
    // Lets do some UI stuff here for now.
    GameObject rangeIndicator;

    void GetRangeIndicator(){
        rangeIndicator = transform.GetChild(0).gameObject;
    }

    void SetRangeIndicatorOnHover(){
        if(CheckIfMouseInRange()){
            rangeIndicator.SetActive(true);
        }else{
            rangeIndicator.SetActive(false);
        }
    }

    public bool CheckIfMouseInRange(){
        if(PositionIsInsideGameObject(GetMouseToWorld(), gameObject)){
            return true;
        }
        return false;
    }


    Vector3 GetMouseToWorld(){
        Vector3 pointTo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(pointTo.x, pointTo.y, 0);
    }


    bool PositionIsInsideGameObject(Vector3 pos, GameObject obj){
        if(obj.GetComponent<Collider2D>().bounds.Contains(pos)){
            return true;
        }
        return false;
    }

}
