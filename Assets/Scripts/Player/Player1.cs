using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//remember how to do this dumby
[RequireComponent(typeof(CharacterController))]
public class Player1 : MonoBehaviour
{
    #region Var
    //variables
    public LayerMask groundLayer;
    public float runSpeed = 8f;
    public float walkSpeed = 6f;
    public float gravity = -15f;
    //public float crouchSpeed = 4f;
    
    public float groundRayDistance = 1.1f;
    public Vector3 moveDirec;
    public CharacterController charController;
    
    public string heldItem;
    public int heldItemNum;
    #endregion

    #region start
    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
    }
    #endregion

    #region update
    // Update is called once per frame
    void Update()
    {
        //Vector3 setup x y axis



        #region Interacting
        //interact with object
        if (Input.GetButtonDown("Interact"))
        {


            //create a ray
            Ray interact;
            //ray from centre of screen
            interact = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            //create hit info
            RaycastHit hitInfo;
            //if this physics raycast hits something within 10 units
            if (Physics.Raycast(interact, out hitInfo, 2))
            {
                if (hitInfo.collider.CompareTag("Interactable"))
                {

                    Debug.Log("interact");
                }

                if (hitInfo.collider.CompareTag("Collectable"))
                {
                    Destroy(hitInfo.collider.gameObject);

                    Debug.Log("Collect");
                    heldItemNum++;
                }

                if (hitInfo.collider.CompareTag("Deposit"))
                {

                    Debug.Log("Deposit");
                    heldItemNum--;
                }
            }


        }
        #endregion



        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");

        //keep speed consistant, speed is not increased when moving diagonal
        Vector3 normalized = new Vector3(inputH, 0f, inputV);
        normalized.Normalize();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            //move = inputH, inputV
            Sprint(normalized.x, normalized.z);
        }
        else
        {
            Walk(normalized.x, normalized.z);
        }

      

       

        //grav acceleration set to 0 if jumping or grounded
        if (IsGrounded()) 
        {

            moveDirec.y = 0f;
        }


        moveDirec.y += gravity * Time.deltaTime;
        charController.Move(moveDirec * Time.deltaTime);
    }
    #endregion
    //move player in given direction horiz/verti
    public void Move(float inputH, float inputV, float speed)
    {
        Vector3 direction = new Vector3(inputH, 0f, inputV);
        // convert local direction to worldspace
        direction = transform.TransformDirection(direction);
        moveDirec.x = direction.x * speed;
        moveDirec.z = direction.z * speed;
       
    }

    //testGrounded
    private bool IsGrounded()
    {
        Ray groundRay = new Ray(transform.position, -transform.up);

        //return Physics.Raycast(groundRay, groundRayDistance)

        //raycast perform
        if (Physics.Raycast(groundRay, groundRayDistance, groundLayer))
        {
            //return true if hit
            return true;
        }

        //else return false
        return false;

        //return exits the function 
    }

    public void Walk(float horizontal, float vertical)
    {
        Move(horizontal, vertical, walkSpeed);
    }

    public void Sprint(float horizontal, float vertical)
    {
        Move(horizontal, vertical, runSpeed);
    }

   


}
