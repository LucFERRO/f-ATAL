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


    void FindAllDice() // récupère tous les dés présent dans la scène et les ajoutes s'il n'existe pas déjà dans Dice liste.
    {
        bool DiceFound = false;

        GameObject[] DicesFound = GameObject.FindGameObjectsWithTag("Dice"); // on liste tous les dés trouver dans la scene

        foreach (GameObject Dice in DicesFound) // pour chaque dés trouvé dans la scène 
        {
            DiceFound = false;

            foreach (GameObject dicesInList in dicesListe) 
            {
                // on compare si l'un des dés trouver dans la scene est deja présent dans la liste
                if (Dice == dicesInList) // si oui on arrete la recherche et le dés est considéré comme déjà présent
                {
                    DiceFound = true;
                    break;
                }
            }

            if (!DiceFound) // si le dés n'est pas dans la liste de dé alors on l'ajoute
            {
                dicesListe.Add(Dice);
            }

            //on continue pour tous les dés trouver dans la scène
        }
    }
}
