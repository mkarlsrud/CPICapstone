﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GoToNextWall : MonoBehaviour
{
    public Queue<Transform> walls;
    private Transform target;
    NavMeshAgent agent;
    Rigidbody rigidbody;
    public int startAtWall = -1;

    // Use this for initialization
    void Start()
    {
        //Next, figure out the rail order
        GameObject rail = GameObject.Find("Rail");
        List<Transform> list = new List<Transform>();
        foreach(Transform block in rail.transform)
        {
            int n;
            if (int.TryParse(block.gameObject.name, out n))
                list.Add(block);
        }
        list = list.OrderBy(o => int.Parse(o.gameObject.name)).ToList();

        walls = new Queue<Transform>(list);

        rigidbody = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        if(startAtWall > -1){
            while(walls.Peek().gameObject.name != startAtWall.ToString()){
                walls.Dequeue();
            }
            
            agent.enabled = false;
            transform.position = GameObject.Find(startAtWall.ToString()).transform.position;
            agent.enabled = true;
        }

        target = walls.Dequeue();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("going to block " + target.gameObject.name);
        agent.SetDestination(target.position);

        //If falling
        if(!agent.enabled)
            transform.Translate(Vector3.forward / agent.speed);
    }

    void OnTriggerEnter(Collider col)
    {
        if (/*col.gameObject.tag == "destination" && */col.gameObject.name == target.gameObject.name)
        {
            target = walls.Dequeue();
            //Destroy(col.gameObject);
        }

        //Special conditions for falling
        if (col.gameObject.name == "72" || col.gameObject.name == "86")
        {
            agent.enabled = false;
            rigidbody.useGravity = true;
        }
        if (col.gameObject.name == "fallOnMe")
        {
            agent.enabled = true;
            rigidbody.useGravity = false;
        }
    }
    
    public void setTarget()
    {
        target = walls.Dequeue();
    }
}
