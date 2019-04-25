using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Threading.Tasks;
using NamelessGame.Combat;
using NamelessGame.Combat.Abilities;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private CombatAbility[] abilities;
    private Animator anim;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        controller = GetComponent<CharacterController>();
        
        abilities = GetComponents<CombatAbility>();
        //blSwipe.Cooldown = globalCD;

        anim = this.gameObject.GetComponent<Animator>();
    }

    void SetupPlayerGlobalCooldowns()
    {
        var globalCD = new Cooldown(10, false);
        var globalCD2 = new Cooldown(15, false);
        var globalCD3 = new Cooldown(20, false);

        foreach (var ability in abilities.Where(x => x.SharedCooldownPool == 1))
        {
            ability.Cooldown = globalCD;
        }
        foreach (var ability in abilities.Where(x => x.SharedCooldownPool == 2))
        {
            ability.Cooldown = globalCD2;
        }
        foreach (var ability in abilities.Where(x => x.SharedCooldownPool == 3))
        {
            ability.Cooldown = globalCD3;
        }
    }

    private void ExecuteAbility(string abilityName, string description)
    {
        var cb = abilities.FirstOrDefault(x => x.AbilityRefName == abilityName);
        if (cb != null)
            cb.abilitySignaled = true;
        else
            throw (new AbilityNotFoundException(abilityName, description));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ExecuteAbility("x", "Player Primary Attack");

            //Testing purposes. Replace with respective Execute ability method call. Uncomment to see animations.

            //The following is for the melee ability.
            //anim.SetTrigger("IsMelee");

            //The following is for the mage attack.
            //anim.SetTrigger("IsMagic");

        }

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
                /*
                anim.SetTrigger("StartJump");
                anim.SetBool("Land", true);
                ApplyWalking();
                */
                anim.SetTrigger("StartJump");
                anim.SetBool("Jump", true);
                anim.SetBool("Land", true);
                //anim.SetBool("InAir", true);
                ApplyWalking();


            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                //super jump
                //moveDirection.x = moveDirection.x * 1.4f;
                //moveDirection.y = jumpSpeed * 3;

                //sprint
            }

           
            
        }
        // gravity
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
        // move
        controller.Move(moveDirection * Time.deltaTime);
        ApplyWalking();
        // anim.SetInteger("count", 1);



    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);



    }

    void ApplyWalking()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))

        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }
}
