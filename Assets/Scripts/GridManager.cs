using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private bool gravity = false;

    public int w, h;
    public float cellSize;
    private TiledGrid<TetriminoGridItem> pg, og;
    [SerializeField] private GameObject po, oo;
    [SerializeField] private GameObject brick;
    [SerializeField] private GameObject[] shapes;

    private bool flipI = false;
    private GameObject activeShape;

    // Start is called before the first frame update
    private void Start()
    {
        MakePlayerGrid();
        MakeObstacleSpawnerGrid();
        InstanciateBrick(pg, po, shapes[Random.Range(0, shapes.Length)], Vector2.zero);
    }
    private void InstanciateBrick(TiledGrid<TetriminoGridItem> grid, GameObject gridParent, GameObject spawnable, Vector2 position)
    {
        grid.GetGridPosition(position, out int x, out int y);

        activeShape = Instantiate(spawnable, grid.GetWorldPosition(x, y), Quaternion.identity);

        activeShape.name = spawnable.name;
        activeShape.transform.parent = gridParent.transform;
        activeShape.transform.localScale = Vector3.one;

        LoadToGrid(pg);
    }

    private void MakePlayerGrid()
    {
        pg = new TiledGrid<TetriminoGridItem>(po.transform, h, w, cellSize, po.transform, (TiledGrid<TetriminoGridItem> g, int x, int y) => new TetriminoGridItem(g, x, y));
    }

    private void MakeObstacleSpawnerGrid()
    {
        og = new TiledGrid<TetriminoGridItem>(oo.transform, h, w, cellSize, oo.transform, (TiledGrid<TetriminoGridItem> g, int x, int y) => new TetriminoGridItem(g, x, y));
    }

    public void MoveActive(Vector2 where, bool check = true, bool gridcheck = true)
    {
        Vector3 w = where * transform.localScale;
        activeShape.transform.localPosition += w;
        if (!ValidPosition(activeShape.transform, out Vector2 error) && check)
        {
            activeShape.transform.localPosition -= w;
        }
    }
    public void RotateActive()
    {
        if (activeShape.name != "Q")
        {
            if (activeShape.name == "I")
            {
                if (flipI)
                {
                    activeShape.transform.RotateAround(activeShape.transform.GetChild(4).position, Vector3.forward, -90);
                }
                else
                {
                    activeShape.transform.RotateAround(activeShape.transform.GetChild(4).position, Vector3.forward, 90);
                }
                flipI = !flipI;
            }
            else
            {
                activeShape.transform.RotateAround(activeShape.transform.GetChild(4).position, Vector3.forward, 90);
            }

            // while (!ValidPosition(activeShape.transform, out Vector2 error))
            // {
            //     if (error.x != 0)
            //     {
            //         MoveActive(new Vector2(-error.x, 0), false);
            //     }
            //     else if (error.y != 0)
            //     {
            //         MoveActive(new Vector2(0, -error.y), false);
            //     }
            // }
        }
        if (gravity) GroundIt();
    }
    private void GroundIt()
    {
        while (EmptyRow(0))
        {
            MoveActive(Vector2.down);
        }
    }

    private bool EmptyRow(int row)
    {
        for (int i = 0; i > w; i++)
        {
            if (pg.GetGridItem(i, row).GetValue() != null)
            {
                return false;
            }
        }
        return true;
    }

    private bool ValidPosition(Transform tetrimino, out Vector2 error)
    {
        bool ret = true;
        error = Vector2.zero;

        foreach (Transform child in tetrimino)
        {
            pg.GetGridPosition(child.position, out int x, out int y);
            if (x < 0)
            {
                error.x = Vector2.left.x;
                ret = false;
            }
            else if (x > w - 1)
            {
                error.x = Vector2.right.x;
                ret = false;
            }
            if (y < 0)
            {
                error.y = Vector2.down.y;
                ret = false;
            }
            else if (y > h - 1)
            {
                error.y = Vector2.up.y;
                ret = false;
            }
        }
        return ret;
    }


    private void LoadToGrid(TiledGrid<TetriminoGridItem> grid)
    {
        // foreach (Transform child in activeShape.transform)
        // {
        //     pg.GetGridPosition(child.position, out int x, out int y);
        //     if (x < 0 || x > w - 1 || y < 0 || y > h - 1)
        //     {

        //     }
        //     else
        //     {
        //         grid.GetGridItem(child.transform.position).SetValue(child.gameObject);
        //     }
        // }
    }
    private void UnloadFromGrid(TiledGrid<TetriminoGridItem> grid)
    {
        // foreach (Transform child in activeShape.transform)
        // {
        //     pg.GetGridPosition(child.position, out int x, out int y);
        //     if (x < 0 || x > w - 1 || y < 0 || y > h - 1)
        //     {

        //     }
        //     else
        //     {
        //         grid.GetGridItem(child.transform.position).SetValue(null);
        //     }
        // }
    }
}
