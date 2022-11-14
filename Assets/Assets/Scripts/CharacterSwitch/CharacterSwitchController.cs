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
        //Gen variable from gamemanager
        switch (gen)
        {
            
            case 2:
                generationTwoDisk.SetActive(true);
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
                break ;
            case 4:
                generationFourDisk.SetActive(true);
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
        genFourButtonFour.interactable = true;
    }

    public void GenTwoButton()
    {
        playerImage.sprite = genTwoVisual;
        genFourButtonFour.interactable = true;
    }
    public void GenThreeButton()
    {
        playerImage.sprite = genThreeVisual;
        genFourButtonFour.interactable = true;
    }

    public void GenFourButton()
    {
        playerImage.sprite = genFourVisual;

    }

}
