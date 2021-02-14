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

    // DEPRICATED
    protected void BaseMove(Vector3 moveVector){
        Debug.Log("BaseMove is Depricated!!!");
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
        if(__control == null)
            setControl();
        return (int)(currentTick + 1 * __control.GameSpeed);
    }

    protected float TimeEffect(float number){
        if(__control == null)
            setControl();
        return number * __control.GameSpeed;
    }
}
