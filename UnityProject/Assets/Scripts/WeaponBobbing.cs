using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBobbing : MonoBehaviour
{
    public enum PlayerSelect { PlayerOne, PlayerTwo, PlayerThree, PlayerFour }
    [Header("Player Select")]
    public PlayerSelect selectPlayer;
    public GameObject PlayCtrl;
    public GameObject Bobbing;
    private float timer = 0.0f;
    float bobbingSpeed;
    float bobbingAmount;
    float midpoint = 0.0f;
    bool isJumping;
    float horizontal;
    float vertical;

    void Update()
    {
        bobbingSpeed = Bobbing.GetComponent<MykesHeadBobbing>().bobbingSpeed;
        bobbingAmount = Bobbing.GetComponent<MykesHeadBobbing>().bobbingAmount;
        isJumping = PlayCtrl.GetComponent<PlayerController>().isJumping;

        if (!isJumping)
        {
            float waveslice = 0.0f;
            SwitchPlayer(selectPlayer);

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
                totalAxes = Mathf.Clamp(totalAxes, 0.1f, 0.1f);
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