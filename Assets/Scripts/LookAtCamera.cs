using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum Mode
    {
        Lookat,
        LookatInverted,
        CameraForward,
        CameraForwardInverted,
    }

    [SerializeField] private Mode mode;
    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.Lookat:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookatInverted:
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                transform.rotation = Quaternion.LookRotation(dirFromCamera);
                break;
            case Mode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }
}
