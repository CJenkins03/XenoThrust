using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AlienMovement : MonoBehaviour
{

    public Rigidbody2D rb;
    private GameManager gameManager;
    public GameObject explosion;

    [Header("Movement")]
    public float upwardForce;
    public float maxSpeed;
    public float maxSpeedDown;
    public bool GodMode;


    [Header("Thruster Animation")]
    public GameObject thrust1;
    public GameObject thrust2;
    public float thrustVisualTimer;
    public float thrustVisualTimerMax;
    public float thrust2Time;
    public bool thrustVisual;



    [Header("Menu Hover Animation")]
    public Transform hoverTop;
    public Transform hoverBottom;
    public float hoverTimer;
    public float hoverTimerMax;
    public float hoverSpeed;
    public Vector3 hoverPositionTarget;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameManager.Instance;
        hoverPositionTarget = hoverBottom.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.gameActive) MainMenuHoverAnimation();
        else
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.gravityScale = 1;
                rb.AddForceY(upwardForce, ForceMode2D.Impulse);
                thrustVisual = true;
            }

            if (Input.GetKeyDown(KeyCode.U)) GameManager.Instance.score = 95;

            if (Input.GetKeyDown(KeyCode.I)) GameManager.Instance.score = 195;

            if (Input.GetKeyDown(KeyCode.O)) GameManager.Instance.score = 295;

            if (Input.GetKeyDown(KeyCode.P)) GameManager.Instance.score = 395;


            //Clamp the max speed
            if (rb.linearVelocityY > maxSpeed)
            {
                rb.linearVelocityY = maxSpeed;
            }
            if (rb.linearVelocityY < -maxSpeedDown)
            {
                rb.linearVelocityY = -maxSpeedDown;
            }


            //Instead of using animations, simple turn on and off sprites when needed
            //Avoids infinte idle animation loop         
            if (thrustVisual) ThrusterVisuals();

        }

    }

    private void MainMenuHoverAnimation()
    {
        //Turn on and off thruster visuals
        ThrusterVisuals();


        //Change the hover target position
        if (hoverTimer < hoverTimerMax)
        {
            hoverTimer += hoverSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, hoverPositionTarget, hoverTimer);


        }
        else
        {
            if (hoverPositionTarget == hoverTop.position) hoverPositionTarget = hoverBottom.position;
            else hoverPositionTarget = hoverTop.position;
            hoverTimer = 0;
        }
    }


    private void ThrusterVisuals()
    {
        thrust1.SetActive(true);
        if (thrustVisualTimer < thrustVisualTimerMax)
        {
            thrustVisualTimer += Time.deltaTime;

            if (thrustVisualTimer < thrust2Time)
            {
                thrust1.SetActive(false);
                thrust2.SetActive(true);
            }

        }
        else
        {
            thrustVisual = false;
            thrust1.SetActive(false);
            thrust2.SetActive(false);
            thrustVisualTimer = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GodMode) return;
        GameManager.Instance.EndGame();
        this.gameObject.SetActive(false);
        Instantiate(explosion, transform.position, Quaternion.identity);
    }


    public void SetGravity()
    {
        Debug.Log("gravity");
        rb.gravityScale = 1;
        thrustVisualTimerMax = .5f;
        thrust2Time = 0.3f;
    }


}
