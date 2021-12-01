using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class ItemDatabase : MonoBehaviour
{
    JsonData itemdata;
    List<Item> database = new List<Item>();

    // Start is called before the first frame update
    void Start()
    {
        //itemdata = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/own/ItemAssets/items.json"));
        itemdata = JsonMapper.ToObject(Resources.Load("items").ToString());
        ConstructItemDatabase();
        //Debug.Log(database[0].Desp);
        //Debug.Log(FetchItemByID(1).Title + FetchItemByID(1).Desp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ConstructItemDatabase()
    {
        for (int i = 0; i < itemdata.Count; i++)
        {
            database.Add(new Item((int)itemdata[i]["id"],
                itemdata[i]["title"].ToString(), (int)itemdata[i]["wastage"],
                itemdata[i]["description"].ToString(), itemdata[i]["madeby"].ToString(), itemdata[i]["image"].ToString()));
                

        }
    }
    public Item FetchItemByID(int _id)
    {
        for (int i = 0; i < database.Count; i++)
        {
            if (_id == database[i].ID)
            {
                return database[i];
            }
        }
        return null;
    }
}
public class Item
{
    public int ID { get; set; }
    public string Title { get; set; }
    public int Wastage { get; set; }
    public string Made { get; set; }
    public string Desp { get; set; }
    public Sprite Sprite { get; set; }
    public Item(int _id,string _title,int _wastage, string _desp,string _made,string _image)
    {
        this.ID = _id;
        this.Title = _title;
        this.Wastage = _wastage;
        this.Desp = _desp;
        this.Made = _made;
        this.Sprite = Resources.Load<Sprite>(_image);

    }
    public Item()
    {
        this.ID = -1;
    }
}