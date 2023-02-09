using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TriggerStory : MonoBehaviour
{
    public GameObject storyTellingPanel;
    public Storytelling storytelling;
    public Button BTNStorytelling;

    void Start()
    {
        BTNStorytelling.onClick.AddListener(Tell);
    }
    public void Tell()
    {
        storytelling.triggerStory.storyTellingPanel.SetActive(true);
        storytelling.topLeftImage.DOFade(1f, 1f);
        storytelling.topRightImage.DOFade(1f, 1f);
        storytelling.bottomLeftImage.DOFade(1f, 1f);
        storytelling.bottomRightImage.DOFade(1f, 1f);
        storytelling.topLeft.GetComponent<Image>().DOFade(1f, 1f);
        storytelling.topRight.GetComponent<Image>().DOFade(1f, 1f);
        storytelling.bottomLeft.GetComponent<Image>().DOFade(1f, 1f);
        storytelling.bottomRight.GetComponent<Image>().DOFade(1f, 1f);
    }
}
