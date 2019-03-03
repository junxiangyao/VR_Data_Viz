using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using static Year;

public class Movie
{
    public Year[] YearObjs;
    // Constructor that takes no arguments:
    public Movie()
    {
        YearObjs = new Year[14];
    }

    public String printInfo()
    {
        //buffer
        StringBuilder sb = new StringBuilder();

        for(int i = 0 ; i  < YearObjs.Length; i++)
        {
            sb.Append("YearObjs["+i+"] = "+YearObjs[i].printInfo()+"\n");
        }
        return sb.ToString();
    }

}
