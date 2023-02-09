using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSwitchController : MonoBehaviour
{
    public Button genTwoButtonOne, genTwoButtonTwo;
    public Button genThreeButtonOne, genThreeButtonTwo, genThreeButtonThree;
    public Button genFourButtonOne, genFourButtonTwo, genFourButtonThree, genFourButtonFour;
    public SpriteRenderer playerImage;
    public GameObject generationTwoDisk, generationThreeDisk, generationFourDisk;
    public Sprite genOneVisual, genTwoVisual, genThreeVisual, genFourVisual;
    [HideInInspector] int gen;
    void Start()
    {
        gen = 4;
        generationTwoDisk.SetActive(false);
        generationThreeDisk.SetActive(false);
        generationFourDisk.SetActive(false);
        playerImage.sprite = genOneVisual;
        //Gen variable from gamemanager
        switch (gen)
        {

            case 2:
                generationTwoDisk.SetActive(true);
                playerImage.sprite = genTwoVisual;
                if (playerImage.sprite == genOneVisual)
                {
                    genTwoButtonOne.interactable = false;
                    genTwoButtonTwo.interactable = true;
                }
                else
                {
                    genTwoButtonTwo.interactable = false;
                    genTwoButtonOne.interactable = true;
                }
                break;
            case 3:
                generationThreeDisk.SetActive(true);
                playerImage.sprite = genThreeVisual;
                if (playerImage.sprite == genOneVisual)
                {
                    genThreeButtonOne.interactable = false;
                    genThreeButtonTwo.interactable = true;
                    genThreeButtonThree.interactable = true;
                }
                else if (playerImage.sprite == genTwoVisual)
                {
                    genThreeButtonTwo.interactable = false;
                    genThreeButtonOne.interactable = true;
                    genThreeButtonThree.interactable = true;
                }
                else
                {
                    genThreeButtonOne.interactable = true;
                    genThreeButtonTwo.interactable = true;
                    genThreeButtonThree.interactable = false;
                }
                break;
            case 4:
                generationFourDisk.SetActive(true);
                playerImage.sprite = genFourVisual;
                if (playerImage.sprite == genOneVisual)
                {
                    genFourButtonOne.interactable = false;
                    genFourButtonTwo.interactable = true;
                    genFourButtonThree.interactable = true;
                    genFourButtonFour.interactable = true;
                }
                else if (playerImage.sprite == genTwoVisual)
                {
                    genFourButtonTwo.interactable = false;
                    genFourButtonOne.interactable = true;
                    genFourButtonThree.interactable = true;
                    genFourButtonFour.interactable = true;
                }
                else if (playerImage.sprite == genThreeVisual)
                {
                    genFourButtonThree.interactable = false;
                    genFourButtonOne.interactable = true;
                    genFourButtonTwo.interactable = true;
                    genFourButtonFour.interactable = true;
                }
                else
                {
                    genFourButtonFour.interactable = false;
                    genFourButtonOne.interactable = true;
                    genFourButtonTwo.interactable = true;
                    genFourButtonThree.interactable = true;
                }
                break;
        }
    }
    public void GenOneButton()
    {
        playerImage.sprite = genOneVisual;
        switch (gen)
        { 
            case 2:
                {
                    genTwoButtonOne.interactable = false;
                    genTwoButtonTwo.interactable = true;
                }
                break;

            case 3:
                {
                    genThreeButtonOne.interactable = false;
                    genThreeButtonTwo.interactable = true;
                    genThreeButtonThree.interactable = true;
                }
                break;

            case 4:
                {
                    genFourButtonOne.interactable = false;
                    genFourButtonTwo.interactable = true;
                    genFourButtonThree.interactable = true;
                    genFourButtonFour.interactable = true;
                }
                break;
        }
    }

    public void GenTwoButton()
    {
        playerImage.sprite = genTwoVisual;
        switch (gen)
        {

            case 2:
                {
                    genTwoButtonOne.interactable = true;
                    genTwoButtonTwo.interactable = false;
                }
                break;

            case 3:
                {
                    genThreeButtonOne.interactable = true;
                    genThreeButtonTwo.interactable = false;
                    genThreeButtonThree.interactable = true;
                }
                break;

            case 4:
                {
                    genFourButtonOne.interactable = true;
                    genFourButtonTwo.interactable = false;
                    genFourButtonThree.interactable = true;
                    genFourButtonFour.interactable = true;
                }
                break;
        }
    }
    public void GenThreeButton()
    {
        playerImage.sprite = genThreeVisual;
        switch (gen)
        {
            case 3:
                {
                    genThreeButtonOne.interactable = true;
                    genThreeButtonTwo.interactable = true;
                    genThreeButtonThree.interactable = false;
                }
                break;

            case 4:
                {
                    genFourButtonOne.interactable = true;
                    genFourButtonTwo.interactable = true;
                    genFourButtonThree.interactable = false;
                    genFourButtonFour.interactable = true;
                }
                break;
        }
    }

    public void GenFourButton()
    {
        playerImage.sprite = genFourVisual;
        genFourButtonOne.interactable = true;
        genFourButtonTwo.interactable = true;
        genFourButtonThree.interactable = true;
        genFourButtonFour.interactable = false;
    }

}
