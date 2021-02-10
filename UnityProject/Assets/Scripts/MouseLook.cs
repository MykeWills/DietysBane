using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Look")]

public class MouseLook : MonoBehaviour {
    public enum PlayerSelect { PlayerOne, PlayerTwo, PlayerThree, PlayerFour }
    [Header("Player Select")]
    public PlayerSelect SelectPlayer;
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 5f;
    public float sensitivityY = 5f;
    public float minimumY = -30f;
    public float maximumY = 45f;
    private float rotationY = 0f;
    public bool SmoothRotation;
    float inputX;
    float inputY;



    void Start()
    {
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
    }
    void Update ()
	{
        SwitchPlayer(SelectPlayer);
        if (axes == RotationAxes.MouseXAndY )
		{
            float rotationX = transform.localEulerAngles.y +  inputX * sensitivityX;
            rotationY += inputY * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
		else if (axes == RotationAxes.MouseX)
		{
            if (SmoothRotation)
            {
                transform.Rotate(0, inputX * Time.deltaTime * sensitivityX, 0);
            }
            else
            {
                transform.Rotate(0, inputX * sensitivityX, 0);
            }
        }
		else
		{
            if (SmoothRotation)
            {
                rotationY += inputY * Time.deltaTime * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
            }
            else
            {
                rotationY += inputY * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
            }
            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		}
    }
    public void SwitchPlayer(PlayerSelect number)
    {
        switch (number)
        {
            case PlayerSelect.PlayerOne:
                {
                    inputX = Input.GetAxis("Look X");
                    inputY = Input.GetAxis("Look Y");
                    break;
                }
            case PlayerSelect.PlayerTwo:
                {
                    inputX = Input.GetAxis("Look X2");
                    inputY = Input.GetAxis("Look Y2");
                    break;
                }
            case PlayerSelect.PlayerThree:
                {
                    inputX = Input.GetAxis("Look X3");
                    inputY = Input.GetAxis("Look Y3");
                    break;
                }
            case PlayerSelect.PlayerFour:
                {
                    inputX = Input.GetAxis("Look X4");
                    inputY = Input.GetAxis("Look Y4");
                    break;
                }
        }
    }
}