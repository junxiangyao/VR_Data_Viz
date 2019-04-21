using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HoverMonth;

public class HoverYear
{
	public int year; // record the year
    public HoverMonth[] months;

    // Constructor that takes one argument:
    public HoverYear(int year)
    {
        this.year = year;
        months = new HoverMonth[12];
    }
}
