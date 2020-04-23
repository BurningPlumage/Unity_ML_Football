using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using MLAgents.Sensors;

public class AIrobot : Agent
{
    public float speed;
    private float count;

    private Rigidbody rig;
    public Rigidbody ballrig;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        count += Time.deltaTime;
    }

    public override void OnEpisodeBegin()
    {
        rig.velocity = Vector3.zero;
        rig.angularVelocity = Vector3.zero;
        ballrig.velocity = Vector3.zero;
        ballrig.angularVelocity = Vector3.zero;

        goal.isgoal = false;
        count = 0;

        ballrig.transform.position = new Vector3(Random.Range(-3.0f, 3.0f), 1, Random.Range(-3.0f, 2.0f));
        rig.transform.position = new Vector3(Random.Range(-3.0f, 3.0f), 1, Random.Range(-6.5f, ballrig.transform.position.z-1));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(ballrig.position);
        sensor.AddObservation(rig.velocity.x);
        sensor.AddObservation(rig.velocity.z);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        Vector3 control = Vector3.zero;
        control.x = vectorAction[0];
        control.z = vectorAction[1];
        rig.AddForce(control * speed);

        //succese
        if (goal.isgoal)
        {
            SetReward(1);
            EndEpisode();
        }

        //fell
        if (ballrig.position.y < -10||rig.position.y<-10||count>=60)
        {
            SetReward(-1);
            EndEpisode();
        }
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxis("Horizontal");
        actionsOut[1] = Input.GetAxis("Vertical");
    }
}
