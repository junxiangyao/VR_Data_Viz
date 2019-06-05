using System;
using UnityEngine;

public class MetaData
{
    public int check_out_times;
    public Vector3 position;
    public Vector3 mini_position;
    public Vector3 wall_position;
    public float radius;
    public float mini_radius;
    public float hit_radius;
    public float angle_radians;               
    // Constructor that takes no arguments:
    public MetaData()
    {
        this.check_out_times = 0;
        this.position = new Vector3();
        this.mini_position = new Vector3();
    }

    // Constructor that takes argument:
    public MetaData(int check_out_times, Vector3 position, Vector3 wall_position, float r, float angle)
    {
        this.check_out_times = check_out_times;
        this.position = position;
        this.wall_position = wall_position;
        // this.position.y = position.y + 1.0f;
        this.position.y = position.y + 0.5f;
        this.wall_position.y += 0.5f;

        this.mini_position.x = position.x / 60f; //local to mini map
        this.mini_position.y = position.y * 2.0f;
        // this.mini_position.y = position.y / 10f -1f;
        this.mini_position.z = position.z / 60f;

        this.radius = r;
        this.mini_radius = this.radius / 60f; 
        this.hit_radius = this.radius/200f; // world scale
        this.angle_radians = angle;
    }


    public void printInfo()
    {
    }

}
