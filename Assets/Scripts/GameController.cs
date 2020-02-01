using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int goldOreAmount;

    public int GoldOreAmount
    {
        get { return goldOreAmount; }
        set { goldOreAmount = value; }
    }

    public int goldBarAmount;

    public int GoldBarAmount
    {
        get { return goldBarAmount; }
        set { goldBarAmount = value; }
    }

    public int moneyAmount;

    public int MoneyAmount
    {
        get { return moneyAmount; }
        set { moneyAmount = value; }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoldOreGain()
    {
        goldOreAmount++;
    }

    public void GoldBarGain()
    {
        if (GoldOreAmount > 0)
        {
            goldOreAmount--;
            goldBarAmount++;
        }
    }

    public void MoneyGained()
    {
        if (GoldBarAmount > 0)
        {
            if (GoldBarAmount > 2)
            {
                goldBarAmount -= 3;
                moneyAmount += 30;
            }
            else
            {
                goldBarAmount--;
                moneyAmount += 10;
            }
        }
    }
}
