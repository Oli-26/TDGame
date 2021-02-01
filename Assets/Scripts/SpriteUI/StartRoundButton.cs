using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRoundButton : Button
{
    public override void clicked(){
        control.GetComponent<RoundManager>().StartNextRound();
    }
}
