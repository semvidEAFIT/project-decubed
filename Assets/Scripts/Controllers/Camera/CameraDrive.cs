using UnityEngine;

/// <summary>
/// Camera drive.
///
/// TODO: Corregir bug de movimiento
/// </summary>
public class CameraDrive : MonoBehaviour
{
    #region Attributes

    public float error = 10.0f;
    public float speedRot = 30.0f;
    public float speedMov = 15.0f;
    public float maxAngle = 30.0f;
    public float zoomSpeed = 100.0f;
    public float maxHeight = 20.0f;
    public float minHeight = -10.0f;
    public GameObject centerObject;
    private GameObject lookingObject;
    private Vector3 currentLookingPosition;
    private Vector3 lookingObjectPosition;
    private Vector3 startLookingPosition;
    private const int positionOffset = 5;

    private bool lerping = false;
    private float lerpingStartTime;

    #endregion Attributes

    #region MonoBehaviour

    protected virtual void Start()
    {
        lookingObject = centerObject;
        currentLookingPosition = lookingObject.transform.position;
        lookingObjectPosition = lookingObject.transform.position;
    }

    protected virtual void LateUpdate()
    {
        if (lookingObject == null)
        {
            Vector3 translation = camera.transform.position - centerObject.transform.position;
            centerObject.transform.Translate(translation);
            lookingObject = centerObject;
        }

        if (currentLookingPosition != lookingObject.transform.position)
        {
            if (lookingObject == centerObject)
            {
                if(lerping)
                {
                    currentLookingPosition = Vector3.Lerp(startLookingPosition, lookingObject.transform.position, (Time.timeSinceLevelLoad - lerpingStartTime) / 4.0f);
                }
                else
                {
                    currentLookingPosition = centerObject.transform.position;
                }
            }
            else
            {
                currentLookingPosition = Vector3.Lerp(startLookingPosition, lookingObject.transform.position, Time.timeSinceLevelLoad - lerpingStartTime);
            }
        }
        else
        {
            if (lerping) lerping = false;
        }

        //camera.transform.LookAt (lookingObject.transform);
        camera.transform.LookAt(currentLookingPosition);

        // Camera Rotation in the Y Axis
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            camera.transform.RotateAround(lookingObject.transform.position, new Vector3(0, -1, 0), speedRot * 2.0f * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            camera.transform.RotateAround(lookingObject.transform.position, new Vector3(0, 1, 0), speedRot * 2.0f * Time.deltaTime);
        }

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && transform.position.y < maxHeight)
        {

            //lookingObjectPosition.Set(

            if (lookingObject.transform.position == centerObject.transform.position)
            {
                centerObject.transform.Translate(Vector3.up * (speedMov) * Time.deltaTime, Space.World);
                camera.transform.Translate(Vector3.up * (speedMov) * Time.deltaTime, Space.World);
            }
            else
            {
                Vector3 v = (currentLookingPosition - camera.transform.position);
                Vector3 forward = transform.forward;
                forward.y = 0;
                float angle = Vector3.Angle(forward, v);
                if (angle < maxAngle || v.normalized.y > 0)
                    camera.transform.Translate(Vector3.up * speedRot * Time.deltaTime);

                //			lookingObjectPosition.Set (lookingObjectPosition.x,
                //				lookingObjectPosition.y + (speedMov) * Time.deltaTime,
                //				lookingObjectPosition.z);

                //			camera.transform.Translate (0, speedMov * Time.deltaTime, 0, lookingObject.transform);
            }
        }
        else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && transform.position.y > minHeight)
        {
            if (lookingObject.transform.position == centerObject.transform.position)
            {
                centerObject.transform.Translate(Vector3.down * (speedMov) * Time.deltaTime, Space.World);
                camera.transform.Translate(Vector3.down * (speedMov) * Time.deltaTime, Space.World);
            }
            else
            {
                Vector3 v = (currentLookingPosition - camera.transform.position);
                Vector3 forward = transform.forward;
                forward.y = 0;
                float angle = Vector3.Angle(forward, v);
                if (angle < maxAngle || v.normalized.y < 0)
                    camera.transform.Translate(Vector3.down * speedRot * Time.deltaTime);

                //			lookingObjectPosition.Set (
                //				lookingObjectPosition.x,
                //				lookingObjectPosition.y + (-speedMov) * Time.deltaTime,
                //				lookingObjectPosition.z);
                //			camera.transform.Translate (0, -speedMov * Time.deltaTime, 0, lookingObject.transform);
            }
        }

        Vector3 distance = (lookingObject.transform.position - transform.position);
        Debug.DrawLine(transform.position, lookingObject.transform.position);
        if ((distance.sqrMagnitude > 10.0f && Input.GetAxis("Mouse ScrollWheel") > 0.0f) || (distance.sqrMagnitude < 200.0f && Input.GetAxis("Mouse ScrollWheel") < 0.0f))
        {
            distance = distance.normalized * Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomSpeed;
            transform.Translate(distance, Space.World);
        }
    }

    #endregion MonoBehaviour

    #region Get-s and Set-s

    public GameObject LookingObject
    {
        get
        {
            return this.lookingObject;
        }
        set
        {
            if (value != null)
            {
                this.lookingObject = value;
            }
            else
            {
                Vector3 translation = new Vector3(0.0f, camera.transform.position.y - centerObject.transform.position.y, 0.0f);
                centerObject.transform.Translate(translation);
                this.lookingObject = centerObject;
            }
            lerping = true;
            lerpingStartTime = Time.timeSinceLevelLoad;
            startLookingPosition = currentLookingPosition;

            //lookingObjectPosition = lookingObject.transform.position;
        }
    }

    #endregion Get-s and Set-s
}