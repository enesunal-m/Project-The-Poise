using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    public static TooltipSystem current;
    public Tooltip tooltip;
    [SerializeField] Transform castingPlace;
    public void Awake()
    {
        current = this;
    }

    public void Show(string content, string header = "")
    {
        tooltip.SetText(content, header);
        tooltip.transform.parent.gameObject.SetActive(true);
        tooltip.transform.position = castingPlace.position + new Vector3(260,120,0);
    }
    public void Hide()
    {
        tooltip.transform.parent.gameObject.SetActive(false);
    }
}
