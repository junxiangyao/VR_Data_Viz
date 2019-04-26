using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HoverObject;

public class HoverDay : MonoBehaviour
{
	public Vector3 position;
	public List<HoverObject> data_list;
	public HoverObject news;
	public GameObject daily_hover_obj;

	public HoverDay(Vector3 pos)
    {
    	this.position = new Vector3(pos.x, 0, pos.z);
        this.data_list = new List<HoverObject>();
        this.daily_hover_obj = new GameObject();
        // this.news = new HoverObject();
    }	
    public HoverDay()
    {
        this.data_list = new List<HoverObject>();
        this.daily_hover_obj = new GameObject();
        // this.news = new HoverObject();
    }

    public void addMovie(Color c, int checkOut, int movie_index, Vector3 pos, int y_, int m_, int d_)
    {
    	bool is_new = true;
    	if(checkOut > 0){
	    	if(data_list.Count > 0){ // if there is already data exist inside the list
	        	for(int i = 0; i < data_list.Count; ++i){
	        		if(data_list[i].check_out == checkOut){ // check if the upcomming data is redandent, because only one data box should be rendered at a cirtain place. Different movies with same check out time should share one data box.
	        			// add movie index
	        			data_list[i].addMovie(movie_index);
	        			is_new = false;
	        			break;
	        		}
	        	}
	        }

	        if(is_new){
	        	data_list.Add(new HoverObject(c, movie_index, checkOut, pos, y_, m_, d_, data_list.Count));
	        }
    	}
    }

    public void drawHoverObjs(){
    	if(data_list.Count > 0){
    		for(int i = 0; i < data_list.Count; ++i){
    			data_list[i].drawCube();
    			data_list[i].hover_obj.transform.SetParent(daily_hover_obj.transform);
    			// data_list[i].hover_obj.tag = "data_node";
    		}
    	}
    }
}
