using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static StandardShaderUtils;

using static Day;
using static MetaData;

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
    public float LINE_WIDTH_MINI = 0.0004f;

    // for mini map
    public GameObject mini_month_data_line;  // main graphic
    public GameObject mini_connection_to_next;  
    public GameObject mini_month_data_wall;  // main graphic
    public GameObject mini_connection_to_next_wall; 
    public Mesh mini_mesh_month;
    public Material mini_material_month; 
    public Mesh mini_mesh_connect;
    public Material mini_material_connect;         
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
        this.data = new MetaData();

        //for mini map
        this.mini_month_data_line = new GameObject();
        this.mini_connection_to_next = new GameObject();
        this.mini_month_data_wall = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer)); 
        this.mini_connection_to_next_wall = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer)); 
        this.mini_mesh_month = new Mesh();
        this.mini_material_month = new Material(Shader.Find("Custom/Standard2Sided"));
        this.mini_mesh_connect = new Mesh();
        this.mini_material_connect = new Material(Shader.Find("Custom/Standard2Sided")); 
    }


    public void addDay(Day day_obj)
    {
        dayList.Add(day_obj);
    }

    public void addMonthData(int cko)
    {
        this.data.check_out_times = cko;
        this.data.position = this.dayList[14].data.position;
        this.data.position.y = cko / 80f;
        this.data.mini_position.x = this.data.position.x / 60f; //local to mini map
        this.data.mini_position.y = this.data.position.y * 1.6f;
        this.data.mini_position.z = this.data.position.z / 60f;
    }

    // draw data, connect every node from the first to the last
    public void drawData(Color c, Material material){
        LineRenderer line_renderer = month_data_line.AddComponent<LineRenderer>();
        line_renderer.material = material;
        line_renderer.widthMultiplier = LINE_WIDTH;
        line_renderer.positionCount = dayList.Count;
        line_renderer.startColor = c;
        line_renderer.endColor = c;
        line_renderer.useWorldSpace = false;
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
        line_renderer.useWorldSpace = false;
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
            vertices[i] = this.dayList[i].data.wall_position;
        }
        for(int i = day_count; i < day_count * 2; ++i)
        {
            vertices[i] = new Vector3(this.dayList[i - day_count].data.wall_position.x, 0,
                this.dayList[i - day_count].data.wall_position.z);
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
        vertices[0] = this.dayList[dayList.Count - 1].data.wall_position;
        vertices[2] = new Vector3(this.dayList[dayList.Count - 1].data.wall_position.x, 0,
                this.dayList[dayList.Count - 1].data.wall_position.z);
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

    //***********************************************************************************************
    // for mini map
    // draw data, connect every node from the first to the last
    public void drawDataMini(Color c, Material material){
        LineRenderer line_renderer = mini_month_data_line.AddComponent<LineRenderer>();
        line_renderer.material = material;
        line_renderer.widthMultiplier = LINE_WIDTH_MINI;
        line_renderer.positionCount = dayList.Count;
        line_renderer.useWorldSpace = false;
        line_renderer.startColor = c;
        line_renderer.endColor = c;

        for(int i = 0; i < this.dayList.Count; ++i){
            line_renderer.SetPosition(i, this.dayList[i].data.mini_position);
        }

    }
    // connect to the beginning node of data line in next month
    public void connectToNextMini(Color c, Material material, Vector3 next_point){
        LineRenderer line_renderer = mini_connection_to_next.AddComponent<LineRenderer>();
        line_renderer.material = material;
        line_renderer.widthMultiplier = LINE_WIDTH_MINI;
        line_renderer.positionCount = 2;
        line_renderer.useWorldSpace = false;
        line_renderer.startColor = c;
        line_renderer.endColor = c;
        line_renderer.SetPosition(0, this.dayList[dayList.Count - 1].data.mini_position);
        line_renderer.SetPosition(1, next_point);
    }


    // walls
    public void drawDataWallMini(Color c){
        Vector3[] vertices = new Vector3[this.dayList.Count * 2];
        int [] triangles = new int[(this.dayList.Count - 1) * 3 * 2];

        /**************************
            0, 1, 2, 3 ...30(?)
            31,32,33,34...61(?)
        ***************************/
        for(int i = 0; i < day_count; ++i)
        {
            vertices[i] = this.dayList[i].data.mini_position;
        }
        for(int i = day_count; i < day_count * 2; ++i)
        {
            vertices[i] = new Vector3(this.dayList[i - day_count].data.mini_position.x, 0,
                this.dayList[i - day_count].data.mini_position.z);
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

        mini_mesh_month.vertices = vertices;
        // mesh.uv = uv;
        mini_mesh_month.triangles = triangles;
        // material.SetFloat("_Mode", 3f);
        StandardShaderUtils.ChangeRenderMode(mini_material_month, StandardShaderUtils.BlendMode.Transparent);
        mini_material_month.color =  new Color(c.r,c.g,c.b,0.3f);
        // Debug.Log("!");
        mini_month_data_wall.GetComponent<MeshFilter>().mesh = mini_mesh_month;
        mini_month_data_wall.GetComponent<MeshRenderer>().material = mini_material_month;
    }

    public void connectWallMini(Color c, Vector3 next_point){
        Vector3[] vertices = new Vector3[4];
        //end point
        vertices[0] = this.dayList[dayList.Count - 1].data.mini_position;
        vertices[2] = new Vector3(this.dayList[dayList.Count - 1].data.mini_position.x, 0,
                this.dayList[dayList.Count - 1].data.mini_position.z);
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

        mini_mesh_connect.vertices = vertices;
        mini_mesh_connect.triangles = triangles;
        StandardShaderUtils.ChangeRenderMode(mini_material_connect, StandardShaderUtils.BlendMode.Transparent);
        mini_material_connect.color =  new Color(c.r,c.g,c.b,0.3f);
        mini_connection_to_next_wall.GetComponent<MeshFilter>().mesh = mini_mesh_connect;
        mini_connection_to_next_wall.GetComponent<MeshRenderer>().material = mini_material_connect;
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
