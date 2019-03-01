using System;

using static Day;
using System.Collections;

public class Month
{
    public int month;
    public ArrayList dayList;                   
    // Constructor that takes no arguments:
    public Month()
    {
        this.dayList = new ArrayList();
    }

    // Constructor that takes one argument:
    public Month(int month)
    {
        this.month = month;
        this.dayList = new ArrayList();
    }


    public void addDay(Day dayobj)
    {
        dayList.Add(dayobj);
    }

    public void printInfo()
    {
        for(int i = 0 ; i  < dayList.Count; i++)
        {
            Console.WriteLine ("dayList["+i+"] = ");
            ((Day)dayList[i]).printInfo();
        }
    }

}
