using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public Text countText;
    public Text bestTimeText;
    public Text timeText;
    public Button restartButton;
    public GameObject WinTextobject;
    public FixedJoystick joystick;
    float startTime;
    float bestTime = Mathf.Infinity;

    private bool gameIsOver = false;




    Rigidbody rb;
    int count;
    // Start is called before the first frame update

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        
        WinTextobject.SetActive(false);

        restartButton.gameObject.SetActive(false);
        restartButton.onClick.AddListener(RestartGame);
        startTime = Time.time;

      

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = joystick.Horizontal;
        float moveVertical = joystick.Vertical;
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
       
        
        
    }

    void Update()
    {
        if (!gameIsOver) 
        {
            float currentTime = Time.time - startTime;
            timeText.text = "Time: " + currentTime.ToString("F2");

            if (currentTime < bestTime)
            {
                bestTime = currentTime;
                bestTimeText.text = "Best Record: " + startTime.ToString("F2");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText() ;
           

        }    
    }

    void SetCountText()
    {
        countText.text = "Count : " + count.ToString();

        if (count >= 12)
        {
            gameIsOver = true;
            WinTextobject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }
            
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
