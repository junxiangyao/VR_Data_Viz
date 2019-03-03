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

    // Constructor that takes arguments:
    public Day(int check_out_times, float[] position)
    {
        data = new MetaData(check_out_times, position);
    }


    public String printInfo()
    {
        return "check_out_times = "+data.check_out_times+" position = "+data.position[0]+", "+data.position[1]+", "+data.position[2];

    }

}
