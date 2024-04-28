using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using Random = System.Random;



public class Walker : MonoBehaviour
{
    //global variables
    public int seed;
    public int time;
    public int x;
    public int z;
    public int rnd;

    List<Vector3> pList = new List<Vector3>();
    List<GameObject> objects = new List<GameObject>();
    public GameObject geometry;

    public Vector3 pos()//position
    {
        Vector3 pos = new Vector3(x, 0, z); //2D representation, Y axis is a vertical one in Unity
        return pos;
    }

    public GameObject cube ()
    {
        GameObject cube = new GameObject();
        return cube;


    }



    public int step(int rnd) //stepping function operating with random value - an integer number
    {
        int choice = rnd;
        if (choice == 0)
        {
            x++;
        }
        else if (choice == 1)
        {
            x--;
        }
        else if (choice == 2)
        {
            z++;
        }
        else if (choice == 3)
        {
            z--;
        }

        return choice;
    }


    // Start is called before the first frame update
    void Start()
    {
        x = 0;
        z = 0;
        rnd = 0;

    }

    // Update is called once per frame
    void Update()
    {

        Random rand = new Random(); //Random rand = new Random(seed); 

        for (int i = 0; i < time; i++)//step each iteration just by one unity, of time =1.
        {
            int rnd = rand.Next(0, 4);
            this.step(rnd);
            pList.Add(this.pos());
            Debug.Log(rnd);
            objects.Add(this.cube());
            
        }

        transform.position = pos();
        Instantiate(geometry, transform.position, transform.rotation);
    }

}
