using System;

public class MetaData
{
    public int borrows;
    public float[] position;
    // Constructor that takes no arguments:
    public MetaData()
    {
        borrows = 0;
        position = new float[3];
    }

    // Constructor that takes one argument:
    public MetaData(int borrows, float[] position)
    {
        this.borrows = borrows;
        this.position = position;
    }


    public void printInfo()
    {
    }

}
