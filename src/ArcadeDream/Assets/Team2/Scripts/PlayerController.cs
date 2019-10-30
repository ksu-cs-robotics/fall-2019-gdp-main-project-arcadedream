using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// Maps to respective fields in the Player table in the database
/// Author: Josh Dotson
/// Version: 1
/// </summary>
public class ADDBPlayerList
{
    public int ID { get; set; }
    public string Username { get; set; }
    public string UserPassword { get; set; }

    public int Coins { get; set; }
    public int Total_Points { get; set; }

    public ulong PermissionsHash { get; set; }
    public ulong EquipmentHash { get; set; }

    public ADDBPlayerList()
    {
        ID = -1;
        Username = "NULL";
        UserPassword = "NULL";
        Coins = 0;
        Total_Points = 0;
        PermissionsHash = 0;
        EquipmentHash = 0;
    }
}

/// <summary>
/// Implements basics controls for WASD, and jumping movements
/// Implements NPC Interaction
/// Author: Josh Dotson, Lew Fortwangler
/// Version: 3
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : NetworkBehaviour // NetworkBehaviour
{
    // This will likely be set from a local config file
    public string PlayerUsername;

    [SerializeField] public float WALKSPEED = 3.0f;
    [SerializeField] public float RUNSPEED = 5.0f;
    [SerializeField] public float JUMPSPEED = 5.0f;

    protected Rigidbody rigidbody_m;

    // Stores references to both the Players UI canvas, as well as the text element regarding interact text
    protected GameObject playerUICanvas_m;
    protected GameObject playerDialogMenu_m; // Probably no longer needed as of Version 3 of InteractController...
    protected Text playerUIInteractText_m;

    // Reference stream to the local player.config file
    protected FileStream _configStream;
    protected static readonly string _configFilePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\Arcade Dream\player.config";

    // Contain information regarding currently equipped items, and unlocked items
    protected ulong _permissions;
    protected ulong _equipment;

    // Containers that store references to all colliders, as well as which one is active for interaction
    protected List<Collider> nearbyInteractableObjects_m;
    public Collider currentInteractableObject_m;
    public bool IsUsingMouseInteract;
    public bool IsInteracting;

    // This acts as a buffer to prevent a interact loop from occuring when the sumbit key is held down
    protected bool SubmitKeyDown;

    void Awake()
    {
        // This will prevent the playerObject from being destroyed when they enter or exit another subgame scene
        // DontDestroyOnLoad(transform.gameObject);

        if (File.Exists(_configFilePath))
        {
            // Open config file
            _configStream = File.Open
            (
                path: _configFilePath,
                mode: FileMode.Open,
                access: FileAccess.ReadWrite,
                share: FileShare.None
            );

            // Serialize, and decrypt config file contents here...
        }
        else { /* Create new empty config file, and use the next step to sync it to the server */ }

        try
        {
            // Attempt to connect, and sync to the database. Sync database to config file if it existed, or vice versa if it didn't...
            /* using (ADDBConnection connection = new ADDBConnection)
            {
                connection.Open();
            
                var playerTuple = connection.Query<ADDBPlayerList>($"SELECT * FROM playerlist WHERE Username = '{PlayerUsername}'");
                _permissions = playerTuple.permissionsHash;
                _equipment = playerTuple.equipmentHash;
            */
        }
        catch { /* Could not open database connection! */ }
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
    }

    void FixedUpdate()
    {
        var movement = new Vector3();
        var jump = new Vector3();

        if (true || isLocalPlayer)
        {
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
        }
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