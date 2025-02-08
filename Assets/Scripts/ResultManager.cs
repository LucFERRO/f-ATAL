using System.Linq;
using UnityEditor;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    private GameObject[] allDices;
    public GameObject[] unusedDices;
    public GameObject[] allRolledDices;
    public DiceData[] allDiceDatas;
    public DiceData[] dicesInUse;
    public DiceData[] globalDicesDataInUse;

    public int numberOfDicesInUse;

    [Header("Roll Parameters")]
    public int maxNumberOfDices = 5;
    public float rollThrowForce;
    public float rollSpinForce;

    [Header("Roll Results")]
    public string[] currentRollResults;
    public string[] globalRollResults;
    public string[] possibleResults;

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
        globalDicesDataInUse = new DiceData[maxNumberOfDices];
        globalRollResults = new string[maxNumberOfDices];

        for (int i = 0; i < allDices.Length; i++)
        {
            allDiceDatas[i] = allDices[i].GetComponent<DiceData>();
        }
    }

    void Update()
    {
        RollDices();

        if (unusedDices.Length > 0)
        {
            return;
        }
        else
        {
            UpdateUnusedDices();
        }

        UpdateRolledDices();
    }

    private void RollDices()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (dicesInUse.Length == 0)
            {
                Debug.Log("NO DICE SELECTED!");
                return;
            }

            for (int i = 0; i < dicesInUse.Length; i++)
            {
                if (!dicesInUse[i].gameObject.CompareTag("RolledDice"))
                {
                    dicesInUse[i].gameObject.tag = "RolledDice";
                }
                Vector3 randomSpinVector = new Vector3 (Random.Range(-1,1), Random.Range(-1, 1), Random.Range(-1, 1));
                Vector3 randomSpinVector2 = new Vector3 (Random.Range(-1,1), Random.Range(-1, 1), Random.Range(-1, 1));

                dicesInUse[i].gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * rollThrowForce, ForceMode.Impulse);
                dicesInUse[i].gameObject.GetComponent<Rigidbody>().AddTorque((randomSpinVector.normalized + randomSpinVector2.normalized) * rollSpinForce, ForceMode.Impulse);
            }
            allRolledDices = GameObject.FindGameObjectsWithTag("RolledDice");
            for (int i = 0; i < allRolledDices.Length; i++)
            {
                globalDicesDataInUse[i] = allRolledDices[i].GetComponent<DiceData>();
            }
        }
    }
    private void UpdateUnusedDices()
    {
        if (allRolledDices.Length >= maxNumberOfDices)
        {
            unusedDices = allDices.Where(dice => dice.CompareTag("Dice")).ToArray();
            for (int i = 0; i < unusedDices.Length; i++)
            {
                unusedDices[i].SetActive(false);
            }
        }
    }

    private void UpdateDicesInUse()
    {
        dicesInUse = allDiceDatas.Where(dice => dice.GetComponent<DiceData>().isInUse).ToArray();
        currentRollResults = new string[dicesInUse.Length];
    }

    private void UpdateRolledDices()
    {
        if (Input.GetKeyDown("space"))
        {
            if (dicesInUse.Length == 0) { 
                dicesInUse = new DiceData[0];
                currentRollResults = new string[0];
                return;
            }

            for (int i = 0; i < dicesInUse.Length; i++)
            {
                currentRollResults[i] = GetDiceRollResult(dicesInUse[i]);
            }

            //UpdateGlobalRollResults();
        }
    }

    private void UpdateGlobalRollResults()
    {
        for (int i = 0; i < globalDicesDataInUse.Length; i++)
        {
            globalRollResults[i] = GetDiceRollResult(globalDicesDataInUse[i]);
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