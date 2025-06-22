using UnityEngine;

public class NormalizeRotation : MonoBehaviour
{
    public Transform objectToNormalize;
    public bool freezeX;
    public bool freezeY;
    public bool freezeZ;

    private Vector3 initialRotation;
    private void Start()
    {
        initialRotation = objectToNormalize.eulerAngles;
    }

    Vector3 l_tempRot;
    private void FixedUpdate()
    {
        l_tempRot = objectToNormalize.rotation.eulerAngles;
        if (freezeX) l_tempRot.x = initialRotation.x;
        if (freezeY) l_tempRot.y = initialRotation.y;
        if (freezeZ) l_tempRot.z = initialRotation.z;

        objectToNormalize.rotation = Quaternion.Euler(l_tempRot);
    }
}
