using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwipeController : MonoBehaviour
{
    [SerializeField]
    bool isDragging, isMobilePlatform;
    Vector2 tapPoint, swipedDelta;
    float minSwipeDelta = 130;

    public enum SwipeType
    {
      Tap
    }
    public delegate void OnSwipeInput(SwipeType type);
    public static event OnSwipeInput SwipeEvent;

    private void Awake()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        isMobilePlatform = false;
#else
        isMobilePlatform = true;
#endif
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isMobilePlatform)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SwipeEvent(SwipeType.Tap);
            }
          
        }
        else
        {
            if (Input.touchCount > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    SwipeEvent(SwipeType.Tap);

                }
               
            }
        }


    }


}
