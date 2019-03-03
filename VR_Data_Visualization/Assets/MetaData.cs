using System;

public class MetaData
{
    public int check_out_times;
    public float[] position;
    // Constructor that takes no arguments:
    public MetaData()
    {
        check_out_times = 0;
        position = new float[3];
    }

    // Constructor that takes argument:
    public MetaData(int check_out_times, float[] position)
    {
        this.check_out_times = check_out_times;
        this.position = position;
    }


    public void printInfo()
    {
    }

}
