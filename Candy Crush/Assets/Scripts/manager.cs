using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manager : MonoBehaviour
{
    public static manager inst; 
    public Text count;
    public int noOfpoewer= 3;
    // Start is called before the first frame update
    void Start()
    {
        inst = this;   
    }

    // Update is called once per frame
    void Update()
    {
        count.text = noOfpoewer.ToString(); 
    }
}
