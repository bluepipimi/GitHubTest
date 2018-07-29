using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Canvas : MonoBehaviour {

    public static Inventory_Canvas instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.transform.gameObject);
        }
    }

    public void Game_Object_Set_On()
    {
        this.gameObject.SetActive(true);
    }
    public void Game_Object_Set_Off()
    {
        this.gameObject.SetActive(false);
    }

} // class







