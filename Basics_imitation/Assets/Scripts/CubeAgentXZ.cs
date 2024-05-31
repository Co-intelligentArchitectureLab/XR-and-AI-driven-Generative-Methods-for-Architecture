using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents; //allows us to use ml agents in Unity
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using Random = UnityEngine.Random;
using System;


public class CubeAgentXZ : Agent //Agent class inheriting all functionality of the ML Agents
{
    //object references
    public GameObject cAgent;
    public GameObject target;
    Rigidbody cAgentRb;
    Agent_Settings agent_Settings;

    //reference collision detector
    colliderDetector cd;

    //parameters
    public float speed = 3f;
    public float stepMove = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        cd = cAgent.GetComponent<colliderDetector>();//accessing the class of the game object component
        agent_Settings = FindObjectOfType<Agent_Settings>();
        cAgentRb = cAgent.GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()

    {
        cd.isCollided = false; //this resets the collision parameter to false

        //cAgent.transform.localPosition = new Vector3(Random.Range(-6.0f, 6.0f), 0, Random.Range(-6.0f, 6.0f));
        cAgent.transform.localPosition=Vector3.zero;
        //set random position of the target
        target.transform.localPosition = new Vector3(Random.Range(-6.0f, 6.0f), 0, Random.Range(-6.0f, 6.0f));
        
        cAgentRb.velocity = Vector3.zero;
        cAgentRb.angularVelocity = Vector3.zero;

    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // what to observe:
        //position of the agent in x and z
        //position of the target in x and z

        sensor.AddObservation(cAgent.transform.localPosition.x);
        sensor.AddObservation(target.transform.localPosition.x);
        sensor.AddObservation(cAgent.transform.localPosition.z);
        sensor.AddObservation(target.transform.localPosition.z);


    }

    public void MoveAgent(ActionSegment<float> act)
    {
        var dirToGo = Vector3.zero;
        var rotateDir = Vector3.zero;

        var action0 = act[0];
        var action1 = act[1];
        //action = Mathf.FloorToInt(act[0]);

        /* switch (action)
         {
             case 1:
                 dirToGo = transform.forward * 1.0f;//forward
                 break;
             case 2:
                 dirToGo = transform.forward * -1.0f;//backward
                 break;
             case 3:
                 rotateDir = transform.up * 0.5f;
                 break;
             case 4:
                 rotateDir = transform.up * -0.5f;
                 break;
             case 5:
                 dirToGo = transform.right * -1.0f;//left
                 break;
             case 6:
                 dirToGo = transform.right * 1.0f;//right
                 break;
         }*/


        /*   switch (action)
           {
               //move in x pos
               case 0:
                   cAgent.transform.localPosition += new Vector3(cAgent.transform.localPosition.x + stepMove, 0, cAgent.transform.localPosition.z); //adding the value to the current position
                   break;
               //move in x neg
               case 1:
                   cAgent.transform.localPosition += new Vector3(cAgent.transform.localPosition.x - stepMove, 0, cAgent.transform.localPosition.z);
                   break;
               //move in z pos
               case 2:
                   cAgent.transform.localPosition += new Vector3(cAgent.transform.localPosition.x, 0, cAgent.transform.localPosition.z + stepMove);
                   break;
               //move in z neg
               case 3:
                   cAgent.transform.localPosition += new Vector3(cAgent.transform.localPosition.x, 0, cAgent.transform.localPosition.z - stepMove);
                   break;
           }*/

        //transform.Rotate(rotateDir, Time.fixedDeltaTime * 100f);
        //cAgentRb.AddForce(dirToGo * agent_Settings.agentRunSpeed,ForceMode.VelocityChange);
        float finalDistanceToTarget = Vector3.Distance(target.transform.localPosition, cAgent.transform.localPosition);

          float xNew = cAgent.transform.localPosition.x + (action0 * speed * Time.deltaTime);
          float zNew = cAgent.transform.localPosition.z + (action1 * speed * Time.deltaTime);
          cAgent.transform.localPosition = new Vector3(xNew, 0, zNew); //movement towards the target
          

        //distance measurement
        
        float distance2target = Vector3.Distance(target.transform.localPosition, cAgent.transform.localPosition);

        //....move towards the target
        //we are finaly able to set the rewards
        if (distance2target < finalDistanceToTarget)
        {
            AddReward(0.3f);
        }
        else
        {
            AddReward(-0.8f);//punishment
        }


        if (cAgent.transform.localPosition.y < 0)
        {
            AddReward(-10f);
            EndEpisode();
        }


    }




