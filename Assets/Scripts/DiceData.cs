using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceData : MonoBehaviour
{
    public int numberOfFaces;
    public bool isInUse;
    [HideInInspector] public GameObject[] facesArray;
    void Start()
    {
        facesArray = new GameObject[numberOfFaces];
        for (int i = 0; i < transform.childCount; i++)
        {
            facesArray[i] = transform.GetChild(i).gameObject;
        }
    }
    void Update()
    {

    }
}
