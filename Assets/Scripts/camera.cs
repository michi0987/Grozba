using UnityEngine;

public class Camera : MonoBehaviour
{

    protected Transform xcamera;


    protected Vector3 localRotation;
    protected float CameraDistance = 10f;

    public float mouseSensitivity = 1f;
    public float scrollSensitivity = 2f;
    public float scrollSpeed = 2f;

    public bool isCameraDisabled = true;
    // Start is called before the first frame update
    void Start()
    {
        this.xcamera = this.transform;

    }

    // Update is called once per frame after the frame
    void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            isCameraDisabled = false;
        }
        else
        {
            isCameraDisabled = true;
        }
        if (!isCameraDisabled)
        {
            //Rotacja kamery przy pomocy myszki
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                localRotation.x -= Input.GetAxis("Mouse X") * mouseSensitivity;
                localRotation.y -= Input.GetAxis("Mouse Y") * mouseSensitivity;

                if (localRotation.y < -10f)
                {
                    localRotation.y = -10f;
                }
                else if (localRotation.y > 10f)
                {
                    localRotation.y = 10f;
                }

                if (localRotation.x < -10f)
                {
                    localRotation.x = -10f;
                }
                else if (localRotation.x > 10f)
                {
                    localRotation.x = 10f;
                }

            }


        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float Scroll = Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity;
            Scroll *= (this.CameraDistance * 0.3f);

            this.CameraDistance += (Scroll * -1f);

            if (CameraDistance < 1f)
            {
                CameraDistance = 1f;
            }
            else if (CameraDistance > 80f)
            {
                CameraDistance = 80f;
            }
        }

        //Ustawienie ustawionych wcześniej właściwości na obiekcie kamery


        if (this.xcamera.localPosition.z != this.CameraDistance * -1f)
        {
            this.xcamera.localPosition = new Vector3(Mathf.Lerp(this.xcamera.localPosition.x, this.localRotation.x, Time.deltaTime * mouseSensitivity), Mathf.Lerp(this.xcamera.localPosition.y, this.localRotation.y,
                Time.deltaTime * mouseSensitivity), Mathf.Lerp(this.xcamera.localPosition.z, this.CameraDistance * -1f, Time.deltaTime * scrollSpeed));

        }

    }
}
