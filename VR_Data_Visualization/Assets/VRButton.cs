using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRButton 
{
	public GameObject button_obj;
	public GameObject button_text;
	public ColorBlock color_buffer;
	public Color off_color;

	public VRButton(Transform canvas_transform, Vector3 pos, Color on, Color highlight, Color off, string t, Font f){
		button_obj = new GameObject();
		button_obj.transform.SetParent(canvas_transform);


		color_buffer = new ColorBlock();


		button_obj.AddComponent<Image>();
		// button_obj.GetComponent<Image>().sprite = Resources.Load <Sprite> ("Sprites/UISprite");
		// button_obj.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Skin/UISprite.psd");


		color_buffer.normalColor = on;
		// off_color.normalColor = Color.red;
		color_buffer.highlightedColor = highlight;	
		color_buffer.colorMultiplier = 1f;
		off_color = off;

		button_obj.AddComponent<Button>();
		button_obj.GetComponent<Button>().colors = color_buffer;
		button_obj.GetComponent<Button>().interactable = true;
		// button_obj.GetComponent<Button>().targetGraphic = button_obj.GetComponent<Image>();


		button_obj.AddComponent<RectTransform>();
		button_obj.GetComponent<RectTransform>().localPosition = pos;
        button_obj.GetComponent<RectTransform>().sizeDelta = new Vector2(120f,24f);
        button_obj.GetComponent<RectTransform>().localScale = new Vector3(1f,1f,1f);
        // button_obj.GetComponent<InfoCube>().c_out = check_out;
        // button_obj.GetComponent<InfoCube>().index = movie_index;   



		button_text = new GameObject();
		button_text.AddComponent<Text>();
		button_text.GetComponent<Text>().text = t;
        button_text.GetComponent<Text>().font = f;
        button_text.GetComponent<Text>().fontSize = 16;
        button_text.GetComponent<Text>().fontStyle = FontStyle.Normal;
        button_text.GetComponent<Text>().color = Color.black;
        button_text.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
		button_text.transform.SetParent(button_obj.transform);
        // Provide Text position and size using RectTransform.
        RectTransform rectTrans;
        rectTrans = button_text.GetComponent<Text>().GetComponent<RectTransform>();
        rectTrans.localPosition = new Vector3(0,0,0);
        rectTrans.transform.localScale = new Vector3(1f,1f,1f);
        rectTrans.sizeDelta = new Vector2(120f,24f);
	}
    
}
