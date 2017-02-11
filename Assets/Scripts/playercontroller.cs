using UnityEngine;
using System.Collections;

public class playercontroller : MonoBehaviour {

    private CharacterController charController;
    private Vector3 moveDirection = Vector3.zero;
    //private Vector3 forward = Vector3.zero;
    //private Vector3 right = Vector3.zero;

    public GameObject gameController;
    public Camera camera;
    public float speed = 5F;
    public float turnSpeed = 1F;
    public float gravity = 20.0F;
    public float jumpSpeed = 0;
    public bool hitPlatform = false;
    public bool hitGoal = false;
    public bool hitWall = false;
    public bool hitCeiling = false;

    public float xAxis = 0;
    public float yAxis = 0;
    private bool button = false;


    // Use this for initialization
    void Start () {

	    charController = gameObject.GetComponent<CharacterController>();

    }
	
	// Update is called once per frame
	void FixedUpdate () {

        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");
        button = Input.GetButton("Fire1");
        
        // Handle player rotation
        transform.Rotate(0, xAxis * turnSpeed, 0);

        // Display engine if button pressed
        GameObject.Find("engine").GetComponent<MeshRenderer>().enabled = button;

        //transform.Rotate(0, 1, 0);
        //camera.transform.RotateAround(transform.position, Vector3.right, 90 * Time.deltaTime);

        if (hitGoal)
        {
            return;
        }

        RaycastHit hit;
        Ray landingRay = new Ray(transform.position, Vector3.down);
        bool hitsomething = false;

        if(Physics.Raycast(landingRay, out hit, 0.5F))
        {
            if(hit.collider.tag == "floor")
            {
                hitsomething = true;
            }

            if (hit.collider.tag == "platform")
            {
                hitsomething = true;
            }
        }

        yAxis = (button ? 2 : 0);
        if (charController.isGrounded || hitPlatform || hitCeiling || hitWall)
        {
            moveDirection = new Vector3(0, 0, yAxis);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            moveDirection.y = jumpSpeed;
            
            if (charController.isGrounded && !hitPlatform)
            {
                jumpSpeed = jumpSpeed / 2;
                if (System.Convert.ToInt32(jumpSpeed) == 0)
                {
                    jumpSpeed = 0f;
                }
                else
                {
                    gameController.GetComponent<gamecontroller>().PlaySound("bounceFloor");
                }
            }

            if (hitCeiling)
            {
                gameController.GetComponent<gamecontroller>().PlaySound("hitwall");
                hitCeiling = false;
                moveDirection.y = 0;
            }

            if (hitPlatform)
            {
                gameController.GetComponent<gamecontroller>().PlaySound("bouncePlatform");

                if (jumpSpeed < 30)
                    jumpSpeed += 4;
                hitPlatform = false;
            }

            if (hitWall)
            {
                gameController.GetComponent<gamecontroller>().PlaySound("hitwall");
                moveDirection *= -speed;
                hitWall = false;
            }

        }
        else
        {
            moveDirection += transform.TransformDirection(new Vector3(0, 0, yAxis * Time.deltaTime * speed));
            moveDirection.y -= gravity * Time.deltaTime;
        }

        charController.Move(moveDirection * Time.deltaTime);


    }

}
