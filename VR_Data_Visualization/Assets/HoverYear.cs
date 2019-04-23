using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HoverMonth;

public class HoverYear
{
	public int year; // record the year
    public HoverMonth[] months;
    public int START_YEAR = 2005;
    public GameObject year_obj;

    // Constructor that takes one argument:
    public HoverYear(int year)
    {
        this.year = START_YEAR + year;
        this.months = new HoverMonth[12];
        this.year_obj = new GameObject();
        for(int i = 0; i < 12; ++i){
        	this.months[i] = new HoverMonth();
        	this.months[i].month_hover_obj.transform.SetParent(this.year_obj.transform);
        }
    }
}
