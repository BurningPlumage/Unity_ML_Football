using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAgents;
using MLAgents.Sensors;

public class AIrobot : Agent
{
    public float speed;
    private float count;

    private Rigidbody rig;
    public Rigidbody ballrig;

    public Text time;
    public Text score;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        count += Time.deltaTime;
        time.text = count.ToString("F3");
    }

    public override void OnEpisodeBegin()
    {
        rig.velocity = Vector3.zero;
        rig.angularVelocity = Vector3.zero;
        ballrig.velocity = Vector3.zero;
        ballrig.angularVelocity = Vector3.zero;

        goal.isgoal = false;
        count = 0;

        ballrig.GetComponent<TrailRenderer>().enabled = false;
        ballrig.transform.position = new Vector3(Random.Range(-3.5f, 3.5f), 1, Random.Range(-3.0f, 2.0f));
        ballrig.GetComponent<TrailRenderer>().enabled = true;

        rig.transform.position = new Vector3(Random.Range(-4.0f, 4.0f), 1, Random.Range(-6.0f, ballrig.transform.position.z-2));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(rig.velocity.x);
        sensor.AddObservation(rig.velocity.z);
        sensor.AddObservation(ballrig.position);
        sensor.AddObservation(ballrig.velocity.x);
        sensor.AddObservation(ballrig.velocity.z);
        //sensor.AddObservation(count);
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
            score.text = "+1";
            EndEpisode();
        }

        //fell
        if (ballrig.position.y < -3||rig.position.y<-3)
        {
            SetReward(-1);
            score.text = "-1";
            EndEpisode();
        }

        //overtime
        if (count >= 30)
        {
            score.text = "0";
            EndEpisode();
        }
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxis("Horizontal");
        actionsOut[1] = Input.GetAxis("Vertical");
    }
}
