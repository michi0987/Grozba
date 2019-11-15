using UnityEngine;

public class camera : MonoBehaviour
{

    protected Transform xcamera;


    protected Vector3 localrotation;
    protected float CameraDistance = 10f;

    public float mousesensitivity = 1f;
    public float scrollsensitivity = 2f;
    public float scrollspeed = 2f;

    public bool cameradisable = true;
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
            cameradisable = false;
        }
        else
        {
            cameradisable = true;
        }
        if (!cameradisable)
        {
            //Rotacja kamery przy pomocy myszki
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                localrotation.x -= Input.GetAxis("Mouse X") * mousesensitivity;
                localrotation.y -= Input.GetAxis("Mouse Y") * mousesensitivity;

                if (localrotation.y < -10f)
                {
                    localrotation.y = -10f;
                }
                else if (localrotation.y > 10f)
                {
                    localrotation.y = 10f;
                }

                if (localrotation.x < -10f)
                {
                    localrotation.x = -10f;
                }
                else if (localrotation.x > 10f)
                {
                    localrotation.x = 10f;
                }

            }


        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float Scroll = Input.GetAxis("Mouse ScrollWheel") * scrollsensitivity;
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
            this.xcamera.localPosition = new Vector3(Mathf.Lerp(this.xcamera.localPosition.x, this.localrotation.x, Time.deltaTime * mousesensitivity), Mathf.Lerp(this.xcamera.localPosition.y, this.localrotation.y,
                Time.deltaTime * mousesensitivity), Mathf.Lerp(this.xcamera.localPosition.z, this.CameraDistance * -1f, Time.deltaTime * scrollspeed));

        }

    }
}
