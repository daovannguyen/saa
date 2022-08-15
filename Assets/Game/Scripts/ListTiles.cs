using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ListTiles : Singleton<ListTiles>
{

    public List<Tile> tiles;

    public GameObject tilesObj;
    public float ratioDistance;


    public List<GameObject> GetAllChildOfModel(GameObject model)
    {
        int count = model.transform.childCount;
        List<GameObject> childs = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            childs.Add(model.transform.GetChild(i).gameObject);
        }
        return childs;
    }

    private void Awake()
    {
        tiles = new List<Tile>();
        List<GameObject> tilesObjs = GetAllChildOfModel(tilesObj);
        foreach (var i in tilesObjs)
        {
            tiles.Add(new Tile(i));
        }
    }
    public int FindIndexTileFirstEmpty()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i].id == -1)
            {
                return i;
            }
        }
        return -1;
    }
    public Tile FindTileFirstEmpty()
    {
        int index = FindIndexTileFirstEmpty();
        if (index != -1)
        {
            return tiles[FindIndexTileFirstEmpty()];
        }
        else
        {
            return null;
        }
    }

    private bool IsFullTiles()
    {
        return tiles[tiles.Count - 1].id != -1;
    }

    // mảng item trong tile sắp xếp tăng dần theo id item
    private int FindIndexTileForItem(Item item)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            // tile có item nhỏ hơn hoặc tile trống
            if (tiles[i].id == -1)
            {
                return i;
            }
            if (item.id < tiles[i].item.id)
            {
                return i;
            }
        }
        return -1;
    }
    private void AddItemToTile(Item item, int indexTile)
    {
        tiles[indexTile].id = item.id;
        tiles[indexTile].item = item;
    }
    public void AddItem(Item item)
    {
        if (!IsFullTiles())
        {
            int indexTile = FindIndexTileForItem(item);
            if (indexTile != -1)
            {
                Destroy(item.rb);

                // gán item vào tile
                for (int i = tiles.Count - 1; i > indexTile; i--)
                {
                    MoveItemToOtherTile(i - 1, i);
                }

                AddItemToTile(item, indexTile);

                Vector3 rotation = new Vector3(0, 720, 0);
                Vector3 scale = new Vector3(0.5f, 0.5f, 0.5f);
                float time = 0.4f;
                MoveItemToTile(item, indexTile, rotation, scale, time);
                StartCoroutine(CheckThreeItemSimilar());
                //
                //SortListTile();
                //CheckThreeItemSimilar();
            }
        }

        else
        {
            Debug.Log("Hết tile để xếp. Game over");
        }
    }


    private void MoveItemToOtherTile(int oldIndexTile, int newIndexTile)
    {
        if (oldIndexTile < 0)
        {
            return;
        }
        if (tiles[oldIndexTile].id != -1)
        {
            tiles[newIndexTile].id = tiles[oldIndexTile].id;
            tiles[newIndexTile].item = tiles[oldIndexTile].item;

            Vector3 rotation = new Vector3(0, 0, 0);
            Vector3 scale = new Vector3(0.5f, 0.5f, 0.5f);
            float time = 0.4f;
            MoveItemToTile(tiles[oldIndexTile].item, newIndexTile, rotation, scale, time);
            tiles[oldIndexTile].SetEmpty();
        }
        else
        {
            tiles[newIndexTile].SetEmpty();
        }
    }

    private IEnumerator CheckThreeItemSimilar()
    {
        for (int i = 0; i < tiles.Count - 2; i++)
        {
            if (tiles[i].id == tiles[i + 1].id && tiles[i].id == tiles[i + 2].id && tiles[i].id != -1)
            {
                Item item1 = tiles[i].item;
                Item item2 = tiles[i + 1].item;
                Item item3 = tiles[i + 2].item;
                //tiles[i].item.DestroyItem(tiles[i + 1].transform.position);
                //tiles[i + 1].item.DestroyItem(tiles[i + 1].transform.position);
                //tiles[i + 2].item.DestroyItem(tiles[i + 1].transform.position);
                tiles[i].SetEmpty();
                tiles[i + 1].SetEmpty();
                tiles[i + 2].SetEmpty();
                //DOVirtual.DelayedCall(2 * Constrain.ID_time, FillListTiles);
                StartCoroutine(FillListTiles());
                yield return new WaitForSeconds(0.4f);
                item1.DestroyItem(tiles[i + 1].transform.position);
                item2.DestroyItem(tiles[i + 1].transform.position);
                item3.DestroyItem(tiles[i + 1].transform.position);
            }
        }
    }
    private IEnumerator FillListTiles()
    {
        int index = 0;
        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i].id != -1 && index != i)
            {
                Item item = tiles[i].item;
                tiles[index].AddItem(tiles[i].item);
                tiles[i].SetEmpty();
                index++;
                yield return new WaitForSeconds(2 * Constrain.ID_time);


                Vector3 rotation = new Vector3(0, 0, 0);
                Vector3 scale = new Vector3(0.5f, 0.5f, 0.5f);
                float time = 0.4f;
                MoveItemToTile(item, index - 1, rotation, scale, time);
            }
            else if (tiles[i].id != -1)
            {
                index++;
            }
        }
        yield return new WaitForSeconds(2 * Constrain.ID_time);
    }
    private void SortListTile()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            for(int j = i + 1; j < tiles.Count; j++)
            {
                if(tiles[i].id > tiles[j].id)
                {
                    Tile tg = tiles[i];
                    tiles[i] = tiles[j];
                    tiles[j] = tg;
                }
            }
        }
    }
    public void MoveItemToTile(Item item, int indexTile, Vector3 rotation, Vector3 scale, float time, Action<bool> onComplete = null)
    {
        //var pos = Vector3.Lerp(Camera.main.transform.position, tiles[indexTile].transform.position, ratioDistance);
        var pos = tiles[indexTile].transform.position + new Vector3(0, item.col.bounds.size.y / 2, 0);
        item.MoveToPointDotween(pos, rotation, scale, time, onComplete);
    }
}
