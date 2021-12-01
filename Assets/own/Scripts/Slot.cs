using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Slot : MonoBehaviour,IDropHandler
{
    public int slotID = 0;
    Inventory inv;
    public void OnDrop(PointerEventData eventData)
    {
        ItemData itemData = eventData.pointerDrag.GetComponent<ItemData>();
        if (inv.items[slotID].ID == -1)
        {
            inv.items[itemData.slotIndex] = new Item();
            itemData.slotIndex = slotID;
            inv.items[slotID] = itemData.item;
        }
        else if(itemData.slotIndex!=slotID)
        {
            Transform item = this.transform.GetChild(0);
            item.GetComponent<ItemData>().slotIndex = itemData.slotIndex;
            item.transform.SetParent(inv.slots[itemData.slotIndex].transform);
            item.transform.position = item.transform.parent.position;

            inv.items[itemData.slotIndex] = item.GetComponent<ItemData>().item;
            itemData.slotIndex = slotID;
            inv.items[slotID] = itemData.item;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        inv = Camera.main.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
