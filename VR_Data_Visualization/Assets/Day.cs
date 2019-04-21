using System;
using UnityEngine;

using static MetaData;

public class Day
{
    public MetaData data;
    // Constructor that takes no arguments:
    public Day()
    {
        this.data = new MetaData();
    }

    // Constructor that takes arguments:
    public Day(int check_out_times, Vector3 position, float radius, float angle)
    {
        this.data = new MetaData(check_out_times, position, radius, angle);
    }


    public String printInfo()
    {
        return "check_out_times = "+data.check_out_times+" position = "+data.position[0]+", "+data.position[1]+", "+data.position[2];

    }

}
