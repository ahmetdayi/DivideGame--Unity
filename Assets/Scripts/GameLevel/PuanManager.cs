using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuanManager : MonoBehaviour
{
    [SerializeField]
    private Text puanText;

    private int sumOfPuans,increaseOfPuan;
   
    void Start()
    {
        puanText.text = sumOfPuans.ToString();
    }

    public void AddPuan(string difficultlyLevel)
    {
        switch (difficultlyLevel)
        {
            case "Easy":
                increaseOfPuan = 5;
            break;
            case "Middle":
                increaseOfPuan = 10;
                break;
            case "Hard":
                increaseOfPuan = 15;
                break;
        }
        sumOfPuans += increaseOfPuan;
        puanText.text = sumOfPuans.ToString();
    }

   
}
