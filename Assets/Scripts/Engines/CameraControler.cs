using UnityEngine;

namespace Assets.Scripts.Engines
{
    class CameraControler
    {
        private static Vector3 MousePosition { get; set; }
        private static Vector3 MouseDelta
        {
            get
            {
                return MousePosition - Input.mousePosition;
            }
        }

        public static void Update()
        {
            Vector3 position = UnityEngine.Camera.main.transform.position;

            float z = Input.GetAxis("Mouse ScrollWheel")*3;


            if (z > 0)
                if (Camera.main.orthographicSize < 15)
                    Camera.main.orthographicSize += z;

            if (z < 0)
                if (Camera.main.orthographicSize > 5)
                    Camera.main.orthographicSize += z;


            if (Input.GetAxis("Fire2")!=0)
            {
                position += MouseDelta * 0.01f;
            }
            MousePosition = Input.mousePosition;
            UnityEngine.Camera.main.transform.position = position;
        }
    }
}
