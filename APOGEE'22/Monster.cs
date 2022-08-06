using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;


public class Monster : MonoBehaviour
{
    public bool canWork = false;
    public GameObject player;
    public NavMeshAgent agent;
    public ThirdPersonCharacter charecter;
    public BossHealth hp;
    public AudioManager am;

    public Animator animator;
    public float meteorAnimationTime = 3f;
    public float spinAnimationTime = 3f;
    public float rainAnimationTime = 3f;
    public float summonAnimationTime = 3f;

    public float startCatch = 10f;

    //Meteor

    public float forceMag = 5f;
    public GameObject meteor;
    public float meteorHeight = 20f;
    public GameObject[] meteors;
    public GameObject meteorShadow; 
    public Transform[] targets;
    public GameObject[] targetPlates;

    //Spin

    public GameObject spinAttack;


    //Rain
    public GameObject crystalRain;
    public float spawnAt;


    //Summon = 0
    public Transform[] summonTargets;

    public bool attackOnGoing = false;
    public float longRange = 30f;
    public float medRange = 15f;

    public float attackTimer;
    public float initialAttackTimer;

    void Start()
    {
        agent.updateRotation = false;
        animator = gameObject.GetComponent<Animator>();
        attackTimer = initialAttackTimer;
    }

    void Update()
    {
        if (hp.cutScenePlaying)
        {
            charecter.Move(Vector3.zero, false, false);
            return;
        }
        if(!canWork)
        {
            return;
        }
           

        attackTimer -= Time.deltaTime;
        if (player != null)
        {
            agent.SetDestination(player.transform.position);
        }
        //Velocity Controls
        if (agent.remainingDistance > agent.stoppingDistance)
        {
            charecter.Move(agent.desiredVelocity, false, false);
        }
        else
        {
            charecter.Move(Vector3.zero, false, false);
        }

        if(attackTimer < 0 && !attackOnGoing)
        {
            attackTimer = Random.Range(initialAttackTimer, initialAttackTimer + 5f);
            attackOnGoing = true;
            Attack();
        }
    }

    void Attack()
    {
        float dist = new Vector3(player.transform.position.x - transform.position.x, 0, player.transform.position.z - transform.position.z).magnitude;
        if (dist > longRange)
        {
            int rnd = Random.Range(0, 100);
            if (rnd > 90)
            {
                Debug.Log("Long Range: " + "Spin");
                StartCoroutine("Spin");
            }
            else if (rnd > 40)
            {
                Debug.Log("Long Range: " + "Rain");
                StartCoroutine("Rain");
            }
            else
            {
                Debug.Log("Long Range: " + "Meteor");
                StartCoroutine("Meteor");
            }
        }
        else if (dist > medRange)
        {

            int rnd = Random.Range(0, 100);
            if (rnd > 70)
            {
                Debug.Log("Medium Range: " + "Spin");
                StartCoroutine("Spin");
            }
            else if (rnd > 55)
            {
                Debug.Log("Medium Range: " + "Rain");
                StartCoroutine("Rain");
            }
            else
            {
                Debug.Log("Medium Range: " + "Meteor");
                StartCoroutine("Meteor");
            }
        }
        else
        {

            int rnd = Random.Range(0, 100);
            if (rnd > 60)
            {
                Debug.Log("Short Range: " + "Spin");
                StartCoroutine("Spin");
            }
            else if (rnd > 20)
            {
                Debug.Log("Short Range: " + "Meteor");
                StartCoroutine("Meteor");
            }
            else
            {
                Debug.Log("Short Range: " + "Rain");
                StartCoroutine("Rain");
                
            }
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            //Debug.Log("Hit");
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }

    private IEnumerator Meteor()
    {
        animator.SetBool("MeteorSpell", true);
        am.PlaySound("BossAttack1");
        meteors = new GameObject[5];
        targetPlates = new GameObject[5];
        Vector3 spawn = gameObject.transform.position + new Vector3(0, meteorHeight, 0);
        for (int i = 0; i < 5; i++)
        {
            meteors[i] = Instantiate(meteor, spawn, Quaternion.identity);
            Vector3 force = (targets[i].position - meteors[i].transform.position).normalized;
            meteors[i].GetComponent<Rigidbody>().AddForce(force * forceMag, ForceMode.VelocityChange);
        }

        for (int i = 0; i < 5; i++)
        {
            Instantiate(meteorShadow, targets[i].position + new Vector3(0, 0.1f, 0),Quaternion.Euler(new Vector3(90,0,0)));
        }

        yield return new WaitForSeconds(meteorAnimationTime);

        animator.SetBool("MeteorSpell", false);
        attackOnGoing = false;

    }

    private IEnumerator Spin()
    {
       animator.SetBool("SpinSpell", true);
       am.PlaySound("BossAttack2");
        yield return new WaitForSeconds(spinAnimationTime);
        spinAttack.SetActive(true);
        animator.SetBool("SpinSpell", false);

        yield return new WaitForSeconds(spinAttack.GetComponent<Spin>().attackTime);
        spinAttack.SetActive(false);
        attackOnGoing = false;
    }

    public IEnumerator Summon()
    {
        animator.SetBool("SummonSpell", true);
        yield return new WaitForSeconds(attackTimer + summonAnimationTime);
        //Summon AI
       
        animator.SetBool("SummonSpell", false);
    }

    public IEnumerator Rain()
    {
        animator.SetBool("RainSpell", true);
        am.PlaySound("BossAttack1");
        yield return new WaitForSeconds(rainAnimationTime);
        animator.SetBool("RainSpell", false);
        Vector3 dir = (player.transform.localPosition - transform.position);
       // Vector3 pos = ((-dir.normalized) * (dir.magnitude * spawnAt)) + new Vector3(0, 20, 0);
       Vector3 pos = transform.position + new Vector3(0, 20, 0);
        GameObject rain = Instantiate(crystalRain, pos, Quaternion.identity);
        yield return new WaitForSeconds(rain.GetComponent<CrystalRain>().destroyIn);
        attackOnGoing = false;

    }
}
