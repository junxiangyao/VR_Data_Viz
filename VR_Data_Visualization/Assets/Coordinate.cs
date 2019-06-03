using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coordinate
{
	public GameObject[] lines;
	public GameObject[] circles;
	public GameObject outer_circle;
    public GameObject inner_circle;
	public GameObject base_circle;
	public GameObject coordinate_object;
	public float LINE_WIDTH;
	public Material line_material = new Material(Shader.Find("Sprites/Default"));
	public Color c = new Color(255 * 1.0f/255, 255 * 1.0f/255, 255 * 1.0f/255);

    public Coordinate(float line_width)
    {
    	this.coordinate_object = new GameObject();
        this.lines = new GameObject[6];
        this.circles = new GameObject[15];
        this.outer_circle = new GameObject();
        this.outer_circle.transform.SetParent(this.coordinate_object.transform);
        this.inner_circle = new GameObject();
        this.inner_circle.transform.SetParent(this.coordinate_object.transform);
        this.base_circle = new GameObject();
        this.base_circle.transform.SetParent(this.coordinate_object.transform);
        this.LINE_WIDTH = line_width;
        for(int i = 0; i < 6; ++i){
            this.lines[i] = new GameObject();
            this.lines[i].transform.SetParent(this.coordinate_object.transform);
        }
        for(int i = 0; i < 15; ++i){
            this.circles[i] = new GameObject();
            this.circles[i].transform.SetParent(this.coordinate_object.transform);
        }
    }

    public void drawCoordinate(float r){
    	for(int i = 0; i < 6; ++i){
    		float theta = Mathf.PI * 30 * i / 180;
    		Vector3 sp = new Vector3(r * Mathf.Sin(theta), 0.011f, r * Mathf.Cos(theta));
    		Vector3 ep = new Vector3(-1 * r * Mathf.Sin(theta), 0.011f, -1 * r * Mathf.Cos(theta));
    		drawLine(lines[i], sp, ep);
    	}
    	// for(int i = 0; i < 14; ++i){
    	// 	drawLine(circles[i], sp, ep);
    	// }
    }


    public void drawLine(GameObject line, Vector3 start_point, Vector3 end_point){
        LineRenderer line_renderer = line.AddComponent<LineRenderer>();
        line_renderer.material = line_material;
        line_renderer.widthMultiplier = LINE_WIDTH;
        line_renderer.positionCount = 2;
        line_renderer.useWorldSpace = false;
        line_renderer.startColor = c;
        line_renderer.endColor = c;
        line_renderer.SetPosition(0, start_point);
        line_renderer.SetPosition(1, end_point);
    }

    public void drawCircle(GameObject circle, float r, int resolution, Color color)
    {
        LineRenderer line_renderer = circle.AddComponent<LineRenderer>();
        line_renderer.material = new Material(Shader.Find("Sprites/Default"));
        line_renderer.widthMultiplier = LINE_WIDTH;
        // line_renderer.sortingOrder = 1;
        if(resolution < 8){
            resolution = 8;
        }
        line_renderer.positionCount = resolution + 1;
        line_renderer.useWorldSpace = false;
        line_renderer.startColor = color;
        line_renderer.endColor = color;
        for(int i = 0; i < resolution + 1; ++i){
            line_renderer.SetPosition(i, new Vector3(r * Mathf.Sin(i * (Mathf.PI * 2) / resolution),0.011f,r * Mathf.Cos(i * (Mathf.PI * 2) / resolution)));
        }
    }


    public void drawContour(GameObject circle, float r, int resolution, Color color)
    {
        LineRenderer line_renderer = circle.AddComponent<LineRenderer>();
        line_renderer.material = new Material(Shader.Find("Sprites/Default"));
        line_renderer.widthMultiplier = LINE_WIDTH;
        // line_renderer.sortingOrder = 1;
        if(resolution < 8){
            resolution = 8;
        }
        line_renderer.positionCount = resolution + 1;
        line_renderer.useWorldSpace = false;
        line_renderer.startColor = color;
        line_renderer.endColor = color;
        for(int i = 0; i < resolution + 1; ++i){
            line_renderer.SetPosition(i, new Vector3(r * Mathf.Sin(i * (Mathf.PI * 2) / resolution),-0.009f,r * Mathf.Cos(i * (Mathf.PI * 2) / resolution)));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
