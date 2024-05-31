using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents; //allows us to use ml agents in Unity
using Unity.MLAgents.Sensors;
using Random = UnityEngine.Random;
using System;
using Unity.MLAgents.Actuators;

public class CubeAgent : Agent //Agent class inheriting all functionality of the ML Agents
{
    //object references
    public GameObject cAgent;
    public GameObject target;

    //reference collision detector
    colliderDetector cd;

    //parameters
    public float speed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        cd = cAgent.GetComponent<colliderDetector>();//accessing the class of the game object component
    }


    public override void OnEpisodeBegin()
        
        {
        cd.isCollided = false; //this resets the collision parameter to false


        cAgent.transform.localPosition = Vector3.zero;
        //set random position of the target
        target.transform.localPosition = new Vector3(Random.Range(-10.0f, 8.0f) * (Random.value <= 0.5 ? 1 : -1), 0, 0);
        }


    public override void CollectObservations(VectorSensor sensor)
    {
        // what to observe:
        //position of the agent
        //position of the agent
        //2 observations with 1 branch (basically one direction only)
        sensor.AddObservation(cAgent.transform.localPosition.x);
        sensor.AddObservation(target.transform.localPosition.x);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var continuousActions = actions.ContinuousActions;//declaration of contiuous actions

        //distance measurement
        float finalDistanceToTarget = Vector3.Distance(target.transform.localPosition, cAgent.transform.localPosition);

        //action defining moving to the right of left, new x value for the agent
        float xNew = cAgent.transform.localPosition.x + (continuousActions[0] * speed * Time.deltaTime);
        cAgent.transform.localPosition = new Vector3(xNew, 0, 0);//take this value and...
        
        //check the distance to target again
        float distance2target = Vector3.Distance(target.transform.localPosition, cAgent.transform.localPosition);
        
        //....move towards the target
        //we are finaly able to set the rewards
        if(distance2target<finalDistanceToTarget)
        {
            AddReward(0.1f);
        }
        else
        {
            AddReward(-0.3f);//punishment
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (cd.isCollided==true)
        {
            AddReward(2f);//final big reward, the treat!
            EndEpisode();
        }

        
    }
}
