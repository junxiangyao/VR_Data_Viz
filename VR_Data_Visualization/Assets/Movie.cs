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
    public GameObject[] date_lines_month;
    public float LINE_WIDTH = 0.002f;  

    // Constructor that takes no arguments:
    public Movie(int num)
    {
        years = new Year[num];
        game_object = new GameObject();
        date_lines_month = new GameObject[12];
        for(int m = 0; m < 12; ++m){
            date_lines_month[m] = new GameObject();
        }
    }

    public void drawDateLines(Color c, Material material, int m, List<Vector3> nodes){
        GameObject date_line_day = new GameObject();
        LineRenderer line_renderer = date_line_day.AddComponent<LineRenderer>();
        line_renderer.material = material;
        line_renderer.widthMultiplier = LINE_WIDTH;
        line_renderer.positionCount = nodes.Count;
        line_renderer.startColor = c;
        line_renderer.endColor = c;
        date_line_day.transform.SetParent(date_lines_month[m].transform, true);
        // line_renderer.SetPositions(nodes);

        for(int i = 0; i < nodes.Count; ++i){
            line_renderer.SetPosition(i, nodes[i]);
        }
        Debug.Log(":" + line_renderer.positionCount);
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
