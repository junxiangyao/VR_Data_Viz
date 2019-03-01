using System;

using static Movie;

public class DataManager
{
    public int startYear;
    public int startMonth;
    public Movie[] MovieObjs;
    // Constructor that takes no arguments:
    public DataManager()
    {
        startYear = 2005;
        startMonth = 1;
        MovieObjs = new Movie[12];
        for(int mv = 0 ; mv<MovieObjs.Length;mv++)
        {
            MovieObjs[mv] = new Movie(); 
            for(int y = 0 ; y<MovieObjs[mv].YearObjs.Length;y++)
            {
                MovieObjs[mv].YearObjs[y] = new Year(startYear+y);
                for(int m = 0; m<12;m++)
                {
                    MovieObjs[mv].YearObjs[y].MonthObjs[m] = new Month(1+m);

                    //for(int d = 0 ; d<5; d++)
                    //{
                    //    years[y].MonthObjs[m].addDay(new Day(2+3*y+d%3,new float[3]{1.2f,2.3f,3.4f}));
                    //}
                }
            }
        }
        Console.WriteLine ("Movie and Month structure created");
    }
    // Constructor that takes one argument:
    //public DataManager(int year)
    //{
    //this.year = year;
    //MovieObjs = new Movie[14];
    //}

    //this function assumes the data is linearly in terms of date
    public void addData(int mv, int y, int m, int borrows, float[] position)
    {
        MovieObjs[mv].YearObjs[y-startYear].MonthObjs[m-startMonth].addDay(new Day(borrows,position));
    }

    public MetaData getData(int mv, int y, int m, int d)
    {
        return ((Day)(MovieObjs[mv].YearObjs[y-startYear].MonthObjs[m-startMonth].dayList[d-1])).data;
    }

    public void printInfo()
    {
        for(int i = 0; i < MovieObjs.Length; i ++)
        {
            Console.WriteLine ("MonthObjs["+i+"] = ");
            MovieObjs[i].printInfo();
        }
    }

}
