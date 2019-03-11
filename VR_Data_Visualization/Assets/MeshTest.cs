using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StandardShaderUtils;

public class MeshTest : MonoBehaviour
{
	public Mesh mesh;
    public Material material;
    public Material material1;
    public Material material2;
    // Start is called before the first frame update

    // StandardShaderUtils s =  new StandardShaderUtils(); 
    void Start()
    {
    	Vector3[] vertices = new Vector3[4];
    	// Vector2[] uv = new Vector2[0];
    	int[] triangles = new int[6];
    	vertices[0] = new Vector3(0,0,1);
    	vertices[1] = new Vector3(0,1,1);
    	vertices[2] = new Vector3(1,0,1);
    	vertices[3] = new Vector3(1,1,1);
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 1;
        triangles[4] = 3;
        triangles[5] = 2;

        

    	mesh = new Mesh();
        // material = new Material(Shader.Find("Standard"));
        material = new Material(Shader.Find("Custom/Standard2Sided"));
        material1 = new Material(Shader.Find("Custom/Standard2Sided"));
        material2 = new Material(Shader.Find("Custom/Standard2Sided"));
        // material = new Material(Shader.Find("Custom/NewSurfaceShader"));



    	GameObject gameObject = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer)); 

 

    	mesh.vertices = vertices;
    	// mesh.uv = uv;
    	mesh.triangles = triangles;
        // material.SetFloat("_Mode", 3f);
        StandardShaderUtils.ChangeRenderMode(material, StandardShaderUtils.BlendMode.Transparent);
        material.color =  new Color(1,0,0,0.1f);
        Debug.Log("!");
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        gameObject.GetComponent<MeshRenderer>().material = material;



        Mesh mesh1 = new Mesh();
        vertices[0] = new Vector3(0,0,2);
        vertices[1] = new Vector3(0,1,2);
        vertices[2] = new Vector3(1,0,2);
        vertices[3] = new Vector3(1,1,2);
        mesh1.vertices = vertices;
        mesh1.triangles = triangles;
        GameObject gameObject1 = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer)); 
        StandardShaderUtils.ChangeRenderMode(material1, StandardShaderUtils.BlendMode.Transparent);
        material1.color =  new Color(0,1,0,0.1f);
        gameObject1.GetComponent<MeshFilter>().mesh = mesh1;
        gameObject1.GetComponent<MeshRenderer>().material = material1;



        Mesh mesh2 = new Mesh();
        vertices[0] = new Vector3(0,0,3);
        vertices[1] = new Vector3(0,1,3);
        vertices[2] = new Vector3(1,0,3);
        vertices[3] = new Vector3(1,1,3);
        mesh2.vertices = vertices;
        mesh2.triangles = triangles;
        GameObject gameObject2 = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer)); 
        StandardShaderUtils.ChangeRenderMode(material2, StandardShaderUtils.BlendMode.Transparent);
        material2.color =  new Color(0,0,1,0.1f);
        gameObject2.GetComponent<MeshFilter>().mesh = mesh2;
        gameObject2.GetComponent<MeshRenderer>().material = material2;
    	
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
