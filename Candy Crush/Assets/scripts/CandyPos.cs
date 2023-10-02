using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyPos : MonoBehaviour
{
    public static CandyPos inst;
    private int rows, cols;
    float angle;
    public Vector2 firstPos, secondPos, pos;
    public Vector3 worldPos;
    // Start is called before the first frame update
    void Start()
    {
        //setposition();
        //getposition();
    }
    public void setposition()
    {
        cols = (int)this.transform.position.x;
        rows = (int)this.transform.position.y;
    }

    public void getposition()
    {
        pos = new Vector2(cols, rows);

        Debug.Log(this.transform.position);
    }

    private void OnMouseDown()
    {
        firstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        print("first pos = "+firstPos);
    }
    private void OnMouseUp()
    {
        secondPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        print("second pos = " + secondPos);
        calculate();

    }
    void calculate()
    {
        Vector2 offset = new Vector2(secondPos.x - firstPos.x, secondPos.y - firstPos.y);
        angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        print(angle);
        direction();
    }
    void direction()
    {
        if (angle <= 45f && angle >= -45f)
        {
            print("Right");

        }else if(angle >=45 && angle <=135)
        {
            print("UP");
        }else if(angle>135 || angle<=-135)
        {
            print("Left");
        }else if(angle <-45 &&  angle >=-135)
        {
            print("Down");
        }

    }
}
