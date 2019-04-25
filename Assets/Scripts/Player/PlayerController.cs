using System.Collections;
using System;
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
    public Collider meleeCollider;
    public Collider spellCleaveCollider;
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

    private bool ExecuteAbility(string abilityName, string description)
    {
        bool offCD = false;
        var cb = abilities.FirstOrDefault(x => x.AbilityRefName == abilityName);
        if (cb != null)
        {
            offCD = cb.Cooldown.Ready();
            cb.abilitySignaled = true;               
        }            
        else
            throw (new AbilityNotFoundException(abilityName, description));

        return offCD;
    }

    private CombatAttack GenerateAbilityAttack(string abilityName)
    {
        var combatAttack = new CombatAttack(0, DamageType.Physical);

        var cb = abilities.FirstOrDefault(x => x.AbilityRefName == abilityName);
        if (cb != null)
        {
            float dmg = cb.damageMag * cb.damageMod;
            combatAttack.SetAttackDamage(dmg);
            combatAttack.SetDamageType(cb.damageType);
        }
        else
            throw (new AbilityNotFoundException(abilityName, ""));

        return combatAttack;
    }

    void MeleeHitCheck(CombatAttack atk, Collider hitbox)
    {
        var hitCheck = hitbox.GetComponent<MeleeHitbox>();
        var cols = hitCheck.GetColliders();

        foreach (var col in cols)
        {         
            try
            {
                var enemy = col.GetComponent<CombatantScript>();
                enemy.DamageCombatant(atk);
            }
            catch (Exception ex) { Debug.Log(ex.Message); }
        }
    }


    // Update is called once per frame
    void Update()
    {
    
        if (Input.GetMouseButtonDown(0))
        {
            bool willExecute = ExecuteAbility("BasicMelee", "Player Primary Attack");
            if (willExecute)
            {
                anim.SetTrigger("IsMelee");
                var attack = GenerateAbilityAttack("BasicMelee");            
                MeleeHitCheck(attack, meleeCollider);
            }              
        }

        if (Input.GetMouseButtonDown(1))
        {
            bool willExecute = ExecuteAbility("BasicMagic", "Player Primary Attack");
            if (willExecute)
            {
                anim.SetTrigger("IsMagic");
                var attack = GenerateAbilityAttack("BasicMagic");
                MeleeHitCheck(attack, spellCleaveCollider);
            }                
        }

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
                moveDirection.x = moveDirection.x * 1.4f;
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
                moveDirection.x = moveDirection.x * 1.4f;
                moveDirection.y = jumpSpeed * 2;

                //sprint
            }

            if (Input.GetKey(KeyCode.Z))
            {
                moveDirection.x += moveDirection.x * .6f;
                moveDirection.z += moveDirection.z * .6f;
            }

           
            
        }
        else
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), moveDirection.y, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection.x = moveDirection.x * speed;
            moveDirection.z = moveDirection.z * speed;
            //moveDirection.x = Input.GetAxis("Vertical");
            //moveDirection.z = Input.GetAxis("Horizontal");
            //moveDirection = transform.TransformDirection(moveDirection);
            //moveDirection.x = moveDirection.x * speed;
            //moveDirection.z = moveDirection.z * speed;
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
