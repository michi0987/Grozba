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

        //ustawianie czułości kamery
        if(CameraDistance < 5f)
        {
            mouseSensitivity = 0.4f;
        }
        else if(CameraDistance < 10f)
        {
            mouseSensitivity = 0.8f;
        }
        else
        {
            mouseSensitivity = 1f;

        }



        if (Input.GetMouseButton(0) || CameraDistance > 9f)
        {
            isCameraDisabled = false;
        }
        else
        {
            isCameraDisabled = true;
        }
        if (!isCameraDisabled)
        {
            // kamery nie da sie obracac gdy zoom jest mały
            if(CameraDistance < 8f)
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
            else
            {
                localRotation.y = 0;
                localRotation.x = 0;

            }



        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float Scroll = Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity;
            Scroll *= (this.CameraDistance * 0.3f);

            this.CameraDistance += (Scroll * -1f);

            if (CameraDistance < 2f)
            {
                CameraDistance = 2f;
            }
            else if (CameraDistance > 12f)
            {
                CameraDistance = 12f;
            }
        }

        //Ustawienie ustawionych wcześniej właściwości na obiekcie kamery

     

        if (this.xcamera.localPosition.z != this.CameraDistance * -1f || this.xcamera.localPosition.x != this.localRotation.x 
            || this.xcamera.localPosition.y != this.localRotation.y)
        {
            if (CameraDistance > 9f) {
                this.xcamera.localPosition = new Vector3(Mathf.Lerp(this.xcamera.localPosition.x, this.localRotation.x, Time.deltaTime * mouseSensitivity),
                 Mathf.Lerp(this.xcamera.localPosition.y, this.localRotation.y,
                 Time.deltaTime * mouseSensitivity), Mathf.Lerp(this.xcamera.localPosition.z, this.CameraDistance * -1f, Time.deltaTime * scrollSpeed));
            }

            else
            {
                this.xcamera.localPosition = new Vector3(this.localRotation.x,
                    this.localRotation.y,
                    Mathf.Lerp(this.xcamera.localPosition.z, this.CameraDistance * -1f, Time.deltaTime * scrollSpeed));
            }
            
        }

    }
}
