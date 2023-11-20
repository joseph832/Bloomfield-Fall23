using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class playerController3D : MonoBehaviour
{
    [Header("move vars")]
    public float speed = 1f;
    public float jumpForce = 5f;

    [Header("look vars")]
    public Vector2 sensMouse = new Vector2(.5f, .5f);
    public GameObject myCam;
    public Vector2 camVertLock = new Vector2(90, -90);

    [Header("debugs")]
    public bool grounded;
    Rigidbody myRB;
    Vector3 myDir;
    float rotY;
    float rotX;

    [Header("cosmetics")]
    public Slider R;
    public Slider G;
    public Slider B;
    MeshRenderer myMeshRender;
    Material myMat;
    
    public MeshFilter myHatMesh;
    public Mesh[] playerMeshes;
    int hatPos = 0;


    public void OnEnable()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked; //this hides the mouse
        myCam.SetActive(true);

    }

    public void OnDisable()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Confined; //this hides the mouse
        myCam.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        grounded = false;
        myDir = Vector3.zero;
        rotY = 0f;
        rotX = 0f;


        //find components in start so we don't have to run a search each update cycle
        //the player should have all these so we don't need to make them public 
        myRB = GetComponent<Rigidbody>();
        myMeshRender = GetComponent<MeshRenderer>();
        myMat = myMeshRender.material;
        DontDestroyOnLoad(this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {

        //debugs for player: look angles
        //first we take a vector from the camera transform then use TransformDirection to convert it from
        //local to world space. Then we draw a ray using that vector to display player look (forward) and left/right 
        Vector3 playerLook = myCam.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, playerLook*2f, Color.green, .3f);

        //for the left/ right vectors we do the same but with the corresponding vector.left or vector.right shorthand
        //then we only pull two of the 3 values since we want to render on a flat 2D plane, not full 3D space
        Vector3 playerLeft = myCam.transform.TransformDirection(Vector3.left);
        playerLeft = new Vector3(playerLeft.x, 0f, playerLeft.z);
        Debug.DrawRay(transform.position, playerLeft, Color.green, .3f);

        //right works the same as the left drawRay, more or less
        Vector3 playerRight = myCam.transform.TransformDirection(Vector3.right);
        playerRight = new Vector3(playerRight.x, 0f, playerRight.z);
        Debug.DrawRay(transform.position, playerRight, Color.green, .3f);

        //float x & z control the translate/movement inputs, taken from Unity preferences
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        //whenever working with vectors for movement or rotation it's good practice
        //to .normalized them - this sets their magnitude equal to 1 and makes them cleaner to work with
        myDir = transform.rotation * new Vector3(x, 0f, z).normalized;

        //after normalizing we multiply the vector by our speed variable to set our player speed in a
        //clean and consistent way. We also set the Translate in Space.World, since our input axes give us world coordinates


        //rotations Y&X are pulled from mouse look, go into camera controls
        rotY += Input.GetAxisRaw("Mouse X") * sensMouse.y;
        rotX += Input.GetAxisRaw("Mouse Y") * sensMouse.x;

        //clamp our Y to avoid looking to far up or down
        if(rotX > camVertLock.x)
        {
            rotX = camVertLock.x;
        }
        if(rotX < camVertLock.y)
        {
            rotX = camVertLock.y;
        }

        //apply the rotations to the player and camera
        transform.rotation = Quaternion.Euler(0f, rotY, 0f);
        myCam.transform.rotation = Quaternion.Euler(-rotX, rotY, 0f);

    }

    void FixedUpdate()
    {
        //check for spacebar up to jump
        if (Input.GetKeyUp(KeyCode.Space) && grounded) { PlayerJump(); }

        //add force from the player direction - use TransformDirection to apply it in world coordinates
        Vector3 currentDir = transform.TransformDirection(Direction());
        myRB.AddForce(currentDir * speed, ForceMode.VelocityChange);

        //debug for player input dir to show in scene edit mode
        Debug.DrawRay(this.transform.position, currentDir * 5f, Color.yellow, 5f);



    }

    //check for collisions with ground and enemies
    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.name == "ground") { grounded = true; }
    }

    //collision exit fires off when we stop colliding so it's
    //useful to check when the player leaves the ground (AKA jumps)
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "ground") { grounded = false; }
    }

    //simple add force up on jump
    void PlayerJump()
    {
        myRB.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.VelocityChange);
    }

    Vector3 Direction()
    {
        //float x & z control the translate/movement inputs, taken from Unity preferences
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        //construct our direction then return it
        myDir = new Vector3(x, 0f, z);
        return myDir;
    }

    public void ChangeRGB()
    {
        Color myCol = new Color(R.value, G.value, B.value, 1f);
        myMat.color = myCol;
    }

    public void ChangeMesh()
    {
        if (hatPos < playerMeshes.Length-1)
        {
            hatPos++;
            myHatMesh.mesh = playerMeshes[hatPos];
        }
        else
        {
            hatPos = 0;
            myHatMesh.mesh = playerMeshes[hatPos];
        }

    }

}
