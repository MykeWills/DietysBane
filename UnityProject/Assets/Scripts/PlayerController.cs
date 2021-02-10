using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
   
    public enum PlayerSelect { PlayerOne, PlayerTwo, PlayerThree, PlayerFour }
    [Header("Player Select")]
    public PlayerSelect SelectPlayer;
    [Space]

    [Header("Player Settings")]
    public  bool fallDamage;
    public float walkSpeed = 6.0f;
    public float runSpeed = 11.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float fallingDamageThreshold = 10.0f;
    public float slideSpeed = 12.0f;
    public float antiBumpFactor = .75f;
    public float playerSpeed;
    public float timePerStep;
    public float stepsPerSecond;
    public float a = 0.24f;
    public float n = 2.0f;
    public float b = 1.68f;
    public float c = 159.0f;
    private float lastFootstepPlayedTime;
    private float velocityPollPeriod = 0.2f; // 5 times a second
    private float fallStartLevel;
    private float slideLimit;
    private float rayDistance;
    private float currentFootstepsWaitingPeriod;
    private bool playerControl = false;
    public bool limitDiagonalSpeed = true;
    public bool slideWhenOverSlopeLimit = false;
    public bool slideOnTaggedObjects = false;
    public bool airControl = false;
    private bool noStam;
    public int antiBunnyHopFactor = 1;
    private int jumpTimer;

    public CharacterController controller;
    Vector3 moveDirection = Vector3.zero;
    public Transform myTransform;
    RaycastHit hit;
    Vector3 contactPoint;
    AudioSource audioSrc;

    Vector3 lastPlayerPosition;

    public AudioSource audioSrc2;
 
    public AudioClip Jump;
    public AudioClip footStep;
    public AudioClip fallhurt;
    public AudioClip fallOof;
    public AudioClip land;
    public bool isMoving = false;
    public bool isGrounded = true;
    public bool isRunning = false;
    public bool isJumping = false;
    public bool isMovingForward;
    public bool isMovingBack;
    public bool isIdle;
    public bool isMovingRight;
    public bool isMovingLeft;
    public bool IsRunning;
    public bool isFalling = false;
    public bool Boost;
    public float BoostForce;
    float inputX;
    float inputY;
    bool SprintButton;
    bool JumpButton;
    ImpactReceiver BoostUp;
    void Awake()
    {
        isGrounded = true;
        GetComponent<Rigidbody>().freezeRotation = true;
        GetComponent<Rigidbody>().useGravity = false;
    }
    private void Start()
    {
        InvokeRepeating("EstimatePlayerVelocity_InvokeRepeating", 1.0f, velocityPollPeriod);
        audioSrc = GetComponent<AudioSource>();
        playerSpeed = walkSpeed;
        rayDistance = controller.height * .5f + controller.radius;
        slideLimit = controller.slopeLimit - .1f;
        jumpTimer = antiBunnyHopFactor;
        //myTransform = this.transform;
        BoostUp = GetComponent<ImpactReceiver>();
    }
    void FixedUpdate()
    {
       
        MovePlayer();
    }
    void Update()
    {
        SwitchPlayer(SelectPlayer);

        noStam = this.GetComponent<PlayerStats>().noStam;
        if (Boost)
        {
            audioSrc.PlayOneShot(Jump);
            BoostUp.AddImpact(Vector3.up, BoostForce);
            Boost = false;
        }
        if (inputX > 0)
        {
            SwitchMovement(MovementType.right);
        }
        else if (inputX < 0)
        {
            SwitchMovement(MovementType.left);
        }
        else if (inputY > 0)
        {
            SwitchMovement(MovementType.forward);
        }
        else if (inputY < 0)
        {
            SwitchMovement(MovementType.back);
        }
        else if (inputX == 0 && inputY == 0)
        {
            SwitchMovement(MovementType.none);
        }
        if (Time.time - lastFootstepPlayedTime > currentFootstepsWaitingPeriod && isMoving && isGrounded && !isJumping)
        {
            if (isRunning)
            {
                audioSrc.volume = Random.Range(0.7f, 1.0f);
                audioSrc.pitch = Random.Range(0.9f, 1.5f);
                SetFootStep();
            }
            else
            {
                audioSrc.volume = Random.Range(0.3f, 0.6f);
                audioSrc.pitch = Random.Range(0.6f, 0.8f);
                SetFootStep();
            }
            audioSrc.Play();
            lastFootstepPlayedTime = Time.time;
        }
        if (SprintButton && !noStam && isMoving && isGrounded)
        {
            isRunning = true;
            playerSpeed = runSpeed;
        }
        else
        {
            isRunning = false;
            playerSpeed = walkSpeed;
        }
    }
    
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Boost"))
        {
            Boost = true;
        }
        isJumping = false;
        if (!fallDamage)
        {
            if (isJumping)
            {
                audioSrc.clip = land;
                
                audioSrc.pitch = Random.Range(0.5f, 1.1f);
                audioSrc.Play();
            }
            else if (!isGrounded)
            {
                audioSrc.clip = land;
 
                audioSrc.pitch = Random.Range(0.5f, 1.1f);
                audioSrc.Play();
            }
        }
        contactPoint = hit.point;
    }
    void FallingDamageAlert(float fallDistance)
    {
        fallDamage = true;
        if (fallDistance > 25)
        {
            audioSrc2.PlayOneShot(fallhurt);
        }
        else if(fallDistance > 3 && fallDistance < 25)
            audioSrc2.PlayOneShot(fallOof);
    }
    public void MovePlayer()
    {
        SwitchPlayer(SelectPlayer);
        float inputModifyFactor = (inputX != 0.0f && inputY != 0.0f && limitDiagonalSpeed) ? .7071f : 1.0f;
        if (isGrounded)
        {
            fallDamage = false;
            bool sliding = false;
            if (Physics.Raycast(myTransform.position, -Vector3.up, out hit, rayDistance))
            {
                if (Vector3.Angle(hit.normal, Vector3.up) > slideLimit)
                    sliding = true;
            }
            else
            {
                Physics.Raycast(contactPoint + Vector3.up, -Vector3.up, out hit);
                if (Vector3.Angle(hit.normal, Vector3.up) > slideLimit)
                    sliding = true;
            }
            if (isFalling)
            {
                isFalling = false;
                if (myTransform.position.y < fallStartLevel - fallingDamageThreshold)
                    FallingDamageAlert(fallStartLevel - myTransform.position.y);
            }
            if ((sliding && slideWhenOverSlopeLimit) || (slideOnTaggedObjects && hit.collider.tag == "Slide"))
            {
                Vector3 hitNormal = hit.normal;
                moveDirection = new Vector3(hitNormal.x, -hitNormal.y, hitNormal.z);
                Vector3.OrthoNormalize(ref hitNormal, ref moveDirection);
                moveDirection *= slideSpeed;
                playerControl = false;
            }
            else
            {
                moveDirection = new Vector3(inputX, -antiBumpFactor, inputY);
                moveDirection = myTransform.TransformDirection(moveDirection) * playerSpeed;
                playerControl = true;
            }
            if (!JumpButton)
                jumpTimer++;   
            else if (jumpTimer >= antiBunnyHopFactor)
            {
                isJumping = true;
                audioSrc2.clip = Jump;
                audioSrc2.volume = Random.Range(0.8f, 1.0f);
                audioSrc2.pitch = Random.Range(0.8f, 1.0f);
                audioSrc2.PlayOneShot(Jump);
                moveDirection.y = jumpSpeed;
                jumpTimer = 0;
            }
        }
        else
        {
            if (!isFalling)
            {
                gravity = 20;
                isFalling = true;
                fallStartLevel = myTransform.position.y;
            }
            if (airControl && playerControl)
            {
                moveDirection.x = inputX * playerSpeed * inputModifyFactor;
                moveDirection.z = inputY * playerSpeed * inputModifyFactor;
                moveDirection = myTransform.TransformDirection(moveDirection);
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        isGrounded = (controller.Move(moveDirection * Time.deltaTime) & CollisionFlags.Below) != 0;
    }
    public void EstimatePlayerVelocity_InvokeRepeating()
    {
        float distanceMagnitude = (this.transform.position - lastPlayerPosition).magnitude;
        lastPlayerPosition = this.transform.position;
        float estimatedPlayerVelocity = distanceMagnitude / velocityPollPeriod;
        if (estimatedPlayerVelocity < 15.0f)
        {
            currentFootstepsWaitingPeriod = Mathf.Infinity;
            return;
        }
        else
        {
            if (isRunning)
            {
                float mappedPlayerSpeed = estimatedPlayerVelocity / 6.0f; //Convert the speed so that walking speed is about 6
                stepsPerSecond = ((a * Mathf.Pow(mappedPlayerSpeed, n)) + (b * mappedPlayerSpeed) + c) / 60.0f;
                timePerStep = (1.0f / stepsPerSecond);
                currentFootstepsWaitingPeriod = timePerStep;
            }
            else
            {
                float mappedPlayerSpeed = estimatedPlayerVelocity / 5.0f; //Convert the speed so that walking speed is about 6
                stepsPerSecond = ((a * Mathf.Pow(mappedPlayerSpeed, n)) + (b * mappedPlayerSpeed) + c) / 60.0f;
                timePerStep = (1.0f / stepsPerSecond);
                currentFootstepsWaitingPeriod = timePerStep;
            }
        } 
    }
    public enum MovementType { forward, back, left, right, none}
    public void SwitchMovement(MovementType type)
    {
        switch (type)
        {
            case MovementType.forward:
                {
                    isMoving = true;
                    isIdle = false;
                    isMovingLeft = false;
                    isMovingRight = false;
                    isMovingForward = true;
                    isMovingBack = false;
                    break;
                }
            case MovementType.back:
                {
                    isMoving = true;
                    isIdle = false;
                    isMovingLeft = false;
                    isMovingRight = false;
                    isMovingForward = false;
                    isMovingBack = true;
                    break;
                }
            case MovementType.left:
                {
                    isMoving = true;
                    isIdle = false;
                    isMovingLeft = true;
                    isMovingRight = false;
                    isMovingForward = false;
                    isMovingBack = false;
                    break;
                }
            case MovementType.right:
                {
                    isMoving = true;
                    isIdle = false;
                    isMovingLeft = false;
                    isMovingRight = true;
                    isMovingForward = false;
                    isMovingBack = false;
                    break;
                }
            case MovementType.none:
                {
                    isIdle = true;
                    isMoving = false;
                    isMovingLeft = false;
                    isMovingRight = false;
                    isMovingForward = false;
                    isMovingBack = false;
                    break;
                }
        }
    }
    public void SetFootStep()
    {
        audioSrc.clip = footStep;
    }
    public void SwitchPlayer(PlayerSelect number)
    {
        switch (number)
        {
            case PlayerSelect.PlayerOne:
                {
                    JumpButton = Input.GetKeyDown(KeyCode.Joystick1Button0);
                    inputX = Input.GetAxisRaw("Horizontal");
                    inputY = Input.GetAxisRaw("Vertical");
                    SprintButton = Input.GetButton("Sprint");
                    break;
                }
            case PlayerSelect.PlayerTwo:
                {
                    JumpButton = Input.GetKeyDown(KeyCode.Joystick2Button0);
                    inputX = Input.GetAxisRaw("Horizontal2");
                    inputY = Input.GetAxisRaw("Vertical2");
                    SprintButton = Input.GetButton("Sprint2");
                    break;
                }
            case PlayerSelect.PlayerThree:
                {
                    inputX = Input.GetAxisRaw("Horizontal3");
                    inputY = Input.GetAxisRaw("Vertical3");
                    SprintButton = Input.GetButton("Sprint3");
                    JumpButton = Input.GetKeyDown(KeyCode.Joystick3Button0);
                    break;
                }
            case PlayerSelect.PlayerFour:
                {
                    inputX = Input.GetAxisRaw("Horizontal4");
                    inputY = Input.GetAxisRaw("Vertical4");
                    SprintButton = Input.GetButton("Sprint4");
                    JumpButton = Input.GetKeyDown(KeyCode.Joystick4Button0);
                    break;
                }
        }
    }
}
