using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject slot;
    public GameObject item;
    GameObject SlotPanel;
    public List<GameObject> slots = new List<GameObject>();
    public List<Item> items = new List<Item>();
    ItemDatabase itemDatabase;
    public GameObject Panel;
    // Start is called before the first frame update
    void Start()
    {
        itemDatabase = GetComponent<ItemDatabase>();
        SlotPanel = GameObject.Find("SwitchPanel");
        for (int i = 0; i < 16; i++)
        {
            slots.Add(Instantiate(slot));
            slots[i].transform.SetParent(SlotPanel.transform);
            slots[i].GetComponent<Slot>().slotID = i;
            items.Add(new Item());
            CameraRoam._instance.Combat.Add(slots[i]);
        }
        Additem(0);
        Additem(1);
        Additem(2);
        Additem(3);
        Additem(4);
        Additem(5);
        Additem(6);
        Additem(7);
        Additem(8);
        Additem(9);
        Additem(10);
        Additem(11);
        Additem(12);
        Additem(13);
        Additem(14);
        Additem(15);
        Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Additem(int _id)
    {
        Item itemToAdd = itemDatabase.FetchItemByID(_id);
        CreatNewItem(itemToAdd);
    }

    void CreatNewItem(Item itemToAdd) //(id)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if(items[i].ID == -1)
            {
                items[i] = itemToAdd;
                GameObject itemObj = Instantiate(item);
                itemObj.transform.SetParent(slots[i].transform);
                itemObj.transform.position = Vector2.zero;
                itemObj.name = items[i].Title;
                itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
                itemObj.GetComponent<ItemData>().item = itemToAdd;
                itemObj.GetComponent<ItemData>().slotIndex = i;
                break;
            }

        }
    }
}
