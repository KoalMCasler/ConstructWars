using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject uICanvas;
    public Transform parentAfterDrag;
    public Image image;
    public GameObject player;
    public bool isEquipped;
    void Start()
    {
        image = this.GetComponent<Image>();
        uICanvas = GameObject.FindWithTag("UI");
        player = GameObject.FindWithTag("Player");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(uICanvas.transform);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
        if(this.GetComponent<InventoryItem>().itemType == "Origin" && isEquipped == true)
        {
            player.GetComponent<PlayerController>().origin = null;
            isEquipped = false;
        }
        if(this.GetComponent<InventoryItem>().itemType == "Heart" && isEquipped == true)
        {
            player.GetComponent<PlayerController>().heart = null;
            isEquipped = false;
        }
        if(this.GetComponent<InventoryItem>().itemType == "Utility" && isEquipped == true)
        {
            player.GetComponent<PlayerController>().utility = null;
            isEquipped = false;
        }
        if(this.GetComponent<InventoryItem>().itemType == "Mobility" && isEquipped == true)
        {
            player.GetComponent<PlayerController>().mobility = null;
            isEquipped = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
        player.GetComponent<PlayerController>().CalculateStats();
    }
}
