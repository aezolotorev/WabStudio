using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance : MonoBehaviour
{
    public Transform[] nodes;
    public float distance;

    void Start()
    {
        distance = 0;
        for(int i=0; i<nodes.Length-1; i++)
        {
            distance+=Vector3.Distance(nodes[i].position, nodes[i + 1].position);
        }
        
    }

    
}
