using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class cube_generator : MonoBehaviour
{   public bool sss = true;
    public List<GameObject> cubes = new List<GameObject>();
    public GameObject Pcube;
    // Start is called before the first frame update
    void Start()
    {
       Pcube = generate_cube(new Vector3(1,1,1));
//        
//        if(sss){
//            for(int i = 0; i < 800; ++i){
//                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
//                cube.name = "" + i;
//    //	        cube.transform.localScale = new Vector3(.1f,.1f,.1f);
//                cube.transform.localScale = new Vector3(1f,1f,1f);
//                cube.transform.position = new Vector3(Random.Range(-200f,200f),Random.Range(0f,8f),Random.Range(-200f,200f));
//                cube.GetComponent<Renderer>().material.color = new Color(Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
//                cube.transform.SetParent(Pcube.transform, true);
//	           cubes.Add(cube);
//
//            }
//        }

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
        cube.transform.localScale = new Vector3(1f,1f,1f);
        cube.transform.position = pos;
        cube.GetComponent<Renderer>().material.color = new Color(0,0,0);
        return cube;
  }

}
