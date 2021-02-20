using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : Button
{
    public string difficulty = "Easy";

    new protected void Update()
    {
        base.Update();
    }
    
     public override void clicked(){
         Debug.Log("CLICKED");
         GameInfo.difficulty = difficulty;
         UnityEngine.SceneManagement.SceneManager.LoadScene(1);
     }
}
