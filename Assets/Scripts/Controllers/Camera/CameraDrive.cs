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
    public float speedRot = 90.0f;
    public float speedMov = 30.0f;
    public float maxDy = -8.0f;
    public float minDy = 2.0f;
    public float zoomSpeed = 2.0f;
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
            if (lerping)
            {
                if (lookingObject == centerObject)
                {
                    currentLookingPosition = Vector3.Lerp(startLookingPosition, lookingObject.transform.position, (Time.timeSinceLevelLoad - lerpingStartTime) / 4.0f);
                }
                else 
                {
                    currentLookingPosition = Vector3.Lerp(startLookingPosition, lookingObject.transform.position, Time.timeSinceLevelLoad - lerpingStartTime);
                }
            }
            else
            {
                if (lookingObject == centerObject)
                {
                    currentLookingPosition = centerObject.transform.position;
                }
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
            camera.transform.RotateAround(lookingObject.transform.position, new Vector3(0, -1, 0), speedRot * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            camera.transform.RotateAround(lookingObject.transform.position, new Vector3(0, 1, 0), speedRot * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {

            //lookingObjectPosition.Set(

            if (lookingObject.transform.position == centerObject.transform.position)
            {
                centerObject.transform.Translate(Vector3.up * (speedMov / 2.0f) * Time.deltaTime, Space.World);
                camera.transform.Translate(Vector3.up * (speedMov / 2.0f) * Time.deltaTime, Space.World);
            }
            else
            {
                float dy = (currentLookingPosition - camera.transform.position).y;
                Debug.Log(dy + " Max:" +maxDy);
                if (dy > maxDy)
                    camera.transform.Translate(Vector3.up * speedMov * Time.deltaTime);

                //			lookingObjectPosition.Set (lookingObjectPosition.x,
                //				lookingObjectPosition.y + (speedMov) * Time.deltaTime,
                //				lookingObjectPosition.z);

                //			camera.transform.Translate (0, speedMov * Time.deltaTime, 0, lookingObject.transform);
            }
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if (lookingObject.transform.position == centerObject.transform.position)
            {
                centerObject.transform.Translate(Vector3.down * (speedMov / 2.0f) * Time.deltaTime, Space.World);
                camera.transform.Translate(Vector3.down * (speedMov / 2.0f) * Time.deltaTime, Space.World);
            }
            else
            {
                float dy = (currentLookingPosition - camera.transform.position).y;
                Debug.Log(dy+" Min:"+minDy);
                if (dy < minDy)
                    camera.transform.Translate(Vector3.down * speedMov * Time.deltaTime);

                //			lookingObjectPosition.Set (
                //				lookingObjectPosition.x,
                //				lookingObjectPosition.y + (-speedMov) * Time.deltaTime,
                //				lookingObjectPosition.z);
                //			camera.transform.Translate (0, -speedMov * Time.deltaTime, 0, lookingObject.transform);
            }
        }

        Vector3 distance = (currentLookingPosition - transform.position);
        Debug.Log("Distance" + distance.sqrMagnitude);
        if ((distance.sqrMagnitude > 10.0f && Input.GetAxis("Mouse ScrollWheel") > 0.0f) || (distance.sqrMagnitude < 200.0f && Input.GetAxis("Mouse ScrollWheel") < 0.0f))
        {
            distance = distance.normalized * Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomSpeed;
            Debug.Log(distance);
            transform.Translate(distance);
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