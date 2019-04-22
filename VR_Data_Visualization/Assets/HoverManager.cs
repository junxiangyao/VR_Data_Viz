using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HoverYear;

public class HoverManager
{
	public HoverYear[] years;
	public int year_num;

	public HoverManager(int num){
		this.years = new HoverYear[num];
		this.year_num = num;
		for(int i = 0; i < num; ++i){
			this.years[i] = new HoverYear(i);
		}
	}

	public void unmute(int current_year, int current_month, int current_day){
		// for(int y = 0; y < year_num; ++i){

		// }
		for(int y = 0; y < year_num; ++y){
			for(int m = 0; m < 12; ++m){
				for(int d = 0; d < years[y].months[m].day_list.Count;++d){
					years[y].months[m].day_list[d].daily_hover_obj.SetActive(false);
				}
			}
		}
		years[current_year-2005].months[current_month-1].day_list[current_day-1].daily_hover_obj.SetActive(true);
	}

	public void mute(int current_year, int current_month, int current_day){
		// for(int y = 0; y < year_num; ++i){
		years[current_year-2005].months[current_month-1].day_list[current_day-1].daily_hover_obj.SetActive(false);
		// }
	}

}
