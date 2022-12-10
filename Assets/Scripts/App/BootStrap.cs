using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FullHouseCasino.App;
using System.Linq;
using FullHouseCasino.UI;

public class BootStrap : MonoBehaviour
{
    public static BootStrap Instance { get { return _instance; } }
    private static BootStrap _instance;
    /*
    private static BootStrap _instance;
    public static BootStrap Instance
    {
        get
        {
            if (_instance == null)
                _instance = new BootStrap();
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }
    */
    private GameObject immutable_obj = null;
 
    private GameCameraMapper _game_camera_mapper;
    public GameCameraMapper get_game_camera_mapper() { return _game_camera_mapper; }
    private void Awake()
    {
        _instance = this;
        if (SceneManager.Instance == null)
            load_immutable_obj();
        else
        {
            if (SceneManager.Instance.Immutableobj == null)
                load_immutable_obj();
        }
        //SceneManager.Instance.Immutableobj.transform.Find("SystemPopupCanvas").gameObject.GetComponent<CanvasScalerExt>().referenceResolution = SceneManager.Instance.canvas_resolution();

        on_kernel_loaded();
    }
    public void load_immutable_obj()
	{  
		// 모든 scene에서 공통으로 사용할 ImmutableObject를 Resources에서 읽어와 생성합니다.
		// 각종 manager와 event handler가 포함되어 있습니다.
		GameObject o = Instantiate(Resources.Load("Immutable_object_minimal")) as GameObject;
		o.name = "Immutable_object_minimal";
        DontDestroyOnLoad(o);
		immutable_obj = o;
        SceneManager.Instance.Immutableobj = o;

#if UNITY_EDITOR
        GameObject o_manager = Instantiate(Resources.Load("OrientationManager")) as GameObject;
        o_manager.name = "OrientationManager";
        o_manager.transform.SetParent(o.transform);

#endif
        on_kernel_loaded();

    }
    public void on_kernel_loaded()
    {
        foreach (Canvas canvas in GameObject.FindObjectsOfType<Canvas>())
        {
            BootStrap.Instance.set_canvas_world_camera(canvas);
        }

        foreach (CanvasScalerExt canvas_scaler in GameObject.FindObjectsOfType<CanvasScalerExt>())
        {
            BootStrap.Instance.set_canvas_scaler_world_camera(canvas_scaler);
        }

    }

    public void set_canvas_world_camera(Canvas canvas)
    {
        string name = canvas.name;
        name = name.Substring(0, name.Length - "Canvas".Length);
        _game_camera_mapper = SceneManager.Instance.Immutableobj.GetComponent<GameCameraMapper>();
        if (_game_camera_mapper.mapper.Where(t => t.name == name).Count() > 0)
        {
            GameCameraMapper.CameraMap map = _game_camera_mapper.mapper.Where(t => t.name == name).FirstOrDefault();
            if (map != null)
                canvas.worldCamera = map.camera;
        }
    }

    public void set_canvas_scaler_world_camera(CanvasScalerExt canvas_scaler)
    {
        string name = canvas_scaler.name;
        name = name.Substring(0, name.Length - "Canvas".Length);
        _game_camera_mapper = SceneManager.Instance.Immutableobj.GetComponent<GameCameraMapper>();


        if (_game_camera_mapper.mapper.Where(t => t.name == name).Count() > 0)
        {
            GameCameraMapper.CameraMap map = _game_camera_mapper.mapper.Where(t => t.name == name).FirstOrDefault();
            if (map != null)
                canvas_scaler.world_camera = map.camera;
        }
    }

    public void destroy_immutableobj()
    {
        Debug.Log("##############destroy immutable obj");
        if (immutable_obj != null)
			Destroy(immutable_obj);
	}
}
