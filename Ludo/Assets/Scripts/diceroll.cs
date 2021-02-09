using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class diceroll : MonoBehaviour
{
   
    public Text diceNumber;
    int number;


    public void diceRoller()
    {

        number = Random.Range(0, 9);
        diceNumber.gameObject.SetActive(false);
        StartCoroutine("legendary");
        
        
    }

    IEnumerator legendary()
    {
        yield return new WaitForSeconds(0.5f);
        diceNumber.gameObject.SetActive(true);
        diceNumber.text = number.ToString();
    }
}


