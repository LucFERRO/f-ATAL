using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceGrab : MonoBehaviour
{

    public List<GameObject> dicesListe = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        FindAllDice();
    }

    private void OnMouseDown()
    {
        foreach (GameObject dice in dicesListe)
        {
            dice.GetComponent<GrabbedBehavior>().enabled = true;
        }
    }

    private void OnMouseUp()
    {
        foreach (GameObject dice in dicesListe)
        {
            
            dice.GetComponent<GrabbedBehavior>().enabled = false;
        }
    }


    void FindAllDice() // r�cup�re tous les d�s pr�sent dans la sc�ne et les ajoutes s'il n'existe pas d�j� dans Dice liste.
    {
        bool DiceFound = false;

        GameObject[] DicesFound = GameObject.FindGameObjectsWithTag("Dice"); // on liste tous les d�s trouver dans la scene

        foreach (GameObject Dice in DicesFound) // pour chaque d�s trouv� dans la sc�ne 
        {
            DiceFound = false;

            foreach (GameObject dicesInList in dicesListe) 
            {
                // on compare si l'un des d�s trouver dans la scene est deja pr�sent dans la liste
                if (Dice == dicesInList) // si oui on arrete la recherche et le d�s est consid�r� comme d�j� pr�sent
                {
                    DiceFound = true;
                    break;
                }
            }

            if (!DiceFound) // si le d�s n'est pas dans la liste de d� alors on l'ajoute
            {
                dicesListe.Add(Dice);
            }

            //on continue pour tous les d�s trouver dans la sc�ne
        }
    }
}
