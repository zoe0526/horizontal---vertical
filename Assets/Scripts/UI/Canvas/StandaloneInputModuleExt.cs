using UnityEngine;
using UnityEngine.EventSystems;

public class StandaloneInputModuleExt : StandaloneInputModule {
    public GameObject GameObjectUnderPointer(int pointer_id = -1) {
        PointerEventData lastPointer = null;
#if UNITY_EDITOR || UNITY_WEBGL
        lastPointer = GetLastPointerEventData(-1);
#else
        lastPointer = GetLastPointerEventData(pointer_id);
#endif

        /*
        if ( null != SceneManager.Instance && SceneManager.Instance.is_slot_scene( SceneManager.Instance.current_scene ) )
            return null;
        */
        if (lastPointer != null)
            return lastPointer.pointerCurrentRaycast.gameObject;
        return null;
    }
}
