using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    private float _lerpSpeed, _rotSpeed;

    [SerializeField]
    private GameObject[] _objToMoveHrz, _objToRotate;

    [SerializeField]
    private Transform _pointA, _pointB;

    private Vector3 _posA, _posB;

    public virtual void FixedUpdate()
    {
        HorizontalMovement();
        RotateAround();
    }

    private void HorizontalMovement()
    {
        if (_objToMoveHrz.Length > 0)
        {
            for (int i = 0; i < _objToMoveHrz.Length; i++)
            {
                _posA = new Vector3(_pointA.position.x, _objToMoveHrz[i].transform.position.y, _objToMoveHrz[i].transform.position.z);
                _posB = new Vector3(_pointB.position.x, _objToMoveHrz[i].transform.position.y, _objToMoveHrz[i].transform.position.z);

                _objToMoveHrz[i].transform.position = Vector3.Lerp(_posA, _posB, (Mathf.Sin(_lerpSpeed * Time.time) + 1.0f) / 2.0f);
            }
        }
    }

    private void RotateAround()
    {
        if (_objToRotate.Length > 0)
        {
            for (int i = 0; i < _objToRotate.Length; i++)
            {
                _objToRotate[i].transform.Rotate(Vector3.up * _rotSpeed * Time.deltaTime);
            }
        }
    }
}
