//using UnityEngine;
//
//
//
//public class Lines : MonoBehaviour
//{
//    // When added to an object, draws colored rays from the
//    // transform position.
//    public int lineCount = 100;
//    public float radius = 3.0f;
//    public int counter = 5113;
//
//    static Material lineMaterial;
//    static void CreateLineMaterial()
//    {
//        if (!lineMaterial)
//        {
//            // Unity has a built-in shader that is useful for drawing
//            // simple colored things.
//            Shader shader = Shader.Find("Hidden/Internal-Colored");
//            lineMaterial = new Material(shader);
//            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
//            // Turn on alpha blending
//            lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
//            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
//            // Turn backface culling off
//            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
//            // Turn off depth writes
//            lineMaterial.SetInt("_ZWrite", 0);
//        }
//    }
//    
//    void Start()
//    {
//
//
//    }
//
//    // Will be called after all regular rendering is done
//    public void OnRenderObject()
//    {
//        CreateLineMaterial();
//        // Apply the line material
//        lineMaterial.SetPass(0);
//
//
//        // Draw lines
//        for(int j = 0; j < 10; ++j){
//            GL.PushMatrix();
//            // Set transformation matrix for drawing to
//            // match our transform
//            GL.MultMatrix(transform.localToWorldMatrix);
//            GL.Begin(GL.LINE_STRIP);
//            GL.Color(new Color(Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f)));
//            for (int i = 0; i < counter; ++i)
//            {
//                GL.Vertex3(Random.Range(-100f,100f),Random.Range(0f,8f),Random.Range(-100f,100f));
//            }
//            GL.End();
//            GL.PopMatrix();
//        }
//
//        if(counter > 10){counter-=10;};
//        Debug.Log("" +counter);
//    }
//}



using UnityEngine;
using System.Collections;

public class Lines : MonoBehaviour
{
    // Creates a line renderer that follows a Sin() function
    // and animates it.

    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    public int lengthOfLineRenderer = 5113;
//    public Color c1 = Color.white;
//    public Color c2 = new Color(1, 1, 1, 0);

    void Start()
    {
        GameObject l1 = new GameObject("l1");
        LineRenderer lineRenderer1 = l1.AddComponent<LineRenderer>();
        lineRenderer1.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer1.widthMultiplier = 0.2f;
        lineRenderer1.positionCount = lengthOfLineRenderer;

        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lineRenderer1.colorGradient = gradient;
        for (int i = 0; i < lengthOfLineRenderer; i++)
        {
            lineRenderer1.SetPosition(i, new Vector3(i * 0.5f, Mathf.Sin(i + 1) + 2, 0.0f));
        }
        
        GameObject l2 = new GameObject("l2");
        LineRenderer lineRenderer2 = l2.AddComponent<LineRenderer>();
        lineRenderer2.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer2.widthMultiplier = 0.01f;
        lineRenderer2.positionCount = lengthOfLineRenderer;
//
//        
//        

        // lineRenderer2.SetColors(c2, c2);
        
        for (int i = 0; i < lengthOfLineRenderer; i++)
        {
            lineRenderer2.SetPosition(i, new Vector3(i * 0.5f, Mathf.Sin(i + 1) + 2, 2.0f));
        }
    }

    void Update()
    {
    }
}