using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum WeaponType { Pistol, MachineGun, Missile }

    //for movement
    public float movement_speed = 7f;
    public float jump_force;
    public float gravity = -15f;

    public GameObject pause_panel;
    //for jump
    [HideInInspector]
    public float y_velocity;
    [HideInInspector]
    public bool is_jumping;

    [HideInInspector]
    public float move_direction;
    [HideInInspector]
    public CharacterController controller;
    [HideInInspector]
    public bool is_ground;

    public ParticleSystem walking_particle_projectile;
    public float particle_cooldown;
    private float particle_cd;
    [HideInInspector]
    private ParticleSystem walking_particle;

    [HideInInspector]
    public bool is_pause;
    public int life = 3;

    [HideInInspector]
    public bool blink_on = false;
    private GameObject player;
    [HideInInspector]
    public bool controller_on = true;
    [HideInInspector]
    public bool invincibile;

    private bool is_start_blink = false;
    private bool is_start_blink_invincible = false;

    [HideInInspector]
    public bool down_jump;
    [HideInInspector]
    public bool crawl;
    [HideInInspector]
    public bool is_stage_ended;
    private float initial_height;
    private float initial_speed;

    private GameObject enemies;
    private float dist_to_ground;
    public List<GameObject> scene_number;
    private int curr_trigger;
    private GameObject boss;

    private GameObject guns_;
    private GameObject boss_check;

    private GameObject pause_control;
    [HideInInspector]
    public bool invincible_mode;

    private bool collision_check;
    private bool falling_check;
    private bool superjump;
    private float superheight;

    //test
    private float wheelrotate;

    public FlickerLight main_light;
    private float slope;
    private Vector3 hitNormal;

    private void Awake()
    {
    }

    void Start()
    {
        move_direction = 1.0f;
        y_velocity = 0f;
        player = GameObject.FindWithTag("Player");
        controller = GetComponent<CharacterController>();
        is_pause = false;
        particle_cd = particle_cooldown;
        controller_on = true;
        invincibile = false;
        down_jump = false;
        crawl = false;
        initial_height = controller.height;
        initial_speed = movement_speed;

        dist_to_ground = GetComponent<BoxCollider>().bounds.extents.y;

        scene_number = Camera.main.GetComponent<CameraLogic>().scene_triggers;
        is_stage_ended = false;
        walking_particle = Instantiate(walking_particle_projectile, transform.position, Quaternion.identity) as ParticleSystem;
        guns_ = gameObject.transform.GetChild(2).gameObject;
        boss_check = GameObject.Find("BossChecker");
        pause_control = GameObject.Find("PauseManager");
        invincible_mode = false;
        collision_check = false;
        falling_check = false;
        wheelrotate = 0;
        superjump = false;
        slope = 0f;
    }
    void SetParticlePosition(float direction)
    {
        Vector3 new_pos = new Vector3(transform.position.x + move_direction, transform.position.y - 1.5f, transform.position.z);
        walking_particle = Instantiate(walking_particle_projectile, new_pos, Quaternion.identity) as ParticleSystem;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 temp = transform.position;
        temp.z = -1;
        transform.position = temp;

        if (life == 0)
        {
            if(!pause_panel.activeSelf)
            {
                PauseGame();
                pause_panel.SetActive(true);
            }
        }

        if (blink_on)
        {
            if (!is_start_blink)
            {
                StartCoroutine(Blink_Object(0.3f, 0.05f));
            }
        }

        if(invincibile)
        {
            if(!is_start_blink_invincible)
            {
                StartCoroutine(Blink_Object_Invincibile(0.3f, 0.05f));
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            PauseGame();
            if(!pause_panel.activeSelf)
                pause_panel.SetActive(true);
            else pause_panel.SetActive(false);
        }

        if (crawl)
            movement_speed = initial_speed * 0.5f;
        else movement_speed = initial_speed;

        if (y_velocity < -15.0f && down_jump)
            down_jump = false;

        if(Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            if (invincible_mode)
            {
                invincible_mode = false;                
            }

            else
            {
                invincible_mode = true;
            }
            
        }

        
        if (controller_on && Time.timeScale != 0f)
        {
            float x_input = Input.GetAxis("Horizontal");
            float x_velocity = x_input * movement_speed * Time.deltaTime;
            is_ground = controller.isGrounded;

            if (is_ground)
            {
                if (superjump)
                {
                    gameObject.GetComponent<SoundManager>().JumpPlatform.Play();
                    controller.Move(new Vector3(x_velocity,
                                               y_velocity * Time.deltaTime,
                                               0));
                    superjump = false;
                }
                else
                {
                    down_jump = false;
                    if (Input.GetKey(KeyCode.X) || Input.GetKeyDown(KeyCode.Joystick1Button0))
                    {
                        SetParticlePosition(move_direction);
                        if (Input.GetKey(KeyCode.DownArrow) || Input.GetAxis("Vertical") < 0 || Input.GetAxis("JoyVertical") < 0)
                        {
                            y_velocity = 1.75f;
                            down_jump = true;
                        }
                        else y_velocity = jump_force;
                    }
                    else y_velocity = 0f;
                }
            }
            //Add Gravity
            y_velocity += (gravity * Time.deltaTime);

            if (x_input > 0.0f)
            {
                move_direction = 1.0f;
                if (is_ground)
                {
                    wheelrotate = 10;
                    RotateWheel();
                    if (particle_cd == particle_cooldown)
                    {
                        SetParticlePosition(move_direction);
                    }
                    particle_cd -= Time.deltaTime;
                    if (particle_cd < 0f)
                        particle_cd = particle_cooldown;
                }
            }
            else if (x_input < 0.0f)
            {
                move_direction = -1.0f;
                if (is_ground)
                {
                    wheelrotate = 10;
                    RotateWheel();
                    if (particle_cd == particle_cooldown)
                    {
                        SetParticlePosition(move_direction);
                    }
                    particle_cd -= Time.deltaTime;
                    if (particle_cd < 0f)
                        particle_cd = particle_cooldown;
                }
            }
            else
            {
                wheelrotate = 0f;
            }
            if(!boss_check.GetComponent<BossChecker>().is_boss_alive)
            {
                is_stage_ended = true;
            }
            KeepInsideScreen(ref x_velocity);
            Quaternion target = Quaternion.Euler(slope, 90f * move_direction, 0.0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, 1f);
            controller.Move(new Vector3(x_velocity,
                                        y_velocity * Time.deltaTime,
                                        0));
            if (player.transform.position.y <= -30.0f && !falling_check)
            {
                falling_check = true;
            }
        }
    }
    void RotateWheel()
    {
        for (int i = 0; i < transform.GetChild(1).childCount - 1; ++i)
        {
            transform.GetChild(1).transform.GetChild(i).transform.Rotate(Vector3.forward * Time.deltaTime, wheelrotate, Space.Self);
        }
    }
    void LateUpdate()
    {
        if (collision_check)
        {
            if (controller_on && !invincibile &&
                boss_check.GetComponent<BossChecker>().is_boss_alive && !invincible_mode)
            {
                if(falling_check)
                {
                    life -= 1;
                    falling_check = false;
                    collision_check = false;
                }
                else
                {
                    life -= 1;
                    blink_on = true;
                    collision_check = false;
                }
            }
        }

        if(falling_check)
        {
            curr_trigger = Camera.main.GetComponent<CameraLogic>().curr_trigger;
            player.transform.position = new Vector3(scene_number[curr_trigger].transform.position.x,
                                                    scene_number[curr_trigger].transform.position.y + 10f, -1);
            collision_check = true;
        }
    }

    public void PauseGame()
    {
        if(is_pause)
        {
            Time.timeScale = 1;
            is_pause = false;
        }
        else
        {
            Time.timeScale = 0;
            is_pause = true;
        }
    }

    void MoveTowardsTarget()
    {
        Vector3 offset = new Vector3(0, 1000, 0);
        y_velocity = -100f;
        controller.Move(offset * Time.deltaTime);
    }

    IEnumerator Blink_Object(float duration, float blink_time)
    {
        is_start_blink = true;
        controller_on = false;

        while (duration > 0f)
        {
            duration -= Time.deltaTime;

            //toggle renderer player
            for(int i = 0; i < transform.GetChild(0).childCount; ++i)
                transform.GetChild(0).transform.GetChild(i).GetComponent<Renderer>().enabled = !transform.GetChild(0).transform.GetChild(i).GetComponent<Renderer>().enabled;
            //toggle renderer gun
            for(int i = 1; i < guns_.transform.childCount; ++i)
                guns_.transform.GetChild(i).GetComponent<Renderer>().enabled = !guns_.transform.GetChild(i).GetComponent<Renderer>().enabled;

            //wait for a bit
            yield return new WaitForSeconds(blink_time);
        }

        blink_on = false;
        MoveTowardsTarget();
        //make sure renderer is enabled when we exit
        for (int i = 0; i < transform.GetChild(0).childCount; ++i)
            transform.GetChild(0).transform.GetChild(i).GetComponent<Renderer>().enabled = true;
        for (int i = 1; i < guns_.transform.childCount; ++i)
            guns_.transform.GetChild(i).GetComponent<Renderer>().enabled = true;
        is_start_blink = false;
        controller_on = true;
        invincibile = true;
    }

    IEnumerator Blink_Object_Invincibile(float duration, float blink_time)
    {
        is_start_blink_invincible = true;
        while (duration > 0f)
        {
            duration -= Time.deltaTime;
            //toggle renderer player
            for (int i = 0; i < transform.GetChild(0).childCount; ++i)
                transform.GetChild(0).transform.GetChild(i).GetComponent<Renderer>().enabled = !transform.GetChild(0).transform.GetChild(i).GetComponent<Renderer>().enabled;
            //toggle renderer gun
            for (int i = 1; i < guns_.transform.childCount; ++i)
                guns_.transform.GetChild(i).GetComponent<Renderer>().enabled = !guns_.transform.GetChild(i).GetComponent<Renderer>().enabled;
            //wait for a bit
            yield return new WaitForSeconds(blink_time);
        }
        for (int i = 0; i < transform.GetChild(0).childCount; ++i)
            transform.GetChild(0).transform.GetChild(i).GetComponent<Renderer>().enabled = true;
        for (int i = 1; i < guns_.transform.childCount; ++i)
            guns_.transform.GetChild(i).GetComponent<Renderer>().enabled = true;
        is_start_blink_invincible = false;
        invincibile = false;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "EnemyBullet") // layer for Boid(FlyingMob)
        {
            if (controller_on && !invincibile &&
                boss_check.GetComponent<BossChecker>().is_boss_alive && !invincible_mode)
            {
                collision_check = true;
            }
                
        }

        else if(other.gameObject.layer == 20)
        {
            if (controller_on && !invincibile && !blink_on &&
                boss_check.GetComponent<BossChecker>().is_boss_alive && !invincible_mode)
            {
                life -= 1;
                blink_on = true;
            }
        }
            

        if (other.gameObject.tag == "PlayerBullet")
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), other.gameObject.GetComponent<Collider>(), true);
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Platform" || other.gameObject.tag == "SpecialLayer")
        {
            if (other.transform.rotation.z > 0)
            {
                Vector3 temp = transform.rotation.eulerAngles;
                if (move_direction > 0f)
                    temp.x = -40f;
                else temp.x = 40f;
                slope = temp.x;
            }
            else slope = 0f;
        }
    }

    void OnCollisionExit(Collision other)
    {

    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (hit.gameObject.tag == "PlayerBullet")
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), hit.gameObject.GetComponent<Collider>(), true);
        }

        if (hit.gameObject.tag == "Enemy" || hit.gameObject.tag == "Boss")
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), hit.gameObject.GetComponent<Collider>(), true);
        }
        if(hit.gameObject.tag == "SuperPlatform")
        {
            if (transform.position.x > hit.gameObject.GetComponent<BoxCollider>().bounds.min.x && transform.position.x < hit.gameObject.GetComponent<BoxCollider>().bounds.max.x
                && transform.position.y >= hit.gameObject.GetComponent<BoxCollider>().bounds.max.y 
                && hit.gameObject.GetComponent<SuperJump>().is_activated == true)
            {
                y_velocity = hit.gameObject.GetComponent<SuperJump>().jumpPower;
                superjump = true;
            }
        }
    }

    void KeepInsideScreen(ref float vel)
    {
        float camToObjZ = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
        float left = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camToObjZ)).x;
        float right = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camToObjZ)).x;
        float posX = transform.position.x;

        if (posX < left && vel < 0.0f)
        {
            vel = 0.0f;
        }

        else if (posX > right && vel > 0.0f)
        {
            vel = 0.0f;
        }
    }

    bool is_grounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, dist_to_ground + 0.1f);
    }
}
