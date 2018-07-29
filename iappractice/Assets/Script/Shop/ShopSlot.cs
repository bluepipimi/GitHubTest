using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour {

    public Text infoTxt;
    public Text nameTxt;
    public Text priceTxt;
    public Image itemImage;

    public int id;
   
    private Shop shop;

    // Prefab을 Instantiate 하고 shop 클래스를 연결해준다.
    public void Start()
    {
        shop = GameObject.Find("Shop").GetComponent<Shop>();   
    }

    public void BuyItem()
    {
        shop.BuyItem(id);
    }
}
