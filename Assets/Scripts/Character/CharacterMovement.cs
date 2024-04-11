using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _minSpeed = 1, _maxSpeed = 10;
    [SerializeField] private float _speedIncreaseDistance = 1000;
    [SerializeField] private AnimationCurve _speedCurve;

    [SerializeField] private float _currentDistance;
    private Vector3 _oldPosition;
    private Rigidbody _body;
    private Vector3 _direction = Vector3.forward;

    public Vector3 Direction { set { _direction = value; } }
    public float CurrentSpeed
    {
        get
        {
            if (_currentDistance > _speedIncreaseDistance)
                return _maxSpeed;

            return Mathf.Lerp(_minSpeed, _maxSpeed, _speedCurve.Evaluate(_currentDistance / _speedIncreaseDistance));
        }
    }

    private void Start()
    {
        _body = GetComponent<Rigidbody>();
        _oldPosition = transform.position;
    }

    private void FixedUpdate()
    {
        CalculateDistance();

        _body.MovePosition(_direction * CurrentSpeed * Time.fixedDeltaTime + _body.position);
    }

    private void CalculateDistance()
    {
        //Компенсация сдвига по y чтобы не учитывались прыжки
        _oldPosition.y = transform.position.y;

        _currentDistance += Vector3.Distance(_oldPosition, transform.position);
        _oldPosition = transform.position;
    }
}
