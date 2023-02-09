using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Storytelling : MonoBehaviour
{

    [HideInInspector] public int i;
    public Transform topLeft, topRight, bottomLeft, bottomRight;
    public Image topLeftImage, topRightImage, bottomLeftImage, bottomRightImage;
    public TriggerStory triggerStory;
    public Transform initialTopLeftPos, initialBottomRightPos, initialTopRightPos, initialBottomLeftPos;

    void Start()
    {
        Init();
    }

    void Slide(int i)
    {
        switch (i % 5)
        {
            case 0:
                topLeftImage.transform.DOMove(topLeft.transform.position, 1f);
                break;
            case 1:
                topRightImage.transform.DOMove(topRight.transform.position, 1f);
                break;
            case 2:
                bottomLeftImage.transform.DOMove(bottomLeft.transform.position, 1f);
                break;
            case 3:
                bottomRightImage.transform.DOMove(bottomRight.transform.position, 1f);
                break;
            case 4:
                triggerStory.storyTellingPanel.GetComponent<Image>().DOFade(0.0f, 1f);
                Sequence fadeSequence = DOTween.Sequence();
                fadeSequence.Append(topLeftImage.DOFade(0.0f, 1f));
                fadeSequence.Join(topRightImage.DOFade(0.0f, 1f));
                fadeSequence.Join(bottomLeftImage.DOFade(0.0f, 1f));
                fadeSequence.Join(bottomRightImage.DOFade(0.0f, 1f));
                fadeSequence.Join(topLeft.GetComponent<Image>().DOFade(0.0f, 1f));
                fadeSequence.Join(topRight.GetComponent<Image>().DOFade(0.0f, 1f));
                fadeSequence.Join(bottomLeft.GetComponent<Image>().DOFade(0.0f, 1f));
                fadeSequence.Join(bottomRight.GetComponent<Image>().DOFade(0.0f, 1f));
                fadeSequence.OnComplete(() => MoveInitialPositions());
                break;
        }
        i++;

        //Move images to initial positions.


        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(() => { Slide(i); });
    }
    void Init()
    {
        i = 0;
        GetComponent<Button>().onClick.AddListener(() => { Slide(i); });
    }

    void MoveInitialPositions()
    {
        Sequence moveSequence = DOTween.Sequence();
        moveSequence.Join(topLeftImage.transform.DOMove(initialTopLeftPos.position, 1f));
        moveSequence.Join(topRightImage.transform.DOMove(initialTopRightPos.position, 1f));
        moveSequence.Join(bottomLeftImage.transform.DOMove(initialBottomLeftPos.position, 1f));
        moveSequence.Join(bottomRightImage.transform.DOMove(initialBottomRightPos.position, 1f));
        moveSequence.OnComplete(() => triggerStory.storyTellingPanel.SetActive(false));
    }
}
