using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour {

    public Vector3 rotations = new Vector3(0, 0, 0);
    Quaternion orgRotation;
    public bool spin;
    public bool bounce;
    public bool fade;
    public float approachSpeed = 0.02f;
    public float growthBound = 2f;
    public float shrinkBound = 0.5f;
    private float currentRatio = 1;
    public bool uiImage;
    public bool uiText;
    public bool wasDisabled;
    private Image image;
    private Text text;
    private bool keepGoing = true;
    public bool StartUIHover;


    private void OnEnable()
    {
        if (wasDisabled)
        {
            if (uiImage)
            {
                this.image = this.gameObject.GetComponent<Image>();
            }
            if (uiText)
            {
                this.text = this.gameObject.GetComponent<Text>();
            }
            if (StartUIHover)
            {
                InitPulse(true);
            }
        }

    }
    void Awake()
    {
        if (uiImage)
        {
            this.image = this.gameObject.GetComponent<Image>();
            orgRotation = this.image.rectTransform.localRotation;
        }
        if (uiText)
        {
            this.text = this.gameObject.GetComponent<Text>();
            orgRotation = this.text.rectTransform.localRotation;
        }
        if (StartUIHover)
        {
            InitPulse(true);
        }

    }

    void Update() {
        // if Spin was updated //
        if (spin)
        {
            if (uiImage)
                this.image.rectTransform.Rotate(new Vector3(rotations.x, rotations.y, rotations.z));
            else if (uiText)
                this.text.rectTransform.Rotate(new Vector3(rotations.x, rotations.y, rotations.z));
        }
        else
        {
            if (uiImage)
            {
                this.image.rectTransform.Rotate(new Vector3(0, 0, 0));
                this.image.rectTransform.localRotation = orgRotation;
            }
            else if (uiText)
            {
                this.text.rectTransform.Rotate(new Vector3(0, 0, 0));
                this.text.rectTransform.localRotation = orgRotation;
            }
        }


        // If fade was updated //
        if (fade)
        {
            if (uiImage)
                this.image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.PingPong(Time.time, 5));
            else if (uiText)
                this.text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.PingPong(Time.time, 1));
        }
        else
        {
            if (uiImage)
                this.image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
            else if (uiText)
                this.text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        }
    }
    IEnumerator PulseText()
    {
        // Run this indefinitely
        while (keepGoing)
        {
            // Get bigger for a few seconds
            while (this.currentRatio != this.growthBound)
            {
                // Determine the new ratio to use
                currentRatio = Mathf.MoveTowards(currentRatio, growthBound, approachSpeed);

                // Update our text element
                this.text.transform.localScale = Vector3.one * currentRatio;

                yield return new WaitForEndOfFrame();
            }

            // Shrink for a few seconds
            while (this.currentRatio != this.shrinkBound)
            {
                // Determine the new ratio to use
                currentRatio = Mathf.MoveTowards(currentRatio, shrinkBound, approachSpeed);

                // Update our text element
                this.text.transform.localScale = Vector3.one * currentRatio;
                yield return new WaitForEndOfFrame();
            }
        }
    }
    IEnumerator PulseImage()
    {
        // Run this indefinitely
        while (keepGoing)
        {
            // Get bigger for a few seconds
            while (this.currentRatio != this.growthBound)
            {
                // Determine the new ratio to use
                currentRatio = Mathf.MoveTowards(currentRatio, growthBound, approachSpeed);

                // Update our text element
                this.image.transform.localScale = Vector3.one * currentRatio;
                yield return new WaitForEndOfFrame();
            }

            // Shrink for a few seconds
            while (this.currentRatio != this.shrinkBound)
            {
                // Determine the new ratio to use
                currentRatio = Mathf.MoveTowards(currentRatio, shrinkBound, approachSpeed);

                // Update our text element
                this.image.transform.localScale = Vector3.one * currentRatio;
                yield return new WaitForEndOfFrame();
            }
        }
    }
    public void StartPulse()
    {
        keepGoing = true;
        if (uiImage)
        {
            StartCoroutine(this.PulseImage());
        }
        if (uiText)
        {
            StartCoroutine(this.PulseText());
        }

    }
    public void StopPulse()
    {
        keepGoing = false;

    }
    public void StartSpin()
    {
        spin = true;
    }
    public void StopSpin()
    {
        spin = false;
    }
    public void StartFade()
    {
        fade = true;
    }
    public void StopFade()
    {
        fade = false;
    }
    public void InitPulse(bool True)
    {
        if (True)
        {
            keepGoing = true;
            if (uiImage)
            {
                StartCoroutine(this.PulseImage());
            }
            if (uiText)
            {
                StartCoroutine(this.PulseText());
            }
            True = false;
        }

    }
}
