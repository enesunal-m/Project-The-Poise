using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardUpgrade : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Panel;
    
    public void OpenPanel()
    {
        if(Panel != null)
        {
            Panel.SetActive(true);

        }
    }
}
