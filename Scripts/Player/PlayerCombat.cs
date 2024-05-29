using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public FloatValue health;
    //public FloatValue mana;
    public PlayerData playerData;
    public GameObject projectile;
    public Transform firePoint;

    //System.Random rnd = new System.Random();

    [Header("Attack")]
    public int attackStrength;
    public List<Transform> attackPoints;
    public float attackRange;
    public int magicManaCost;

    [Header("Time")]
    public float waitTime;
    private float timer;

    [Header("Spike Jump")]
    public float spikeJumpTime;
    public BoolValue spikeJumping;

    private float holdDownStart;
    public Animator anim;
    public LayerMask enemyLayers;
    private bool attackAnim = false;

    // Update is called once per frame
    void Update()
    {
        FixOverFlows();
        DownSlash();
        UpSlash();
        Slash();
        HandleMana();
        ManageTime();
    }

    void Slash()
    {
        if (Input.GetMouseButtonDown(0) && timer < 0)
        {
            NailAnimation();
            Hit();
            RestoreTime();
        }

    }

    void DownSlash()
    {
        if (Input.GetKeyDown(KeyCode.S) && timer < 0)
        {
            Hit();
            anim.SetTrigger("SlashDown");
            RestoreTime();
        }
    }

    void UpSlash()
    {
        if(Input.GetKeyDown(KeyCode.W) && timer < 0)
        {
            Hit();
            anim.SetTrigger("SlashUp");
            RestoreTime();
        }
    }

    private void ManageTime()
    {
        timer -= Time.deltaTime;
    }

    private void RestoreTime()
    {
        timer = waitTime;
    }

    void Hit()
    {
        foreach(Transform attackPoint in attackPoints)
        {
            Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hit)
            {
                if (enemy.isTrigger)
                {
                    enemy.GetComponent<BasicEnemy>().GetDamage(attackStrength);
                    playerData.mana.Restore(1);
                    if(Input.GetKeyDown(KeyCode.S)) //Down Pogo if S is pressed
                    {
                        spikeJumping.currentValue = true;
                    }
                }
            }
        }
    }

    private IEnumerator PauseBetweenAttacks()
    {
        yield return new WaitForSeconds(2f);
    }

    void FixOverFlows()
    {
        CheckHealthOverflow();
        CheckManaOverflow();
    }

    void CheckHealthOverflow()
    {
        if(playerData.health.currentValue > playerData.health.initialValue)
        {
            playerData.health.currentValue = playerData.health.initialValue;
        }

        if(playerData.health.currentValue < 0)
        {
            playerData.health.currentValue = 0;
        }
    }

    void CheckManaOverflow()
    {
        if(playerData.mana.currentValue > playerData.mana.initialValue)
        {
            playerData.mana.currentValue = playerData.mana.initialValue;
        }

        if (playerData.mana.currentValue < 0)
        {
            playerData.mana.currentValue = 0;
        }
    }

    void HandleMana()
    {
        if(Input.GetButtonDown("Ability"))
        {
            holdDownStart = Time.time;
        }

        if (Input.GetButtonUp("Ability"))
        {
            float holdDownTime = Time.time - holdDownStart;
            ManaAbilities(holdDownTime);
        }
    }

    void ManaAbilities(float time)
    {
        if(time > 0.5f)
        {
            Heal();
        }
        else
        {
            Shoot();
        }
    }

    void Heal()
    {
        if(playerData.mana.currentValue >= magicManaCost)
        {
            playerData.health.Restore(1);
            playerData.mana.Restore(-3);
        }
    }

    void Shoot()
    {
        if(playerData.mana.currentValue >= magicManaCost)
        {
            playerData.mana.currentValue -= magicManaCost;
            Instantiate(projectile, firePoint.position, firePoint.rotation);
        }
    }

    void NailAnimation()
    {
        if(!attackAnim)
        {
            anim.SetTrigger("AttackOne");
            attackAnim = true;
        }
        else
        {
            anim.SetTrigger("AttackTwo");
            attackAnim = false;
        }
    }

    public void TakeDamage(int damage)
    {
        if (playerData.temporaryHealth.currentValue > 0)
        {
            if (playerData.temporaryHealth.currentValue < damage)
            {
                damage -= (int)playerData.temporaryHealth.currentValue;
                playerData.temporaryHealth.currentValue = 0;
                playerData.health.currentValue -= damage;
            }
            else
            {
                playerData.temporaryHealth.currentValue -= damage;
            }
        }
        else
        {
            playerData.health.currentValue -= damage;
        }
    }

    private void OnDrawGizmosSelected()
    {
        foreach(Transform attackPoint in attackPoints)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}