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
        [SerializeField] private AudioClip _shotSound;
        [SerializeField] private AudioClip _explosionSound;

        private Camera _camera;
        private bool _isMoving;
        private float _flySpeed = 10;   // left shift for flying
        private float _warpSpeed = 40;  // w for warping
        //private ArrowLook _ArrowLook;

        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private Vector3 _originalCamPosition;
        private AudioSource m_AudioSource;

        // shots:
        public Transform shot;
        public Transform shotSpawn;
        private float nextFire = 0.5f;
        public float fireRate;

        //cam rotation
        private float smoothTime = 5f;
        private Quaternion m_CharacterTargetRot;
        private Quaternion m_CameraTargetRot;


        // Use this for initialization
        private void Start() {
            m_CharacterController = GetComponent<CharacterController>();
            _camera = Camera.main;
            _isMoving = true;
            _originalCamPosition = _camera.transform.localPosition;
            m_FovKick.Setup(_camera);
            m_AudioSource = GetComponent<AudioSource>();

            m_CharacterTargetRot = transform.localRotation;
            m_CameraTargetRot = _camera.transform.localRotation;
        }


        // Update is called once per frame
        private void Update() {
            RotateView(transform, _camera.transform);
            //pressed the firebutton AND loaded weapons?
            if(Input.GetButton("Fire1") && Time.time >= nextFire ) {
                nextFire = Time.time + fireRate;
                shotSpawn.rotation = _camera.transform.rotation;
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

                PlayShotSound();
            }
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

            //if(speed == _warpSpeed) {
            //    _moveDir.z = desiredMove.z * speed;
            //} else {
            //    _moveDir.z = desiredMove.z * speed;
            //}
            forward *= speed;
            m_CollisionFlags = m_CharacterController.Move(forward * Time.deltaTime);

            UpdateCameraPosition();
        }

        public void RotateView(Transform character, Transform camera) {

            float yRot = Input.GetAxis("Horizontal");
            float xRot = Input.GetAxis("Vertical");

            m_CharacterTargetRot *= Quaternion.Euler (0f, yRot, 0f);
            m_CameraTargetRot *= Quaternion.Euler (-xRot, 0f, 0f);

            character.localRotation = Quaternion.Slerp (character.localRotation, m_CharacterTargetRot,
                smoothTime * Time.deltaTime);
            camera.localRotation = Quaternion.Slerp (camera.localRotation, m_CameraTargetRot,
                smoothTime * Time.deltaTime);
        }


        private void UpdateCameraPosition() {
            Vector3 newCameraPosition = _camera.transform.localPosition;
            newCameraPosition.y = _originalCamPosition.y;
            newCameraPosition.x = _originalCamPosition.x;
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

        private void PlayShotSound() {
            m_AudioSource.clip = _shotSound;
            m_AudioSource.volume = 0.2f;
            m_AudioSource.Play();
        }

        private void PlayExplosionSound() {
            m_AudioSource.clip = _explosionSound;
            m_AudioSource.Play();
        }


        private void OnControllerColliderHit(ControllerColliderHit hit) {
            print("### Collision!  " + hit);
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below) {
                return;
            }

            if (body == null || body.isKinematic) {
                return;
            }
            //body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
        }
    }
