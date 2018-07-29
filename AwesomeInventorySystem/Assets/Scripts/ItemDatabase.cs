using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.UI;

public class ItemDatabase : MonoBehaviour { // Inventory와 같은 오브젝트에 들어있다.
    private List<Item> database = new List<Item>();
    private JsonData jsonItemData;
    private TextAsset itemData;

    public Text test;

    void Start()
    {
        //Debug.Log("Application.dataPath : " + Application.dataPath);
        // Debug.Log("Application.persistentDataPath : " + Application.persistentDataPath);
        //Item item = new Item(0,"Ball", 5);
        //database.Add(item);
        //Debug.Log(database[0].Title);

        // <PC 버전> - 유튜브 강좌에서 사용한 방법
        //jsonItemData = JsonMapper.ToObject(File.ReadAllText(
        //    Application.dataPath + "/StreamingAssets/Items.json"));
        // Application.dataPath는 PC버전은 되는데 안드로이드버전에서는 먹통이된다.

        // <Android 버전 : 방법 1>
        jsonItemData = JsonMapper.ToObject(File.ReadAllText(
            Application.persistentDataPath + "/Items.json"));
        // C:\Users\kktkk\AppData\LocalLow\DefaultCompany\AwesomeInv 에 접근해서
        // 직접 Items.json을 넣어줘야 된다.
        // PC에선 되는데 Android에서 안된다... 2번 이상 테스트 해봐도 안된다...

        // <Android 버전 : 방법 2>
        //itemData = Resources.Load<TextAsset>("Items"); // 맨앞 슬래쉬(/), 확장자 없다.
        //jsonItemData = JsonMapper.ToObject(itemData.text);
        // 성공...


        Debug.Log(jsonItemData.Count);
        test.text = jsonItemData.Count.ToString();
        ConstructItemDatabase();
        
        //Debug.Log(itemData[1]["title"]);
        Debug.Log(FetchItemByID(0).Description);
    }

    public Item FetchItemByID(int id) 
    {
        for(int i = 0; i < database.Count; i++) // database는 Item 클래스가 원소인 리스트이다.
        {
            if (database[i].ID == id) // database에서 원하는 id를 찾고 그 id에 해당하는 Item 을 꺼낸다.
                return database[i];
        }
        return null; // 없으면 null을 꺼낸다.
    }

    void ConstructItemDatabase()
    {
        for(int i =0; i<jsonItemData.Count; i++) // JsonData의 인덱스 개수만큼 반복해서 database 리스트에 넣는다.
        {
            database.Add(new Item((int)jsonItemData[i]["id"],
                jsonItemData[i]["title"].ToString(),(int)jsonItemData[i]["value"]
                ,(int)jsonItemData[i]["stats"]["power"],(int)jsonItemData[i]["stats"]["defence"],
                (int)jsonItemData[i]["stats"]["vitality"], jsonItemData[i]["description"].ToString(),
                (bool)jsonItemData[i]["stackable"],(int)jsonItemData[i]["rarity"],
                jsonItemData[i]["slug"].ToString()));
        }
    }
}

public class Item // 아이템 정보를 넣어두는 클래스이다.
{
    public int ID { get; set; }
    public string Title { get; set; }
    public int Value { get; set; }
    public int Power { get; set; }
    public int Defence { get; set; }
    public int Vitality { get; set; }
    public string Description { get; set; }
    public bool Stackable { get; set; }
    public int Rarity { get; set; }
    public string Slug { get; set; }
    public Sprite Sprite { get; set; }

    public Item(int id, string title, int value, int power,
        int defence, int vitality, string description,
        bool stackable, int rarity, string slug)
    {
        this.ID = id;
        this.Title = title;
        this.Value = value;
        this.Power = power;
        this.Defence = defence;
        this.Vitality = vitality;
        this.Description = description;
        this.Stackable = stackable;
        this.Rarity = rarity;
        this.Slug = slug;
        this.Sprite = Resources.Load<Sprite>("Sprites/Items/" + slug); // 확장자가 안붙는다. 
    }

    public Item() // 기본 생성자이다.
    {
        this.ID = -1; // ID가 -1이면 아이템이 없다는 뜻이다.
    }

}
