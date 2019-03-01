using System;

using static Month;

public class Year
{
    public int year;
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


    public void printInfo()
    {
        for(int i = 0; i < 12 ; i ++)
        {
            Console.WriteLine ("MonthObjs["+i+"] = ");
            MonthObjs[i].printInfo();
        }
    }

}
