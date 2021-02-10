using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MykesHeadBobbing : MonoBehaviour {
    public enum PlayerSelect { PlayerOne, PlayerTwo, PlayerThree, PlayerFour }
    [Header("Player Select")]
    public PlayerSelect selectPlayer;
    private float timer = 0.0f;
    public GameObject PlayCtrl;
    public float bobbingSpeed;
    public float bobbingAmount;
    public float bobbingRunSpeed;
    public float bobbingRunAmount;
    public float bobbingWalkSpeed;
    public float bobbingWalkAmount;
    float midpoint = 0.5f;
    bool isGrounded;
    bool isRunning;
    bool isMoving;
    float horizontal;
    float vertical;

    private void FixedUpdate()
    {
        isMoving = PlayCtrl.GetComponent<PlayerController>().isMoving;
    }

    void Update()
    {
        SwitchPlayer(selectPlayer);
        isGrounded = PlayCtrl.GetComponent<PlayerController>().isGrounded;
        isRunning = PlayCtrl.GetComponent<PlayerController>().isRunning;
        float waveslice = 1.0f;
        if (isRunning)
        {
            bobbingSpeed = bobbingRunSpeed;
            bobbingAmount = bobbingRunAmount;
        }
        else if (!isRunning)
        {
            bobbingSpeed = bobbingWalkSpeed;
            bobbingAmount = bobbingWalkAmount;
        }

        

        if (isMoving && isGrounded)
        {
            Vector3 cSharpConversion = transform.localPosition;
            if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
            {
                timer = 0.0f;
            }
            else
            {
                waveslice = Mathf.Sin(timer);
                timer = timer + bobbingSpeed;
                if (timer > Mathf.PI * 2)
                {
                    timer = timer - (Mathf.PI * 2);
                }
            }
            if (waveslice != 0)
            {
                float translateChange = waveslice * bobbingAmount;
                float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
                totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
                translateChange = totalAxes * translateChange;
                cSharpConversion.y = midpoint + translateChange;
            }
            else
            {
                cSharpConversion.y = midpoint;
            }

            transform.localPosition = cSharpConversion;
        }
    }
    public void SwitchPlayer(PlayerSelect number)
    {
        switch (number)
        {
            case PlayerSelect.PlayerOne:
                {
                    horizontal = Input.GetAxis("Horizontal");
                    vertical = Input.GetAxis("Vertical");
                    break;
                }
            case PlayerSelect.PlayerTwo:
                {
                    horizontal = Input.GetAxis("Horizontal2");
                    vertical = Input.GetAxis("Vertical2");
                    break;
                }
            case PlayerSelect.PlayerThree:
                {
                    horizontal = Input.GetAxis("Horizontal3");
                    vertical = Input.GetAxis("Vertical3");
                    break;
                }
            case PlayerSelect.PlayerFour:
                {
                    horizontal = Input.GetAxis("Horizontal4");
                    vertical = Input.GetAxis("Vertical4");
                    break;
                }
        }
    }
}