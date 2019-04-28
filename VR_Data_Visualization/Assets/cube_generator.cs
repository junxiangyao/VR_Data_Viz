using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

using static DataManager;
using static HoverManager;


public class cube_generator : MonoBehaviour
{  
    public List<GameObject> cubes = new List<GameObject>();
    // public GameObject Pcube;

    /****************************************
        rendering 
    *****************************************/

    // main graphics
    public static float INNER_RADIUS = 4.0f; // 4 meter 
    public static float INCREASE = 0.00078f*6;
    public float current_radius = INNER_RADIUS;
    public DataManager dm; 
    static Material lineMaterial; // for all the lines


    /****************************************
        UI 
    *****************************************/
    ////////////////////////////////
    // world objects
    ////////////////////////////////
    public HoverManager hm;
    GameObject left_controller;
    GameObject right_controller;
    VRTK_Pointer pointer; // pointer from right hand
    GameObject play_area; // teleport play area
    VRTK_BasicTeleport teleporter; 
    GameObject player_world; // player in the world coordinate. This is actually the center of the play area, which will not change with the movement of the headset.
    GameObject base_world; // base in the world coorinate will show the real time location of the headset base (y = 0).

    GameObject [] year_circles;

    GameObject year_this;
    GameObject year_next;
    GameObject local_date;    
    GameObject year_this_text;
    GameObject year_next_text;
    GameObject local_date_text;

    GameObject label_node_data;
    GameObject label_node_news;

    float real_polar_angle;
    

    GameObject hover_label_movie;
    GameObject hover_label_movie_num;
    GameObject hover_label_movie_date;
    GameObject hover_label_movie_name;
    GameObject hover_label_news;
    /////////////////////////////
    // mini map
    /////////////////////////////
    GameObject mini_map;
    GameObject mini_map_out;
    GameObject player_marker; // marker on the mini map
    float rotation_counter;
    float yRotation = 0.0f; // rotation of the mini map
    float dist_mini = 0.0f;
    float dist_real = 0.0f;  
    float pos_polar_angle = 0.0f;  
    GameObject hit_point; // the point hit by pointer
    Coordinate mini_coordinate;

    GameObject canvas_label_mini;
    // GameObject canvas_label_pointer;

    GameObject [] month_label_mini;
    GameObject [] year_label_mini;
    string [] month_text = {"Jan.","Feb.","Mar.","Apr.","May.","Jun.","Jul.","Aug.","Sep.","Oct.","Nov.","Dec."};
    string [] title_short = {"Star Wars I", "Star Wars II", "Star Wars III", "Star Wars IV", "Star Wars V",
    "Star Wars VI", "Star Wars VII", "Star Wars VIII", "Rogue One", "Solo"};   
    string [] title_full = {"The Phantom Menace", "Attack of the Clones", "Revenge of the Sith", "A New Hope", "The Empire Strikes Back",
    "Return of the Jedi", "The Force Awakens", "The Last Jedi", "Rogue One", "Solo"};

    GameObject label_mini_pointer;
    GameObject label_mini_pointer_text;
    GameObject label_mini_pointer_line;





    // string[] year_text;

    int YEAR_COUNT = 14;
    ////////////////////////////////////
    // UI menu & buttons
    ////////////////////////////////////
    GameObject canvas_for_movies;
    Button b;
    ColorBlock color_buffer;


