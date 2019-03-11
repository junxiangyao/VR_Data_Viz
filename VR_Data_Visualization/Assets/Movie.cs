using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

using static Year;

public class Movie
{
    public Year[] years;
    public GameObject game_object;

    // Constructor that takes no arguments:
    public Movie(int num)
    {
        years = new Year[num];
        game_object = new GameObject();
    }

    public String printInfo()
    {
        //buffer
        StringBuilder sb = new StringBuilder();

        for(int i = 0 ; i  < years.Length; i++)
        {
            sb.Append("YearObjs["+i+"] = "+years[i].printInfo()+"\n");
        }
        return sb.ToString();
    }

}
