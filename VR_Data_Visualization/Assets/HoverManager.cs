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
	}

	public void unmute(int current_year, int current_month, int current_day){
		// for(int y = 0; y < year_num; ++i){

		// }
		years[current_year-2005].months[current_month-1].day_list[current_day-1].daily_hover_obj.SetActive(true);
	}

	public void mute(int current_year, int current_month, int current_day){
		// for(int y = 0; y < year_num; ++i){
		years[current_year-2005].months[current_month-1].day_list[current_day-1].daily_hover_obj.SetActive(false);
		// }
	}

}
