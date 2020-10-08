using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnWorld : MonoBehaviour
{
    Collider collider;
    public GameObject world;
    public Transform Player;
    public float speed;
    public int direction;
    public Turn turn;
    public bool CanRotate;
    public int maxturn;
    public enum Turn
    {
        Left,
        Right,
    }
    void Start()
    {
        collider = GetComponent<Collider>();
        direction = (turn == Turn.Left )? 1 : -1;
        maxturn = 90;
    }

    // Update is called once per frame
    void Update()
    {
        
            if (CanRotate)
            {
                world.transform.RotateAround(Player.position, Vector3.up, direction*speed);
            }

            if (Mathf.Abs(world.transform.localEulerAngles.magnitude) >= 90.0f)
            {
                CanRotate = false;
            
            }

    }

    private void OnTriggerEnter(Collider other)
    {
        CanRotate = true;
    }
  



}
