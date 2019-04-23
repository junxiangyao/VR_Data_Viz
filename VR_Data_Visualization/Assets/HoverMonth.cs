using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HoverDay;

public class HoverMonth
{
	public List<HoverDay> day_list; // a list of day objects containing data from that day  
	public GameObject month_hover_obj;
	public bool should_draw = false;
	public HoverMonth()
    {
        this.day_list = new List<HoverDay>();
        this.month_hover_obj = new GameObject();
    }

    public void addDay(HoverDay day_obj)
    {
        day_list.Add(day_obj);
    }

    public void set_parent(){
    	for(int i = 0; i < day_list.Count; ++i){
    		day_list[i].daily_hover_obj.transform.SetParent(month_hover_obj.transform);
    	}
    }
}
