using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class findMatches : MonoBehaviour
{
    public static findMatches inst;
    public List<GameObject> matches = new List<GameObject>();
    Board board;
    public int col, row;
    

    // Start is called before the first frame update
    void Start()
    {
        inst = this;
        board = FindObjectOfType<Board>();
      
        col = board.cols;
        row = board.rows;
        Debug.Log("Row = " + row + "  col =  " + col);
    }

    // Update is called once per frame
    void Update()
    {
        //FindMatches();
    }
    public void FindMatches(int col, int row)
    {
        if (col > 0 && col < board.cols - 1)
        {
            GameObject curCandy = board.allCandies[col, row];
            if (curCandy != null)
            {
                GameObject leftCandy = board.allCandies[col - 1, row];
                GameObject rightCandy = board.allCandies[col + 1, row];
                if (leftCandy != null && rightCandy != null)
                {
                    if (leftCandy.tag == curCandy.tag && rightCandy.tag == curCandy.tag)
                    {
                        curCandy.GetComponent<Candy>().isMatched = true;
                        leftCandy.GetComponent<Candy>().isMatched = true;
                        rightCandy.GetComponent<Candy>().isMatched = true;
                        if (!matches.Contains(curCandy))
                        {
                            matches.Add(curCandy);
                        }
                        if (!matches.Contains(leftCandy))
                        {
                            matches.Add(leftCandy);
                        }
                        if (!matches.Contains(rightCandy))
                        {
                            matches.Add(rightCandy);
                        }
                        if (curCandy.GetComponent<Candy>().isColBomb )
                        {
                            
                            collectCollumCandies(curCandy.GetComponent<Candy>().col);
                            Debug.Break();
                        }
                        if (leftCandy.GetComponent<Candy>().isColBomb)
                        {
                            collectCollumCandies(leftCandy.GetComponent<Candy>().col);
                        }
                        if (rightCandy.GetComponent<Candy>().isColBomb)
                        {
                            collectCollumCandies(rightCandy.GetComponent<Candy>().col);
                        }

                        if (curCandy.GetComponent<Candy>().isRowBomb)
                        {

                        }
                        if (leftCandy.GetComponent<Candy>().isRowBomb)
                        {

                        }
                        if (rightCandy.GetComponent<Candy>().isRowBomb)
                        {

                        }
                    }

                }
            }
        }
        if (row > 0 && row < board.rows - 1)
        {
            GameObject curCandy = board.allCandies[col, row];
            if (curCandy != null)
            {
                GameObject downCandy = board.allCandies[col, row - 1];
                GameObject upCandy = board.allCandies[col, row + 1];
                if (downCandy != null && upCandy != null)
                {
                    if (downCandy.tag == curCandy.tag && upCandy.tag == curCandy.tag)
                    {
                        curCandy.GetComponent<Candy>().isMatched = true;
                        downCandy.GetComponent<Candy>().isMatched = true;
                        upCandy.GetComponent<Candy>().isMatched = true;
                        if (!matches.Contains(downCandy))
                        {
                            matches.Add(downCandy);
                        }
                        if (!matches.Contains(upCandy))
                        {
                            matches.Add(upCandy);
                        }
                        if (!matches.Contains(curCandy))
                        {
                            matches.Add(curCandy);
                        }   
                    }

                }
            }
        }
    }
    void collectCollumCandies(int col)
    {
        for (int i =0;i<board.rows;i++)
        {
            if (!matches.Contains(board.allCandies[col,i]))
            {
                board.allCandies[col, i].GetComponent<Candy>().isMatched = true;    
                matches.Add(board.allCandies[col, i]);
            }
            
        }
    }
    public void checkForBomb()
    {
        print("Hedllo out");
        if (board.CurMoveCandy != null)
        {
            print("Hello in");
            
            //board.CurMoveCandy.tag = "bomb";
            if (board.CurMoveCandy.GetComponent<Candy>().isMatched)
            {
                board.CurMoveCandy.GetComponent<Candy>().isMatched = false;
                if (board.CurMoveCandy.tag == "blue") board.CurMoveCandy.GetComponent<SpriteRenderer>().sprite = board.blue;
                if (board.CurMoveCandy.tag == "pink") board.CurMoveCandy.GetComponent<SpriteRenderer>().sprite = board.pink;
                if (board.CurMoveCandy.tag == "orange") board.CurMoveCandy.GetComponent<SpriteRenderer>().sprite = board.orange;
                if (board.CurMoveCandy.tag == "green") board.CurMoveCandy.GetComponent<SpriteRenderer>().sprite = board.green;
                if (board.CurMoveCandy.tag == "purple") board.CurMoveCandy.GetComponent<SpriteRenderer>().sprite = board.purple;
                if (board.CurMoveCandy.tag == "yellow") board.CurMoveCandy.GetComponent<SpriteRenderer>().sprite = board.yellow;
                if (board.CurMoveCandy.tag == "red") board.CurMoveCandy.GetComponent<SpriteRenderer>().sprite = board.red;
                if (board.CurMoveCandy.tag == "white") board.CurMoveCandy.GetComponent<SpriteRenderer>().sprite = board.white;
                Candy curCan = board.CurMoveCandy.GetComponent<Candy>();
                if ((curCan.angle <= 45f && curCan.angle >= -45f) || (curCan.angle <= -135f || curCan.angle >= 135f))
                {
                    curCan.isRowBomb = true;
                }
                else
                {
                    curCan.isColBomb = true;
                }
            }else if (board.CurMoveCandy.GetComponent<Candy>().oppCandy != null)
            {
                Candy oppCandyScr = board.CurMoveCandy.GetComponent<Candy>().oppCandy.GetComponent<Candy>();
                if (oppCandyScr.isMatched)
                {
                    oppCandyScr.isMatched = false;
                    if (oppCandyScr.gameObject.tag == "blue") oppCandyScr.gameObject.GetComponent<SpriteRenderer>().sprite = board.blue;
                    if (oppCandyScr.gameObject.tag == "pink") oppCandyScr.gameObject.GetComponent<SpriteRenderer>().sprite = board.pink;
                    if (oppCandyScr.gameObject.tag == "orange") oppCandyScr.gameObject.GetComponent<SpriteRenderer>().sprite = board.orange;
                    if (oppCandyScr.gameObject.tag == "green") oppCandyScr.gameObject.GetComponent<SpriteRenderer>().sprite = board.green;
                    if (oppCandyScr.gameObject.tag == "purple") oppCandyScr.gameObject.GetComponent<SpriteRenderer>().sprite = board.purple;
                    if (oppCandyScr.gameObject.tag == "yellow") oppCandyScr.gameObject.GetComponent<SpriteRenderer>().sprite = board.yellow;
                    if (oppCandyScr.gameObject.tag == "red") oppCandyScr.gameObject.GetComponent<SpriteRenderer>().sprite = board.red;
                    if (oppCandyScr.gameObject.tag == "white") oppCandyScr.gameObject.GetComponent<SpriteRenderer>().sprite = board.white;

                    if ((board.CurMoveCandy.GetComponent<Candy>().angle <= 45f && board.CurMoveCandy.GetComponent<Candy>().angle >= -45f) || (board.CurMoveCandy.GetComponent<Candy>().angle <= -135f || board.CurMoveCandy.GetComponent<Candy>().angle >= 135f))
                    {
                        oppCandyScr.isRowBomb = true;
                        print("Row");
                    }
                    else
                    {
                        print("Col");
                        oppCandyScr.isColBomb = true;
                    }
                }
            }
            board.CurMoveCandy = null;
        }
    }
}
