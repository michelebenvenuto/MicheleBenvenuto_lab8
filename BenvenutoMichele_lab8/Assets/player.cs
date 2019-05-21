using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    public Light[] lights1;
    public Light[] lights2;
    public Light light3;
    public Light flashLight;
    public Text objectSeen;
    public AudioClip flashlightOn;
    public AudioClip houseEnter;
    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray aim = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
        RaycastHit hitInfo;
        if (Physics.Raycast(aim, out hitInfo))
        {
            string newText = hitInfo.collider.tag;
            if (newText == "Untagged"|| newText=="Small1"||newText =="Small2"|| newText=="Big")
            {
                objectSeen.text = "";
            }
            else
            {
                objectSeen.text = newText;
            }
        }
        //flashlight on and off
        if (Input.GetMouseButtonDown(0))
        {
            if (flashLight.enabled)
            {
                audio.PlayOneShot(flashlightOn);
                flashLight.enabled = false;
            }
            else if (!flashLight.enabled)
            {
                audio.PlayOneShot(flashlightOn);
                flashLight.enabled = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        audio.PlayOneShot(houseEnter);
        if (other.gameObject.CompareTag("Small1"))
        {
            for (int i = 0; i < lights1.Length; i++)
            {
                lights1[i].enabled = true;
            }
        }
        else if (other.gameObject.CompareTag("Small2"))
        {
            for (int i = 0; i < lights2.Length; i++)
            {
                lights2[i].enabled = true;
            }
        }
        else if (other.gameObject.CompareTag("Big"))
        {
            light3.enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        audio.PlayOneShot(houseEnter);
        if (other.CompareTag("Small1"))
        {
            
            for (int i = 0; i < lights1.Length; i++)
            {
                lights1[i].enabled = false;
            }
        }
        else if (other.gameObject.CompareTag("Small2"))
        {
            for (int i = 0; i < lights2.Length; i++)
            {
                lights2[i].enabled = false;
            }
        }
        else if (other.gameObject.CompareTag("Big"))
        {
            light3.enabled = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Big"))
        {
            float newIntesity = light3.intensity + Mathf.Sin(Time.time);
            light3.intensity = newIntesity;
        }
    }
}
