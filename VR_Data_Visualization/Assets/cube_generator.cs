using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static DataManager;

public class cube_generator : MonoBehaviour
{   public bool sss = true;
    public List<GameObject> cubes = new List<GameObject>();
    public GameObject Pcube;
    // Start is called before the first frame update

    public static float INNER_RADIUS = 30.0f; // 20 meter 
    public static float INCREASE = 0.00078f * 3;
    public float current_radius = INNER_RADIUS;
    public DataManager dm; 
    static Material lineMaterial;

    
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
        Debug.Log("md.check_out_times = " + md.check_out_times);


        dm.drawData();
        // dm.drawDate();

        //Test Boxes
        Color c = new Color(0.75f,0.75f,0.75f);

        // for(int mv = 0; mv < 12; ++mv){
        // for(int y = 0; y < 14; ++y){
        //     for(int m = 0; m < 12; ++m){
        //         for(int d = 0; d < dm.MovieObjs[mv].years[y].months[m].dayList.Count; ++d){
        //             Pcube = generate_cube(dm.MovieObjs[mv].years[y].months[m].dayList[d].data.position, c);
        //         }
        //     }
        // }
        // }

        c = new Color(1,0,0);
        // Pcube = generate_cube(new Vector3(0,0,0), c);
        // Pcube = generate_cube(new Vector3(0,2,0), c);
        // Pcube = generate_cube(new Vector3(0,1,0), c);
        // Pcube = generate_cube(new Vector3(0,0,1), c);
        // Pcube = generate_cube(new Vector3(1,0,0), c);
        // Pcube = generate_cube(new Vector3(-1,0,0), c);
        // Pcube = generate_cube(new Vector3(0,0,-1), c);
        Debug.Log("Current radius: " + current_radius);
    }

    // Update is called once per frame
    void Update()
    {
        /*************************************************
            key board control
        **************************************************/

        if (Input.GetKeyDown(KeyCode.Z))
        {
            dm.show_date_lines = !dm.show_date_lines;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            dm.show_wall = !dm.show_wall;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            for(int mv = 0; mv < 12; ++mv)
            {
                dm.show_movies[mv] = !dm.show_movies[mv]; 
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            dm.show_movies[0] = !dm.show_movies[0]; 
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            dm.show_movies[1] = !dm.show_movies[1]; 
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            dm.show_movies[2] = !dm.show_movies[2]; 
        }


        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            dm.show_years[0] = !dm.show_years[0]; 
            dm.show_years[4] = !dm.show_years[4]; 
            dm.show_years[7] = !dm.show_years[7]; 
            dm.show_years[11] = !dm.show_years[11]; 
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            dm.show_months[0] = !dm.show_months[0]; 
            dm.show_months[3] = !dm.show_months[3]; 
            dm.show_months[8] = !dm.show_months[8];     
        }


        // dm.drawDate();




        for(int mv = 0; mv < 12; ++mv)
        {
            for(int m = 0 ; m < 12; ++m){
                dm.MovieObjs[mv].date_lines_month[m].SetActive(dm.show_months[m]);
            }
        }


        /*************************************************
            movie, year, month level show/hide control
        **************************************************/

        for(int mv = 0; mv < 12; ++mv)
        {
            // show/ hide movie
            dm.MovieObjs[mv].game_object.SetActive(dm.show_movies[mv]); 
            for(int y = 0; y < 14; ++y)
            {
                // show / hide year
                dm.MovieObjs[mv].years[y].year_game_object.SetActive(dm.show_years[y]); 
                for(int m = 0; m < 12; ++m)
                {
                    // show / hide month
                    dm.MovieObjs[mv].years[y].months[m].month_data_line.SetActive(dm.show_months[m]);  // main graphic
                    dm.MovieObjs[mv].years[y].months[m].month_data_wall.SetActive(dm.show_months[m]&&dm.show_wall);
                    if(m < 11)
                    {
                        // if next month won't be drawn
                        if(!dm.show_months[m+1])
                        {
                            dm.MovieObjs[mv].years[y].months[m].connection_to_next.SetActive(dm.show_months[m+1]);
                            dm.MovieObjs[mv].years[y].months[m].connection_to_next_wall.SetActive(dm.show_months[m+1]);
                        }else{
                            dm.MovieObjs[mv].years[y].months[m].connection_to_next.SetActive(dm.show_months[m]);
                            if(dm.show_months[m] && dm.show_wall)
                            {
                                dm.MovieObjs[mv].years[y].months[m].connection_to_next_wall.SetActive(true);
                            }else{
                                dm.MovieObjs[mv].years[y].months[m].connection_to_next_wall.SetActive(false);
                            }
                        }
                    }
                }

                // ;
                //because the last year has nothing to connect with, the last year doesn't have a connector;
                //thus, only the first 13 years needed to be checked
                if(y < 13){
                    //if next year is not shown, hide the connector. connector is the line connecting the end of this year and the start of next year.
                    // or if January is not shown in the scene, hide Decemembers connector.
                    if(!dm.show_years[y+1] || !dm.show_months[0]){
                        dm.MovieObjs[mv].years[y].months[11].connection_to_next.SetActive(false); 
                        dm.MovieObjs[mv].years[y].months[11].connection_to_next_wall.SetActive(false); 
                    }else{
                        dm.MovieObjs[mv].years[y].months[11].connection_to_next.SetActive(dm.show_months[11]);
                        dm.MovieObjs[mv].years[y].months[11].connection_to_next_wall.SetActive(dm.show_wall);  
                    }
                }
           }
        }

        
        /*************************************************
            ray cast control
        **************************************************/
        
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


    //    static Material lineMaterial;
   static void CreateLineMaterial()
   {
       if (!lineMaterial)
       {
           // Unity has a built-in shader that is useful for drawing
           // simple colored things.
           Shader shader = Shader.Find("Hidden/Internal-Colored");
           lineMaterial = new Material(shader);
           lineMaterial.hideFlags = HideFlags.HideAndDontSave;
           // Turn on alpha blending
           lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
           lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
           // Turn backface culling off
           lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
           // Turn off depth writes
           lineMaterial.SetInt("_ZWrite", 0);
       }
   }


    // Will be called after all regular rendering is done
    public void OnRenderObject()
    {
        CreateLineMaterial();
        // Apply the line material
        lineMaterial.SetPass(0);

        if(dm.show_date_lines){
             // Draw lines
            for(int mv = 0; mv < 12; ++mv){
                if(dm.show_movies[mv])
                {
                    for (int m = 0; m < 12; ++m)
                    {
                        if(dm.show_months[m])
                        {
                            for(int d = 0; d < dm.DAYS_IN_MONTH[m]; ++d)
                            {
                                GL.PushMatrix();
                                // Set transformation matrix for drawing to
                                // match our transform
                                // GL.MultMatrix(transform.localToWorldMatrix);
                                GL.Begin(GL.LINE_STRIP);
                                GL.Color(dm.movie_colors[mv]);
                                for (int y = 0; y < 14; ++y)
                                {
                                    if(dm.show_years[y])
                                    {
                                        GL.Vertex3(dm.MovieObjs[mv].years[y].months[m].dayList[d].data.position.x,
                                                dm. MovieObjs[mv].years[y].months[m].dayList[d].data.position.y,
                                                dm.MovieObjs[mv].years[y].months[m].dayList[d].data.position.z);
                                    }
                                }
                                GL.End();
                                GL.PopMatrix();
                            }
                        }
                    }
                }
            }

        }
    }

  
    public GameObject generate_cube(Vector3 pos, Color color)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        // cube.transform.localScale = new Vector3(0.03f,0.03f,0.03f);
        cube.transform.localScale = new Vector3(0.01f,0.01f,0.01f);
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
