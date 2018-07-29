using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,
    IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    // item은 Inventory 클래스의 AddItem() 함수에서 초기화해준다.
    public int amount; // 개수
    public int slot; // Inventory클래스의 AddItem()에서 초기화 해준다.
    // 현재 아이템이 들어 있는 슬롯번호/슬롯ID를 의미한다.
    private Inventory inv;
    private Tooltip tooltip;
    private Vector2 offset;

    // Use this for initialization
    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        tooltip = inv.GetComponent<Tooltip>();
        // Inventory 게임오브젝트에 Tooltip 컴포넌트가 붙어있다.
    }

    // 마우스, 스마트폰 터치 전부 지원하는 함수다. 마우스나,
    // 터치하는 지점을 통틀어 Pointer라고 부른다.
    public void OnBeginDrag(PointerEventData eventData) // 드래그 시작시
    {
        //Debug.Log("OnBeginDrag");
        if(item != null)
        {
            offset = eventData.position - 
                new Vector2(this.transform.position.x, this.transform.position.y);
            this.transform.SetParent(this.transform.parent.parent);
            this.transform.position = eventData.position - offset;
            // 오프셋으로 처음 눌렀던 지점에서 그대로 드래그가 가능해진다.
            // 오프셋을 하지 않으면 무조건 Pointer 정중앙으로 아이콘이 움직여서
            // 조금 거슬리는 연출이 되버린다.
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            // 광선 발사를 막지 않는다.
        }
    }

    public void OnDrag(PointerEventData eventData) // 드래그 하고 있을 때
    {
        //Debug.Log("OnDrag");
        if (item != null)
        {
            this.transform.position = eventData.position - offset; 
        }
    }

    public void OnEndDrag(PointerEventData eventData) // 드래그하다가 마우스 버튼을 땔 때
    {
        //Debug.Log("OnEndDrag");
        if (item != null)
        {
           
            this.transform.SetParent(inv.slots[slot].transform);
            this.transform.position = inv.slots[slot].transform.position;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            // 광선 발사를 막는다.
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
        if(item != null)
        {
            offset = eventData.position -
                new Vector2(this.transform.position.x, this.transform.position.y);
        }
    }

    // 물체위에 마우스를 올려놨다.
    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.Activate(item);
    }

    // 물체위에서 마우스를 치웠다.
    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Deactivate();
    }
}
