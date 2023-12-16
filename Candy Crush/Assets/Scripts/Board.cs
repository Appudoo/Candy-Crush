using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int rows, cols;
    public GameObject[] allPrefabs;
    public GameObject[,] allCandies;
    public int nocnt;
    public GameObject powerCandy;
    powerCandy p;
    public GameObject CurMoveCandy;
    public Sprite blue, red, purple, orange, yellow, pink, green, white;
    // Start is called before the first frame update
    void Start()
    {
        //p = FindAnyObjectByType<powerCandy>();
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
                Vector2 pos = new Vector2(i, j);
                while (checkMatchesAt(i, j, allPrefabs[r]))
                {
                    print("Hello ");
                    r = Random.Range(0, allPrefabs.Length);
                }
                GameObject g = Instantiate(allPrefabs[r], pos, Quaternion.identity);
                g.GetComponent<Candy>().row = j;
                g.GetComponent<Candy>().col = i;
                g.transform.SetParent(transform);
                g.name = "(" + i + "," + j + ")";
                allCandies[i, j] = g;
            }
        }
        genpowerCandy();
    }
    bool checkMatchesAt(int col, int row, GameObject g)
    {
        if (col > 1 && row > 1)
        {

            if (allCandies[col - 1, row].tag == g.tag && allCandies[col - 2, row].tag == g.tag)
            {
                return true;
            }
            if (allCandies[col, row - 1].tag == g.tag && allCandies[col, row - 2].tag == g.tag)
            {
                return true;
            }
        }
        else if (col > 1)
        {
            if (allCandies[col - 1, row].tag == g.tag && allCandies[col - 2, row].tag == g.tag)
            {
                return true;
            }
        }
        else if (row > 1)
        {
            if (allCandies[col, row - 1].tag == g.tag && allCandies[col, row - 2].tag == g.tag)
            {
                return true;
            }
        }
        return false;
    }
    public void destroyMatchCandies()
    {
        print("CNt = " + findMatches.inst.matches.Count);
        if(findMatches.inst.matches.Count > 3)
        {
            findMatches.inst.checkForBomb();
        }
        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (allCandies[i, j] != null)
                {
                    if (allCandies[i, j].GetComponent<Candy>().isMatched)
                    {
                        Destroy(allCandies[i, j]);
                        allCandies[i, j] = null;
                    }
                }
            }
        }
        findMatches.inst.matches.Clear();
        //Debug.Break();
        StartCoroutine(decreaseRowNum());
    }
    IEnumerator decreaseRowNum()
    {
        yield return new WaitForSeconds(0.5f);
        int nullCnt = 0;
        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (allCandies[i, j] == null)
                {
                    nullCnt++;
                }
                else if (nullCnt > 0)
                {
                    allCandies[i, j].GetComponent<Candy>().row -= nullCnt;
                    allCandies[i, j] = null;
                }
            }
            nullCnt = 0;
        }
        StartCoroutine(genDestroyCandy());
    }


    void refill()
    {
        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (allCandies[i, j] == null)
                {
                    int r = Random.Range(0, allPrefabs.Length);

                    Vector2 pos = new Vector2(i, j+2);
                    GameObject g = Instantiate(allPrefabs[r], pos, Quaternion.identity);
                    g.GetComponent<Candy>().row = j;
                    g.GetComponent<Candy>().col = i;
                    
                    g.transform.SetParent(transform);
                    g.name = "(" + i + "," + j + ")";
                    allCandies[i, j] = g;
                }
            }
        }
    }
    IEnumerator genDestroyCandy()
    {
       
        yield return new WaitForSeconds(0.7f);
        refill();
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (allCandies[i, j] != null)
                {
                    if (allCandies[i,j].GetComponent<Candy>().isMatched)
                    {
                        destroyMatchCandies();
                    }
                }
            }
        }
        //yield return new WaitForSeconds(0.5f);
        //Debug.Break();
        //Candy.Instance.checkgenmatches();
    }

    public void genpowerCandy()
    {
        int randomPosCol = Random.Range(0, cols);
        Debug.Log((rows - 1) + "  " + randomPosCol);
        Destroy(allCandies[randomPosCol, rows - 1]);
        GameObject g = Instantiate(powerCandy, allCandies[randomPosCol, rows - 1].transform.position, Quaternion.identity);
        g.transform.SetParent(transform);
        g.name = "(" + randomPosCol + "," + (rows - 1) + ")";
        g.GetComponent<Candy>().row = rows - 1;
        g.GetComponent<Candy>().col = randomPosCol;
        allCandies[randomPosCol, rows - 1] = g;
    }

}
