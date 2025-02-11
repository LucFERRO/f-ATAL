using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    private ResultManager resultManager;
    public TMP_Text numberOfRollsLeftText;
    public GameObject confirmRollsButton;
    void Start()
    {
        resultManager = GetComponent<ResultManager>();
        numberOfRollsLeftText.text = resultManager.maxNumberOfRolls.ToString();
        confirmRollsButton.GetComponent<Button>().interactable = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateNumberOfRollsLeft()
    {
        numberOfRollsLeftText.text = resultManager.currentNumberOfRolls.ToString();
    }
}
