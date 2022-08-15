using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
// id = -1 là không chứa item nào
public class Tile 
{
    public int id;
    public Transform transform;
    public Item item;

    public Tile(GameObject model)
    {
        id = -1;
        transform = model.transform;
    }
    public void SetEmpty()
    {
        id = -1;
        item = null;
    }
    public void AddItem(Item item)
    {
        id = item.id;
        this.item = item;
    }
}