    // Start is called before the first frame update
    void Start()
    {
        /****************************************
            Loading data and draw main graphics



        *****************************************/
        int counter = 0;  
        string line;  

        dm = new DataManager();
        hm = new HoverManager(YEAR_COUNT);

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

        int pcounter = 0;
        for(int y = 0; y < YEAR_COUNT; ++y){
            for(int m = 0; m < 12; ++m){
                for(int d = 0; d < dm.MovieObjs[0].years[y].months[m].dayList.Count; ++d){
                    // hm.years[y].months[m].addDay(new HoverDay());
                    hm.years[y].months[m].addDay(new HoverDay(dm.MovieObjs[0].years[y].months[m].dayList[d].data.position));
                    for(int i = 0; i < 10; ++i){
                        // if(dm.MovieObjs[i].years[y].months[m].dayList[d].data.check_out_times > 0){
                            hm.years[y].months[m].day_list[d].addMovie(dm.movie_colors[i], 
                            dm.MovieObjs[i].years[y].months[m].dayList[d].data.check_out_times,
                            i,
                            dm.MovieObjs[i].years[y].months[m].dayList[d].data.position,
                            y,m,d);
                        // }
                    }
                    for(int dt = 0; dt < hm.years[y].months[m].day_list[d].data_list.Count; ++dt){
                        if(pcounter < hm.years[y].months[m].day_list[d].data_list[dt].movie_count){
                            pcounter = hm.years[y].months[m].day_list[d].data_list[dt].movie_count;
                        }
                    }
                    hm.years[y].months[m].day_list[d].drawHoverObjs();
                    hm.years[y].months[m].day_list[d].daily_hover_obj.SetActive(false);
                }
                hm.years[y].months[m].set_parent();
            }
        }
        

        MetaData md = dm.getData(3,2012,2,29);
        Debug.Log("md.check_out_times = " + md.check_out_times);

        dm.drawDate();
        dm.drawData();

        string line_sw;
        System.IO.StreamReader file_news_sw = new System.IO.StreamReader(@"Assets/seattle_times_news_sw.csv");  
        while((line_sw = file_news_sw.ReadLine()) != null)  
        {  
            
            string[] splits_sw = line_sw.Split(new[] { ',' });
            // Debug.Log("@@@" + splits_sw[3]);
        }  
        file_news_sw.Close();
        // System.IO.StreamReader file_news_spl = new System.IO.StreamReader(@"Assets/seattle_times_news_spl.csv"); 
        // while((line = file_news_spl.ReadLine()) != null)  
        // {  
        //     int[] splits = parseLine(line);
        // }   
        // file_news_spl.Close();
        //Test Boxes
        // Color c = new Color(0.75f,0.75f,0.75f);
        // for(int mv = 0; mv < 12; ++mv){
        //     for(int y = 0; y < 14; ++y){
        //         for(int m = 0; m < 12; ++m){
        //             for(int d = 0; d < dm.MovieObjs[mv].years[y].months[m].dayList.Count; ++d){
        //                 Pcube = generate_cube(dm.MovieObjs[mv].years[y].months[m].dayList[d].data.position, c);
        //             }
        //         }
        //     }
        // }

        Debug.Log("Current radius: " + current_radius);


        /****************************************
            UI initialization


        *****************************************/

        Font arial;
        arial = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf"); // Load the Arial font from the Unity Resources folder.

        /////////////////////////////////////////
        // world objects
        ////////////////////////////////////////
        right_controller = GameObject.FindGameObjectWithTag("ControllerRight");
        left_controller = GameObject.FindGameObjectWithTag("ControllerLeft");
        pointer = right_controller.GetComponent<VRTK_Pointer>();
        play_area = GameObject.FindGameObjectWithTag("Teleport");
        player_world = GameObject.Find("[VRTK_SDKSetups]").transform.GetChild(3).GetChild(0).gameObject;
        base_world = GameObject.Find("[VRTK_SDKSetups]").transform.GetChild(3).GetChild(1).GetChild(0).gameObject;
        teleporter = play_area.GetComponent<VRTK_BasicTeleport>(); 
        year_circles = new GameObject[15];
        //-----------------------------------------circles-------------------------------------
        for(int i = 0; i < 15; ++i){
            if(i == 14){
                year_circles[i] = generate_circle(current_radius + INCREASE, 400, new Color(180 * 1.0f/255, 180 * 1.0f/255, 180 * 1.0f/255));
            }else{
                year_circles[i] = generate_circle(dm.getData(0,2005 + i,1,1).radius, 100 + i * 20, new Color(180 * 1.0f/255, 180 * 1.0f/255, 180 * 1.0f/255));       
            }
        }
        //------------------------------------------ lables on the floor --------------------------------------
        label_node_data = new GameObject();
        label_node_news = new GameObject();

        year_this = GameObject.Find("ThisYear");
        year_next = GameObject.Find("NextYear");
        local_date = GameObject.Find("CurrentDate");

        year_this_text = new GameObject();
        year_this_text.AddComponent<Text>();
        year_this_text.GetComponent<Text>().text = "2000";        
        year_this_text.GetComponent<Text>().font = arial;
        year_this_text.GetComponent<Text>().fontSize = 32;
        year_this_text.GetComponent<Text>().fontStyle = FontStyle.Normal;
        year_this_text.GetComponent<Text>().color = Color.white;
        year_this_text.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        year_this_text.transform.SetParent(year_this.transform);
        // Provide Text position and size using RectTransform.
        RectTransform rectTransform_this;
        rectTransform_this = year_this_text.GetComponent<Text>().GetComponent<RectTransform>();
        rectTransform_this.localPosition = new Vector3(0,0f,-0.01f);
        rectTransform_this.transform.localScale = new Vector3(0.004f,0.004f,0.004f);
        rectTransform_this.sizeDelta = new Vector2(160,100);
        year_this_text.transform.eulerAngles = new Vector3(90, 0, 0);



        year_next_text = new GameObject();
        year_next_text.AddComponent<Text>();
        year_next_text.GetComponent<Text>().text = "2001";        
        year_next_text.GetComponent<Text>().font = arial;
        year_next_text.GetComponent<Text>().fontSize = 32;
        year_next_text.GetComponent<Text>().fontStyle = FontStyle.Normal;
        year_next_text.GetComponent<Text>().color = Color.white;
        year_next_text.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        year_next_text.transform.SetParent(year_next.transform);
        // Provide Text position and size using RectTransform.
        RectTransform rectTransform_next;
        rectTransform_next = year_next_text.GetComponent<Text>().GetComponent<RectTransform>();
        rectTransform_next.localPosition = new Vector3(0,0f,-0.01f);
        rectTransform_next.transform.localScale = new Vector3(0.004f,0.004f,0.004f);
        rectTransform_next.sizeDelta = new Vector2(160,100);
        year_next_text.transform.eulerAngles = new Vector3(90, 0, 0);


        local_date_text = new GameObject();
        local_date_text.AddComponent<Text>();
        local_date_text.GetComponent<Text>().text = "Jan.31";        
        local_date_text.GetComponent<Text>().font = arial;
        local_date_text.GetComponent<Text>().fontSize = 32;
        local_date_text.GetComponent<Text>().fontStyle = FontStyle.Normal;
        local_date_text.GetComponent<Text>().color = Color.white;
        local_date_text.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        local_date_text.transform.SetParent(local_date.transform);
        // Provide Text position and size using RectTransform.
        RectTransform rectTransform_local;
        rectTransform_local = local_date_text.GetComponent<Text>().GetComponent<RectTransform>();
        rectTransform_local.localPosition = new Vector3(0,0.5f,-0.01f);
        rectTransform_local.transform.localScale = new Vector3(0.01f,0.01f,0.01f);
        rectTransform_local.sizeDelta = new Vector2(160,100);
        local_date_text.transform.eulerAngles = new Vector3(90, 0, 0);
        local_date.transform.SetParent(base_world.transform);

        //--------------------------------------------------------pointer hovering-----------------------------------------------
        hover_label_movie = GameObject.Find("HoverLabelMovie");
        hover_label_movie_num = new GameObject();
        hover_label_movie_num.AddComponent<Text>();
        hover_label_movie_num.GetComponent<Text>().text = "0";        
        hover_label_movie_num.GetComponent<Text>().font = arial;
        hover_label_movie_num.GetComponent<Text>().fontSize = 32;
        hover_label_movie_num.GetComponent<Text>().fontStyle = FontStyle.Normal;
        hover_label_movie_num.GetComponent<Text>().color = Color.white;
        hover_label_movie_num.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        hover_label_movie_num.transform.SetParent(hover_label_movie.transform);
        // Provide Text position and size using RectTransform.
        RectTransform rectTransform_num;
        rectTransform_num = hover_label_movie_num.GetComponent<Text>().GetComponent<RectTransform>();
        rectTransform_num.localPosition = new Vector3(-0.1f,0f,-0.01f);
        rectTransform_num.transform.localScale = new Vector3(0.004f,0.004f,0.004f);
        rectTransform_num.sizeDelta = new Vector2(160,100);
 
        hover_label_movie_date = new GameObject();
        hover_label_movie_date.AddComponent<Text>();
        hover_label_movie_date.GetComponent<Text>().text = "Jan.31.2005";        
        hover_label_movie_date.GetComponent<Text>().font = arial;
        hover_label_movie_date.GetComponent<Text>().fontSize = 12;
        hover_label_movie_date.GetComponent<Text>().fontStyle = FontStyle.Normal;
        hover_label_movie_date.GetComponent<Text>().color = Color.white;
        hover_label_movie_date.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        hover_label_movie_date.transform.SetParent(hover_label_movie.transform);
        // Provide Text position and size using RectTransform.
        RectTransform rectTransform_date;
        rectTransform_date = hover_label_movie_date.GetComponent<Text>().GetComponent<RectTransform>();
        rectTransform_date.localPosition = new Vector3(0.14f,0.1f,-0.01f);
        rectTransform_date.transform.localScale = new Vector3(0.004f,0.004f,0.004f);
        rectTransform_date.sizeDelta = new Vector2(160,100);


        hover_label_movie_name = new GameObject();
        hover_label_movie_name.AddComponent<Text>();
        hover_label_movie_name.GetComponent<Text>().text = "Star Wars VIII";        
        hover_label_movie_name.GetComponent<Text>().font = arial;
        hover_label_movie_name.GetComponent<Text>().fontSize = 16;
        hover_label_movie_name.GetComponent<Text>().fontStyle = FontStyle.Normal;
        hover_label_movie_name.GetComponent<Text>().color = Color.black;
        hover_label_movie_name.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
        hover_label_movie_name.transform.SetParent(hover_label_movie.transform);
        // Provide Text position and size using RectTransform.
        RectTransform rectTransform_name;
        rectTransform_name = hover_label_movie_name.GetComponent<Text>().GetComponent<RectTransform>();
        rectTransform_name.localPosition = new Vector3(1.204f,0f,-0.01f);
        rectTransform_name.transform.localScale = new Vector3(0.004f,0.004f,0.004f);
        rectTransform_name.sizeDelta = new Vector2(600,100);


        // hover_label_news = GameObject.Find("HoverLabelNews");

        ///////////////////////////////////////
        // mini map
        //////////////////////////////////////
        mini_map = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        mini_map.transform.localScale = new Vector3(0.3f,0.01f,0.3f);
        mini_map.GetComponent<Renderer>().material.color = new Color(56 * 1.0f/255, 48 * 1.0f/255, 60 * 1.0f/255);
        mini_map.transform.SetParent(left_controller.transform); // set the left controller to be the parent instead of mini map because mini map is scaled, and its local coordinate is distorted. 
        mini_map.transform.localPosition = new Vector3(0,0.02f,0);
        Destroy(mini_map.GetComponent<CapsuleCollider>());
        mini_map.AddComponent<BoxCollider>();
        // out line
        mini_map_out = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        mini_map_out.transform.localScale = new Vector3(0.34f,0.01f,0.34f);
        mini_map_out.GetComponent<Renderer>().material.color = new Color(56 * 1.0f/255, 48 * 1.0f/255, 60 * 1.0f/255);
        mini_map_out.transform.SetParent(mini_map.transform);
        mini_map_out.transform.localPosition = new Vector3(0,0.0f,0);
        Destroy(mini_map_out.GetComponent<CapsuleCollider>());

        //--------------------------------------------- graphics on mini map ----------------------------------------
        dm.drawDateMini();
        dm.drawDataMini();
        dm.mini_object.transform.localPosition = new Vector3(0,1f,0);
        dm.mini_object.transform.SetParent(mini_map.transform, false);

        //---------------------------------------------- marker on mini map ------------------------------------------
        player_marker = GameObject.CreatePrimitive(PrimitiveType.Cube);
        player_marker.transform.localScale = new Vector3(0.004f,0.1f,0.004f);
        player_marker.transform.SetParent(mini_map.transform);
        player_marker.transform.localPosition = new Vector3(0,5f,0); // impacted by the scaling of its parent
        hit_point = new GameObject();

        //------------------------------------------------ menus and buttons ------------------------------------------
        canvas_for_movies = GameObject.Find("Canvas");
        canvas_for_movies.transform.SetParent(left_controller.transform, true);
        b = canvas_for_movies.transform.GetChild(1).gameObject.GetComponent<Button>();
        b.onClick.AddListener(CustomButton_onClick);
        color_buffer = new ColorBlock();

        //--------------------------------------------------- labels -------------------------------------------------------
        canvas_label_mini = GameObject.Find("LabelMini");
        canvas_label_mini.GetComponent<Collider>().isTrigger = true;
        canvas_label_mini.transform.SetParent(mini_map.transform, false);

        month_label_mini = new GameObject[12]; 
        year_label_mini = new GameObject[7];

        //month labels
        for(int i = 0; i < 12; ++i){
            month_label_mini[i] = new GameObject();
            month_label_mini[i].AddComponent<Text>();
            month_label_mini[i].GetComponent<Text>().text = month_text[i];        
            month_label_mini[i].GetComponent<Text>().font = arial;
            month_label_mini[i].GetComponent<Text>().fontSize = 14;
            month_label_mini[i].GetComponent<Text>().fontStyle = FontStyle.Normal;
            month_label_mini[i].GetComponent<Text>().color = Color.white;
            month_label_mini[i].GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            month_label_mini[i].transform.SetParent(canvas_label_mini.transform);
            // Provide Text position and size using RectTransform.
            RectTransform rectTransform;
            rectTransform = month_label_mini[i].GetComponent<Text>().GetComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(0.52f * Mathf.Sin((15 + 30 * i) * Mathf.PI / 180),0.52f * Mathf.Cos((15 + 30 * i) * Mathf.PI / 180),-1.1f);
            rectTransform.localScale = new Vector3(0.002f,0.002f,1f);
            rectTransform.sizeDelta = new Vector2(160,100);
            month_label_mini[i].transform.eulerAngles = new Vector3(90, 15 + 30 * i, 0);
        }

        // year labels
        int startYear = 2005;
        for(int i = 0; i < 7; ++i){
            year_label_mini[i] = new GameObject();
            year_label_mini[i].AddComponent<Text>();
            year_label_mini[i].GetComponent<Text>().text = startYear.ToString();        
            year_label_mini[i].GetComponent<Text>().font = arial;
            year_label_mini[i].GetComponent<Text>().fontSize = 14;
            year_label_mini[i].GetComponent<Text>().fontStyle = FontStyle.Normal;
            year_label_mini[i].GetComponent<Text>().color = Color.white;
            year_label_mini[i].GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            year_label_mini[i].transform.SetParent(canvas_label_mini.transform);
            // Provide Text position and size using RectTransform.
            RectTransform rectTransform;
            rectTransform = year_label_mini[i].GetComponent<Text>().GetComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(0, 0.08f + i * 0.057f,-1.1f);
            rectTransform.localScale = new Vector3(0.0014f,0.0014f,1f);
            rectTransform.sizeDelta = new Vector2(160,100);
            year_label_mini[i].transform.eulerAngles = new Vector3(90, 0, 0);
            startYear += 2;
        }

        // draw coordinate lines
        mini_coordinate = new Coordinate(0.0003f);
        mini_coordinate.coordinate_object.transform.SetParent(mini_map.transform);
        mini_coordinate.coordinate_object.transform.localPosition = new Vector3(0,0,0);
        // mini_coordinate.drawCoordinate((current_radius+INCREASE)/200f);
        mini_coordinate.drawCoordinate(34/200f); // line renderer is using world scale https://www.reddit.com/r/Unity3D/comments/6p8tis/line_renderer_start_position_problem/
        for(int i = 0; i < 14; ++i){
            mini_coordinate.drawCircle(mini_coordinate.circles[i], dm.getData(0,2005 + i,1,1).hit_radius, 80, Color.white);
        }
        mini_coordinate.drawCircle(mini_coordinate.circles[14], dm.getData(0,2018,12,31).hit_radius+INCREASE/60f, 80, Color.white);
        mini_coordinate.drawCircle(mini_coordinate.outer_circle, 34/200f , 80, Color.white);
        mini_coordinate.drawCircle(mini_coordinate.inner_circle, 29/200f , 80, Color.white);
        
        // hover effect: date 
        label_mini_pointer = GameObject.Find("LabelMiniPointer");
        label_mini_pointer.transform.SetParent(left_controller.transform, false);
        label_mini_pointer.SetActive(false);
        label_mini_pointer_text = new GameObject();
        label_mini_pointer_text.AddComponent<Text>();
        label_mini_pointer_text.GetComponent<Text>().text = "!!!!!!!!!!!!!!!!!!!S";        
        label_mini_pointer_text.GetComponent<Text>().font = arial;
        label_mini_pointer_text.GetComponent<Text>().fontSize = 14;
        label_mini_pointer_text.GetComponent<Text>().fontStyle = FontStyle.Normal;
        label_mini_pointer_text.GetComponent<Text>().color = Color.white;
        label_mini_pointer_text.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        label_mini_pointer_text.transform.SetParent(label_mini_pointer.transform);
        RectTransform rectTransform_lable_mini_pointer;
        rectTransform_lable_mini_pointer = label_mini_pointer_text.GetComponent<Text>().GetComponent<RectTransform>();
        rectTransform_lable_mini_pointer.localPosition = new Vector3(0, 0, 0);
        rectTransform_lable_mini_pointer.localScale = new Vector3(0.001f,0.001f,0.001f);
        rectTransform_lable_mini_pointer.sizeDelta = new Vector2(160,100);
        label_mini_pointer_line = new GameObject();
        label_mini_pointer_line = generate_line(new Vector3(0,0.1f,0), new Vector3(0,0.285f,0), Color.black);
        label_mini_pointer_line.transform.SetParent(label_mini_pointer.transform);
            
        // Debug.Log("try:" + hm.years[3].months[0].day_list[1].data_list[0].hover_obj.GetComponent<InfoCube>().c_out);
        // Debug.Log("try:" + hm.years[3].months[0].day_list[1].data_list[0].hover_obj.GetComponent<InfoCube>().index[0]);
        // Debug.Log("try:" + hm.years[3].months[0].day_list[1].data_list[1].hover_obj.GetComponent<InfoCube>().c_out);
        // Debug.Log("try:" + hm.years[3].months[0].day_list[1].data_list[1].hover_obj.GetComponent<InfoCube>().index[0]);
        // Debug.Log("try:" + hm.years[3].months[0].day_list[1].data_list[2].hover_obj.GetComponent<InfoCube>().c_out);
        // Debug.Log("try:" + hm.years[3].months[0].day_list[1].data_list[2].hover_obj.GetComponent<InfoCube>().index[0]);        
        // Debug.Log("try:" + hm.years[3].months[0].day_list[1].data_list[3].hover_obj.GetComponent<InfoCube>().c_out);
        // Debug.Log("try:" + hm.years[3].months[0].day_list[1].data_list[3].hover_obj.GetComponent<InfoCube>().index[0]);        
        // Debug.Log("try:" + hm.years[3].months[0].day_list[1].data_list[1].hover_obj.GetComponent<InfoCube>().c_out);
        // Debug.Log("try:" + hm.years[3].months[0].day_list[1].data_list[1].hover_obj.GetComponent<InfoCube>().index[1]);
        // Debug.Log("try:" + hm.years[3].months[0].day_list[1].data_list[0].hover_obj.GetComponent<InfoCube>().index[2]);
        // Debug.Log("C:"+pcounter);
        // Debug.Log("45:"+Mathf.Atan2(1, 1) * Mathf.Rad2Deg);
        // Debug.Log("135:"+Mathf.Atan2(1, -1) * Mathf.Rad2Deg);
        // Debug.Log("225:"+Mathf.Atan2(-1, -1) * Mathf.Rad2Deg);
        // Debug.Log("315:"+Mathf.Atan2(-1, 1) * Mathf.Rad2Deg);
        // Debug.Log("2005/2 angle:"+ dm.getData(0,2005,2,1).angle_radians * Mathf.Rad2Deg);
        // Debug.Log("2006/2 angle:"+ dm.getData(0,2006,2,1).angle_radians * Mathf.Rad2Deg);
        // Debug.Log("2006/2 angle:"+ dm.getData(0,2006,5,1).angle_radians * Mathf.Rad2Deg);
        // Debug.Log("2006/2 angle:"+ dm.getData(0,2006,8,1).angle_radians * Mathf.Rad2Deg);
        // Debug.Log("2006/2 angle:"+ dm.getData(0,2006,11,1).angle_radians * Mathf.Rad2Deg);

    }

