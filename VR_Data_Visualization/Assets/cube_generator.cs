using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static DataManager;

public class cube_generator : MonoBehaviour
{   public bool sss = true;
    public List<GameObject> cubes = new List<GameObject>();
    public GameObject Pcube;
    // Start is called before the first frame update

    public static float INNER_RADIUS = 2.0f; // 1 meter 
    public static float INCREASE = 0.00078f * 8;
    public float current_radius = INNER_RADIUS;

    
    void Start()
    {


        int counter = 0;  
        string line;  

        DataManager dm = new DataManager();

        // int yearIdx = -1;
        // int monthIdx = -1;
        // int check_out_times = -1;
        // Vector3 position = new Vector3();

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
            current_radius += INCREASE;
            //Debug.Log("object at year["+splits[0]+"], mon["+splits[1]+"], day["+splits[2]+"]");
            counter++;  
        }  

        file.Close();
        
        MetaData md = dm.getData(3,2012,2,29);
        Debug.Log("md.check_out_times = "+md.check_out_times);


        dm.drawData();

        //Test Boxes
        Color c = new Color(0,0,0);

        // for(int y = 0; y < 14; ++y){
        //     for(int m = 0; m < 12; ++m){
        //         for(int d = 0; d < dm.MovieObjs[8].YearObjs[y].MonthObjs[m].dayList.Count; ++d){
        //             Pcube = generate_cube(dm.MovieObjs[8].YearObjs[y].MonthObjs[m].dayList[d].data.position, c);
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
            sss = ! sss;
        }
        foreach(GameObject cube in cubes)
        {
            Pcube.SetActive(sss);
        }
        
        
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
  
    GameObject generate_cube(Vector3 pos, Color color)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(0.03f,0.03f,0.03f);
        cube.transform.position = pos;
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
