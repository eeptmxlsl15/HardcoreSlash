using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    public GameObject[] titles;

    public void Lose()
    {
        Debug.Log("lose");
        titles[0].SetActive(true);
    }

    public void Win()
    {
        Debug.Log("win");
   
           titles[1].SetActive(true);
    }
}
