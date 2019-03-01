using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static DataManager;

public class cube_generator : MonoBehaviour
{   public bool sss = true;
    public List<GameObject> cubes = new List<GameObject>();
    public GameObject Pcube;
    // Start is called before the first frame update
    
    
    
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
    
    void Start()
    {

//        for(int i = 0; i < 10; ++i){
//            for(int j = 0; j < 10; ++j){
//                for(int k = 0; k < 10; ++k){
//                    Pcube = generate_cube(new Vector3(i+10,j,k+10));
//                }
//            }
//        }


        int counter = 0;  
        string line;  


        DataManager dm = new DataManager();

        int yearIdx = -1;
        int monthIdx = -1;
        int borrows = -1;
        float[] position = new float[3];

        // Read the file and display it line by line.  
        System.IO.StreamReader file = new System.IO.StreamReader(@"Assets/sw.csv");  
        while((line = file.ReadLine()) != null)  
        {  

            //System.Console.WriteLine(line);
            int[] splits = parseLine(line);
            for(int movieIdx = 0 ; movieIdx<12 ; movieIdx++)
            {
                dm.addData(movieIdx,splits[0],splits[1],splits[3+movieIdx],new float[3]{1.2f,2.3f,3.4f});
            }

            //Debug.Log("object at year["+splits[0]+"], mon["+splits[1]+"], day["+splits[2]+"]");
            counter++;  
        }  

        file.Close();
        
        MetaData md = dm.getData(3,2018,12,27);
        Debug.Log("md.borrows = "+md.borrows);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sss = ! sss;
        }
        foreach(GameObject cube in cubes){
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
  
  
  GameObject generate_cube(Vector3 pos){
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(0.2f,0.2f,0.2f);
        cube.transform.position = pos;
        cube.GetComponent<Renderer>().material.color = new Color(0,0,0);
        return cube;
  }

}
