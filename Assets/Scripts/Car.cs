using System.Collections;
using System.Collections.Generic;
using UnityEditor.TextCore.Text;
using UnityEngine;

public class Car : MonoBehaviour
{
    public bool moveForward;
    bool isStopperActive = false;


    public GameObject[] wheelTrace;

    public Transform parent;

    public GameManager GameManager;

    public GameObject ParticlePoint;


    void Update()
    {
        if (!isStopperActive)
            transform.Translate(7f * Time.deltaTime * transform.forward);

        if (moveForward)
            transform.Translate(15f * Time.deltaTime * transform.forward);
    }

    private void OnCollisionEnter(Collision collision)
    {


        if (collision.gameObject.CompareTag("Parking"))
        {
            carTechnics();
            transform.SetParent(parent);

            GameManager.carSpawn();
        }


        else if (collision.gameObject.CompareTag("Car"))
        {
            GameManager.accidentEffect.transform.position = ParticlePoint.transform.position;
            GameManager.accidentEffect.Play();
            carTechnics();
            GameManager.Lose();
        }

    }

    void carTechnics()
    {
        moveForward = false;
        wheelTrace[0].SetActive(false);
        wheelTrace[1].SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stopper"))
        {
            isStopperActive = true;

        }
        else if (other.CompareTag("Diamond"))
        {
            other.gameObject.SetActive(false);
            GameManager.DiamondNumber++;
            GameManager.audios[0].Play();
        }
        else if (other.CompareTag("OrtaGobek"))
        {
            GameManager.accidentEffect.transform.position = ParticlePoint.transform.position;
            GameManager.accidentEffect.Play();
            carTechnics(); 
            GameManager.Lose();
        }
        else if (other.CompareTag("FrontParking"))
        {
            //   other.gameObject.GetComponent<frontParking>().activateParking();
            other.gameObject.GetComponent<frontParking>().parking.SetActive(true);
        }
    }
}

