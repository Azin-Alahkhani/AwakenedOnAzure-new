using UnityEngine;
using System.Collections;



	public class AstronautPlayer : MonoBehaviour {

        public static AstronautPlayer instance;
        public bool canMove = true;

		private Animator anim;
		private CharacterController controller;

		public float speed = 600.0f;
		public float turnSpeed = 400.0f;
		private Vector3 moveDirection = Vector3.zero;
		public float gravity = 20.0f;

        public static AstronautPlayer GetInstance()
        {
            return instance;
        }
        private void Awake()
        {
            if(instance == null)
            instance = this;
            
        }
        void Start () {
			controller = GetComponent <CharacterController>();
			anim = gameObject.GetComponentInChildren<Animator>();
            
		}
    private void ApplyGravity()
    {
        if (!controller.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        else
        {
            moveDirection.y = 0f;
        }
        controller.Move(new Vector3(0, moveDirection.y, 0) * Time.deltaTime);
    }

    void Update (){

        if (!canMove)
        {
            anim.SetBool("isWalking_b", false);
            ApplyGravity();
            return; // Exit the Update method if canMove is false
        }

        if (controller.isGrounded)
        {
            moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            anim.SetBool("isWalking_b", true);
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            anim.SetBool("isWalking_b", false);
        }

        float turn = Input.GetAxis("Horizontal");
        transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);

        ApplyGravity();
        controller.Move(moveDirection * Time.deltaTime);

    }
	}

