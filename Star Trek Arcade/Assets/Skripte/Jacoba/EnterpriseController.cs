using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;


    [RequireComponent(typeof (CharacterController))]
    [RequireComponent(typeof (AudioSource))]
    public class EnterpriseController : MonoBehaviour
    {

        [SerializeField] private bool m_UseFovKick;
        [SerializeField] private FOVKick m_FovKick = new FOVKick();

        [SerializeField] private AudioClip _warpSound;
        [SerializeField] private AudioClip _explosionSound;

        private Camera _camera;
        private bool _isMoving;
        private float _flySpeed = 10;   // left shift for flying
        private float _warpSpeed = 40;  // w for warping
        //private ArrowLook _ArrowLook;

        //private float m_YRotation;
        //private Vector2 m_Input;
        //private Vector3 _moveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private Vector3 m_OriginalCameraPosition;
        private AudioSource m_AudioSource;

//------------------------------------------------------------------------------------------------
        //ArrowLook
        // TODO: extra file
        public bool clampVerticalRotation = true;
        private float MinimumX = -360F;
        private float MaximumX = 360F;
        private float smoothTime = 5f;

        private Quaternion m_CharacterTargetRot;
        private Quaternion m_CameraTargetRot;


        public void InitArrowLook(Transform character, Transform camera) {
            m_CharacterTargetRot = character.localRotation;
            m_CameraTargetRot = camera.localRotation;
        }

        public void LookRotation(Transform character, Transform camera) {

            float yRot = CrossPlatformInputManager.GetAxis("Horizontal");
            float xRot = CrossPlatformInputManager.GetAxis("Vertical");

            m_CharacterTargetRot *= Quaternion.Euler (0f, yRot, 0f);
            m_CameraTargetRot *= Quaternion.Euler (-xRot, 0f, 0f);

            if(clampVerticalRotation)
                m_CameraTargetRot = ClampRotationAroundXAxis (m_CameraTargetRot);

            character.localRotation = Quaternion.Slerp (character.localRotation, m_CharacterTargetRot,
                    smoothTime * Time.deltaTime);
                camera.localRotation = Quaternion.Slerp (camera.localRotation, m_CameraTargetRot,
                    smoothTime * Time.deltaTime);
        }

        Quaternion ClampRotationAroundXAxis(Quaternion q){
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

            angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);

            q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }
// -------------------------------------------------------------------------------------------------------------------------


        // Use this for initialization
        private void Start() {
            m_CharacterController = GetComponent<CharacterController>();
            _camera = Camera.main;
            _isMoving = true;
            m_OriginalCameraPosition = _camera.transform.localPosition;
            m_FovKick.Setup(_camera);

            m_AudioSource = GetComponent<AudioSource>();
        /*_ArrowLook.*/InitArrowLook(transform , _camera.transform);
        }


        // Update is called once per frame
        private void Update() {
            RotateView();
            //_moveDir.y = 0f;
        }

        private void FixedUpdate() {
            float speed;
            GetInput(out speed);
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 forward = transform.TransformDirection(Vector3.forward);

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                               m_CharacterController.height/2f);

            forward = Vector3.ProjectOnPlane(forward, hitInfo.normal).normalized;

            if(speed == _warpSpeed) {
            //    _moveDir.z = desiredMove.z * speed;

            } else {
            //    _moveDir.z = desiredMove.z * speed;
            }
            forward *= speed;
            m_CollisionFlags = m_CharacterController.Move(forward * Time.deltaTime);

            UpdateCameraPosition();
        }

        private void RotateView() {
             /*_ArrowLook.*/LookRotation (transform, _camera.transform);
        }

        private void UpdateCameraPosition() {
            Vector3 newCameraPosition = _camera.transform.localPosition;
            newCameraPosition.y = m_OriginalCameraPosition.y;
            _camera.transform.localPosition = newCameraPosition;
        }


        private void GetInput(out float speed) {

            bool wasflying = _isMoving;
            speed = 0f;

#if !MOBILE_INPUT

            // keep track of whether or not the enterprise is flying or 'warping''
            _isMoving = !Input.GetKey(KeyCode.LeftShift) || !Input.GetKey(KeyCode.W);

            // set the desired speed to be flying or 'warping'
            if(Input.GetKeyDown(KeyCode.W)) {
                PlayWarpSound();
            }
            if(Input.GetKey(KeyCode.W)) {
                speed = _warpSpeed;
            }

            if(Input.GetKey(KeyCode.LeftShift)) {
                speed = _flySpeed;
            }
#endif
            //speed = _isMoving ? _flySpeed : _warpSpeed;


            // float h = Input.GetAxis("Horizontal");
            // float v = Input.GetAxis("Vertical");
            // m_Input = new Vector2(h, v);
            //
            // // normalize input if it exceeds 1 in combined length:
            //  if (m_Input.sqrMagnitude > 1) {
            //      m_Input.Normalize();
            //  }

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            if (_isMoving != wasflying && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0) {
                StopAllCoroutines();
                StartCoroutine(!_isMoving ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
            }
        }

        private void PlayWarpSound() {
            m_AudioSource.clip = _warpSound;
            m_AudioSource.Play();
        }

        private void PlayExplosionSound() {
            m_AudioSource.clip = _explosionSound;
            m_AudioSource.Play();
        }


        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            print("### Collision!  " + hit);
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below) {
                return;
            }

            if (body == null || body.isKinematic) {
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
        }
    }
