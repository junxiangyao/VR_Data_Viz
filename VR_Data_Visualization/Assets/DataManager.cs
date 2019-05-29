using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

using static Movie;
using static Coordinate;

public class DataManager
{
    public int startYear;
    public int startMonth;
    public Movie[] MovieObjs;
    public int [] DAYS_IN_MONTH = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};
    public Vector3 position = new Vector3();
    public Vector3 wall_position = new Vector3();
    public Material line_material = new Material(Shader.Find("Sprites/Default"));
    public Material mesh_material = new Material(Shader.Find("Custom/Standard2Sided"));
    public static float color_converter = 1.0f/255;
    public static int NUMBER_OF_MOVIES = 12;
    public static int NUMBER_OF_YEARS = 14;

    public GameObject mini_object;
    public GameObject main_object;



    public Color [] movie_colors = {

      new Color(255 * 1.0f/255, 0, 0), 
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


      new Color(255 * 1.0f/255, 255 * 1.0f/255, 255 * 1.0f/255), 

      new Color(209 * 1.0f/255, 192 * 1.0f/255, 165 * 1.0f/255)

    }; 



    public bool [] show_movies = new bool[NUMBER_OF_MOVIES];
    public bool [] show_years = new bool[NUMBER_OF_YEARS];
    public bool [] show_months = new bool[12];
    public bool show_wall = false;
    public bool show_wall_mini = true;
    public bool show_date_lines = true;
    public bool show_month_mesh = false;

    // Constructor that takes no arguments:
    public DataManager()
    {
        this.startYear = 2005;
        this.startMonth = 1;
        this.MovieObjs = new Movie[NUMBER_OF_MOVIES];

        this.mini_object = new GameObject();
        // this.main_object = new GameObject();

        for(int i = 0; i < NUMBER_OF_MOVIES; ++i)
        {
            this.show_movies[i] = true;
        }
        // show_movies[10] = false;
        this.show_movies[11] = false;
        for(int i = 0; i < NUMBER_OF_YEARS; ++i)
        {
            this.show_years[i] = true;
        }
        for(int i = 0; i < 12; ++i)
        {
            this.show_months[i] = true;
        }
        for(int mv = 0 ; mv < MovieObjs.Length;mv++)
        {
            this.MovieObjs[mv] = new Movie(NUMBER_OF_YEARS); 
            for(int y = 0 ; y<MovieObjs[mv].years.Length;y++)
            {
                this.MovieObjs[mv].years[y] = new Year(startYear+y);
                for(int m = 0; m < 12; m++)
                {
                    if((y == 3 || y == 7 || y == 11)&& m == 1){
                        //leap year
                        this.MovieObjs[mv].years[y].months[m] = new Month(1+m, 29);
                    }else{
                        this.MovieObjs[mv].years[y].months[m] = new Month(1+m, DAYS_IN_MONTH[m]);
                    }

                    //for(int d = 0 ; d<5; d++)
                    //{
                    //    years[y].months[m].addDay(new Day(2+3*y+d%3,new float[3]{1.2f,2.3f,3.4f}));
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
            point_height /= 2.48f; 
        }

        // float point_height =  (float)check_out_times  * 1.7f/8;
        // if(mv == 10 || mv == 11){
        //     point_height /= 3.0f; 
        // }
        position = new Vector3(current_radius * Mathf.Sin(Mathf.PI / (6 * DAYS_IN_MONTH[m-startMonth]) * (d-1) + (m-startMonth) * Mathf.PI / 6), 
                      point_height, // 1.7 m is the average height in exhibition spatial design
        current_radius * Mathf.Cos(Mathf.PI / (6 * DAYS_IN_MONTH[m-startMonth]) * (d-1) + (m-startMonth) * Mathf.PI / 6));
        wall_position = new Vector3((current_radius + mv * 0.001f) * Mathf.Sin(Mathf.PI / (6 * DAYS_IN_MONTH[m-startMonth]) * (d-1) + (m-startMonth) * Mathf.PI / 6), 
                      point_height, // 1.7 m is the average height in exhibition spatial design
        (current_radius + mv * 0.001f) * Mathf.Cos(Mathf.PI / (6 * DAYS_IN_MONTH[m-startMonth]) * (d-1) + (m-startMonth) * Mathf.PI / 6));
  
        // day hasn't been implemented yet. considering add a day_count and month buffer in the main script.
        MovieObjs[mv].years[y-startYear].months[m-startMonth].addDay(new Day(check_out_times,position, wall_position, current_radius, 
            // Mathf.PI));
            Mathf.PI / (6 * DAYS_IN_MONTH[m-startMonth]) * (d-1) + (m-startMonth) * Mathf.PI / 6));
    }

    public void addMonthData(int mv, int y, int m, int check_out_times){ // Call this after calling addData
        MovieObjs[mv].years[y-startYear].months[m-startMonth].addMonthData(check_out_times);
    }

    public MetaData getData(int mv, int y, int m, int d)
    {
        return MovieObjs[mv].years[y-startYear].months[m-startMonth].dayList[d-1].data;
    }

    public void drawData()
    {
        for(int mv = 0; mv < 12; ++mv){
            // draw main data line
            // if(mv==11||mv==10){continue;}
            for(int y = 0; y < 14; ++y){

            // optimization
                if(mv == 6){
                    if(y < 9){
                        continue;
                    }
                }else if(mv == 7){
                    if(y < 11){
                        continue;
                    }
                }else if(mv == 8){
                    if(y < 10){
                        continue;
                    }
                }else if(mv == 9){
                    if(y < 12){
                        continue;
                    }
                } 
                for(int m = 0; m < 12; ++m){
                    //generate all the lines 
                    MovieObjs[mv].years[y].months[m].drawData(movie_colors[mv],line_material);
                    MovieObjs[mv].years[y].months[m].drawDataWall(movie_colors[mv]);
                    //set parent
                    MovieObjs[mv].years[y].months[m].month_data_line.transform.SetParent(MovieObjs[mv].years[y].year_game_object.transform, true);
                    MovieObjs[mv].years[y].months[m].month_data_wall.transform.SetParent(MovieObjs[mv].years[y].year_game_object.transform, true);
                    //for the first 11 months, draw connectors that connect to next month
                    if(m < 11){
                        MovieObjs[mv].years[y].months[m].connectToNext(movie_colors[mv],line_material, MovieObjs[mv].years[y].months[m+1].dayList[0].data.position);
                        MovieObjs[mv].years[y].months[m].connection_to_next.transform.SetParent(MovieObjs[mv].years[y].year_game_object.transform, true);
                        MovieObjs[mv].years[y].months[m].connectWall(movie_colors[mv],MovieObjs[mv].years[y].months[m+1].dayList[0].data.wall_position);
                        MovieObjs[mv].years[y].months[m].connection_to_next_wall.transform.SetParent(MovieObjs[mv].years[y].year_game_object.transform, true);
                    }  
                    if(m==11 && y < 13){
                        //for the last month, connect to next year
                        MovieObjs[mv].years[y].months[11].connectToNext(movie_colors[mv],line_material, MovieObjs[mv].years[y+1].months[0].dayList[0].data.position);
                        MovieObjs[mv].years[y].months[11].connection_to_next.transform.SetParent(MovieObjs[mv].years[y].year_game_object.transform, true);
                        MovieObjs[mv].years[y].months[11].connectWall(movie_colors[mv],MovieObjs[mv].years[y+1].months[0].dayList[0].data.wall_position);
                        MovieObjs[mv].years[y].months[11].connection_to_next_wall.transform.SetParent(MovieObjs[mv].years[y].year_game_object.transform, true);
                    }
                }


                MovieObjs[mv].years[y].year_game_object.transform.SetParent(MovieObjs[mv].game_object.transform, true);
            }

            for(int m = 0; m < 12; ++m){
                    //date lines set parent to movie objs
                
            }
        }
        // MovieObjs[mv].game_object.transform.SetParent(this.main_object.transform, false);
    }

    public void drawMonthDataMini(){
        for(int mv = 0; mv < 10; ++mv)
        // for(int mv = 0; mv < 1; ++mv)
        {
            List<Vector3> month_buffer = new List<Vector3>();
            for(int y = 0; y < NUMBER_OF_YEARS; ++y)
            {
                        // if(show_years[y])
                        // {

                //optimization
                if(mv == 6){
                    if(y < 9){
                        continue;
                    }
                }else if(mv == 7){
                    if(y < 11){
                        continue;
                    }
                }else if(mv == 8){
                    if(y < 10){
                        continue;
                    }
                }else if(mv == 9){
                    if(y < 12){
                        continue;
                    }
                }

                for(int m = 0; m < 12; ++m){
                    month_buffer.Add(MovieObjs[mv].years[y].months[m].data.mini_position);      
                }
                        // }
            }
            MovieObjs[mv].drawMonthDataMini(movie_colors[mv], line_material, month_buffer);
            MovieObjs[mv].mini_month_object.transform.SetParent(MovieObjs[mv].mini_game_object.transform, true);
            // counter++;
        }
        

    }


    public void drawDate()
    {
        for(int mv = 0; mv < NUMBER_OF_MOVIES; ++mv)
        // for(int mv = 0; mv < 1; ++mv)
        {
            // if(mv==10||mv==11){continue;}
            int counter = 0;
            for(int m = 0; m < 12; ++m)
            {
                // MovieObjs[mv].date_lines_month[m] = new GameObject();
                for(int d = 0; d < DAYS_IN_MONTH[m]; ++d)
                {
                    List<Vector3> day_buffer = new List<Vector3>();
                    for(int y = 0; y < NUMBER_OF_YEARS; ++y)
                    {
                        // if(show_years[y])
                        // {

                        //optimization
                        if(mv == 6){
                            if(y < 9){
                                continue;
                            }
                        }else if(mv == 7){
                            if(y < 11){
                                continue;
                            }
                        }else if(mv == 8){
                            if(y < 10){
                                continue;
                            }
                        }else if(mv == 9){
                            if(y < 12){
                                continue;
                            }
                        }


                        day_buffer.Add(MovieObjs[mv].years[y].months[m].dayList[d].data.position);
                        // }
                    }
                    MovieObjs[mv].drawDateLines(movie_colors[mv], line_material, m, day_buffer,counter);
                    counter++;
                }
                MovieObjs[mv].date_lines_month[m].transform.SetParent(MovieObjs[mv].game_object.transform, true);
            }
        }

    }


    public void drawDataMini()
    {
        for(int mv = 0; mv < 12; ++mv){           
            // draw main data line
            if(mv==11||mv==10){continue;}
            for(int y = 0; y < 14; ++y){
            // optimization
                if(mv == 6){
                    if(y < 9){
                        continue;
                    }
                }else if(mv == 7){
                    if(y < 11){
                        continue;
                    }
                }else if(mv == 8){
                    if(y < 10){
                        continue;
                    }
                }else if(mv == 9){
                    if(y < 12){
                        continue;
                    }
                } 

                for(int m = 0; m < 12; ++m){
                    //generate all the lines 
                    MovieObjs[mv].years[y].months[m].drawDataMini(movie_colors[mv],line_material);
                    MovieObjs[mv].years[y].months[m].drawDataWallMini(movie_colors[mv]);
                    //set parent
                    MovieObjs[mv].years[y].months[m].mini_month_data_line.transform.SetParent(MovieObjs[mv].years[y].mini_year_game_object.transform, false);
                    MovieObjs[mv].years[y].months[m].mini_month_data_wall.transform.SetParent(MovieObjs[mv].years[y].mini_year_game_object.transform, true);
                    //for the first 11 months, draw connectors that connect to next month
                    if(m < 11){
                        MovieObjs[mv].years[y].months[m].connectToNextMini(movie_colors[mv],line_material, MovieObjs[mv].years[y].months[m+1].dayList[0].data.mini_position);
                        MovieObjs[mv].years[y].months[m].mini_connection_to_next.transform.SetParent(MovieObjs[mv].years[y].mini_year_game_object.transform, true);
                        MovieObjs[mv].years[y].months[m].connectWallMini(movie_colors[mv],MovieObjs[mv].years[y].months[m+1].dayList[0].data.mini_position);
                        MovieObjs[mv].years[y].months[m].mini_connection_to_next_wall.transform.SetParent(MovieObjs[mv].years[y].mini_year_game_object.transform, true);
                    }
                }
                if(y < 13){
                    //for the last month, connect to next year
                    MovieObjs[mv].years[y].months[11].connectToNextMini(movie_colors[mv],line_material, MovieObjs[mv].years[y+1].months[0].dayList[0].data.mini_position);
                    MovieObjs[mv].years[y].months[11].mini_connection_to_next.transform.SetParent(MovieObjs[mv].years[y].mini_year_game_object.transform, true);
                    MovieObjs[mv].years[y].months[11].connectWallMini(movie_colors[mv],MovieObjs[mv].years[y+1].months[0].dayList[0].data.mini_position);
                    MovieObjs[mv].years[y].months[11].mini_connection_to_next_wall.transform.SetParent(MovieObjs[mv].years[y].mini_year_game_object.transform, true);
                }

                MovieObjs[mv].years[y].mini_year_game_object.transform.SetParent(MovieObjs[mv].mini_game_object.transform, false);
            }
            MovieObjs[mv].mini_game_object.transform.SetParent(this.mini_object.transform, false);
        }
    }

    public void drawDateMini()
    {
        for(int mv = 0; mv < NUMBER_OF_MOVIES; ++mv)
        // for(int mv = 0; mv < 1; ++mv)
        {
            if(mv==10||mv==11){continue;}
            int counter = 0;
            for(int m = 0; m < 12; ++m)
            {
                // MovieObjs[mv].date_lines_month[m] = new GameObject();
                for(int d = 0; d < DAYS_IN_MONTH[m]; ++d)
                {
                    List<Vector3> day_buffer = new List<Vector3>();
                    for(int y = 0; y < NUMBER_OF_YEARS; ++y)
                    {
                        // optimization
                        if(mv == 6){
                            if(y < 9){
                                continue;
                            }
                        }else if(mv == 7){
                            if(y < 11){
                                continue;
                            }
                        }else if(mv == 8){
                            if(y < 10){
                                continue;
                            }
                        }else if(mv == 9){
                            if(y < 12){
                                continue;
                            }
                        }
                            day_buffer.Add(MovieObjs[mv].years[y].months[m].dayList[d].data.mini_position);
                        // }
                    }
                    MovieObjs[mv].drawDateLinesMini(movie_colors[mv], line_material, m, day_buffer,counter);
                    counter++;
                }
                MovieObjs[mv].mini_date_lines_month[m].transform.SetParent(MovieObjs[mv].mini_game_object.transform, true);
            }
        }
    }    

    public void drawMonthLineMini()
    {
        for(int mv = 0; mv < NUMBER_OF_MOVIES; ++mv)
        // for(int mv = 0; mv < 1; ++mv)
        {
            if(mv==10||mv==11){continue;}
            for(int m = 0; m < 12; ++m)
            {
                // MovieObjs[mv].date_lines_month[m] = new GameObject();
                    List<Vector3> month_buffer = new List<Vector3>();
                    for(int y = 0; y < NUMBER_OF_YEARS; ++y)
                    {
                        // optimization
                        if(mv == 6){
                            if(y < 9){
                                continue;
                            }
                        }else if(mv == 7){
                            if(y < 11){
                                continue;
                            }
                        }else if(mv == 8){
                            if(y < 10){
                                continue;
                            }
                        }else if(mv == 9){
                            if(y < 12){
                                continue;
                            }
                        }
                        month_buffer.Add(MovieObjs[mv].years[y].months[m].data.mini_position);  
                        // }
                    }

                MovieObjs[mv].drawMonthLinesMini(movie_colors[mv], line_material, month_buffer, m);
                // MovieObjs[mv].mini_month_object.transform.SetParent(MovieObjs[mv].mini_game_object.transform, true);

                // MovieObjs[mv].mini_date_lines_month[m].transform.SetParent(MovieObjs[mv].mini_game_object.transform, true);
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
