using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltWeapon : MonoBehaviour {
    public enum PlayerSelect { PlayerOne, PlayerTwo, PlayerThree, PlayerFour }
    [Header("Player Select")]
    public PlayerSelect selectPlayer;
    public float MoveAmount = 1;
    public float MoveSpeed = 1;
    public GameObject Gun;
    float MoveOnX;
    float MoveOnY;
    Vector3 defaultPos;
    Vector3 NewGunPos;
    public bool ONOFF;


    // Use this for initialization
    void Start () {
        //ONOFF = true;
        defaultPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        SwitchPlayer(selectPlayer);
       

        if (ONOFF == true)
        {
            NewGunPos = new Vector3(defaultPos.x + MoveOnX, defaultPos.y + MoveOnY, defaultPos.z);
            Gun.transform.localPosition = Vector3.Lerp(Gun.transform.localPosition, NewGunPos, MoveSpeed * Time.deltaTime);
        }
        else
        {
            ONOFF = false;
            Gun.transform.localPosition = Vector3.Lerp(Gun.transform.localPosition, defaultPos, MoveSpeed * Time.deltaTime);
        }
   
    }
    public void SwitchPlayer(PlayerSelect number)
    {
        switch (number)
        {
            case PlayerSelect.PlayerOne:
                {
                    MoveOnX = Input.GetAxis("Look X") * Time.deltaTime * MoveAmount;
                    MoveOnY = Input.GetAxis("Look Y") * Time.deltaTime * MoveAmount;
                    break;
                }
            case PlayerSelect.PlayerTwo:
                {
                    MoveOnX = Input.GetAxis("Look X2") * Time.deltaTime * MoveAmount;
                    MoveOnY = Input.GetAxis("Look Y2") * Time.deltaTime * MoveAmount;
                    break;
                }
            case PlayerSelect.PlayerThree:
                {
                    MoveOnX = Input.GetAxis("Look X3") * Time.deltaTime * MoveAmount;
                    MoveOnY = Input.GetAxis("Look Y3") * Time.deltaTime * MoveAmount;
                    break;
                }
            case PlayerSelect.PlayerFour:
                {
                    MoveOnX = Input.GetAxis("Look X4") * Time.deltaTime * MoveAmount;
                    MoveOnY = Input.GetAxis("Look Y4") * Time.deltaTime * MoveAmount;
                    break;
                }
        }
    }
}
