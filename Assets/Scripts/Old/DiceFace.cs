using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceFace : MonoBehaviour
{

    public GameObject[] facesDuDes;

    // Update is called once per frame
    void Update()
    {
       

        if (Input.GetKeyDown("space"))
        {
            Debug.Log(CheckFace());
        }

    }

    

    public string CheckFace()
    {
        string faceDessusString ="null";

        foreach (GameObject face in facesDuDes)
        {
            if (face.transform.up == Vector3.up)
            {
                faceDessusString = face.GetComponent<FaceComponent>().faceType;
            }
        }

        return faceDessusString;
    }


}
