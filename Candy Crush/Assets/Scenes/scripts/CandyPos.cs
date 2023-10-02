using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyPos : MonoBehaviour
{
    public static CandyPos inst;
    private int  rows ,cols;
    public Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
        setposition();
        getposition();
    }
    public void setposition()
    {
        cols = (int) this.transform.position.x;
        rows = (int)this.transform.position.y;
    }

    public void getposition()
    {
       pos = new Vector2(cols, rows);
        Debug.Log(this.transform.position);
    }

}
