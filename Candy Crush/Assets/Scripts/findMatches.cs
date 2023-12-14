using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class findMatches : MonoBehaviour
{
    public static findMatches inst;
    public List<GameObject> matches = new List<GameObject>();
    Board board;
    public int col, row;
    //public bool isMatched;

    // Start is called before the first frame update
    void Start()
    {
        inst = this;
        board = FindObjectOfType<Board>();
        //col = this.GetComponent<Candy>().col;
        //row = this.GetComponent<Candy>().row;
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
                            matches.Add(curCandy);
                        if (!matches.Contains(leftCandy))
                            matches.Add(leftCandy);
                        if (!matches.Contains(rightCandy))
                            matches.Add(rightCandy);
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
                        if(!matches.Contains(curCandy))
                            matches.Add(curCandy);
                        if (!matches.Contains(downCandy))
                            matches.Add(    downCandy);
                        if (!matches.Contains(upCandy))
                            matches.Add(upCandy);
                    }

                }
            }
        }
    }
    public void checkForBomb()
    {
        if(board.curMoveCandy!=null)
        {
            board.curMoveCandy = null;
            Debug.Log("don't destroy");
            board.curMoveCandy.GetComponent<Candy>().isMatched = false;
            if (board.curMoveCandy.tag == "blue") board.curMoveCandy.GetComponent<SpriteRenderer>().sprite = board.blue;
            if (board.curMoveCandy.tag == "pink") board.curMoveCandy.GetComponent<SpriteRenderer>().sprite = board.pink;
            if (board.curMoveCandy.tag == "orange") board.curMoveCandy.GetComponent<SpriteRenderer>().sprite = board.orange;
            if (board.curMoveCandy.tag == "green") board.curMoveCandy.GetComponent<SpriteRenderer>().sprite = board.green;
            if (board.curMoveCandy.tag == "purple") board.curMoveCandy.GetComponent<SpriteRenderer>().sprite = board.purpal;
            if (board.curMoveCandy.tag == "yellow") board.curMoveCandy.GetComponent<SpriteRenderer>().sprite = board.yellow;
            if (board.curMoveCandy.tag == "red") board.curMoveCandy.GetComponent<SpriteRenderer>().sprite = board.red;
            board.curMoveCandy.tag = "bomb";
        }
    }
}
