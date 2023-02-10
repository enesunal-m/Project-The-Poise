using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneRouter
{
    public static void GoToScene(SceneType sceneType)
    {
        //SceneManager.LoadScene(Constants.SceneConstants.sceneIndexes[sceneType]);
        LoadingData.sceneToLoad = Constants.SceneConstants.sceneIndexes[sceneType];
        SceneManager.LoadSceneAsync(Constants.SceneConstants.sceneIndexes[SceneType.Loading]);
    }

    public static void GoToSceneWithString(string sceneTypeString)
    {
        SceneType sceneType = new SceneType();

        Enum.TryParse(sceneTypeString, out sceneType);

        SceneManager.LoadSceneAsync(Constants.SceneConstants.sceneIndexes[sceneType]);
    }
}
