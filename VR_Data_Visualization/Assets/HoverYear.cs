using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HoverMonth;

public class HoverYear
{
	public int year; // record the year
    public HoverMonth[] months;
    public int START_YEAR = 2005;

    // Constructor that takes one argument:
    public HoverYear(int year)
    {
        this.year = START_YEAR + year;
        months = new HoverMonth[12];
        for(int i = 0; i < 12; ++i){
        	months[i] = new HoverMonth();
        }
    }
}
