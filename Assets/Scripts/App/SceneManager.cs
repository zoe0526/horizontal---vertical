using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FullHouseCasino.UI;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance { get { return _instance; } }
    private static SceneManager _instance;

    [HideInInspector]
    public GameObject Immutableobj = null;

    private void Awake()
    {
        _instance = this; 
        
    }
    private void Start()
    {
        if (curr_height <= 0)
            curr_height = Screen.height;
        if (curr_width <= 0)
            curr_width = Screen.width; 
    }
    public enum scene : int
    {
        UNKNOWN=-1,
        Scene_A =0,
        Scene_B=1
    }

    private scene _next_scene = scene.UNKNOWN; 
    public void scene_load(scene scene)
    { 
        _next_scene = scene; 
        StartCoroutine(load_async()); 
    }
    private int curr_width = 0;
    private int curr_height = 0;
    void set_resolution()
    { 
        if (_next_scene==scene.Scene_A)
        { 
            curr_width = 1280;
            curr_height = 720;
            if (Screen.orientation != ScreenOrientation.Landscape)
                Screen.orientation = ScreenOrientation.Landscape;
        }
        else if (_next_scene==scene.Scene_B)
        { 
            curr_width = 720;
            curr_height = 1280;
            if (Screen.orientation != ScreenOrientation.Portrait)
                Screen.orientation = ScreenOrientation.Portrait;
        }
#if UNITY_EDITOR
        OrientationManager.Instance.set_curr_orientation(curr_width, curr_height);
#endif
        //SystemPopupManager.Instance.set_canvas_resolution(curr_width, curr_height);
    }
    public void set_curr_resolution(int width, int height)
    {
        curr_width = width;
        curr_height = height;
            
    }
    public Vector2 canvas_resolution()
    {
        return new Vector2(curr_width, curr_height);
    }

    private FullHouseCasino.kernel.system.thread.AsyncOperation _load_async_operation;
    private IEnumerator load_async()
    {  
        //로딩화면 켜주고   
        set_resolution(); 
        Immutableobj.transform.Find("SystemPopupCanvas").gameObject.GetComponent<CanvasScalerExt>().referenceResolution = canvas_resolution();

        yield return new WaitForEndOfFrame();

        string next_scene_name = _next_scene.ToString();
         
        if (_load_async_operation == null)
            _load_async_operation = new FullHouseCasino.kernel.system.thread.AsyncOperation();
        else
            _load_async_operation.reset();
          
        UnityEngine.AsyncOperation p = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(next_scene_name, UnityEngine.SceneManagement.LoadSceneMode.Single);
        //SystemPopupManager.Instance.set_loading_pop(true);
        if (p != null)
        { 
            SystemPopupManager.Instance.set_loading_pop(true);
            while (p.isDone == false)
            {
                _load_async_operation.update_progress(0.8f + 0.1f * p.progress);
                yield return null;
            }

            _load_async_operation.update_progress(0.9f);
            yield return null;
             
            _next_scene = scene.UNKNOWN;
        } 
        yield return new WaitForSeconds(3f);
        SystemPopupManager.Instance.set_loading_pop(false);
    } 

}