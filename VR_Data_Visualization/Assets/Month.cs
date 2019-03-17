using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static StandardShaderUtils;

using static Day;

public class Month
{
    public int month_order; //the order of the month 
    public List<Day> dayList; // a list of day objects containing data from that day  
    public MetaData data; // containing monthly data
    public int day_count; // number of the days in month
    public GameObject month_data_line;  // main graphic
    public GameObject connection_to_next;  
    public GameObject month_data_wall;  // main graphic
    public GameObject connection_to_next_wall; 
    public Mesh mesh_month;
    public Material material_month; 
    public Mesh mesh_connect;
    public Material material_connect; 
    public float LINE_WIDTH = 0.005f;        
    // Constructor that takes no arguments:
    public Month()
    {
        this.dayList = new List<Day>();
        this.data = new MetaData();
    }

    // Constructor that takes one argument:
    public Month(int month_order, int day_count)
    {
        this.month_order = month_order;
        this.dayList = new List<Day>();
        this.data = new MetaData();
        this.day_count = day_count;
        this.month_data_line = new GameObject();
        this.connection_to_next = new GameObject();
        this.month_data_wall = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer)); 
        this.connection_to_next_wall = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer)); 
        this.mesh_month = new Mesh();
        this.material_month = new Material(Shader.Find("Custom/Standard2Sided"));
        this.mesh_connect = new Mesh();
        this.material_connect = new Material(Shader.Find("Custom/Standard2Sided")); 
    }


    public void addDay(Day day_obj)
    {
        dayList.Add(day_obj);
    }
    // draw data, connect every node from the first to the last
    public void drawData(Color c, Material material){
        LineRenderer line_renderer = month_data_line.AddComponent<LineRenderer>();
        line_renderer.material = material;
        line_renderer.widthMultiplier = LINE_WIDTH;
        line_renderer.positionCount = dayList.Count;
        line_renderer.startColor = c;
        line_renderer.endColor = c;
        for(int i = 0; i < this.dayList.Count; ++i){
            line_renderer.SetPosition(i, this.dayList[i].data.position);
        }

    }
    // connect to the beginning node of data line in next month
    public void connectToNext(Color c, Material material, Vector3 next_point){
        LineRenderer line_renderer = connection_to_next.AddComponent<LineRenderer>();
        line_renderer.material = material;
        line_renderer.widthMultiplier = LINE_WIDTH;
        line_renderer.positionCount = 2;
        line_renderer.startColor = c;
        line_renderer.endColor = c;
        line_renderer.SetPosition(0, this.dayList[dayList.Count - 1].data.position);
        line_renderer.SetPosition(1, next_point);
    }


    // walls
    public void drawDataWall(Color c){
        Vector3[] vertices = new Vector3[this.dayList.Count * 2];
        int [] triangles = new int[(this.dayList.Count - 1) * 3 * 2];

        /**************************
            0, 1, 2, 3 ...30(?)
            31,32,33,34...61(?)
        ***************************/
        for(int i = 0; i < day_count; ++i)
        {
            vertices[i] = this.dayList[i].data.position;
        }
        for(int i = day_count; i < day_count * 2; ++i)
        {
            vertices[i] = new Vector3(this.dayList[i - day_count].data.position.x, 0,
                this.dayList[i - day_count].data.position.z);
        }
        int counter = 0;
        for(int i = 0; i < day_count - 1; ++i) // each iteration draws two triangles
        {
            triangles[counter++] = i;   //0
            triangles[counter++] = i+1; //1
            triangles[counter++] = day_count+i; //2
            triangles[counter++] = i+1; //1
            triangles[counter++] = day_count+i+1; //3
            triangles[counter++] = day_count+i; //2
        }

        mesh_month.vertices = vertices;
        // mesh.uv = uv;
        mesh_month.triangles = triangles;
        // material.SetFloat("_Mode", 3f);
        StandardShaderUtils.ChangeRenderMode(material_month, StandardShaderUtils.BlendMode.Transparent);
        material_month.color =  new Color(c.r,c.g,c.b,0.3f);
        // Debug.Log("!");
        month_data_wall.GetComponent<MeshFilter>().mesh = mesh_month;
        month_data_wall.GetComponent<MeshRenderer>().material = material_month;
    }

    public void connectWall(Color c, Vector3 next_point){
        Vector3[] vertices = new Vector3[4];
        //end point
        vertices[0] = this.dayList[dayList.Count - 1].data.position;
        vertices[2] = new Vector3(this.dayList[dayList.Count - 1].data.position.x, 0,
                this.dayList[dayList.Count - 1].data.position.z);
        //start point of next
        vertices[1] = next_point;
        vertices[3] = new Vector3(next_point.x, 0, next_point.z);
        int [] triangles = new int[6];
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 1;
        triangles[4] = 3;
        triangles[5] = 2;

        mesh_connect.vertices = vertices;
        mesh_connect.triangles = triangles;
        StandardShaderUtils.ChangeRenderMode(material_connect, StandardShaderUtils.BlendMode.Transparent);
        material_connect.color =  new Color(c.r,c.g,c.b,0.3f);
        connection_to_next_wall.GetComponent<MeshFilter>().mesh = mesh_connect;
        connection_to_next_wall.GetComponent<MeshRenderer>().material = material_connect;
    }

    public String printInfo()
    {  
        //buffer
        StringBuilder sb = new StringBuilder();

        for(int i = 0 ; i  < dayList.Count; i++)
        {
            sb.Append("dayList["+i+"] = "+dayList[i].printInfo()+"\n");
        }
        return sb.ToString();
    }

}
