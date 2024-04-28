using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using Random = System.Random;



public class Walker3D : MonoBehaviour
{
    //global variables
    public int seed;
    public double time;
    public float x;
    public float y;
    public float z;
    public int bound; //starting value of the boundary condition 
    public int upperBound;//maximumm value of the boundary condition
    public int rnd;

    List<Vector3> pList = new List<Vector3>();
    List<GameObject> objects = new List<GameObject>();
    List<int> indexList = new List<int>();//each point has an index value. This help us to assign to every index certain time value, expressed in the counter variable.
    public GameObject geometry;
    Random rand = new Random(); //Random rand = new Random(seed);
    //int counter = 0;

    public Vector3 pos()//position
    {
        Vector3 pos = new Vector3(x * 0.1f, y * 0.1f, z * 0.1f); //3D representation, Y axis is a vertical one in Unity
        return pos;
    }

    public GameObject cube()
    {
        GameObject cube = new GameObject();
        return cube;
    }



    public int step(int rnd) //stepping function operating with random value - an integer number
    {
        int choice = rnd;
        //logic of the choice
        if (choice <= 1)
        {
            x++;
        }
        else if (choice <= 3)
        {
            x--;
        }
        else if (choice <= 5)
        {
            z++;
        }
        else if (choice <= 7)
        {
            z--;
        }
        else if (choice <= 9)
        {
            y--;
        }
        else if (choice <= 14)
        {
            y++;
        }

        //if the walker reaches the boundaries, it starts from 0 again = constraints-boundary conditions

        if (x == bound || x == -bound)
        {
            x = 0;
            y = 0;
            z = 0;
        }
        if (z == bound || z == -bound)
        {
            x = 0;
            y = 0;
            z = 0;
        }
        if (y < 0 || y == upperBound)
        {
            x = 0;
            y = 0;
            z = 0;
        }



        return choice;
    }


    // Start is called before the first frame update
    void Start()
    {
        x = 0;
        y = 0;
        z = 0;
        bound = 15;
        upperBound = 45;
        rnd = 0;

    }

    // Update is called once per frame
    void Update()
    {
        

            for (int i = 0; i < time; i++)
        {
            int rnd = rand.Next(0, 15);
            this.step(rnd);
            pList.Add(this.pos());
            //counter++;//count iterations


            /*  //add a new index into the list of indexes each iteration
              if (this.x == 0 && this.z == 0 && this.y == 0)
              {
                  indexList.Add(counter);
                  counter = 0;
              }*/

            Debug.Log(rnd);
            objects.Add(this.cube());

        }

        
        transform.position = this.pos();
        Instantiate(geometry, transform.position, transform.rotation);
        
    }

}
