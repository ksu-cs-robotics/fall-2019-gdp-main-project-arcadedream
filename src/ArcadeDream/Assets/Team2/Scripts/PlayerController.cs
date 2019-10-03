using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Implements basics controls for WASD, and jumping movements
/// Implements NPC Interaction
/// Author: Josh Dotson, Lew Fortwangler
/// Version: 3
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour // NetworkBehaviour
{
    [SerializeField] public float WALKSPEED = 3.0f;
    [SerializeField] public float RUNSPEED = 5.0f;
    [SerializeField] public float JUMPSPEED = 5.0f;

    protected Rigidbody rigidbody_m;

    // Stores references to both the Players UI canvas, as well as the text element regarding interact text
    protected GameObject playerUICanvas_m;
    protected GameObject playerDialogMenu_m;
    protected Text playerUIInteractText_m;

    // Containers that store references to all colliders, as well as which one is active for interaction
    protected List<Collider> nearbyInteractableObjects_m;
    public Collider currentInteractableObject_m;
    public bool IsUsingMouseInteract;
    public bool IsInteracting;

    // protected bool talkFlag_m = false;       //flag to keep track of whether or not the player wants to talk to NPC
    // public GameObject DIALOGUECANVAS;

    // This acts as a buffer to prevent a interact loop from occuring when the sumbit key is held down
    protected bool SubmitKeyDown;

    void Awake()
    {
        // This will prevent the playerObject from being destroyed when they enter or exit another subgame scene
        DontDestroyOnLoad(transform.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody_m = GetComponent<Rigidbody>();

        playerUICanvas_m = gameObject.transform.Find("UI").gameObject;
        playerDialogMenu_m = gameObject.transform.Find("UI").gameObject;
        playerUIInteractText_m = playerUICanvas_m.transform.Find("InteractText").gameObject.GetComponent<UnityEngine.UI.Text>();

        // Probably should have made an actual constructor to put this in but YOLO
        nearbyInteractableObjects_m = new List<Collider>();

        // Stores whether or not a character is already interacting with someone, and if so, ignore interact input
        IsUsingMouseInteract = false;
        IsInteracting = false;

        // DIALOGUECANVAS.SetActive(false);    //doesnt need to be in player, can be moved to an enviroment start script at a later time
        //implemented here for now for clariy
    }

    void FixedUpdate()
    {
        var movement = new Vector3();
        var jump = new Vector3();

        // If the character is interacting with a game, ignore movement input so they don't act out there actions in the subgame
        if (!IsInteracting)
        {
            movement.Set(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            movement = movement.normalized * Time.deltaTime;
            jump.Set(0f, Input.GetAxisRaw("Jump"), 0f);

            // Choose movement multiplier based on whether or not the sprint input is used
            if (!Input.GetAxisRaw("Sprint").Equals(0)) { movement *= RUNSPEED; }
            else { movement *= WALKSPEED; }
        }

        // Process user movement input...
        if (!movement.Equals(Vector3.zero))
        {
            Vector3 newPosition = transform.position + movement;
            Vector3 newPositionVector = newPosition - transform.position;
            newPositionVector.y = 0f;

            var newRotation = Quaternion.LookRotation(newPositionVector, Vector3.up);
            var interpolatedRotation = Quaternion.Slerp(newRotation, transform.rotation, Time.deltaTime * 30);

            rigidbody_m.MovePosition(transform.position + movement);
            rigidbody_m.MoveRotation(interpolatedRotation);
        }

        // Process jumping input
        if (!jump.Equals(Vector3.zero))
        {
            if (rigidbody_m.velocity.y == 0)
            {
                rigidbody_m.AddRelativeForce(Vector3.up * JUMPSPEED, ForceMode.Impulse);
            }
        }

        /*if(Input.GetKeyDown("x") && talkFlag_m == true)     //display canvas that houses all NPC dialogue is displayed if
        {                                                   //the player is in the tigger area (talkFlag_m is set to true) and
            DIALOGUECANVAS.SetActive(true);                 //the player pushes "x" and wants to talk
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        // Display interaction text onto the players UI (Would rather use ?? for this but unity is being stupid about it so if statement it is)
        if (currentInteractableObject_m != null && !IsInteracting)
        {
            playerUIInteractText_m.text = currentInteractableObject_m.GetComponent<InteractController>().INTERACTTEXT;
        }
        else
        {
            playerUIInteractText_m.text = String.Empty;
        }

        if (!Input.GetAxisRaw("Fire1").Equals(0))
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Create a RaycastHit variable to store information about what was hit by the ray.
            RaycastHit gameObjectHit;

            if (Physics.Raycast(camRay, out gameObjectHit))
            {
                if (gameObjectHit.transform.gameObject.GetComponent<InteractController>())
                {
                    currentInteractableObject_m = gameObjectHit.transform.gameObject.GetComponent<Collider>();
                    IsUsingMouseInteract = true;
                }
                else
                {
                    IsUsingMouseInteract = false;

                    currentInteractableObject_m =
                        nearbyInteractableObjects_m.Find((c) => c.gameObject.tag == "NPC") ??
                        nearbyInteractableObjects_m.Find((c) => c.gameObject.tag == "Game") ??
                        nearbyInteractableObjects_m.Find((c) => c.gameObject.tag == "Player");
                }
            }
        }

        // Process interaction input ([E])
        if (!Input.GetAxisRaw("Submit").Equals(0) && !SubmitKeyDown)
        {
            SubmitKeyDown = true;

            try
            {
                if (!IsInteracting && currentInteractableObject_m != null)
                {
                    currentInteractableObject_m.GetComponent<InteractController>().Interact(this.gameObject);
                    IsInteracting = true;
                }
                else
                {
                    currentInteractableObject_m.GetComponent<InteractController>().Interact(this.gameObject);
                    IsInteracting = false;
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show($"Error: {ex.Message}")
            }
            /*finally { }*/ // If the player doesnt need to know if someone blocked them, use finally instead
        }
        else if (Input.GetAxisRaw("Submit").Equals(0)) // This is to prevent server DDOSing from holding down the sumbit key...
        {
            SubmitKeyDown = false;
        }
    }

    // When object comes in contact with the player, add it to the list of nearby objects
    private void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject.tag == "NPC")
        {
            talkFlag_m = true;
        }*/

        // If other is not interactable, just forget it
        if (!other.GetComponent<InteractController>())
            return;

        // If other does not already exist in the container and it isnt null...
        if (other != null && !nearbyInteractableObjects_m.Contains(other))
        {
            // Add other to the list of nearby interactable objects
            nearbyInteractableObjects_m.Add(other);

            // Reevaluate the current proximal interactable object
            if (!IsUsingMouseInteract)
            {
                currentInteractableObject_m =
                    nearbyInteractableObjects_m.Find((c) => c.gameObject.tag == "NPC") ??
                    nearbyInteractableObjects_m.Find((c) => c.gameObject.tag == "Game") ??
                    nearbyInteractableObjects_m.Find((c) => c.gameObject.tag == "Player");
            }
        }
    }

    // When object leaves the proximity of the player, remove it from the list of nearby objects
    private void OnTriggerExit(Collider other)
    {
        /*if(other.gameObject.tag == "NPC")
        talkFlag_m = false;
        DIALOGUECANVAS.SetActive(false); */
        
        // Remove other, as it just moved out of range
        nearbyInteractableObjects_m.Remove(other);

        // If the object that just moved out of range was the current interactable object. The null check is so its not resorted when other is null
        if (currentInteractableObject_m != null && currentInteractableObject_m.Equals(other))
        {
            // Find new proximal interactable object
            if (!IsUsingMouseInteract)
            {
                currentInteractableObject_m =
                    nearbyInteractableObjects_m.Find((c) => c.gameObject.tag == "NPC") ??
                    nearbyInteractableObjects_m.Find((c) => c.gameObject.tag == "Game") ??
                    nearbyInteractableObjects_m.Find((c) => c.gameObject.tag == "Player");
            }
        }
    }
}