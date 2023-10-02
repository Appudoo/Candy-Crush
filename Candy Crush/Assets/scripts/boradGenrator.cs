using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class boradGenrator : MonoBehaviour
{

    public int rows, cols;
    public GameObject[] allPrefabs;
    public GameObject[,] allCandies;
    bool isValid = false;
    // Start is called before the first frame update
    void Start()
    {
        allCandies = new GameObject[cols, rows];
        generateBoard();
    }

    void generateBoard()
    {
        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                int r = Random.Range(0, allPrefabs.Length);
              
                do
                {
                    r = Random.Range(0, allPrefabs.Length);
                    isValid = !CheckCandiesInRow(i, j, r) && !CheckCandiesInColumn(i, j, r);
                }
                while (!isValid);


                Vector2 pos = new Vector2(i, j);
                GameObject g = Instantiate(allPrefabs[r], pos, Quaternion.identity);
                g.transform.SetParent(transform);
                g.name="("+j+","+i+")";
                allCandies[i, j] = g;
            }
        }
    }
    private void Update()
    {

    }

    bool CheckCandiesInColumn(int cols, int rows, int r)
    {
        if (cols >= 2 && allCandies[cols - 1, rows].tag == allPrefabs[r].tag && allCandies[cols - 2, rows].tag==(allPrefabs[r].tag))
            return true;

        if (cols <= cols - 3 && allCandies[cols + 1, rows].tag==(allPrefabs[r].tag) && allCandies[cols + 2, rows].tag==(allPrefabs[r].tag))
            return true;

        return false;
    }

    bool CheckCandiesInRow(int cols, int rows, int r)
    {

        if (rows >= 2 && allCandies[cols, rows - 1].tag==(allPrefabs[r].tag) && allCandies[cols, rows - 2].tag==(allPrefabs[r].tag))
            return true;

        if (rows <= rows - 3 && allCandies[cols, rows + 1].tag==(allPrefabs[r].tag) && allCandies[cols, rows + 2].tag==(allPrefabs[r].tag))
            return true;

        return false;
    }


}