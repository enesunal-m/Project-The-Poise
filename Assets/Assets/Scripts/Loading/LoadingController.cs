using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    public Image icon;
    private Sequence sequence;

    // Start is called before the first frame update
    void Start()
    {
        LoadingIconAnim();
        StartCoroutine("LoadScene");
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1.5f);
        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(LoadingData.sceneToLoad);
        asyncLoad.allowSceneActivation = false;

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {

            Debug.Log(asyncLoad.progress);
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    public void LoadingIconAnim()
    {
        sequence = DOTween.Sequence();
        sequence.Append(icon.DOFade(0.0f, 1.5f));
        sequence.Append(icon.DOFade(1.0f, 1.5f));
        sequence.SetLoops(-1);
        sequence.Play();
    }
}
