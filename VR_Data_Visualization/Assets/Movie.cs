using System;

using static Year;

public class Movie
{
    public Year[] YearObjs;
    // Constructor that takes no arguments:
    public Movie()
    {
        YearObjs = new Year[14];
    }

    // Constructor that takes one argument:
    //public Movie(int year)
    //{
    //this.year = year;
    //YearObjs = new Year[14];
    //}




    public void printInfo()
    {
        for(int i = 0; i < YearObjs.Length; i ++)
        {
            Console.WriteLine ("YearObjs["+i+"] = ");
            YearObjs[i].printInfo();
        }
    }

}
