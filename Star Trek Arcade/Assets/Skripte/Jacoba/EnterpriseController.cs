using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
//using Random = UnityEngine.Random;

namespace Jacoba
{
    [RequireComponent(typeof (CharacterController))]
    [RequireComponent(typeof (AudioSource))]
    public class EnterpriseController : MonoBehaviour
    {
        [SerializeField] private bool m_isFlying;
        [SerializeField] private float m_FlySpeed;
        [SerializeField] private float m_WarpSpeed;  // left shift for running

        [SerializeField] private ArrowLook m_ArrowLook;

        [SerializeField] private bool m_UseFovKick;
        [SerializeField] private FOVKick m_FovKick = new FOVKick();

        [SerializeField] private AudioClip m_WarpSound;
        [SerializeField] private AudioClip m_explosionSound;


        private Camera m_Camera;
        //private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;

        private Vector3 m_OriginalCameraPosition;

        private AudioSource m_AudioSource;


        // Use this for initialization
        private void Start()
        {
            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_FovKick.Setup(m_Camera);

            m_AudioSource = GetComponent<AudioSource>();

            m_ArrowLook.Init(transform , m_Camera.transform);
        }


        // Update is called once per frame
        private void Update()
        {
            RotateView();
            m_MoveDir.y = 0f;
        }

        private void FixedUpdate()
        {
            float speed;
            GetInput(out speed);
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward*m_Input.y + transform.right*m_Input.x;

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                               m_CharacterController.height/2f);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            m_MoveDir.x = desiredMove.x*speed;
            m_MoveDir.z = desiredMove.z*speed;

            if(!m_isFlying) {
                PlayWarpSound();
            }

            //
            // if (m_CharacterController.isGrounded) {
            //     m_MoveDir.y = -m_StickToGroundForce;
            //
            // } else {

            m_MoveDir += Physics.gravity * 0 * Time.fixedDeltaTime;
    //    }
            m_CollisionFlags = m_CharacterController.Move(m_MoveDir*Time.fixedDeltaTime);

            //UpdateCameraPosition();
        }


        private void UpdateCameraPosition()
        {
            Vector3 newCameraPosition = m_Camera.transform.localPosition;
            newCameraPosition.y = m_OriginalCameraPosition.y;

            m_Camera.transform.localPosition = newCameraPosition;
        }


        private void GetInput(out float speed)
        {
            // Read input
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");

            bool wasflying = m_isFlying;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the enterprise is flying or 'warping''
            m_isFlying = !Input.GetKey(KeyCode.LeftShift);
#endif
            // set the desired speed to be flying or 'warping'
            speed = m_isFlying ? m_FlySpeed : m_WarpSpeed;
            m_Input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1) {
                m_Input.Normalize();
            }

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            if (m_isFlying != wasflying && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0) {
                StopAllCoroutines();
                StartCoroutine(!m_isFlying ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
            }
        }


        private void RotateView()
        {
             m_ArrowLook.LookRotation (transform, m_Camera.transform);
        }



        private void PlayWarpSound()
        {
            m_AudioSource.clip = m_WarpSound;
            m_AudioSource.Play();
        }

        private void PlayExplosionSound()
        {
            m_AudioSource.clip = m_explosionSound;
            m_AudioSource.Play();
        }


        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Console.WriteLine("### Collision!  " + hit);
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
}