using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEffected : MonoBehaviour
{
    GeneralController __control;
    void Start()
    {
        setControl();
    }

    void setControl(){
        __control = GameObject.Find("Control").GetComponent<GeneralController>();
    }

    protected void BaseMove(Vector3 moveVector){
        if(__control == null)
            setControl();
        transform.position += moveVector; 
    }

    protected float TimePassed(){
        if(__control == null)
            setControl();
        return Time.deltaTime*__control.GameSpeed;
    }

    protected int Tick(int currentTick){
        return (int)(currentTick + 1 * __control.GameSpeed);
    }
}
