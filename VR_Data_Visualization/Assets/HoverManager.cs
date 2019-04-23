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
		muteAll();
		if(current_year == 2018){
			drawOneYear(2018, current_month);
			drawOneYear(2017, current_month);
			drawOneYear(2016, current_month);
		}else if(current_year == 2017){
			drawOneYear(2018, current_month);
			drawOneYear(2017, current_month);
			drawOneYear(2016, current_month);
			drawOneYear(2015, current_month);
		}else if(current_year == 2006){
			drawOneYear(2008, current_month);
			drawOneYear(2007, current_month);
			drawOneYear(2006, current_month);
			drawOneYear(2005, current_month);
		}else if(current_year == 2005){
			drawOneYear(2007, current_month);
			drawOneYear(2006, current_month);
			drawOneYear(2005, current_month);
		}else{
			drawOneYear(current_year + 2, current_month);
			drawOneYear(current_year + 1, current_month);
			drawOneYear(current_year, current_month);
			drawOneYear(current_year - 1, current_month);
			drawOneYear(current_year - 2, current_month);
		}

		// years[y].months[m].day_list[day].daily_hover_obj.SetActive(true);
	
		// int after_buffer = 0;
		// int d = current_day - 1;
		// int m = current_month - 1;
		// int y = current_year - 2005;
		// for(int i = 0; i < 15; ++i){
		// 	int day = d + i - after_buffer;
		// 	years[y].months[m].day_list[day].daily_hover_obj.SetActive(true);
		// 	if(m < 11 && day + 1 == years[y].months[m].day_list.Count){ // if this month is not december, at today is the last of this month
		// 		m += 1; // for next point, add 1 to month
		// 		d = 0; // set d to 0;
		// 		after_buffer = i; // day would be 0 + i - after_buffer, which will count from 0.
		// 	}else if( m == 11 && day + 1 == years[y].months[m].day_list.Count){ // if today is Dec, 31st
		// 		if(y != 13)// if this year is not 2018, which is the last year in the list
		// 		{
		// 			y += 1;
		// 			m = 0;
		// 			d = 0;
		// 			after_buffer = i;
		// 		}else{
		// 			break; // break if this is the last day in the calendar
		// 		}
		// 	}
		// }
		// int before_buffer = 0;
	}

	public void muteAll(){
		for(int y = 0; y < year_num; ++y){
			for(int m = 0; m < 12; ++m){
				for(int d = 0; d < years[y].months[m].day_list.Count;++d){
					years[y].months[m].day_list[d].daily_hover_obj.SetActive(false);
				}
				years[y].months[m].should_draw = false;
			}
		}
	}

	public void drawOneYear(int current_year, int current_month){
		if(current_year == 2018){
			if(current_month == 12){
				for(int d = 0; d < years[13].months[11].day_list.Count; ++d){
					years[13].months[11].day_list[d].daily_hover_obj.SetActive(true);
				}
				years[13].months[11].should_draw = true;				
				for(int d = 0; d < years[13].months[10].day_list.Count; ++d){
					years[13].months[10].day_list[d].daily_hover_obj.SetActive(true);
				}
				years[13].months[10].should_draw = true;
			}else if(current_month == 1){
				for(int d = 0; d < years[current_year - 2005].months[0].day_list.Count; ++d){
					years[current_year - 2005].months[0].day_list[d].daily_hover_obj.SetActive(true);
				}
				years[13].months[0].should_draw = true;				
				for(int d = 0; d < years[current_year - 2005].months[1].day_list.Count; ++d){
					years[current_year - 2005].months[1].day_list[d].daily_hover_obj.SetActive(true);
				}
				years[13].months[1].should_draw = true;
				for(int d = 0; d < years[current_year - 2005 - 1].months[11].day_list.Count; ++d){
					years[current_year - 2005 - 1].months[11].day_list[d].daily_hover_obj.SetActive(true);
				}
				years[12].months[11].should_draw = true;
			}else{
				for(int d = 0; d < years[current_year - 2005].months[current_month - 1].day_list.Count; ++d){
					years[current_year - 2005].months[current_month - 1].day_list[d].daily_hover_obj.SetActive(true);
				}
				years[current_year - 2005].months[current_month - 1].should_draw = true;				
				for(int d = 0; d < years[current_year - 2005].months[current_month].day_list.Count; ++d){
					years[current_year - 2005].months[current_month].day_list[d].daily_hover_obj.SetActive(true);
				}
				years[current_year - 2005].months[current_month].should_draw = true;
				for(int d = 0; d < years[current_year - 2005].months[current_month - 2].day_list.Count; ++d){
					years[current_year - 2005].months[current_month - 2].day_list[d].daily_hover_obj.SetActive(true);
				}
				years[current_year - 2005].months[current_month - 2].should_draw = true;
			}
		}else if(current_year == 2005){
			if(current_month == 1){
				for(int d = 0; d < years[0].months[0].day_list.Count; ++d){
					years[0].months[0].day_list[d].daily_hover_obj.SetActive(true);
				}
				years[0].months[0].should_draw = true;				
				for(int d = 0; d < years[0].months[1].day_list.Count; ++d){
					years[0].months[1].day_list[d].daily_hover_obj.SetActive(true);
				}
				years[0].months[1].should_draw = true;
			}else if(current_month == 12){
				for(int d = 0; d < years[current_year - 2005].months[11].day_list.Count; ++d){
					years[current_year - 2005].months[11].day_list[d].daily_hover_obj.SetActive(true);
				}
				years[current_year - 2005].months[11].should_draw = true;				
				for(int d = 0; d < years[current_year - 2005].months[10].day_list.Count; ++d){
					years[current_year - 2005].months[10].day_list[d].daily_hover_obj.SetActive(true);
				}
				years[current_year - 2005].months[10].should_draw = true;
				for(int d = 0; d < years[current_year - 2005 + 1].months[0].day_list.Count; ++d){
					years[current_year - 2005 + 1].months[0].day_list[d].daily_hover_obj.SetActive(true);
				}
				years[current_year - 2005 + 1].months[0].should_draw = true;
			}else{
				for(int d = 0; d < years[current_year - 2005].months[current_month - 1].day_list.Count; ++d){
					years[current_year - 2005].months[current_month - 1].day_list[d].daily_hover_obj.SetActive(true);
				}
				years[current_year - 2005].months[current_month - 1].should_draw = true;				
				for(int d = 0; d < years[current_year - 2005].months[current_month].day_list.Count; ++d){
					years[current_year - 2005].months[current_month].day_list[d].daily_hover_obj.SetActive(true);
				}
				years[current_year - 2005].months[current_month].should_draw = true;
				for(int d = 0; d < years[current_year - 2005].months[current_month - 2].day_list.Count; ++d){
					years[current_year - 2005].months[current_month - 2].day_list[d].daily_hover_obj.SetActive(true);
				}
				years[current_year - 2005].months[current_month - 2].should_draw = true;
			}
		}else{
			if(current_month == 12){
				for(int d = 0; d < years[current_year - 2005].months[11].day_list.Count; ++d){
					years[current_year - 2005].months[11].day_list[d].daily_hover_obj.SetActive(true);
				}
				years[current_year - 2005].months[11].should_draw = true;				
				for(int d = 0; d < years[current_year - 2005].months[10].day_list.Count; ++d){
					years[current_year - 2005].months[10].day_list[d].daily_hover_obj.SetActive(true);
				}
				years[current_year - 2005].months[10].should_draw = true;
				for(int d = 0; d < years[current_year - 2005 + 1].months[0].day_list.Count; ++d){
					years[current_year - 2005 + 1].months[0].day_list[d].daily_hover_obj.SetActive(true);
				}
				years[current_year - 2005 + 1].months[0].should_draw = true;
			}else if(current_month == 1){
				for(int d = 0; d < years[current_year - 2005].months[0].day_list.Count; ++d){
					years[current_year - 2005].months[0].day_list[d].daily_hover_obj.SetActive(true);
				}	
				years[current_year - 2005].months[0].should_draw = true;			
				for(int d = 0; d < years[current_year - 2005].months[1].day_list.Count; ++d){
					years[current_year - 2005].months[1].day_list[d].daily_hover_obj.SetActive(true);
				}
				years[current_year - 2005].months[1].should_draw = true;
				for(int d = 0; d < years[current_year - 2005 - 1].months[11].day_list.Count; ++d){
					years[current_year - 2005 - 1].months[11].day_list[d].daily_hover_obj.SetActive(true);
				}
				years[current_year - 2005 - 1].months[11].should_draw = true;
			}else{
				for(int d = 0; d < years[current_year - 2005].months[current_month - 1].day_list.Count; ++d){
					years[current_year - 2005].months[current_month - 1].day_list[d].daily_hover_obj.SetActive(true);
				}
				years[current_year - 2005].months[current_month - 1].should_draw = true;				
				for(int d = 0; d < years[current_year - 2005].months[current_month].day_list.Count; ++d){
					years[current_year - 2005].months[current_month].day_list[d].daily_hover_obj.SetActive(true);
				}
				years[current_year - 2005].months[current_month].should_draw = true;
				for(int d = 0; d < years[current_year - 2005].months[current_month - 2].day_list.Count; ++d){
					years[current_year - 2005].months[current_month - 2].day_list[d].daily_hover_obj.SetActive(true);
				}
				years[current_year - 2005].months[current_month - 2].should_draw = true;
			}
		}
	}
}
