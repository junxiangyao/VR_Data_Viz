using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverObject
{
	public GameObject hover_obj;
	public Color hover_color;
	public int movie_count = 0;
	public List<int> movie_index;
	public int check_out;
	public Vector3 position;

	public HoverObject(Color c, int i, int record, Vector3 pos){
		this.hover_obj = new GameObject();
		this.hover_color = c;
		this.movie_count = 1;
		this.movie_index = new List<int>();
		this.movie_index.Add(i);
		this.check_out = record;
		this.position = pos;
		this.hover_obj.SetActive(true);
	}

	public void addMovie(int i){
		movie_index.Add(i);
		movie_count++;
		hover_color = Color.gray;
	}

	public void drawCube(){
        hover_obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        // cube.transform.localScale = new Vector3(0.03f,0.03f,0.03f);
        hover_obj.transform.localScale = new Vector3(0.02f,0.02f,0.02f);
        hover_obj.transform.position = position;
        // cube.GetComponent<Collider>().isTrigger = true;
        hover_obj.GetComponent<Renderer>().material.color = hover_color;
	}

}
