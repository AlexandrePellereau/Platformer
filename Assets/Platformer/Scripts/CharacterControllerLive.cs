using System;
using UnityEngine;

public class CharacterControllerLive : MonoBehaviour
{
    public float acceleration = 10f;
    public float maxSpeed = 10f;
    public float jumpImpulse = 50f;
    public float jumpBoost = 3f;
    public bool isGrounded;
    
    private Rigidbody _rigidbody;
    private Collider _collider;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }
    
    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        _rigidbody.velocity += Vector3.right * (horizontalMovement * acceleration * Time.deltaTime);

        float halfHeight = _collider.bounds.extents.y + 0.03f;

        Vector3 startPoint = transform.position;
        Vector3 endPoint = startPoint + Vector3.down * halfHeight;

        isGrounded = Physics.Raycast(startPoint, Vector3.down, halfHeight);
        Color lineColor = isGrounded ? Color.red : Color.blue;
        Debug.DrawLine(startPoint, endPoint, lineColor, 0f, false);
        
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            _rigidbody.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
        else if (!isGrounded && Input.GetKey(KeyCode.Space) && _rigidbody.velocity.y > 0)
            _rigidbody.AddForce(Vector3.up * jumpBoost, ForceMode.Force);

        if (Math.Abs(_rigidbody.velocity.x) > maxSpeed)
        {
            Vector3 newVelocity = _rigidbody.velocity;
            newVelocity.x = Math.Clamp(newVelocity.x, -maxSpeed, maxSpeed);
            _rigidbody.velocity = newVelocity;
        }
        
        if (isGrounded && Math.Abs(horizontalMovement) < 0.5f)
        {
            Vector3 newVelocity = _rigidbody.velocity;
            newVelocity.x *= 1 - Time.deltaTime;
            _rigidbody.velocity = newVelocity;
        }
        
        float yaw = _rigidbody.velocity.x > 0 ? 90 : -90;
        transform.rotation = Quaternion.Euler(0, yaw, 0);
    }
}
