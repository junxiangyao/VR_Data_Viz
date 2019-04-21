using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HoverDay;

public class HoverMonth
{
	public List<HoverDay> day_list; // a list of day objects containing data from that day  
	public HoverMonth()
    {
        this.day_list = new List<HoverDay>();
    }

    public void addDay(HoverDay day_obj)
    {
        day_list.Add(day_obj);
    }
}
