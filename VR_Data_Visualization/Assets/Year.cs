using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

using static Month;

public class Year
{
    public int year; // record the year
    public GameObject year_game_object; // parent of all the monthly graphics
    public Month[] MonthObjs;
    // Constructor that takes no arguments:
    public Year()
    {
        MonthObjs = new Month[12];
    }

    // Constructor that takes one argument:
    public Year(int year)
    {
        this.year = year;
        this.year_game_object = new GameObject();
        MonthObjs = new Month[12];
    }


    public String printInfo()
    {
        //buffer
        StringBuilder sb = new StringBuilder();

        for(int i = 0 ; i  < 12; i++)
        {
            sb.Append("MonthObjs["+i+"] = "+MonthObjs[i].printInfo()+"\n");
        }
        return sb.ToString();
    }

}
