using FullHouseCasino.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BManager : MonoBehaviour
{
    private void Awake()
    {
        if(Screen.orientation!=ScreenOrientation.Portrait)
        {
            Debug.Log("############Bscene is not portrait");
            Screen.orientation = ScreenOrientation.Portrait;
        } 
    } 
    public void on_click_moveto_A()
    { 
        SceneManager.Instance.scene_load(SceneManager.scene.Scene_A); 
    }
    private void OnDestroy()
    {
#if UNITY_EDITOR
        OrientationManager.Instance.set_curr_orientation(1280, 720);
#endif  
        //BootStrap.Instance.destroy_immutableobj();
    }
}
