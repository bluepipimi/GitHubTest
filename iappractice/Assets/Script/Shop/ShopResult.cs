using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopResult : MonoBehaviour {

    public Text[] buyItems; // 3개가 들어간다.

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeScene()
    {
        SceneManager.LoadScene("Shop");
    }

    public void ShowInventory()
    {
        Debug.Log("DataTunnel의 아이템 개수 : " + DataTunnel.ItemInfos.Count);
        for(int i=0; i< DataTunnel.ItemInfos.Count; i++)
        {
            buyItems[i].text = DataTunnel.ItemInfos[i].id.ToString();

            //buyItems[i].text = DataTunnel.ItemInfos[i].name;
        }
    }
}
