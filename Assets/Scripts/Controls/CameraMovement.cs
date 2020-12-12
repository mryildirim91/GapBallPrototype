using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameObject Ball;
    [SerializeField] Vector3 offset=new Vector3(0,10,-7);

    Vector3 cameraInitialPosition;
    float shakeMagnetude = 0.15f;
    float shakeTime = 0.3f;

    void LateUpdate()
    {
        FollowBall();

    }

    private void FollowBall()
    {
        //DifBetweenBallAndCamera = Ball.transform.position- transform.position;
        transform.position = Ball.transform.position + offset;
    }

    public void ShakeIt()
    {
        cameraInitialPosition = transform.position;
        InvokeRepeating("StartCameraShaking", 0.1f, 0.001f);
        Invoke("StopCameraShaking", shakeTime);
    }

    private void StartCameraShaking()
    {
        float cameraShakingOffsetX = Random.value * shakeMagnetude * 2 - shakeMagnetude;
        float cameraShakingOffsetZ = Random.value * shakeMagnetude * 2 - shakeMagnetude;
        Vector3 cameraIntermadiatePosition = transform.position;
        cameraIntermadiatePosition.x += cameraShakingOffsetX;
        cameraIntermadiatePosition.z += cameraShakingOffsetZ;
        transform.position = cameraIntermadiatePosition;
    }

    private void StopCameraShaking()
    {
        CancelInvoke("StartCameraShaking");
        transform.position = cameraInitialPosition;
    }
}
