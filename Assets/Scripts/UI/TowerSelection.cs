using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerSelection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool dragging;
    private bool mouseIsOver;
    private Vector3 clickOffset;


    public GameObject NameTag;

    public void Update() {
        if(Input.GetMouseButtonDown(0) &&  mouseIsOver && gameObject.active){
            dragging = true;
            clickOffset = GetMouseToWorld() - transform.position;
        }
        if(Input.GetMouseButtonUp(0)){
            dragging = false;
        }
        if (dragging) {
            transform.position = GetMouseToWorld() - clickOffset;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        mouseIsOver = true;
    }
      
    public void OnPointerExit(PointerEventData eventData) {
        mouseIsOver = false;
    }

    Vector3 GetMouseToWorld(){
        Vector3 pointTo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(pointTo.x, pointTo.y, transform.position.z);
    }

    public void Close(){
        mouseIsOver = false;
        dragging = false;
        gameObject.SetActive(false);
    }

    public void Populate(string name){
        NameTag.GetComponent<InputField>().text = name;
    }
}
