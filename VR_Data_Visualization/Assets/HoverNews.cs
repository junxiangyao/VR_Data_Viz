using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InfoCube;

public class HoverNews
{
	public List<string> news_sw;
	public List<string> news_spl;
	public int news_count = 0;
	public GameObject hover_obj;
	public Color hover_color;
	public int check_out;
	public Vector3 position;
	public int y;
	public int m;
	public int d;
	public int id = 10;
	public float angle; //degree for cube rotation 
	public HoverNews(Color c, int record, Vector3 pos, int y_, int m_, int d_, float a) 
	{
		this.hover_obj = new GameObject();
		this.hover_color = c;
		this.check_out = record;
		this.position = pos;
		this.hover_obj.SetActive(true);
		this.news_sw = new List<string>();
		this.news_spl = new List<string>();
		this.y = y_;
		this.m = m_;
		this.d = d_;
		this.angle = a; // degree
	}

	public void drawNews(){
        hover_obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        hover_obj.AddComponent<InfoCube>();
        hover_obj.GetComponent<InfoCube>().c_out = check_out;
        // hover_obj.GetComponent<InfoCube>().index = movie_index;        
        hover_obj.GetComponent<InfoCube>().yb = y;
        hover_obj.GetComponent<InfoCube>().mb = m;
        hover_obj.GetComponent<InfoCube>().db = d;
        // hover_obj.GetComponent<InfoCube>().id = id;
        hover_obj.tag = "news_node";
        hover_obj.transform.position = position;
        hover_obj.transform.localScale = new Vector3(0.03f + 0.01f * news_count,0.03f + 0.01f * news_count,0.03f + 0.01f * news_count);
        // hover_obj.transform.localScale = new Vector3(0.02f,0.01f,0.01f);
        // hover_obj.transform.localScale = new Vector3(0.06f,0.06f,0.06f);
        hover_obj.transform.localRotation = Quaternion.Euler(0, angle, 0);
        // dm.getData(0,current_year,current_month,current_day).angle_radians * Mathf.Rad2Deg
        

        // cube.GetComponent<Collider>().isTrigger = true;
        hover_obj.GetComponent<Renderer>().material.color = hover_color;
	}
}
