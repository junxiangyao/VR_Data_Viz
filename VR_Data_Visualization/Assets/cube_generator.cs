﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static DataManager;

public class cube_generator : MonoBehaviour
{   public bool sss = true;
    public List<GameObject> cubes = new List<GameObject>();
    public GameObject Pcube;
    // Start is called before the first frame update

    public static float INNER_RADIUS = 20.0f; // 1 meter 
    public static float INCREASE = 0.00078f * 4;
    public float current_radius = INNER_RADIUS;
    public DataManager dm; 

    
    void Start()
    {


        int counter = 0;  
        string line;  

        dm = new DataManager();

        // Read the file and display it line by line.  
        System.IO.StreamReader file = new System.IO.StreamReader(@"Assets/sw.csv");  
        while((line = file.ReadLine()) != null)  
        {  

            //System.Console.WriteLine(line);
            int[] splits = parseLine(line);
            for(int movieIdx = 0 ; movieIdx < 12 ; movieIdx++)
            {
                dm.addData(movieIdx,splits[0],splits[1],splits[2],splits[3+movieIdx],current_radius);
            }

            // growing radius
            current_radius += INCREASE;
            //Debug.Log("object at year["+splits[0]+"], mon["+splits[1]+"], day["+splits[2]+"]");
            counter++;  
        }  

        file.Close();
        
        MetaData md = dm.getData(3,2012,2,29);
        Debug.Log("md.check_out_times = "+md.check_out_times);


        dm.drawData();

        //Test Boxes
        Color c = new Color(0.75f,0.75f,0.75f);

        // for(int y = 0; y < 14; ++y){
        //     for(int m = 0; m < 12; ++m){
        //         for(int d = 0; d < dm.MovieObjs[8].year_objects[y].MonthObjs[m].dayList.Count; ++d){
        //             Pcube = generate_cube(dm.MovieObjs[8].year_objects[y].MonthObjs[m].dayList[d].data.position, c);
        //         }
        //     }
        // }

        c = new Color(1,0,0);
        Pcube = generate_cube(new Vector3(0,0,0), c);
        Pcube = generate_cube(new Vector3(0,2,0), c);
        Pcube = generate_cube(new Vector3(0,1,0), c);
        Pcube = generate_cube(new Vector3(0,0,1), c);
        Pcube = generate_cube(new Vector3(1,0,0), c);
        Pcube = generate_cube(new Vector3(-1,0,0), c);
        Pcube = generate_cube(new Vector3(0,0,-1), c);
        Debug.Log("Current radius: " + current_radius);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for(int mv = 0; mv < 12; ++mv)
            {
                dm.show_movies[mv] = !dm.show_movies[mv]; 
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            dm.show_years[2] = !dm.show_years[2]; 
        }

        for(int mv = 0; mv < 12; ++mv)
        {
            dm.MovieObjs[mv].game_object.SetActive(dm.show_movies[mv]); 
        }
        for(int mv = 0; mv < 12; ++mv)
        {
            for(int y = 0; y < 14; ++y)
            {
                dm.MovieObjs[mv].years[y].year_game_object.SetActive(dm.show_years[y]); 
                //because the last year has nothing to connect with, the last year doesn't have a connector;
                //thus, only the first 13 years needed to be checked
                if(y < 13){
                    //if next year is not shown, hide the connector. connector is the line connecting the end of this year and the start of next year.
                    if(!dm.show_years[y+1]){
                        dm.MovieObjs[mv].years[y].MonthObjs[11].connection_to_next.SetActive(dm.show_years[y+1]); 
                    }else{
                        dm.MovieObjs[mv].years[y].MonthObjs[11].connection_to_next.SetActive(dm.show_years[y]); 
                    }
                }
           }
        }





        // foreach(GameObject cube in cubes)
        // {

        // }
        
        
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    Debug.Log("Hit!");
                    hit.transform.gameObject.GetComponent<Renderer>().material.color = new Color(Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));

                }
            }
        }
    
    }
  
    public GameObject generate_cube(Vector3 pos, Color color)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        // cube.transform.localScale = new Vector3(0.03f,0.03f,0.03f);
        cube.transform.localScale = new Vector3(0.12f,0.12f,0.12f);
        cube.transform.position = pos;
        cube.GetComponent<Collider>().isTrigger = true;
        cube.GetComponent<Renderer>().material.color = color;
        return cube;
    }


    static int[] parseLine(string line)
    {
        char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
        string[] spl = line.Split(delimiterChars);
        int[] output = new int[spl.Length];
        //System.Console.WriteLine("spl.Length = "+spl.Length);
        for(int i = 0 ; i  < spl.Length ; i++)
        {
            //System.Console.WriteLine("spl[i] = "+spl[i]);
            output[i] = int.Parse(spl[i]);
        }
        //System.Console.WriteLine("output.Length = "+output.Length);
        return output;
    }

}
