using System.Linq;
using UnityEditor;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    private GameObject[] allDices;
    private DiceData[] allDiceDatas;
    private DiceData[] dicesInUse;
    public string[] rollResults;
    void Start()
    {
        allDices = GameObject.FindGameObjectsWithTag("Dice");
        allDiceDatas = new DiceData[allDices.Length];

        for (int i = 0; i < allDices.Length; i++)
        {
            allDiceDatas[i] = allDices[i].GetComponent<DiceData>();
        }

        dicesInUse = allDiceDatas.Where(dice => dice.GetComponent<DiceData>().isInUse).ToArray();
        rollResults = new string[dicesInUse.Length];
    }

    void Update()
    {
        UpdateRolledDices();
    }

    private void UpdateRolledDices()
    {
        if (Input.GetKeyDown("space"))
        {
            for (int i = 0; i < dicesInUse.Length; i++)
            {
                rollResults[i] = GetDiceRollResult(dicesInUse[i]);
            }
        }
    }
    private string GetDiceRollResult1(DiceData dice)
    {
        string rollResult = "";
        foreach (GameObject face in dice.facesArray)
        {
            if (Vector3.Dot(face.transform.up, Vector3.up) == 1)
            {
                rollResult = face.GetComponent<FaceComponent>().faceType;
            }
        }
        return rollResult;
    }    
    private string GetDiceRollResult(DiceData dice)
    {
        string[] stringResultArray = new string[dice.numberOfFaces];
        float[] vectorDotResultArray = new float[dice.numberOfFaces];
        float closestVectorDot = 0;
        int closestIndex = 0;

        for (int i = 0; i < dice.numberOfFaces; i++)
        {
            stringResultArray[i] = dice.facesArray[i].GetComponent<FaceComponent>().faceType;
            vectorDotResultArray[i] = Vector3.Dot(dice.facesArray[i].transform.up, Vector3.up);

            if (vectorDotResultArray[i] >= closestVectorDot)
            {
                closestVectorDot = vectorDotResultArray[i];
                closestIndex = i;
            }
        }
        //Debug.Log("stringResultArray");
        //for (int i = 0; i < dice.numberOfFaces; i++)
        //{
            
        //    Debug.Log(i + " " +stringResultArray[i]);
        //}        
        
        //Debug.Log("VectorDotResultArray");
        //for (int i = 0; i < dice.numberOfFaces; i++)
        //{
            
        //    Debug.Log(i + " "+ vectorDotResultArray[i]);
        //}

        //Debug.Log("closestVectorDot "+ closestVectorDot);
        //Debug.Log("closestIndex " + closestIndex);

        return stringResultArray[closestIndex];
    }
}
