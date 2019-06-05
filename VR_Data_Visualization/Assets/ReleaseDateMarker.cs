using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InfoCube;

public class ReleaseDateMarker : MonoBehaviour
{	
	public GameObject marker_obj;
	public Color marker_color;
	public int movie_index;
	public int y;
	public int m;
	public int d;
	public Vector3 position;
	public GameObject locating_line;


	public ReleaseDateMarker(Color c, int i, Vector3 pos, int y_, int m_, int d_){
		this.marker_obj = new GameObject();
		this.locating_line = new GameObject();
		this.marker_color = c;
		this.movie_index = i;
		this.position = pos;
		this.marker_obj.SetActive(true);
		this.y = y_;
		this.m = m_;
		this.d = d_;
	}

	public void drawSphere(float h){
        marker_obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        marker_obj.AddComponent<InfoCube>();
        marker_obj.GetComponent<InfoCube>().index = new List<int>();        
        marker_obj.GetComponent<InfoCube>().index.Add(movie_index);        
        marker_obj.GetComponent<InfoCube>().yb = y;
        marker_obj.GetComponent<InfoCube>().mb = m;
        marker_obj.GetComponent<InfoCube>().db = d;
        marker_obj.tag = "date_marker_mini";
        marker_obj.transform.localScale = new Vector3(0.008f,0.008f,0.008f);
        // marker_obj.transform.localScale = new Vector3(4f,4f,4f);
        // hover_obj.transform.localScale = new Vector3(0.06f,0.06f,0.06f);
        marker_obj.transform.position = new Vector3(position.x * 0.3f, h, position.z * 0.3f);
        

        // cube.GetComponent<Collider>().isTrigger = true;
        marker_obj.GetComponent<Renderer>().material.color = marker_color;

        LineRenderer line_renderer = locating_line.AddComponent<LineRenderer>();
        line_renderer.material = new Material(Shader.Find("Sprites/Default"));
        line_renderer.widthMultiplier = 0.00072f;
        line_renderer.positionCount = 2;
        line_renderer.useWorldSpace = false;
        line_renderer.startColor = marker_color;
        line_renderer.endColor = marker_color;

        line_renderer.SetPosition(0, marker_obj.transform.position);
        line_renderer.SetPosition(1, new Vector3(marker_obj.transform.position.x, 0.01f, marker_obj.transform.position.z));
        locating_line.transform.SetParent(marker_obj.transform);

        // hover_obj.GetComponent<Renderer>().material.color = hover_color;
        
	}
}