    /* public override void OnActionReceived(ActionBuffers actions)
     {
         //var continuousActions = actions.ContinuousActions;//declaration of contiuous actions
         var discreteActions = actions.DiscreteActions;//declaration of contiuous actions
         //distance measurement
         float finalDistanceToTarget = Vector3.Distance(target.transform.localPosition, cAgent.transform.localPosition);
         var action = Mathf.FloorToInt(discreteActions[0]);//forcing casting the value to get an integer



         switch (action)
         {
             //move in x pos
             case 0:
                 cAgent.transform.localPosition = new Vector3(cAgent.transform.localPosition.x + stepMove, 0, cAgent.transform.localPosition.z); //adding the value to the current position
                 break;
             //move in x neg
             case 1:
                 cAgent.transform.localPosition = new Vector3(cAgent.transform.localPosition.x - stepMove, 0, cAgent.transform.localPosition.z);
                 break;
             //move in z pos
             case 2:
                 cAgent.transform.localPosition = new Vector3(cAgent.transform.localPosition.x , 0, cAgent.transform.localPosition.z + stepMove);
                 break;
             //move in z neg
             case 3:
                 cAgent.transform.localPosition = new Vector3(cAgent.transform.localPosition.x , 0, cAgent.transform.localPosition.z - stepMove);
                 break;
         }

         //action defining moving to the right of left, new x value for the agent
       //  float xNew = cAgent.transform.localPosition.x + (action * speed * Time.deltaTime);
      //   cAgent.transform.localPosition = new Vector3(xNew, 0, 0);//take this value and...

         //check the distance to target again
         float distance2target = Vector3.Distance(target.transform.localPosition, cAgent.transform.localPosition);

         //....move towards the target
         //we are finaly able to set the rewards
         if (distance2target < finalDistanceToTarget)
         {
             AddReward(0.3f);
         }
         else
         {
             AddReward(-0.8f);//punishment
         }

         if (cAgent.transform.localPosition.y < 0)
         {
             AddReward(-10f);
             EndEpisode();
         }
     }*/

    public override void OnActionReceived(ActionBuffers actionBuffers)

    {
        // Move the agent using the action.
        MoveAgent(actionBuffers.ContinuousActions);

        // Penalty given each step to encourage agent to finish task quickly.
       // AddReward(-1f / MaxStep);
    }



    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        if (Input.GetKey(KeyCode.LeftArrow)) // movement along the x axis negative
        {
            continuousActionsOut[0] = -1f;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            continuousActionsOut[1] = 1f;   // movement along the z axis
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            continuousActionsOut[0] = 1f;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            continuousActionsOut[1] = -1f;
        }
    }

   /* public override void Heuristic(in ActionBuffers actionsOut)//this is the setup of controls for a demonstrator to position a player component, which is then observed 
                                                               //also don't forget for a Decision requester script to add!
    {
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;

        if (Input.GetKey(KeyCode.RightArrow)) discreteActions[0] = 0;
        if (Input.GetKey(KeyCode.LeftArrow)) discreteActions[0] = 1;
        if (Input.GetKey(KeyCode.UpArrow)) discreteActions[0] = 2;
        if (Input.GetKey(KeyCode.DownArrow)) discreteActions[0] = 3;


    }*/

    // Update is called once per frame
    void Update()
    {
        if (cd.isCollided == true)
        {
            AddReward(10f);//final big motivation reward, the treat!
            EndEpisode();
        }

    }







}
