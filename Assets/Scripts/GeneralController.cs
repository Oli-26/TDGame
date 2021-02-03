using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralController : MonoBehaviour
{
    bool gameSpedUp = false;
    public float GameSpeed = 1f;

    Slider speedSlider;
    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 60;
        speedSlider = GameObject.Find("SpeedSlider").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGameSpeedFromSlider(){
        GameSpeed = speedSlider.value;
    }

    public void ToggleGameSpeed(){
        if(gameSpedUp){
            GameSpeed = 1f;
            gameSpedUp = !gameSpedUp;
        }else{
            GameSpeed = 2f;
            gameSpedUp = !gameSpedUp;
        }
    }
}
