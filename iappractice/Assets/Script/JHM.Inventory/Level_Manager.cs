using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Manager : MonoBehaviour {

    

    public void Go_To_Game_Origin_Scene()
    {
        SceneManager.LoadScene("Game_Origin");
        Inventory_Canvas.instance.Game_Object_Set_Off();
    }

    public void Go_To_Inventory_Scene()
    {
        SceneManager.LoadScene("Game_Inventory");
        Inventory_Canvas.instance.Game_Object_Set_On();
        Inventory_Controller.instance.Add_Item(30001);
    }








} // class












