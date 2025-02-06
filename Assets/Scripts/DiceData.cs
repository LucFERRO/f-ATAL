using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceData : MonoBehaviour
{
    public int numberOfFaces;
    public bool isInUse;
    [HideInInspector] public GameObject[] facesArray;
    private ResultManager resultManager;

    void Start()
    {
        resultManager = transform.parent.GetComponent<ResultManager>();
        facesArray = new GameObject[numberOfFaces];
        for (int i = 0; i < transform.childCount; i++)
        {
            facesArray[i] = transform.GetChild(i).gameObject;
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)) {
            isInUse = !isInUse;
            resultManager.NumberOfDicesInUse += isInUse ? 1 : -1;
        }
    }
}
