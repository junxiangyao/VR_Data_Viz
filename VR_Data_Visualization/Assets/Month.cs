using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

using static Day;

public class Month
{
    public int month_order; // 
    public List<Day> dayList;  
    public MetaData data;
    public int day_count;
    public GameObject month_data_line;  
    public GameObject connection_to_next;             
    // Constructor that takes no arguments:
    public Month()
    {
        this.dayList = new List<Day>();
        this.data = new MetaData();
    }

    // Constructor that takes one argument:
    public Month(int month_order, int day_count)
    {
        this.month_order = month_order;
        this.dayList = new List<Day>();
        this.data = new MetaData();
        this.day_count = day_count;
        this.month_data_line = new GameObject();
        this.connection_to_next = new GameObject();
    }


    public void addDay(Day day_obj)
    {
        dayList.Add(day_obj);
    }

    public void drawData(Color c, Material material){
        LineRenderer line_renderer = month_data_line.AddComponent<LineRenderer>();
        line_renderer.material = material;
        line_renderer.widthMultiplier = 0.005f;
        line_renderer.positionCount = dayList.Count;
        line_renderer.SetColors(c, c);
        for(int i = 0; i < this.dayList.Count; ++i){
            line_renderer.SetPosition(i, this.dayList[i].data.position);
        }
    }

    public void connectToNext(Color c, Material material){
        LineRenderer line_renderer = connection_to_next.AddComponent<LineRenderer>();
        line_renderer.material = material;
        line_renderer.widthMultiplier = 0.01f;
        line_renderer.positionCount = 2;
        line_renderer.SetColors(c, c);
    }

    public String printInfo()
    {  
        //buffer
        StringBuilder sb = new StringBuilder();

        for(int i = 0 ; i  < dayList.Count; i++)
        {
            sb.Append("dayList["+i+"] = "+dayList[i].printInfo()+"\n");
        }
        return sb.ToString();
    }

}
