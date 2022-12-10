using System;
using System.Collections; 
using UnityEngine;
using UnityEngine.SceneManagement;
using FullHouseCasino.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class AManager : MonoBehaviour
{
    private void Awake()
    {
        if(Screen.orientation!=ScreenOrientation.Landscape)
            Screen.orientation = ScreenOrientation.Landscape; 
    }
    // Start is called before the first frame update
    void Start()
    { 
        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
 
    }

    public void on_click_moveto_B()
    { 
        SceneManager.Instance.scene_load(SceneManager.scene.Scene_B); 
    }

}
