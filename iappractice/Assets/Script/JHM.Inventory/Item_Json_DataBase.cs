using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class Item_Json_DataBase : MonoBehaviour {

    public static Item_Json_DataBase instance;

    public List<Items_Info> items_Base = new List<Items_Info>();

    private string mobile_Path;

    private TextAsset json_File;

    private JsonData load_Data;


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

    private void Start()
    {
        mobile_Path = Application.persistentDataPath;
        Item_Info_From_Json_Data_Parsing();
    }


    private void Item_Info_From_Json_Data_Parsing()
    {
        json_File = Resources.Load<TextAsset>("JHM.Resources.Json/Defualt_Json_Data/Inventory_Items");

        load_Data = JsonMapper.ToObject(json_File.text);

        for (int i = 0; i < load_Data.Count; i++)
        {
            items_Base.Add(new Items_Info((int)load_Data[i]["ID"],load_Data[i]["ITEM_NAME"].ToString(),load_Data[i]["ITEM_DESCRIPTION"].ToString(),
                (int)load_Data[i]["ITEM_VALUETYPE"],(int)load_Data[i]["ITEM_VALUE"],(int)load_Data[i]["ITEM_PRICE"],
                (int)load_Data[i]["ITEM_USETYPE"],(int)load_Data[i]["ITEM_EQUIPTYPE"],load_Data[i]["ITEM_SLUG"].ToString()));

            switch (items_Base[i].id)
            {
                case 30001:
                    {
                        items_Base[i].item_Img = Resources.Load<Sprite>("JHM.Img/" + items_Base[i].slug);
                        items_Base[i].stackable = false;
                    }break;
                case 30002:
                    {
                        items_Base[i].item_Img = Resources.Load<Sprite>("JHM.Img/" + items_Base[i].slug);
                        items_Base[i].stackable = true;
                    }
                    break;
                case 30003:
                    {
                        items_Base[i].item_Img = Resources.Load<Sprite>("JHM.Img/" + items_Base[i].slug);
                        items_Base[i].stackable = false;
                    }
                    break;
            }
        }
    }

    public Items_Info Search_For_Item(int _id)
    {
        for(int i = 0; i< items_Base.Count; i++)
        {
            if(items_Base[i].id == _id)
            {
                return items_Base[i];
            }
        }
        return null;
    }






} // class












