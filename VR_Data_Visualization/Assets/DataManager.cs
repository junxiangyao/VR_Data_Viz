using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

using static Movie;

public class DataManager
{
    public int startYear;
    public int startMonth;
    public Movie[] MovieObjs;
    public int [] DAYS_IN_MONTH = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};
    public Vector3 position = new Vector3();
    public Material line_material = new Material(Shader.Find("Sprites/Default"));
    public static float color_converter = 1.0f/255;
    public static int NUMBER_OF_MOVIES = 12;
    public static int NUMBER_OF_YEARS = 14;



    public Color [] movie_colors = {

      new Color(255, 0, 0), 
      new Color(255 * 1.0f/255, 83 * 1.0f/255, 83 * 1.0f/255), 
      new Color(255 * 1.0f/255, 150 * 1.0f/255, 150 * 1.0f/255), 

      new Color(179 * 1.0f/255, 211 * 1.0f/255, 251 * 1.0f/255), 
      //color(25, 191, 231),
      //color(127, 139, 253),
      new Color(0 * 1.0f/255, 113 * 1.0f/255, 255 * 1.0f/255), 
      new Color(111 * 1.0f/255, 82 * 1.0f/255, 198 * 1.0f/255), 

      new Color(168 * 1.0f/255, 204 * 1.0f/255, 26 * 1.0f/255), 
      new Color(119 * 1.0f/255, 138 * 1.0f/255, 45 * 1.0f/255), 

      new Color(255 * 1.0f/255, 92 * 1.0f/255, 161 * 1.0f/255), 
      new Color(163 * 1.0f/255, 37 * 1.0f/255, 235 * 1.0f/255), 


      new Color(255,255,255), 

      new Color(209 * 1.0f/255, 192 * 1.0f/255, 165 * 1.0f/255)

    }; 



    public bool [] show_movies = new bool[NUMBER_OF_MOVIES];
    public bool [] show_years = new bool[NUMBER_OF_YEARS];
    public bool [] show_months = new bool[12];

    // Constructor that takes no arguments:
    public DataManager()
    {
        startYear = 2005;
        startMonth = 1;
        MovieObjs = new Movie[NUMBER_OF_MOVIES];
        for(int i = 0; i < NUMBER_OF_MOVIES; ++i)
        {
            show_movies[i] = true;
        }
        show_movies[10] = false;
        show_movies[11] = false;
        for(int i = 0; i < NUMBER_OF_YEARS; ++i)
        {
            show_years[i] = true;
        }
        for(int i = 0; i < 12; ++i)
        {
            show_months[i] = true;
        }
        for(int mv = 0 ; mv < MovieObjs.Length;mv++)
        {
            MovieObjs[mv] = new Movie(NUMBER_OF_YEARS); 
            for(int y = 0 ; y<MovieObjs[mv].years.Length;y++)
            {
                MovieObjs[mv].years[y] = new Year(startYear+y);
                for(int m = 0; m < 12; m++)
                {
                    if((y == 3 || y == 7 || y == 11)&& m == 1){
                        //leap year
                        MovieObjs[mv].years[y].MonthObjs[m] = new Month(1+m, 29);
                    }else{
                        MovieObjs[mv].years[y].MonthObjs[m] = new Month(1+m, DAYS_IN_MONTH[m]);
                    }

                    //for(int d = 0 ; d<5; d++)
                    //{
                    //    years[y].MonthObjs[m].addDay(new Day(2+3*y+d%3,new float[3]{1.2f,2.3f,3.4f}));
                    //}
                }
            }
        }
        Debug.Log("Movie and Month structure created");
    }

    //this function assumes the data is linearly in terms of date??
    //input the current radius of the point about to be drawn, and generate x and z based on it with y based on the check_out_data 
    public void addData(int mv, int y, int m, int d, int check_out_times, float current_radius)
    {   
        // the int m in the arguments is actually starts from 1, not 0, since there is no month 0 in real life.
        // int d is also starts from 1, not 0
        // current_radius = 3.0f;
        float point_height =  (float)check_out_times / 16.0f;
        if(mv == 10 || mv == 11){
            point_height /= 3.0f; 
        }

        // float point_height =  (float)check_out_times  * 1.7f/8;
        // if(mv == 8 || mv == 11){
        //     point_height /= 3.0f; 
        // }
        position = new Vector3(current_radius * Mathf.Sin(Mathf.PI / (6 * DAYS_IN_MONTH[m-startMonth]) * (d-1) + (m-startMonth) * Mathf.PI / 6), 
                      point_height, // 1.7 m is the average height in exhibition spatial design
        current_radius * Mathf.Cos(Mathf.PI / (6 * DAYS_IN_MONTH[m-startMonth]) * (d-1) + (m-startMonth) * Mathf.PI / 6));
  
        // day hasn't been implemented yet. considering add a day_count and month buffer in the main script.
        MovieObjs[mv].years[y-startYear].MonthObjs[m-startMonth].addDay(new Day(check_out_times,position));
    }

    public MetaData getData(int mv, int y, int m, int d)
    {
        return MovieObjs[mv].years[y-startYear].MonthObjs[m-startMonth].dayList[d-1].data;
    }

    public void drawData()
    {
        for(int mv = 0; mv < 12; ++mv){
            // draw main data line
            // if(mv==8){continue;}
            for(int y = 0; y < 14; ++y){
                for(int m = 0; m < 12; ++m){
                    //generate all the lines 
                    MovieObjs[mv].years[y].MonthObjs[m].drawData(movie_colors[mv],line_material);
                    //set parent
                    MovieObjs[mv].years[y].MonthObjs[m].month_data_line.transform.SetParent(MovieObjs[mv].years[y].year_game_object.transform, true);
                    //for the first 11 months, draw connectors that connect to next month
                    if(m < 11){
                        MovieObjs[mv].years[y].MonthObjs[m].connectToNext(movie_colors[mv],line_material, MovieObjs[mv].years[y].MonthObjs[m+1].dayList[0].data.position);
                        MovieObjs[mv].years[y].MonthObjs[m].connection_to_next.transform.SetParent(MovieObjs[mv].years[y].year_game_object.transform, true);
                    }
                }
                if(y < 13){
                    //for the last month, connect to next year
                    MovieObjs[mv].years[y].MonthObjs[11].connectToNext(movie_colors[mv],line_material, MovieObjs[mv].years[y+1].MonthObjs[0].dayList[0].data.position);
                    MovieObjs[mv].years[y].MonthObjs[11].connection_to_next.transform.SetParent(MovieObjs[mv].years[y].year_game_object.transform, true);
                }

                MovieObjs[mv].years[y].year_game_object.transform.SetParent(MovieObjs[mv].game_object.transform, true);
            }
        }
    }


    public void drawDate()
    {
        for(int mv = 0; mv < NUMBER_OF_MOVIES; ++mv)
        // for(int mv = 0; mv < 1; ++mv)
        {
            if(mv==8||mv==11){continue;}
            for(int m = 0; m < 12; ++m)
            {
                MovieObjs[mv].date_lines_month[m] = new GameObject();
                for(int d = 0; d < DAYS_IN_MONTH[m]; ++d)
                {
                    List<Vector3> day_buffer = new List<Vector3>();
                    for(int y = 0; y < NUMBER_OF_YEARS; ++y)
                    {
                        if(show_years[y])
                        {
                            day_buffer.Add(MovieObjs[mv].years[y].MonthObjs[m].dayList[d].data.position);
                        }
                    }
                    MovieObjs[mv].drawDateLines(movie_colors[mv], line_material, m, day_buffer);
                }
            }
        }

    }

    public String printInfo()
    {
        //buffer
        StringBuilder sb = new StringBuilder();

        for(int i = 0 ; i  < MovieObjs.Length; i++)
        {
            sb.Append("dayList["+i+"] = "+MovieObjs[i].printInfo()+"\n");
        }

        return sb.ToString();
    }

}
