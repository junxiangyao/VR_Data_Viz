using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using static Day;

public class Month
{
    public int month_order; // 
    public List<Day> dayList;  
    public MetaData data;               
    // Constructor that takes no arguments:
    public Month()
    {
        this.dayList = new List<Day>();
        this.data = new MetaData();
    }

    // Constructor that takes one argument:
    public Month(int month_order)
    {
        this.month_order = month_order;
        this.dayList = new List<Day>();
        this.data = new MetaData();
    }


    public void addDay(Day day_obj)
    {
        dayList.Add(day_obj);
    }

    public String printInfo()
    {  
        //buffer
        StringBuilder sb = new StringBuilder();

        for(int i = 0 ; i  < dayList.Count; i++)
        {
            sb.Append("dayList["+i+"] = "+dayList[i].printInfo()+"\n");
        }
        return sb.ToString();
    }

}
