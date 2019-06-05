using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

using static Year;

public class Movie
{
    public Year[] years;
    public GameObject game_object;
    public GameObject mini_game_object;
    public GameObject mini_month_object;
    public GameObject[] mini_month_lines;
    public GameObject[] date_lines_month;
    public float LINE_WIDTH = 0.002f; 
    public float LINE_WIDTH_MINI = 0.00012f; 
    public GameObject[] date_lines_day;
    public GameObject[] mini_date_lines_month;  
    public GameObject[] mini_date_lines_day;
    // Constructor that takes no arguments:
    public Movie(int num)
    {
        this.years = new Year[num];
        this.game_object = new GameObject();
        this.mini_game_object = new GameObject();
        this.mini_month_object = new GameObject();

        this.date_lines_month = new GameObject[12];
        this.date_lines_day = new GameObject[365];
        this.mini_month_lines = new GameObject[12];
        this.mini_date_lines_month = new GameObject[12];
        this.mini_date_lines_day = new GameObject[365];
        for(int m = 0; m < 12; ++m){
            this.date_lines_month[m] = new GameObject();
            this.mini_date_lines_month[m] = new GameObject();
            this.mini_month_lines[m] = new GameObject();
        }
        for(int d = 0; d < 365; ++d){
            this.date_lines_day[d] = new GameObject();
            this.mini_date_lines_day[d] = new GameObject();
        }
        
    }


    public void drawMonthDataMini(Color c, Material material, List<Vector3> nodes){
        LineRenderer line_renderer = mini_month_object.AddComponent<LineRenderer>();
        line_renderer.material = material;
        line_renderer.widthMultiplier = LINE_WIDTH_MINI * 5;
        line_renderer.positionCount = nodes.Count;
        line_renderer.useWorldSpace = false;
        line_renderer.startColor = c;
        line_renderer.endColor = c;
        // line_renderer.sortingOrder=1;
        for(int i = 0; i < nodes.Count; ++i){
            line_renderer.SetPosition(i, nodes[i]);
        }
    }

    public void drawMonthLinesMini(Color c, Material material, List<Vector3> nodes, int m){
        LineRenderer line_renderer = mini_month_lines[m].AddComponent<LineRenderer>();
        line_renderer.material = material;
        line_renderer.widthMultiplier = LINE_WIDTH_MINI * 6;
        line_renderer.positionCount = nodes.Count;
        line_renderer.useWorldSpace = false;
        line_renderer.startColor = c;
        line_renderer.endColor = c;
        line_renderer.sortingOrder=1;
        for(int i = 0; i < nodes.Count; ++i){
            line_renderer.SetPosition(i, nodes[i]);
        }
        this.mini_month_lines[m].transform.SetParent(this.mini_month_object.transform, true);
        // Debug.Log(":" + line_renderer.positionCount);
        
    }

    public void drawDateLines(Color c, Material material, int m, List<Vector3> nodes, int index){
        LineRenderer line_renderer = date_lines_day[index].AddComponent<LineRenderer>();
        line_renderer.material = material;
        line_renderer.widthMultiplier = LINE_WIDTH;
        line_renderer.positionCount = nodes.Count;
        line_renderer.useWorldSpace = false;
        line_renderer.startColor = c;
        line_renderer.endColor = c;
        // line_renderer.sortingOrder=1;
        for(int i = 0; i < nodes.Count; ++i){
            line_renderer.SetPosition(i, nodes[i]);
        }
        // Debug.Log(":" + line_renderer.positionCount);
        date_lines_day[index].transform.SetParent(date_lines_month[m].transform, true);
    }

    public void drawDateLinesMini(Color c, Material material, int m, List<Vector3> nodes, int index){
        LineRenderer line_renderer = mini_date_lines_day[index].AddComponent<LineRenderer>();
        line_renderer.material = material;
        line_renderer.widthMultiplier = LINE_WIDTH_MINI;
        line_renderer.positionCount = nodes.Count;
        line_renderer.useWorldSpace = false;
        line_renderer.startColor = c;
        line_renderer.endColor = c;
        line_renderer.sortingOrder=1;
        for(int i = 0; i < nodes.Count; ++i){
            line_renderer.SetPosition(i, nodes[i]);
        }
        // Debug.Log(":" + line_renderer.positionCount);
        mini_date_lines_day[index].transform.SetParent(mini_date_lines_month[m].transform, true);
    }
    public String printInfo()
    {
        //buffer
        StringBuilder sb = new StringBuilder();

        for(int i = 0 ; i  < years.Length; i++)
        {
            sb.Append("YearObjs["+i+"] = "+years[i].printInfo()+"\n");
        }
        return sb.ToString();
    }
}
