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
    public Month[] months;
    // Constructor that takes no arguments:
    public Year()
    {
        months = new Month[12];
    }

    // Constructor that takes one argument:
    public Year(int year)
    {
        this.year = year;
        this.year_game_object = new GameObject();
        months = new Month[12];
    }


    public String printInfo()
    {
        //buffer
        StringBuilder sb = new StringBuilder();

        for(int i = 0 ; i  < 12; i++)
        {
            sb.Append("months["+i+"] = "+ months[i].printInfo()+"\n");
        }
        return sb.ToString();
    }

}
