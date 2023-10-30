using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rbPlayer;
    GameObject FocalPoint;
    Renderer rendererPlayer;
    public float speed = 30.0f;
    public float powerUpSpeed = 10.0f;
    public GameObject powerUpInd;

    bool hasPowerUp = false;
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

        powerUpInd.transform.position = transform.position;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUPCountDown());
            powerUpInd.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(hasPowerUp && collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody rbEnemy = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayDir = collision.gameObject.transform.position - transform.position;

            rbEnemy.AddForce(awayDir * powerUpSpeed, ForceMode.Impulse);
        }
    }

    IEnumerator PowerUPCountDown()
    {
        yield return new WaitForSeconds(8);
        hasPowerUp = false;
        powerUpInd.SetActive(false);
    }

}
