using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rbPlayer;
    GameObject FocalPoint;
    Renderer rendererPlayer;
    public float speed = 30.0f;
    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
        FocalPoint = GameObject.Find("FocalPoint");
        rendererPlayer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float magnitude = forwardInput * speed * Time.deltaTime;
        rbPlayer.AddForce(FocalPoint.transform.forward * forwardInput * speed * Time.deltaTime, ForceMode.Impulse);

        Debug.Log("Mag:" + magnitude);
        Debug.Log("FI:" + forwardInput);

        if(forwardInput > 0)
        {
            rendererPlayer.material.color = new Color(1.0f - forwardInput, 1.0f, 1.0f - forwardInput);
        }

        else
        {
            rendererPlayer.material.color = new Color(1.0f + forwardInput, 1.0f, 1.0f + forwardInput);
        }
    }

}