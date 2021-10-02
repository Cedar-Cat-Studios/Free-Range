using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInput : MonoBehaviour
{
    public InputAction movement;
    public InputAction peck;
    public InputAction dash;
    public float Speed;
    public float dashForce;
    public float radius, power;
    public Rigidbody rb;
    public Transform interactionTransform;
    public LayerMask enemy;
    public Collider col;
    public Animator henAnimator;
    public ParticleSystem hotDash;
    bool isRun;
    bool isPeck;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        isRun = false;
        isPeck = false;
        hotDash.Stop();
    }

    void OnEnable() 
    {
        movement.Enable();
        peck.Enable();
        dash.Enable();
    }

    void OnDisable() 
    {
        movement.Disable();
        peck.Disable();
        dash.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if(movement.ReadValue<Vector2>() == new Vector2 (0,0)) {
            henAnimator.SetBool("isWalk", false);
        }
        else {
            henAnimator.SetBool("isWalk", true);
        }
        // //Read the input value for x and y vector location and use that for the movement of the character.
        Vector2 inputVector = movement.ReadValue<Vector2>();
        Vector3 finalVector = new Vector3();
        finalVector.x = inputVector.x;
        finalVector.z = inputVector.y;

        // The character moves based off the input values from the above code and looks in the last known direction.
        transform.LookAt(finalVector + transform.position);
        rb.MovePosition(transform.position + finalVector * Time.deltaTime * Speed);

        // If the dash button is pressed then it will add force to dash to the character.
        if (dash.WasPressedThisFrame())
        {
            rb.AddForce(transform.forward * dashForce);
            // ADD: A cloud animation and sound probably
            isRun = true;
            henAnimator.SetBool("isRun", true);
            hotDash.Play();
            Debug.Log(isRun);
            StartCoroutine(AnimatorBlender());

        }
        if (peck.WasPressedThisFrame())
        {
            Debug.Log("Peck");
            henAnimator.SetBool("isPeck", true);
            Vector3 explosionPos = interactionTransform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius, enemy);
            foreach (Collider hit in colliders)
            {
                // UPDATE: The Run Away call will have the enemy run away from the hen and eggs.
                hit.GetComponent<EnemyManager>().RunAway(col);
            }
            isPeck = true;
            StartCoroutine(AnimatorBlender());
        }
    }
    IEnumerator AnimatorBlender()
    {        
        yield return new WaitForSeconds(1f);
        if (isPeck) {
            henAnimator.SetBool("isPeck", false);
            isPeck = false;
        }
        if (isRun) {
            henAnimator.SetBool("isRun", false);
            isRun = false;
            hotDash.Stop();
        }
    }
}

public static class InputActionExtensions
{
    public static bool IsPressed(this InputAction inputAction)
    {
        return inputAction.ReadValue<float>() > 0f;
    }
 
    public static bool WasPressedThisFrame(this InputAction inputAction)
    {
        return inputAction.triggered && inputAction.ReadValue<float>() > 0f;
    }
 
    public static bool WasReleasedThisFrame(this InputAction inputAction)
    {
        return inputAction.triggered && inputAction.ReadValue<float>() == 0f;
    }
}
