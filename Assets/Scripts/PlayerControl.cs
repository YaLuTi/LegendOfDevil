using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour {

    GameObject ChoosingGrid;
    GameObject ChoosingEnemy;
    public GameObject Hex;

    public Camera camera;
    int layermask;
    int EnemyLayer;
    public Material choosingMat;
    public Material normalMat;

    public bool Controllable = false;
    public bool Aiming = false;

    [Header("Fire Object")]
    public GameObject Bullet;
    public GameObject FirePoint;
    public GameObject BlackPanel;
    RaycastHit AttackHit;
    
    PlayerState playerState;

    Animator animator;

    public enum State
    {
        Idle,
        Moving,
        Acting,
        Attacking
    }

    public State state = new State();
    State pre_State;

    Vector3 target;

    private void Awake()
    {
        playerState = GetComponent<PlayerState>();
    }

    void Start()
    {
        Hex.SetActive(false);
        layermask = LayerMask.GetMask("Ground", "UI");
        EnemyLayer = LayerMask.GetMask("Player");
        animator = GetComponent<Animator>();
        target = this.transform.position;
        state = State.Moving;
        pre_State = state;
    }

    // Update is called once per frame
    void Update()
    {
        BeAiming();
        if (!Controllable) return;
        switch (state)
        {
            case State.Moving:
                MovingEvent();
                break;
            case State.Attacking:
                AttackingEvent();
                break;
            case State.Acting:
                break;
            case State.Idle:
                break;
        }
        
        // Gao's new scriptssssssssssssssssssssssssssssssssssssssssssss
        if (Input.GetButtonDown("Jump"))
        {
            //playerHealth.currentAction[1] = 3;
        }
        // Gao's new scriptssssssssssssssssssssssssssssssssssssssssssss

    }

    void BeAiming()
    {
        if (Aiming)
        {
            Hex.SetActive(true);
        }
        else
        {
            Hex.SetActive(false);
        }
    }

    void MovingEvent()
    {
        RaycastHit hit;
        if (EventSystem.current.IsPointerOverGameObject())
        {
            if(ChoosingGrid!=null)ChoosingGrid.GetComponent<Grid>().choosing = false;
            return;
        }
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000, layermask))
        {
            if (ChoosingGrid != hit.transform.gameObject)
            {
                if (ChoosingGrid != null)
                {
                    ChoosingGrid.GetComponent<Grid>().choosing = false;
                }
                ChoosingGrid = hit.transform.gameObject;
                ChoosingGrid.GetComponent<Grid>().choosing = true;
            }


            if (Input.GetButtonDown("Fire1"))
            {
                StartCoroutine((EnergySubtract()));
                StartCoroutine(MoveSpeedSet());
                target = hit.transform.position;
            }
            /*if (Input.GetButtonDown("Fire2"))
            {
                ChoosingGrid.GetComponent<Grid>().GridType = 1;
            }*/
        }

        Move();
    }

    void AttackingEvent()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out AttackHit, 1000, EnemyLayer))
        {
            if (ChoosingEnemy != AttackHit.transform.gameObject)
            {
                if (ChoosingEnemy != null)
                {
                    // ChoosingEnemy.GetComponent<PlayerControl>().Aiming = false;
                }
                ChoosingEnemy = AttackHit.transform.gameObject;
                transform.LookAt(ChoosingEnemy.transform);
                Quaternion quaternion = transform.rotation;
                quaternion.z = 0;
                quaternion.x = 0;
                transform.rotation = quaternion;
                // ChoosingEnemy.GetComponent<PlayerControl>().Aiming = true;

            }


            if (Input.GetButtonDown("Fire1"))
            {
                StartCoroutine((EnergySubtract()));
                animator.SetTrigger("Fire");
            }
        }
        else
        {
            if (ChoosingEnemy != null)
            {
              // ChoosingEnemy.GetComponent<PlayerControl>().Aiming = false;
              ChoosingEnemy = null;
            }
        }
    }

    void Shoot()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out AttackHit, 1000, EnemyLayer);

        GameObject g = Instantiate(Bullet, transform.position, Quaternion.identity);
        g.GetComponent<ShootFireLine>().start = FirePoint.transform.position;
        Vector3 a = AttackHit.transform.position;
        a.y += 1;
        g.GetComponent<ShootFireLine>().end = a;
        Destroy(g, 2f);
    }

    public void AttackChange()
    {
        if (state == State.Moving)
        {
            animator.SetBool("Aiming", true);
            state = State.Attacking;
            BlackPanel.SetActive(true);
        }
        else if(state == State.Attacking)
        {
            animator.SetBool("Aiming", false);
            state = State.Moving;
            BlackPanel.SetActive(false);
        }
    }
    public void MoveChange()
    {
        state = State.Moving;
    }
    void Move()
    {
        Vector3 distance = target - this.transform.position;
        target.y = 0;
        distance.y = 0;
        /*transform.position += distance * 5 * Time.deltaTime;*/
        transform.DOMove(target, 1f).SetEase(Ease.Linear);

        float angle = Mathf.Atan2(distance.x, distance.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
    }

    public void SetEnergy()
    {
        playerState.EnergyCount = 3;
    }

    IEnumerator MoveSpeedSet()
    {
        animator.SetFloat("Speed", 1);
        yield return new WaitForSecondsRealtime(1);
        animator.SetFloat("Speed", 0);
        yield return 0;
    }

    IEnumerator EnergySubtract()
    {
        pre_State = state;
        state = State.Acting;
        yield return new WaitForSecondsRealtime(0.75f);
        playerState.EnergyCount--;
        state = pre_State;
        if(playerState.EnergyCount <= 0)
        {
            if (ChoosingGrid != null)
            {
                ChoosingGrid.GetComponent<Grid>().choosing = false;
            }
            if (ChoosingEnemy != null)
            {
                ChoosingEnemy.GetComponent<PlayerControl>().Aiming = false;
                ChoosingEnemy = null;
            }
        }
        yield return 0;
    }

    public int EnergyCount()
    {
        return playerState.EnergyCount;
    }
}
