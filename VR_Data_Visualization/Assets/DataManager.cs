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
    public float color_converter = 1.0f/255;
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
      new Color(255,255,255), 

      new Color(255 * 1.0f/255, 92 * 1.0f/255, 161 * 1.0f/255), 
      new Color(163 * 1.0f/255, 37 * 1.0f/255, 235 * 1.0f/255), 


      new Color(209 * 1.0f/255, 192 * 1.0f/255, 165 * 1.0f/255)

    }; 
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
                for(int m = 0; m < 12; m++)
                {
                    if((y == 3 || y == 7 || y == 11)&& m == 1){
                        //leap year
                        MovieObjs[mv].YearObjs[y].MonthObjs[m] = new Month(1+m, 29);
                    }else{
                        MovieObjs[mv].YearObjs[y].MonthObjs[m] = new Month(1+m, DAYS_IN_MONTH[m]);
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
        float point_height =  (float)check_out_times * 1.7f / 7.0f;
        if(mv == 8 || mv == 11){
            point_height /= 5.0f; 
        }
        position = new Vector3(current_radius * Mathf.Sin(Mathf.PI / (6 * DAYS_IN_MONTH[m-startMonth]) * (d-1) + (m-startMonth) * Mathf.PI / 6), 
                      point_height, // 1.7 m is the average height in exhibition spatial design
        current_radius * Mathf.Cos(Mathf.PI / (6 * DAYS_IN_MONTH[m-startMonth]) * (d-1) + (m-startMonth) * Mathf.PI / 6));
  
        // day hasn't been implemented yet. considering add a day_count and month buffer in the main script.
        MovieObjs[mv].YearObjs[y-startYear].MonthObjs[m-startMonth].addDay(new Day(check_out_times,position));
    }

    public MetaData getData(int mv, int y, int m, int d)
    {
        return MovieObjs[mv].YearObjs[y-startYear].MonthObjs[m-startMonth].dayList[d-1].data;
    }

    public void drawData(){
        for(int mv = 0; mv < 6; ++mv){
            for(int y = 0; y < 14; ++y){
                for(int m = 0; m < 12; ++m){
                    MovieObjs[mv].YearObjs[y].MonthObjs[m].drawData(movie_colors[mv],line_material);
                    //connect to next month
                    if(m < 11){
                        MovieObjs[mv].YearObjs[y].MonthObjs[m].connectToNext(movie_colors[mv],line_material, MovieObjs[mv].YearObjs[y].MonthObjs[m+1].dayList[0].data.position);
                    }
                }
                if(y < 13){
                    //connect to next year
                    MovieObjs[mv].YearObjs[y].MonthObjs[11].connectToNext(movie_colors[mv],line_material, MovieObjs[mv].YearObjs[y+1].MonthObjs[0].dayList[0].data.position);
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
