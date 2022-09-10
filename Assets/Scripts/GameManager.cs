using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("---CARS SETTINGS---")]
    public GameObject[] cars; 
    public int carsNumber;
    int remainingCarValue;
    int activeCarIndex = 0;    
    

    [Header("---CANVAS SETTINGS---")]
    public Sprite greenCarImage;
    public TextMeshProUGUI remainingCarNumber; //alternative UI
    public GameObject[] carCanvasImages;
    public TextMeshProUGUI[] texts;
    public GameObject[] Panels;
    public GameObject[] TapToButtons;

    [Header("---PLATFORM SETTINGS---")]
    public GameObject platform1;
    public GameObject platform2;
    public float[] rotateSpeed;
    bool isRotate;


    [Header("---LEVEL SETTINGS---")]
    public int DiamondNumber;
    public ParticleSystem accidentEffect;
    public AudioSource[] audios;
    void Start()
    {
        isRotate = true;
        checkDefaultValues();

        remainingCarValue = carsNumber;
        
       // remainingCarNumber.text = remainingCarValue.ToString();
        for (int i = 0; i < carsNumber; i++)
        {
            carCanvasImages[i].SetActive(true);
        }           

    }

    public void carSpawn()
    {
        
        remainingCarValue--;


        if (activeCarIndex < carsNumber)
        {
            cars[activeCarIndex].SetActive(true);
        }
        else
        {
            Win();
        }

            carCanvasImages[activeCarIndex - 1].GetComponent<Image>().sprite = greenCarImage;
            //add -1 because of in the below code (++)

            
         //   remainingCarNumber.text = remainingCarValue.ToString();
        

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            cars[activeCarIndex].GetComponent<Car>().moveForward = true;
            activeCarIndex++;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Panels[0].SetActive(false);
            Panels[3].SetActive(true);
        }

        if(isRotate)
        platform1.transform.Rotate(new Vector3(0, 0, -rotateSpeed[0]), Space.Self);
    }


    

    public void Lose()
    {
        isRotate=false;
        texts[6].text = PlayerPrefs.GetInt("Diamond").ToString();
        texts[7].text = SceneManager.GetActiveScene().name;
        texts[8].text = (carsNumber - remainingCarValue).ToString();
        texts[9].text = DiamondNumber.ToString();

        audios[1].Play();
        audios[3].Play();

        Panels[1].SetActive(true);
        Panels[3].SetActive(false);
        Invoke("loseinvokeButton", 2f);

    }

    void loseinvokeButton()
    {
        TapToButtons[0].SetActive(true);
    }

    void wininvokeButton()
    {
        TapToButtons[1].SetActive(true);
    }

    void Win()
    {
        PlayerPrefs.SetInt("Diamond", PlayerPrefs.GetInt("Diamond") + DiamondNumber);
        texts[2].text = PlayerPrefs.GetInt("Diamond").ToString();
        texts[3].text = SceneManager.GetActiveScene().name;
        texts[4].text = (carsNumber - remainingCarValue).ToString();
        texts[5].text = DiamondNumber.ToString();

        audios[2].Play();

        Panels[2].SetActive(true);
        Panels[3].SetActive(false);

        Invoke("wininvokeButton", 2f);
    }

    //memory mangement

    void checkDefaultValues()
    {
        if (!PlayerPrefs.HasKey("Diamond"))
        {
            PlayerPrefs.SetInt("Diamond", 0);
            PlayerPrefs.SetInt("Level", 1);
        }
         
        texts[0].text = PlayerPrefs.GetInt("Diamond").ToString();
        texts[1].text = SceneManager.GetActiveScene().name;


    }

    public void watchAndGo()
    {
        //will be improved
    }

    public void watchAndearnMoreReward()
    {
        //will be improved
    }

    public void replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void nextLevel()
    {
        PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }


}
