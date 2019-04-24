﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InfoCube;

public class HoverObject
{
	public GameObject hover_obj;
	public Color hover_color;
	public int movie_count = 0;
	public List<int> movie_index;
	public int check_out;
	public Vector3 position;
	public int sharing_counter = 0; // how many movies that is active in the scene are sharing this object
	public InfoCube ic;

	public HoverObject(Color c, int i, int record, Vector3 pos){
		this.hover_obj = new GameObject();
		this.hover_color = c;
		this.movie_count = 1;
		this.movie_index = new List<int>();
		this.movie_index.Add(i);
		this.check_out = record;
		this.position = pos;
		this.hover_obj.SetActive(true);
		this.ic = new InfoCube();

	}

	public void addMovie(int i){
		movie_index.Add(i);
		movie_count++;
		hover_color = Color.gray;
	}

	public void drawCube(){
        hover_obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        hover_obj.AddComponent<InfoCube>();
        hover_obj.GetComponent<InfoCube>().c_out = check_out;
        hover_obj.GetComponent<InfoCube>().index = movie_index;

        // cube.transform.localScale = new Vector3(0.03f,0.03f,0.03f);
        hover_obj.transform.localScale = new Vector3(0.06f,0.06f,0.06f);
        hover_obj.transform.position = position;
        // cube.GetComponent<Collider>().isTrigger = true;
        hover_obj.GetComponent<Renderer>().material.color = hover_color;
	}

}