    // Update is called once per frame
    void Update()
    {
        local_date.SetActive(false);
        year_this.SetActive(false);
        year_next.SetActive(false);
        // Debug.Log("base:"+base_world.transform.position);
        int current_year = 0;
        int current_month = 0;
        int current_day = 0;
        dist_real = Vector2.Distance(new Vector2(0,0),new Vector2(base_world.transform.position.x, base_world.transform.position.z));
        if(dist_real >= dm.getData(0,2018,1,1).radius && dist_real <= current_radius + INCREASE){
            current_year = 2018;
        }else{
            for(int i = 0; i < 13; ++i){
                if(dist_real >= dm.getData(0,2005 + i,1,1).radius && dist_real < dm.getData(0,2005 + i + 1,1,1).radius)
                {
                    current_year = 2005 + i;
                    // Debug.Log(2005+i);
                    break;
                }
            }        
        }
        real_polar_angle = Mathf.Atan2(base_world.transform.position.x, base_world.transform.position.z) * Mathf.Rad2Deg;
        if(real_polar_angle < 0){real_polar_angle += 360;}
        // Debug.Log("angle: " + pos_polar_angle);
        if(current_year != 0){
            local_date.SetActive(true);
            if(real_polar_angle >= dm.getData(0,current_year,12,1).angle_radians * Mathf.Rad2Deg)
            {
                current_month = 12;
            }else{
                for(int i = 0; i < 11; ++i){
                    if(real_polar_angle >= dm.getData(0,current_year,i+1,1).angle_radians * Mathf.Rad2Deg && real_polar_angle < dm.getData(0,current_year,i+2,1).angle_radians * Mathf.Rad2Deg)
                    {
                        current_month = i+1;
                        break;
                    }
                }
            }
            for(int i = 0; i < dm.MovieObjs[0].years[current_year-2005].months[current_month-1].day_count - 1; ++i)
            {
                if(real_polar_angle >= dm.getData(0,current_year,current_month,i + 1).angle_radians * Mathf.Rad2Deg && 
                real_polar_angle < dm.getData(0,current_year,current_month,i + 2).angle_radians * Mathf.Rad2Deg)
                {
                    current_day = i+1;
                    break;
                }else{
                    current_day = dm.MovieObjs[0].years[current_year-2005].months[current_month-1].day_count;
                }
            }
            local_date_text.GetComponent<Text>().text = month_text[current_month-1] + " " + current_day + " " + current_year;
            year_this.SetActive(true);
            year_this_text.GetComponent<Text>().text = "" + current_year;
            year_this.transform.localPosition = new Vector3(dm.getData(0,current_year,1,1).radius * Mathf.Sin(dm.getData(0,current_year,current_month,current_day).angle_radians),
                0.01f,
                dm.getData(0,current_year,1,1).radius * Mathf.Cos(dm.getData(0,current_year,current_month,current_day).angle_radians));
            year_this.transform.localRotation = Quaternion.Euler(90, dm.getData(0,current_year,current_month,current_day).angle_radians * Mathf.Rad2Deg, 0);
            if(current_year != 2018){
                year_next.SetActive(true);
                year_next_text.GetComponent<Text>().text = "" + (current_year + 1);
                year_next.transform.localPosition = new Vector3(dm.getData(0,current_year + 1,1,1).radius * Mathf.Sin(dm.getData(0,current_year + 1,current_month,current_day).angle_radians),
                0.01f,
                dm.getData(0,current_year + 1,1,1).radius * Mathf.Cos(dm.getData(0,current_year + 1,current_month,current_day).angle_radians));
                year_next.transform.localRotation = Quaternion.Euler(90, dm.getData(0,current_year + 1,current_month,current_day).angle_radians * Mathf.Rad2Deg, 0);
            } 
            hm.unmute(current_year, current_month, current_day);

            // controlling
            // for(int y = 0; y < 14; ++y){
            //     hm.years[y].year_obj.SetActive(dm.show_years[y]);
            //     for(int m = 0; m < 12; ++m){
            //         hm.years[y].months[m].month_hover_obj.SetActive(dm.show_months[m]);
            //         if(hm.years[y].months[m].month_hover_obj.active && hm.years[y].months[m].should_draw){ // if this month is active
            //             for(int d = 0; d < hm.years[y].months[m].day_list.Count; ++d){
            //                 for(int dt = 0; dt < hm.years[y].months[m].day_list[d].data_list.Count; ++dt){
            //                     if(hm.years[y].months[m].day_list[d].data_list[dt].movie_count == 1){
            //                         hm.years[y].months[m].day_list[d].data_list[dt].hover_obj.SetActive(dm.show_movies[hm.years[y].months[m].day_list[d].data_list[dt].movie_index[0]]);
            //                     }

            //                     if(hm.years[y].months[m].day_list[d].data_list[dt].movie_count > 1){
            //                         for(int i = 0; i < hm.years[y].months[m].day_list[d].data_list[dt].movie_count; ++i){
            //                             if(dm.show_movies[hm.years[y].months[m].day_list[d].data_list[dt].movie_index[i]] == true){
            //                                 hm.years[y].months[m].day_list[d].data_list[dt].sharing_counter++;  
            //                                 hm.years[y].months[m].day_list[d].data_list[dt].hover_obj.GetComponent<Renderer>().material.color = 
            //                                 dm.movie_colors[hm.years[y].months[m].day_list[d].data_list[dt].movie_index[i]];         
            //                             }
            //                         }
            //                         if(hm.years[y].months[m].day_list[d].data_list[dt].sharing_counter == 1){
            //                             hm.years[y].months[m].day_list[d].data_list[dt].hover_obj.SetActive(true);
            //                         }else if(hm.years[y].months[m].day_list[d].data_list[dt].sharing_counter == 0){
            //                             hm.years[y].months[m].day_list[d].data_list[dt].hover_obj.SetActive(false);
            //                         }else{
            //                             hm.years[y].months[m].day_list[d].data_list[dt].hover_obj.SetActive(true);
            //                             hm.years[y].months[m].day_list[d].data_list[dt].hover_obj.GetComponent<Renderer>().material.color = 
            //                                 hm.years[y].months[m].day_list[d].data_list[dt].hover_color; 
            //                         }
            //                         // hm.years[y].months[m].day_list[d].data_list[dt].hover_obj.SetActive(dm.show_movies[hm.years[y].months[m].day_list[d].data_list[dt].movie_index[0]]);
            //                     }
            //                 }

            //             // for(int mv = 0; mv < 10; ++mv){
            //             //     
            //             // }
            //             }
            //         }
            //     }
            // }
        }



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

        if (Input.GetKeyDown(KeyCode.M))
        {
            // for(int mv = 0; mv < 12; ++mv)
            // {
                dm.show_movies[10] = !dm.show_movies[10]; 
            // }
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

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            dm.show_movies[3] = !dm.show_movies[3]; 
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            dm.show_movies[4] = !dm.show_movies[4]; 
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            dm.show_movies[5] = !dm.show_movies[5]; 
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




        // for(int mv = 0; mv < 12; ++mv)
        // {
        //     for(int m = 0 ; m < 12; ++m){
        //         dm.MovieObjs[mv].date_lines_month[m].SetActive(dm.show_months[m]);
        //     }
        // }


        /*************************************************
            movie, year, month level show/hide control
        **************************************************/

        for(int mv = 0; mv < 12; ++mv)
        {
            // show/ hide movie
            dm.MovieObjs[mv].game_object.SetActive(dm.show_movies[mv]);
            dm.MovieObjs[mv].mini_game_object.SetActive(dm.show_movies[mv]); 
            for(int y = 0; y < 14; ++y)
            {
                // show / hide year
                dm.MovieObjs[mv].years[y].year_game_object.SetActive(dm.show_years[y]); 
                dm.MovieObjs[mv].years[y].mini_year_game_object.SetActive(dm.show_years[y]); 
                for(int m = 0; m < 12; ++m)
                {
                
                    // show / hide month
                    dm.MovieObjs[mv].years[y].months[m].month_data_line.SetActive(dm.show_months[m]);  // main graphic
                    dm.MovieObjs[mv].years[y].months[m].month_data_wall.SetActive(dm.show_months[m]&&dm.show_wall);
                    dm.MovieObjs[mv].date_lines_month[m].SetActive(dm.show_months[m]&&dm.show_date_lines);

                    dm.MovieObjs[mv].years[y].months[m].mini_month_data_line.SetActive(dm.show_months[m]);  // mini graphic
                    dm.MovieObjs[mv].mini_date_lines_month[m].SetActive(dm.show_months[m]&&dm.show_date_lines);
                    if(m < 11)
                    {
                        // if next month won't be drawn
                        if(!dm.show_months[m+1])
                        {
                            dm.MovieObjs[mv].years[y].months[m].connection_to_next.SetActive(dm.show_months[m+1]);
                            dm.MovieObjs[mv].years[y].months[m].connection_to_next_wall.SetActive(dm.show_months[m+1]);
                            dm.MovieObjs[mv].years[y].months[m].mini_connection_to_next.SetActive(dm.show_months[m+1]);
                        }else{
                            dm.MovieObjs[mv].years[y].months[m].connection_to_next.SetActive(dm.show_months[m]);
                            dm.MovieObjs[mv].years[y].months[m].mini_connection_to_next.SetActive(dm.show_months[m]);
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

        if(current_year != 0){
            for(int y = 0; y < 14; ++y){
                hm.years[y].year_obj.SetActive(dm.show_years[y]);
                for(int m = 0; m < 12; ++m){
                    hm.years[y].months[m].month_hover_obj.SetActive(dm.show_months[m]);
                    if(hm.years[y].months[m].month_hover_obj.active && hm.years[y].months[m].should_draw){ // if this month is active based on boolean in dm and it should be shown based on location
                        for(int d = 0; d < hm.years[y].months[m].day_list.Count; ++d){
                            for(int dt = 0; dt < hm.years[y].months[m].day_list[d].data_list.Count; ++dt){
                                if(hm.years[y].months[m].day_list[d].data_list[dt].movie_count == 1){
                                    hm.years[y].months[m].day_list[d].data_list[dt].hover_obj.SetActive(dm.show_movies[hm.years[y].months[m].day_list[d].data_list[dt].movie_index[0]]);
                                }
                                // sharing points
                                else if(hm.years[y].months[m].day_list[d].data_list[dt].movie_count > 1){
                                    for(int i = 0; i < hm.years[y].months[m].day_list[d].data_list[dt].movie_count; ++i){
                                        if(dm.show_movies[hm.years[y].months[m].day_list[d].data_list[dt].movie_index[i]] == true){
                                            hm.years[y].months[m].day_list[d].data_list[dt].sharing_counter++;  
                                            hm.years[y].months[m].day_list[d].data_list[dt].hover_obj.GetComponent<Renderer>().material.color = 
                                            dm.movie_colors[hm.years[y].months[m].day_list[d].data_list[dt].movie_index[i]];         
                                        }
                                    }
                                    if(hm.years[y].months[m].day_list[d].data_list[dt].sharing_counter == 1){
                                        hm.years[y].months[m].day_list[d].data_list[dt].hover_obj.SetActive(true);
                                    }else if(hm.years[y].months[m].day_list[d].data_list[dt].sharing_counter == 0){
                                        hm.years[y].months[m].day_list[d].data_list[dt].hover_obj.SetActive(false);
                                    }else{
                                        hm.years[y].months[m].day_list[d].data_list[dt].hover_obj.SetActive(true);
                                        hm.years[y].months[m].day_list[d].data_list[dt].hover_obj.GetComponent<Renderer>().material.color = 
                                            hm.years[y].months[m].day_list[d].data_list[dt].hover_color; 
                                    }
                                    // hm.years[y].months[m].day_list[d].data_list[dt].hover_obj.SetActive(dm.show_movies[hm.years[y].months[m].day_list[d].data_list[dt].movie_index[0]]);
                                }
                                hm.years[y].months[m].day_list[d].data_list[dt].sharing_counter = 0;
                            }

                        // for(int mv = 0; mv < 10; ++mv){
                        //     
                        // }
                        }
                    }
                }
            }
            // Debug.Log(hm.years[0].months[11].day_list[27].data_list[0].sharing_counter);
        }


                // Debug.Log(dm.MovieObjs[0].years[0].months[0].mini_month_data_line.transform.position);

        /*************************************************
            menu updates
        *************************************************/
        color_buffer = b.colors;
        if(dm.show_date_lines){
            color_buffer.normalColor = Color.green;
        }else{
            color_buffer.normalColor = Color.red;
        };
        b.colors = color_buffer;


        /*************************************************
            mini map updates
        *************************************************/
        // dm.mini_object.transform.SetParent(mini_map.transform, true);
        label_mini_pointer.SetActive(false);
        // the location of player marker on the mini map will reflect player's location in the world coordinate.
        // player_marker.transform.localPosition = new Vector3(player_world.transform.position.x/60f,
        //     5f,
        //     player_world.transform.position.z/60f);        
        player_marker.transform.localPosition = new Vector3(base_world.transform.position.x/60f,
            5f,
            base_world.transform.position.z/60f);

        // mini map rotation
        if (left_controller.GetComponent<VRTK_ControllerEvents>().gripPressed)
        {
            yRotation += 5.0f;
            // transform.eulerAngles = new Vector3(0, yRotation, 0);
            mini_map.transform.localRotation = Quaternion.Euler(0, yRotation, 0);
        }

        Debug.Log("RRR" + base_world.transform.rotation.eulerAngles.y);

        /*************************************************
            pointer controls
        *************************************************/
        if (right_controller.GetComponent<VRTK_ControllerEvents>().triggerTouched)
        {
            RaycastHit hit = pointer.pointerRenderer.GetDestinationHit();
            // if(hit.transform.gameObject.CompareTag("MiniMap"))
            // if(GameObject.ReferenceEquals(hit.transform.gameObject, mini_map)||
            //         GameObject.ReferenceEquals(hit.transform.gameObject, mini_map_out))
            // Debug.Log("+++" + hit.transform.gameObject);
            if(hit.transform.gameObject.CompareTag("data_node")){
                
                hover_label_movie.transform.localPosition = new Vector3(
                    hit.transform.gameObject.transform.position.x,
                    hit.transform.gameObject.transform.position.y,
                    hit.transform.gameObject.transform.position.z);
                hover_label_movie.transform.localRotation = Quaternion.Euler(0, base_world.transform.rotation.eulerAngles.y, 0);
                hover_label_movie.transform.localPosition = hover_label_movie.transform.TransformPoint(new Vector3(0.03f, 0, -0.03f));
                hover_label_movie.GetComponent<Image>().color = hit.transform.gameObject.GetComponent<Renderer>().material.color;

                hover_label_movie_num.GetComponent<Text>().text = "" +  hit.transform.gameObject.GetComponent<InfoCube>().c_out;
                hover_label_movie_num.GetComponent<Text>().color = hit.transform.gameObject.GetComponent<Renderer>().material.color;

                hover_label_movie_date.GetComponent<Text>().text = month_text[hit.transform.gameObject.GetComponent<InfoCube>().mb] +
                (hit.transform.gameObject.GetComponent<InfoCube>().db + 1) + "." + (hit.transform.gameObject.GetComponent<InfoCube>().yb+2005);
                if(hm.years[hit.transform.gameObject.GetComponent<InfoCube>().yb].months[hit.transform.gameObject.GetComponent<InfoCube>().mb].day_list[hit.transform.gameObject.GetComponent<InfoCube>().db].data_list[hit.transform.gameObject.GetComponent<InfoCube>().id].movie_index.Count > 1){
                    hover_label_movie_name.GetComponent<Text>().text = "";
                    float extend_width = 0;
                    for(int i = 0; i < hm.years[hit.transform.gameObject.GetComponent<InfoCube>().yb].months[hit.transform.gameObject.GetComponent<InfoCube>().mb].day_list[hit.transform.gameObject.GetComponent<InfoCube>().db].data_list[hit.transform.gameObject.GetComponent<InfoCube>().id].movie_index.Count; ++i){
                        int multi_buffer = hm.years[hit.transform.gameObject.GetComponent<InfoCube>().yb].months[hit.transform.gameObject.GetComponent<InfoCube>().mb].day_list[hit.transform.gameObject.GetComponent<InfoCube>().db].data_list[hit.transform.gameObject.GetComponent<InfoCube>().id].movie_index[i];
                        if(dm.show_movies[multi_buffer]){
                            hover_label_movie_name.GetComponent<Text>().text += title_short[multi_buffer] + " ";
                            if(multi_buffer < 8){
                                extend_width += 0.34f + multi_buffer * 0.01f;
                            }else if(multi_buffer == 8){
                                extend_width += 0.36f;
                            }else{
                                extend_width += 0.3f;
                            }
                            hover_label_movie.GetComponent<RectTransform>().sizeDelta = new Vector2(extend_width, 0.1f);
                            hover_label_movie_num.GetComponent<RectTransform>().localPosition = new Vector3(-0.1f,0f,-0.01f);
                            hover_label_movie_date.GetComponent<RectTransform>().localPosition = new Vector3(0.136f,0.1f,-0.01f);
                            hover_label_movie_name.GetComponent<RectTransform>().localPosition = new Vector3(1.204f,0f,-0.01f);
                        } 
                    }
                }else{
                    int index_buffer = hm.years[hit.transform.gameObject.GetComponent<InfoCube>().yb].months[hit.transform.gameObject.GetComponent<InfoCube>().mb].day_list[hit.transform.gameObject.GetComponent<InfoCube>().db].data_list[hit.transform.gameObject.GetComponent<InfoCube>().id].movie_index[0]; 
                    hover_label_movie_name.GetComponent<Text>().text = title_short[index_buffer];
                    // hover_label_movie.GetComponent<RectTransform>().sizeDelta = new Vector2(0.6f, 0.1f);
                    if(index_buffer < 8){
                        float bg_width = 0.34f + index_buffer * 0.01f;
                        hover_label_movie.GetComponent<RectTransform>().sizeDelta = new Vector2(bg_width, 0.1f);
                    }else if(index_buffer ==8){
                        hover_label_movie.GetComponent<RectTransform>().sizeDelta = new Vector2(0.36f, 0.1f);
                    }else{
                        hover_label_movie.GetComponent<RectTransform>().sizeDelta = new Vector2(0.3f, 0.1f);
                    }
                    hover_label_movie_num.GetComponent<RectTransform>().localPosition = new Vector3(-0.1f,0f,-0.01f);
                    hover_label_movie_date.GetComponent<RectTransform>().localPosition = new Vector3(0.136f,0.1f,-0.01f);
                    hover_label_movie_name.GetComponent<RectTransform>().localPosition = new Vector3(1.204f,0f,-0.01f);

                }
                
                Debug.Log("!!!!!");
            };
            if(GameObject.ReferenceEquals(hit.transform.gameObject, mini_map))
            {
                hit_point.transform.position = left_controller.transform.worldToLocalMatrix.MultiplyPoint3x4(hit.point);
                label_mini_pointer.transform.localPosition = new Vector3(hit_point.transform.position.x, 0.2f ,hit_point.transform.position.z);
                // hit_point.transform.position = left_controller.transform.InverseTransformPoint(hit.point); // this line basically does the same thing as the line above
                hit_point.transform.position = Quaternion.Euler(0, -yRotation, 0) * hit_point.transform.position; // rotate back to get the correct hit point on the map
                // Debug.Log("x" + hit_point.transform.position.x);  
                // Debug.Log("y" + hit_point.transform.position.y);  
                // Debug.Log("z" + hit_point.transform.position.z);  
                dist_mini = Vector3.Distance(new Vector3(0,0.03f,0), hit_point.transform.position);
                // Debug.Log(dist_mini);

                if(dist_mini <= 0.145f) // if hit in the legit area
                {     

                    int hover_year = 0;
                    int hover_month = 0;
                    int hover_day = 0;
                    if(dist_mini >= dm.getData(0,2018,1,1).hit_radius && dist_mini <= dm.getData(0,2018,12,31).hit_radius)
                    {
                        hover_year = 2018;
                        // Debug.Log(2018);
                    }else{
                        for(int i = 0; i < 13; ++i){
                            if(dist_mini >= dm.getData(0,2005 + i,1,1).hit_radius && dist_mini < dm.getData(0,2005 + i + 1,1,1).hit_radius)
                            {
                                hover_year = 2005 + i;
                                // Debug.Log(2005+i);
                                break;
                            }
                        }
                    }

                    pos_polar_angle = Mathf.Atan2(hit_point.transform.position.x, hit_point.transform.position.z) * Mathf.Rad2Deg;
                    if(pos_polar_angle < 0){pos_polar_angle += 360;}
                    // Debug.Log("angle: " + pos_polar_angle);
                    if(hover_year != 0){
                        if(pos_polar_angle >= dm.getData(0,hover_year,12,1).angle_radians * Mathf.Rad2Deg)
                        {
                            hover_month = 12;
                            // Debug.Log(2018);
                        }else{
                            for(int i = 0; i < 11; ++i){
                                if(pos_polar_angle >= dm.getData(0,hover_year,i+1,1).angle_radians * Mathf.Rad2Deg && pos_polar_angle < dm.getData(0,hover_year,i+2,1).angle_radians * Mathf.Rad2Deg)
                                {
                                    hover_month = i+1;
                                    // Debug.Log(2005+i);
                                    break;
                                }
                            }
                        }


                        for(int i = 0; i < dm.MovieObjs[0].years[hover_year-2005].months[hover_month-1].day_count - 1; ++i)
                        {
                            if(pos_polar_angle >= dm.getData(0,hover_year,hover_month,i + 1).angle_radians * Mathf.Rad2Deg && 
                                pos_polar_angle < dm.getData(0,hover_year,hover_month,i + 2).angle_radians * Mathf.Rad2Deg)
                            {
                                hover_day = i+1;
                                // Debug.Log(2005+i);
                                break;
                            }else{
                                hover_day = dm.MovieObjs[0].years[hover_year-2005].months[hover_month-1].day_count;
                            }
                        }
                        Debug.Log("year: " + hover_year + "month: " + hover_month + "day: " + hover_day);
                        label_mini_pointer_text.GetComponent<Text>().text = month_text[hover_month-1] + " " + hover_day +". " + hover_year; 
                        label_mini_pointer.SetActive(true); 
                    }



                    // Debug.Log("!!!!!!"+dist_mini);
                    if (right_controller.GetComponent<VRTK_ControllerEvents>().triggerPressed)
                    {
                        teleporter.ForceTeleport(new Vector3(hit_point.transform.position.x * 333f * 0.6f, 0, hit_point.transform.position.z * 333f * 0.6f),Quaternion.Euler(new Vector3(0, 0, 0)));
                        // teleporter.ForceTeleport(new Vector3(30, 0, 0),Quaternion.Euler(new Vector3(0, 0, 0)));
                    }
                }
            }else{
                // label_mini_pointer.SetActive(false);
            }
        }
        



        if(current_year != 0){
            for(int y = 0; y < 14; ++y){
                for(int m = 0; m < 12; ++m){
                    if(hm.years[y].months[m].month_hover_obj.active && hm.years[y].months[m].should_draw){ // if this month is active based on boolean in dm and it should be shown based on location
                        for(int d = 0; d < hm.years[y].months[m].day_list.Count; ++d){
                            for(int dt = 0; dt < hm.years[y].months[m].day_list[d].data_list.Count; ++dt){
                                // hm.years[y].months[m].day_list[d].data_list[dt].sharing_counter = 0;
                            }
                        }
                    }
                }
            }
        }
        // /*************************************************
        //     ray cast control
        // **************************************************/
        
        // if (Input.GetMouseButtonDown(0))
        // {
        //     RaycastHit hit;
        //     // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));

        //     if (Physics.Raycast(ray, out hit))
        //     {
        //         if (hit.collider != null)
        //         {
        //             Debug.Log("Hit!");
        //             hit.transform.gameObject.GetComponent<Renderer>().material.color = new Color(Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));

        //         }
        //     }
        // }
    
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

    public GameObject generate_line(Vector3 start, Vector3 end, Color color)
    {
        GameObject line = new GameObject();
        LineRenderer line_renderer = line.AddComponent<LineRenderer>();
        line_renderer.material = new Material(Shader.Find("Sprites/Default"));
        line_renderer.widthMultiplier = 0.002f;
        line_renderer.sortingOrder = 1;
        line_renderer.positionCount = 2;
        line_renderer.useWorldSpace = false;
        line_renderer.startColor = color;
        line_renderer.endColor = color;
        line_renderer.SetPosition(0, start);
        line_renderer.SetPosition(1, end);
        return line;
    }    

    public GameObject generate_circle(float r, int resolution, Color color)
    {
        GameObject circle = new GameObject();
        LineRenderer line_renderer = circle.AddComponent<LineRenderer>();
        line_renderer.material = new Material(Shader.Find("Sprites/Default"));
        line_renderer.widthMultiplier = 0.004f;
        // line_renderer.sortingOrder = 1;
        if(resolution < 8){
            resolution = 8;
        }
        line_renderer.positionCount = resolution + 1;
        line_renderer.useWorldSpace = true;
        line_renderer.startColor = color;
        line_renderer.endColor = color;
        for(int i = 0; i < resolution + 1; ++i){
            line_renderer.SetPosition(i, new Vector3(r * Mathf.Sin(i * (Mathf.PI * 2) / resolution),0,r * Mathf.Cos(i * (Mathf.PI * 2) / resolution)));
        }
        return circle;
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


    void CustomButton_onClick()
    {
        dm.show_date_lines = !dm.show_date_lines;
    }

}
