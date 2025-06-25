using System.Collections.Generic;
using UnityEngine;

public class ColourChanger : MonoBehaviour
{
    public static ColourChanger Instance { get; private set; }

    public SpriteRenderer background;

    public bool changeColour;
    public float changeColourTimer;
    public float changeColourTimerMax;

    public List<Color> colorList;

    public Color currentColour;
    public int currentColourIndex;

    private void Awake()
    {
        Instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        if (changeColour)
        {   
            //Lerp from the current colour to the next colour in list
            if (changeColourTimer < changeColourTimerMax)
            {
                changeColourTimer += Time.deltaTime;
                background.color = Color.Lerp(currentColour, colorList[currentColourIndex + 1], changeColourTimer);

            }
            else
            {
                changeColourTimer = 0;
                changeColour = false;
                currentColourIndex++;
            }         
        }
    }

    public void ChangeColour()
    {
        currentColour = colorList[currentColourIndex];
        changeColour = true;
        
    }
}
