using System;
using System.Collections.Generic;
using UnityEngine;

namespace FullHouseCasino.App {
    public class GameCameraMapper : MonoBehaviour {
        [Serializable]
        public class CameraMap {
            public string name;
            public Camera camera;
        }

        [SerializeField] public CameraMap[] mapper;

        public static GameCameraMapper Instance {
            get { return _instance; }
        }

        private static GameCameraMapper _instance;

        private void Awake() {
            _instance = this;
        }



        public void init_camera_data()
        {
            for( int i = 0; i < mapper.Length; i++ )
            {
                mapper[ i ].camera.transform.localPosition  =   Vector3.zero;
            }

        }

        /*
        public void set_move_camera( int camera_mapper_index_, Vector3 v_move_pos_, float move_time_ )
        {
            LeanTween.moveLocal( mapper[ camera_mapper_index_ ].camera.gameObject, v_move_pos_, move_time_ );
        }
        */

        public Camera get_game_pop_camera()
        {
            return mapper[ 6 ].camera;
        }
         
        public enum CameraType // mapper배열 안에 들어올 카메라 갯수만큼 타입 추가
        { 
            Popup 
        }

        public void set_camera_projection(GameCameraMapper.CameraType type, bool isOrthographic) // 해당 타입의 카메라의 projection 옵션 변경
        {
            int index = (int)type;
            if (mapper.Length == 0 || mapper.Length < index) return;
            Camera camera = mapper[index].camera;
            if (!camera.orthographic && isOrthographic)
            {
                float orthographicSize = Math.Abs(camera.transform.position.z) * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad); // 카메라 perspective에서 orthographic 변경시 size 값 구하기
                camera.orthographicSize = orthographicSize;
            }
            camera.orthographic = isOrthographic;

        }

        public void reset_Camera_projection()
        {
            for (int i = 0; i < mapper.Length; i++)
            {
                if (mapper[i].camera) mapper[i].camera.orthographic = false;
            }
        }
    }
}
