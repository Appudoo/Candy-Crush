using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Candy : MonoBehaviour
{
    public static Candy Instance;
    public int row, col, targetX, targetY, prevRow, prevCol;
    Vector2 firstPos, secondPos, pos;
    float angle;
    public GameObject powercandy;
    public Vector3 worldPos;
    GameObject oppCandy;
    Board board;
    public bool isMatched;
    public int count = 3;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        board = FindObjectOfType<Board>();
    }

    // Update is called once per frame
    void Update()
    {
        targetX = col;
        targetY = row;
        if (Mathf.Abs(targetX - transform.position.x) > 0f)
        {
            Vector2 pos = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, pos, .5f);
            if (this.gameObject != board.allCandies[col, row])
            {
                board.allCandies[col, row] = this.gameObject;
            }
        }
        if (Mathf.Abs(targetY - transform.position.y) > 0f)
        {
            Vector2 pos = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, pos, .5f);
            if (this.gameObject != board.allCandies[col, row])
            {
                board.allCandies[col, row] = this.gameObject;
            }
        }
        //findMatches();
        findMatches.inst.FindMatches(targetX, targetY);

        power();
        //board.destroyMatchCandies();
        //if(isMatched)
        //Destroy(this.gameObject);
    }
    private void OnMouseDown()
    {
        board.curMoveCandy = this.gameObject;
        firstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        print(firstPos);
    }
    private void OnMouseUp()
    {
        secondPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        print(secondPos);
        calculateAngle();
    }
    void calculateAngle()
    {
        Vector2 offset = new Vector2(secondPos.x - firstPos.x, secondPos.y - firstPos.y);
        angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        //print(angle);
        MoveDirection();
    }
    void MoveDirection()
    {
        if (angle <= 45f && angle >= -45f)
        {
            print("right");
            prevRow = row;
            prevCol = col;
            oppCandy = board.allCandies[col + 1, row];
            oppCandy.GetComponent<Candy>().col -= 1;
            col += 1;
        }
        else if (angle <= 135f && angle >= 45f)
        {
            prevRow = row;
            prevCol = col;
            oppCandy = board.allCandies[col, row + 1];
            oppCandy.GetComponent<Candy>().row -= 1;
            row += 1;
            print("UP");
        }
        else if (angle >= 135f || angle <= -135f)
        {
            prevRow = row;
            prevCol = col;
            print("left");
            oppCandy = board.allCandies[col - 1, row];
            oppCandy.GetComponent<Candy>().col += 1;
            col -= 1;
        }
        else if (angle >= -135f && angle <= -45f)
        {
            prevRow = row;
            prevCol = col;
            print("down");
            oppCandy = board.allCandies[col, row - 1];
            oppCandy.GetComponent<Candy>().row += 1;
            row -= 1;
        }
        StartCoroutine(checkMatchesCo());
    }


    IEnumerator checkMatchesCo()
    {
        yield return new WaitForSeconds(0.5f);
        if (oppCandy != null)
        {
            if (!isMatched && !oppCandy.GetComponent<Candy>().isMatched)
            {
                oppCandy.GetComponent<Candy>().row = row;
                oppCandy.GetComponent<Candy>().col = col;
                row = prevRow;
                col = prevCol;
                oppCandy = null;
            }
            else
            {
                board.destroyMatchCandies();
                oppCandy = null;
            }
        }
    }
    void power()
    {
        if (this.tag == "powerCandy" && row == 0)
        {
            powercandy = this.gameObject;
            manager.inst.noOfpoewer = manager.inst.noOfpoewer - 1;
            isMatched = true;
            board.destroyMatchCandies();
            board.genpowerCandy();
        }
    }

    public void checkgenmatches()
    {

        Debug.Log("CALL");
        //findMatches.inst.FindMatches();
        board.destroyMatchCandies();
        //StartCoroutine(checkMatchesCo());
    }
}
