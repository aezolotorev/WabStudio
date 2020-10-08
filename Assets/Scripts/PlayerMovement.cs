using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _charController;
    [SerializeField]
    private PlayerProperties playerProp;
    Vector3 moveVec;
    [SerializeField]
    float startspeed;
    [SerializeField]
    float plusspeed;
    [SerializeField]
    float speed;
    [SerializeField]
    float speedturn;
    [SerializeField]
    float jumpSpeed;
    Vector3 gravity;
    [SerializeField]
    GameManager GM;
    public Joystick joystick;
    public float  distanceGo, distanceREady,alldistance;
    public Vector3 lastposition;
    public Slider slider;
    public Distance dist;
    public GameObject swcontroller;
    public GameObject prefinish;
    public Energy energy;
    public int addeng;
    private const string banner = "ca-app-pub-7412699532104812/6212400035";

    private void Awake()
    {
        prefinish.SetActive(true);
    }

    void Start()
    {
        speed =  playerProp.startspeed;
        jumpSpeed = playerProp.jumpspeed;
        speedturn = playerProp.turnspeed;
        _charController = GetComponent<CharacterController>();
        gravity = Vector3.zero;
        playerProp.isMoving = true;
        playerProp.isFlying = false;
        playerProp.isAlive = true;
        playerProp.inJumpPlatform = false;
        alldistance = dist.distance;
        lastposition = transform.position;
        SwipeController.SwipeEvent += Tap;
        BannerView bannerView = new BannerView(banner, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice("F5695C1E268D083B").Build();
        bannerView.LoadAd(request);
        prefinish.SetActive(false);
    }
    void FixedUpdate()
    {
        distanceGo = Mathf.Abs(transform.position.x - lastposition.x);
        distanceREady += distanceGo;
        lastposition.x = transform.position.x;
        slider.value = distanceREady/alldistance;
        
    }

    
    void Update()
    {
        StartCoroutine(DownSpeed());
        if (playerProp.isAlive)
        {
            Move();
        }
        
    }


    void Tap(SwipeController.SwipeType type)
    {
        if (type == SwipeController.SwipeType.Tap)
        {
            energy.AddEnergy(addeng);
        }

    }
    private void Move()
    {
        if (_charController.isGrounded)
        {
            gravity = Vector3.zero;
            playerProp.isFlying = false;
            if (Input.GetAxisRaw("Vertical") > 0 || playerProp.inJumpPlatform)
            {
                gravity.y = jumpSpeed;
                playerProp.isJump = true;
                playerProp.isMoving = false;


            }


        }

        else
        {
            gravity += Physics.gravity * Time.deltaTime * 2;
            playerProp.isJump = false;
            playerProp.isFlying = false;
            if (_charController.velocity.y < 0)
            {
                playerProp.isFlying = true;
            }
        }




        float inputZ = joystick.Horizontal* speedturn;
        
        moveVec = new Vector3(inputZ*speedturn, 0, speed);
        moveVec += gravity;
        moveVec *= Time.deltaTime;
        moveVec = transform.TransformDirection(moveVec);
        _charController.Move(moveVec);
    }

    void Speed(float number)
    {
        speed += number;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)

    {
        if (hit.gameObject.CompareTag("Complete"))
        {
                       
            Destroy(hit.gameObject);
            GM.Levelcomplete();

        }

        if (hit.gameObject.CompareTag("Finishjump"))
        {
            playerProp.inJumpPlatform = true;
            jumpSpeed = jumpSpeed  * energy.currentenergy /80;
            Destroy(hit.gameObject);
            StartCoroutine(JumpPlatform());

        }


        if (hit.gameObject.CompareTag("Finish"))
        {
            swcontroller.SetActive(true);
            prefinish.SetActive(true);

            Destroy(hit.gameObject);
            

        }

        if (hit.gameObject.CompareTag("Speed"))
        {
            Destroy(hit.gameObject);
            Speed(2);         

        }


        if (hit.gameObject.CompareTag("JumpPlatform"))
        {
            playerProp.inJumpPlatform = true;
            StartCoroutine(JumpPlatform());
        }
    
        if (hit.gameObject.CompareTag("Diamonds"))
        {
            
            GM.AddCoin(1);
            Destroy(hit.gameObject);
        }
        if (hit.gameObject.CompareTag("DeathPlane"))
        {
            playerProp.isAlive = false;
            GM.GameOver();
        }
        
        if (!hit.gameObject.CompareTag("Saw"))
            return;
     
        StartCoroutine(Death());
    }

    IEnumerator JumpPlatform()
    {
        yield return new WaitForSeconds(0.1f);
        playerProp.inJumpPlatform = false;
    }

    IEnumerator Death()
    {
        playerProp.isAlive = false;
        yield return new WaitForSeconds(2);
        GM.GameOver();
    }

    void SpeedDown()
    {
          if (speed > startspeed)
                speed -= 0.5f*Time.deltaTime;
    }

    IEnumerator DownSpeed()
    {

        yield return new WaitForFixedUpdate();
        SpeedDown();
    }
}
