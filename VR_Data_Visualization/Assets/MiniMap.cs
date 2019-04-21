using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.UI;


public class MiniMap : MonoBehaviour
{
	VRTK_Pointer pointer;
	GameObject left_controller;
	GameObject right_controller;
	GameObject play_area;
	GameObject player_marker;
	GameObject player_world;
	float rotation_counter;
	float yRotation = 0.0f;
	float dist_mini = 0.0f;
	float angle_sin = 0.0f;
	float angle_cos = 0.0f;
	float dist_real = 0.0f;
	
	VRTK_BasicTeleport teleporter;
	GameObject hit_point;

    GameObject c;
    Button b;
    bool boolean = true;
    ColorBlock theColor;

    // Start is called before the first frame update
    void Start()
    {
		right_controller = GameObject.FindGameObjectWithTag("ControllerRight");
		left_controller = GameObject.FindGameObjectWithTag("ControllerLeft");
    	pointer = right_controller.GetComponent<VRTK_Pointer>();
    	play_area = GameObject.FindGameObjectWithTag("Teleport");
    	player_world = GameObject.Find("[VRTK_SDKSetups]").transform.GetChild(3).GetChild(0).gameObject;
        teleporter = play_area.GetComponent<VRTK_BasicTeleport>(); 

        player_marker = GameObject.CreatePrimitive(PrimitiveType.Cube);
        player_marker.transform.localScale = new Vector3(0.01f,0.1f,0.01f);
        transform.SetParent(left_controller.transform);
        transform.localPosition = new Vector3(0,0.02f,0);
       	player_marker.transform.SetParent(transform);

        hit_point = new GameObject();
        c = GameObject.Find("Canvas");
        c.transform.SetParent(left_controller.transform);
        b = c.transform.GetChild(1).gameObject.GetComponent<Button>();
        b.onClick.AddListener(CustomButton_onClick);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(b);
        
        player_marker.SetActive(boolean);

        theColor = b.colors;
        if(boolean){
            theColor.normalColor = Color.green;
        }else{
            theColor.normalColor = Color.red;
        };
        b.colors = theColor;
    	// marker_position is the value of a world
    	// marker_Position = transform.TransformPoint(player_world.transform.position.x/100f,
    	// 	0f,
    	// 	player_world.transform.position.z/100f);

    	// player_marker.transform.localPosition = marker_Position;


    	player_marker.transform.localPosition = new Vector3(player_world.transform.position.x/100f,
    		5f,
    		player_world.transform.position.z/100f);

 		if (left_controller.GetComponent<VRTK_ControllerEvents>().gripPressed)
        {
        	yRotation += 5.0f;
        	// transform.eulerAngles = new Vector3(0, yRotation, 0);
        	transform.localRotation = Quaternion.Euler(0, yRotation, 0);
        }




        if (right_controller.GetComponent<VRTK_ControllerEvents>().triggerTouched)
        {
        	RaycastHit hit = pointer.pointerRenderer.GetDestinationHit();

            if (right_controller.GetComponent<VRTK_ControllerEvents>().triggerPressed)
            {
            	if(hit.transform.gameObject.CompareTag("MiniMap"))
            	{
            		hit_point.transform.position = left_controller.transform.worldToLocalMatrix.MultiplyPoint3x4(hit.point);
                     // hit_point.transform.position = left_controller.transform.InverseTransformPoint(hit.point);
                    hit_point.transform.position = Quaternion.Euler(0, -yRotation, 0) * hit_point.transform.position;
                   
            		dist_mini = Vector3.Distance(new Vector3(0,0,0), hit_point.transform.position);

            		// Debug.Log("!!!!!!"+dist_mini);


            		if(dist_mini <= 0.15f)
            		{
            			teleporter.ForceTeleport(new Vector3(hit_point.transform.position.x * 333, 0, hit_point.transform.position.z * 333),Quaternion.Euler(new Vector3(0, 0, 0)));
            		}
            	}
            	
            }
        }
    }

    void CustomButton_onClick()
    {
        boolean = ! boolean;
    }
}
