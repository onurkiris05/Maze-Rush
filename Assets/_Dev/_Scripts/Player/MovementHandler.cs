using UnityEngine;

namespace Game.Player
{
    public class MovementHandler : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float rotationSpeed = 10f;

        private PlayerController _player;
        private Rigidbody _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");
            var movement = new Vector3(horizontal, 0f, vertical).normalized;

            MovePlayer(movement);
            RotatePlayer(movement);
            UpdateAnimationBlend(movement);
        }

        public void Init(PlayerController player) => _player = player;

        private void MovePlayer(Vector3 movement)
        {
            var moveDirection = new Vector3(movement.x, 0f, movement.z);

            _rb.velocity = moveDirection * moveSpeed;
        }

        private void RotatePlayer(Vector3 movement)
        {
            if (movement != Vector3.zero)
            {
                var toRotation = Quaternion.LookRotation(movement, Vector3.up);

                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }

        private void UpdateAnimationBlend(Vector3 movement)
        {
            var movementMagnitude = movement.magnitude;

            _player.ProcessMovementMagnitude(movementMagnitude);
        }
    }
}