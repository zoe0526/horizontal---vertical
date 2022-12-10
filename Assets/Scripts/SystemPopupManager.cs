using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FullHouseCasino.UI;

public class SystemPopupManager : MonoBehaviour
{
    public static SystemPopupManager Instance { get { return _instance; } }
    private static SystemPopupManager _instance;

    private void Awake()
    {
        _instance = this;
    }
    public GameObject loading_popup;
    
    public void set_loading_pop(bool loaded=false)
    {
        if(loading_popup!=null)
            loading_popup.SetActive(loaded);
    }
    public void set_canvas_resolution(float width, float height)
    {  
        gameObject.GetComponent<CanvasScalerExt>().referenceResolution = new Vector2(width, height);
    }


}
