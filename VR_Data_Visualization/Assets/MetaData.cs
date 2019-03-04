using System;
using UnityEngine;

public class MetaData
{
    public int check_out_times;
    public Vector3 position;
    // Constructor that takes no arguments:
    public MetaData()
    {
        this.check_out_times = 0;
        this.position = new Vector3();
    }

    // Constructor that takes argument:
    public MetaData(int check_out_times, Vector3 position)
    {
        this.check_out_times = check_out_times;
        this.position = position;
    }


    public void printInfo()
    {
    }

}
