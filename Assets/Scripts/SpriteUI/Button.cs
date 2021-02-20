using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject control = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            Debug.Log("Trying to click");
            if(GetComponent<Collider2D>().bounds.Contains(GetMouseToWorld())){
                clicked();
            }
        }
    }

    protected Vector3 GetMouseToWorld(){
        Vector3 pointTo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(pointTo.x, pointTo.y, gameObject.transform.position.z);
    }

    public virtual void clicked(){

    }
}
