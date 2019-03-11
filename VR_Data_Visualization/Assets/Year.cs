using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using static Month;

public class Year
{
    public int year;
    public GameObject year_object;
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
