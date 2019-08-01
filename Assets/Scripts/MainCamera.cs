using UnityEngine;

namespace Assets.Scripts
{
    public class MainCamera : MonoBehaviour
    {
        private Vector3 offset;

        public float smoothSpeed;
        public float swipeSpeed;

        public float minFOW;
        public float maxFOW;

        private Vector3 startPosition;
        private Vector3 desiredPosition;
        private Vector3 smoothPosition;

        private bool move;

        public Vector4 limitPosition;

        public float perspectiveZoomSpeed = 0.5f;

        private Camera camera;

        void Start()
        {
            desiredPosition = transform.position;
            offset = transform.position;
            camera = Camera.main;
        }

        public void Move()
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPosition = Input.mousePosition;
                move = true;
                offset = transform.position;
            }
            
            if (move)
            {
                var swipeVector = startPosition - Input.mousePosition;
                desiredPosition = new Vector3(swipeVector.x , 0, swipeVector.y) * swipeSpeed + offset;
                
            }

            if (Input.GetMouseButtonUp(0))
            {
                move = false;
                desiredPosition = PositionLimiter(desiredPosition);
            }

            smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothPosition;
        }

        private Vector3 PositionLimiter(Vector3 vector)
        {
            vector.x = Mathf.Max(vector.x, limitPosition.x);
            vector.x = Mathf.Min(vector.x, limitPosition.y);

            vector.z = Mathf.Max(vector.z, limitPosition.w);
            vector.z = Mathf.Min(vector.z, limitPosition.z);

            return vector;
        }

        public void Zoom()
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

            camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, minFOW, maxFOW);
        }

    }
}
