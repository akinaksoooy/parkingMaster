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

    public GameManager _gameManager;

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


        if (collision.gameObject.tag == "Parking")
        {
            carTechnics();
            transform.SetParent(parent);

            _gameManager.carSpawn();
        }


        else if (collision.gameObject.CompareTag("Car"))
        {
            _gameManager.accidentEffect.transform.position = ParticlePoint.transform.position;
            _gameManager.accidentEffect.Play();
            carTechnics();
            _gameManager.Lose();
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
            _gameManager.DiamondNumber++;
            _gameManager.audios[0].Play();
        }
        else if (other.CompareTag("OrtaGobek"))
        {
            _gameManager.accidentEffect.transform.position = ParticlePoint.transform.position;
            _gameManager.accidentEffect.Play();
            carTechnics(); 
            _gameManager.Lose();
        }
        else if (other.CompareTag("FrontParking"))
        {
            //   other.gameObject.GetComponent<frontParking>().activateParking();
            other.gameObject.GetComponent<frontParking>().parking.SetActive(true);
        }
    }
}

