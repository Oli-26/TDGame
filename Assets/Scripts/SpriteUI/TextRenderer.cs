using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextRenderer : MonoBehaviour
{

    private static int sortOrder = 4; 

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] textObjects = GameObject.FindGameObjectsWithTag("text");
        
        foreach(GameObject obj in textObjects) {
            obj.GetComponent<MeshRenderer>().sortingOrder = sortOrder;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
