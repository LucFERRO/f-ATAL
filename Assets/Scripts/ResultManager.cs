using System.Linq;
using UnityEditor;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    private GameObject[] allDices;
    public DiceData[] allDiceDatas;
    public DiceData[] dicesInUse;
    public string[] rollResults;

    public int numberOfDicesInUse;

    [Header("Roll Parameters")]
    public float rollThrowForce;
    public float rollSpinForce;

    public int NumberOfDicesInUse
    {
        get
        {
            return numberOfDicesInUse;
        }
        set
        {
            numberOfDicesInUse = value;
            UpdateDicesInUse();
        }
    }
    void Start()
    {
        allDices = GameObject.FindGameObjectsWithTag("Dice");
        allDiceDatas = new DiceData[allDices.Length];

        for (int i = 0; i < allDices.Length; i++)
        {
            allDiceDatas[i] = allDices[i].GetComponent<DiceData>();
        }
    }

    void Update()
    {
        RollDices();
        UpdateRolledDices();
    }

    private void RollDices()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (dicesInUse.Length == 0)
            {
                Debug.Log("NO DICES SELECTED!");
                return;
            }

            for (int i = 0; i < dicesInUse.Length; i++)
            {
                Vector3 randomSpinVector = new Vector3 (Random.Range(-1,1), Random.Range(-1, 1), Random.Range(-1, 1));
                dicesInUse[i].gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * rollThrowForce, ForceMode.Impulse);
                dicesInUse[i].gameObject.GetComponent<Rigidbody>().AddTorque(randomSpinVector.normalized * rollSpinForce, ForceMode.Impulse);
            }
        }
    }

    private void UpdateDicesInUse()
    {
        dicesInUse = allDiceDatas.Where(dice => dice.GetComponent<DiceData>().isInUse).ToArray();
        rollResults = new string[dicesInUse.Length];
    }

    private void UpdateRolledDices()
    {
        if (Input.GetKeyDown("space"))
        {
            if (dicesInUse.Length == 0) { 
                dicesInUse = new DiceData[0];
                rollResults = new string[0];
                return;
            }

            for (int i = 0; i < dicesInUse.Length; i++)
            {
                rollResults[i] = GetDiceRollResult(dicesInUse[i]);
            }
        }
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