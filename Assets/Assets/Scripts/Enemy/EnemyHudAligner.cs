using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class EnemyHudAligner : MonoBehaviour
{
    [SerializeField] Transform enemyIntentionHud, enemyHealthHud, enemySprite;
    string id;
    void Start()
    {
        id = gameObject.GetComponent<Enemy>().id;
        switch (id)
        {
            case ("mayrotta"):
                enemyHealthHud.GetComponent<RectTransform>().localPosition = new Vector3(12.5f, 20.95f, 0);
                enemyIntentionHud.GetComponent<RectTransform>().localPosition = new Vector3(12.5f, 19.2f, 0);
                break;
            case ("mayex"):
                enemyHealthHud.GetComponent<RectTransform>().localPosition = new Vector3(6.25f, 13.45f, 0);
                enemyIntentionHud.GetComponent<RectTransform>().localPosition = new Vector3(6.25f, 11.7f, 0);
                break;
            case ("maypo"):
                enemyHealthHud.GetComponent<RectTransform>().localPosition = new Vector3(8.75f, 20.95f, 0);
                enemyIntentionHud.GetComponent<RectTransform>().localPosition = new Vector3(8.75f, 19.2f, 0);
                break;
        }

    }

    // Update is called once per frame
    void LateUpdate()
    {

    }
}
