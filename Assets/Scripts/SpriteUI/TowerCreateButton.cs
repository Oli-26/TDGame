using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCreateButton : Button
{
    public int type = 1;

    public override void clicked(){
        control.GetComponent<PlacementManager>().CreateTower(type);
    }
}
