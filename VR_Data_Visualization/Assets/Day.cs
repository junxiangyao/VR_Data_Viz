using System;

using static MetaData;

public class Day
{
    public MetaData data;
    // Constructor that takes no arguments:
    public Day()
    {
        data = new MetaData();
    }

    // Constructor that takes one argument:
    public Day(int borrows,float[] position)
    {
        data = new MetaData(borrows,position);
    }


    public void printInfo()
    {
        Console.WriteLine ("DataObj: borrows = "+data.borrows+" pos = "+data.position[0]+", "+data.position[1]+", "+data.position[2]);

    }

}
