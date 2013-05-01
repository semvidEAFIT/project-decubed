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
    public float yMax = 10f;
    public float yMin = 2f;

    public GameObject centerObject;
    private GameObject lookingObject;
    private Vector3 currentLookingPosition;
    private Vector3 lookingObjectPosition;

    private const int positionOffset = 5;

    #endregion Attributes

    #region MonoBehaviour

    protected virtual void Start()
    {
        lookingObject = centerObject;
        currentLookingPosition = lookingObject.transform.position;
        lookingObjectPosition = lookingObject.transform.position;
    }

    protected virtual void Update()
    {
        if (lookingObject == null)
        {
            lookingObject = centerObject;
        }
        if (currentLookingPosition != lookingObject.transform.position)
        {
            currentLookingPosition = Vector3.Lerp(currentLookingPosition, lookingObject.transform.position, Time.deltaTime / 4f);
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
            float y = camera.transform.position.y;

            //lookingObjectPosition.Set(
            if (y < yMax)
                camera.transform.Translate(Vector3.up * speedMov * Time.deltaTime);

            //			lookingObjectPosition.Set (lookingObjectPosition.x,
            //				lookingObjectPosition.y + (speedMov) * Time.deltaTime,
            //				lookingObjectPosition.z);

            //			camera.transform.Translate (0, speedMov * Time.deltaTime, 0, lookingObject.transform);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            float y = camera.transform.position.y;
            if (y > yMin)
                camera.transform.Translate(Vector3.down * speedMov * Time.deltaTime);

            //			lookingObjectPosition.Set (
            //				lookingObjectPosition.x,
            //				lookingObjectPosition.y + (-speedMov) * Time.deltaTime,
            //				lookingObjectPosition.z);
            //			camera.transform.Translate (0, -speedMov * Time.deltaTime, 0, lookingObject.transform);
        }

        if (camera.transform.position.y - 2 >= lookingObjectPosition.y && Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Vector3 mov = (lookingObjectPosition - camera.transform.position);
            mov = mov.normalized * Time.deltaTime * 20.0f;
            camera.transform.position += mov;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Vector3 mov = (lookingObject.transform.position - camera.transform.position);
            mov = mov.normalized * -Time.deltaTime * 20.0f;
            camera.transform.position += mov;
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
                this.lookingObject = centerObject;
            }

            //lookingObjectPosition = lookingObject.transform.position;
        }
    }

    #endregion Get-s and Set-s
}