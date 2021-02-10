using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStats : MonoBehaviour {

    public float health;
    public int armor;
    public float stamina;
    AudioSource audioSrc;
    public AudioClip hurt;
    public AudioClip nearDeath;
    public AudioClip death;
    public AudioClip quake;
    public AudioClip tired;
    public GameObject head;
    public Text healthText;
    Color healthTextColor;
    public Text armorText;
    public Image healthFillAmount;
    public Image staminaFillAmount;
    public Image armourFillAmount;

    bool fullHealth;
    bool fullArmor;

    public Image UIFlash;

    bool isRunning;
    public bool noStam;
    public bool fullStam;
    public float stamIncreaseSpeed;
    public float stamDecreaseSpeed;
    public float catchBreathTime;


    Transform playerTransform;
    public Vector3 lastPosition;
    public Quaternion lastRotation;

    public bool ScreenFlashEnabled = false;
    public float ScreenFlashTimer = 0.1f;
    private float ScreenFlashTimerStart;
    Image flashScreen;
    

    public Transform camTransform;
    public Vector3 originalPos;

    ImpactReceiver Impact;
    Rigidbody rb;

    public bool InitQuake;
    bool startQuake = false;
    float quakeTimer = 5.0f;

    public bool blasterHit;
    public bool flakHit;
    public bool photonHit;
    public bool piercerHit;
    public bool titanHit;
    public bool hyperHit;
    public bool vortexHit;

    GameObject hitElement;
    Vector3 direction;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        audioSrc = GetComponent<AudioSource>();
        Impact = GetComponent<ImpactReceiver>();
        ScreenFlashTimerStart = ScreenFlashTimer;
        playerTransform = transform;
        lastPosition = playerTransform.position;
        lastRotation = playerTransform.rotation;
        camTransform = Camera.main.transform;
        originalPos = camTransform.localPosition;
        healthTextColor = healthText.color;
        noStam = false;
        setUIText(ChangeStat.armor);
        setUIText(ChangeStat.health);
	}
	
	// Update is called once per frame
	void Update () {
        AutoRegenStam();
        handler();
        OnPlayerDamage();

        if (InitQuake)
        {
            if (quakeTimer > 0)
            {
                ShakeScreen(0.5f, true);
                quakeTimer -= Time.deltaTime;
            }
            else if (quakeTimer < 0)
            {
                quakeTimer = 5;
                ShakeScreen(0.5f, false);
                InitQuake = false;
            }

        }
        if (health <= 25)
        {
            UIFlash.enabled = true;
            if (health <= 10)
            {
                UIFlash.color = new Color(1, 0, 0, Mathf.PingPong(Time.time * 10, 1));
                healthText.color = new Color(Mathf.Sin(Time.time * 30), 0f, 0f, 1.0f);
            }
            else
            {
                UIFlash.color = new Color(1, 0, 0, Mathf.PingPong(Time.time, 1));
                healthText.color = new Color(Mathf.Sin(Time.time * 10), 0f, 0f, 1.0f);
            }
        }
        else if (health >= 26)
        {
            healthText.color = healthTextColor;
            UIFlash.enabled = false;
        }

        if (ScreenFlashEnabled == true)
        {
            UIFlash.enabled = true;
            ScreenFlashTimer -= Time.deltaTime;
        }

        if (ScreenFlashTimer < 0)
        {
            UIFlash.enabled = false;
            ScreenFlashTimer = ScreenFlashTimerStart;
            ScreenFlashEnabled = false;
        }
    }
    void handler()
    {
        healthFillAmount.fillAmount = Map(health, 0, 100, 0, 1);
        armourFillAmount.fillAmount = Map(armor, 0, 200, 0, 1);
        staminaFillAmount.fillAmount = Map(stamina, 0, 100, 0, 1);
    }
    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
    public enum DamageElement { Blaster, Flak, Photon, Piercer, Titan, Hyper, Vortex}
    public void AddDamageType(DamageElement element)
    {
        switch (element)
        {
            case DamageElement.Blaster:
                {
                    Impact.AddImpact(direction, 5);
                    takeDamage(5);
                    blasterHit = false;
                    break;
                }
            case DamageElement.Flak:
                {
                    Impact.AddImpact(direction, 5);
                    takeDamage(3);
                    flakHit = false;
                    break;
                }
            case DamageElement.Photon:
                {
                    Impact.AddImpact(direction, 3);
                    takeDamage(4);
                    photonHit = false;
                    break;
                }
            case DamageElement.Piercer:
                {
                    Impact.AddImpact(direction, 1);
                    takeDamage(1);
                    piercerHit = false;
                    break;
                }
            case DamageElement.Titan:
                {
                    Impact.AddImpact(direction, 50);
                    takeDamage(10);
                    titanHit = false;
                    break;
                }
            case DamageElement.Hyper:
                {
                    Impact.AddImpact(direction, 30);
                    takeDamage(30);
                    hyperHit = false;
                    break;
                }
            case DamageElement.Vortex:
                {
                    Impact.AddImpact(direction, 50);
                    takeDamage(80);
                    vortexHit = false;
                    break;
                }
        }
    }
    public void takeDamage(int amount)
    {
        if (armor > 0)
        {
            UIFlash.color = new Color(1f, 1f, 1f, 1f);
            ScreenFlashEnabled = true;
            armor -= amount;
            audioSrc.volume = Random.Range(0.8f, 1.0f);
            audioSrc.pitch = Random.Range(0.8f, 1.0f);
            audioSrc.PlayOneShot(hurt);
            setUIText(ChangeStat.armor);
        }
        else if (armor <= 0)
        {
            armor = 0;
            health -= amount;
            UIFlash.color = new Color(1f, 0f, 0f, 1f);
            ScreenFlashEnabled = true;
            audioSrc.volume = Random.Range(0.8f, 1.0f);
            audioSrc.pitch = Random.Range(0.8f, 1.0f);
            audioSrc.PlayOneShot(hurt);
            setUIText(ChangeStat.armor);
            setUIText(ChangeStat.health);
            if (health <= 0)
            {
                health = 100;
                setUIText(ChangeStat.health);
                UIFlash.color = new Color(UIFlash.color.r, UIFlash.color.g, UIFlash.color.b, 1);
                healthText.color = healthTextColor;
                playerTransform.position = lastPosition;
                playerTransform.rotation = lastRotation;
                ResetStam();
            }
        }
        
    }
    public void OnPlayerDamage()
    {
        if (blasterHit)
            AddDamageType(DamageElement.Blaster);
        else if (flakHit)
            AddDamageType(DamageElement.Flak);
        else if (photonHit)
            AddDamageType(DamageElement.Photon);
        else if (piercerHit)
            AddDamageType(DamageElement.Piercer);
        else if (titanHit)
            AddDamageType(DamageElement.Titan);
        else if (hyperHit)
            AddDamageType(DamageElement.Hyper);
        else if (vortexHit)
            AddDamageType(DamageElement.Vortex);
    }
    public enum ChangeStat { armor, health }
    void setUIText(ChangeStat stat)
    {
        switch (stat)
        {
            case ChangeStat.health:
                {
                    healthText.text = Mathf.CeilToInt(health).ToString();
                    break;
                }
            case ChangeStat.armor:
                {
                    armorText.text = armor.ToString();
                    break;
                }
        }
    }
    public void AutoRegenStam()
    {
        if (!noStam)
        {
            isRunning = this.GetComponent<PlayerController>().isRunning;
            if (!isRunning)
            {
                stamina += Time.deltaTime * stamIncreaseSpeed;
                if (stamina >= 100)
                {
                    fullStam = true;
                    stamina = 100;
                }
            }
            else if (isRunning)
            {
                fullStam = false;
                stamina -= Time.deltaTime * stamDecreaseSpeed;
                if (stamina < 0)
                {
                    stamina = 0;
                    catchBreathTime += 2;
                    audioSrc.PlayOneShot(tired);
                    noStam = true;
                }
            }
        }

        if (catchBreathTime > 0)
            catchBreathTime -= Time.deltaTime;
        
        else if (catchBreathTime < 0)
        {
            catchBreathTime = 0;
            noStam = false;
        }
    }
    public void ResetStam()
    {
        stamina += 100;
        catchBreathTime = 0;
        noStam = false;
        if (stamina >= 100)
            stamina = 100;
    }
    public void ShakeScreen(float Strength, bool True)
    {
        if (True)
        {
            if (!startQuake)
            {
                audioSrc.PlayOneShot(quake);
                startQuake = true;
            }
            camTransform.localPosition = originalPos + Random.insideUnitSphere * Strength;
        }
        else
        {
            camTransform.localPosition = originalPos;
            startQuake = false;
            InitQuake = false;
        }
    }
    public void QuakeState()
    {
        InitQuake = true;
    }
    public enum ItemUI { health5, health25, health100, Armor1, Armor50, Armor200}
    public void AddHealth(int amount, ItemUI type)
    {
        switch (type)
        {
            case ItemUI.health5:
                {
                    UIFlash.color = new Color(0f, 1f, 0f, 1f);
                    break;
                }
            case ItemUI.health25:
                {
                    UIFlash.color = new Color(0f, 0f, 1f, 1f);
                    break;
                }
            case ItemUI.health100:
                {
                    UIFlash.color = new Color(1f, 0f, 0f, 1f);
                    break;
                }
        }
        ScreenFlashEnabled = true;
        health += amount;
        if (health >= 100)
            health = 100;
        setUIText(ChangeStat.health);
    }
    public void AddArmor(int amount, ItemUI type)
    {
        switch (type)
        {
            case ItemUI.Armor1:
                {
                    UIFlash.color = new Color(1f, 1f, 1f, 1f);
                    break;
                }
            case ItemUI.Armor50:
                {
                    UIFlash.color = new Color(1f, 1f, 1f, 1f);
                    break;
                }
            case ItemUI.Armor200:
                {
                    UIFlash.color = new Color(1f, 1f, 1f, 1f);
                    break;
                }
        }
        ScreenFlashEnabled = true;
        armor += amount;
        if (armor >= 200)
            health = 200;
        setUIText(ChangeStat.armor);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
            direction = (this.transform.position - hit.transform.position).normalized;
            Impact.AddImpact(direction, 50);
            takeDamage(25);
        }
      
        if (hit.gameObject.CompareTag("BlasterProj"))
        {
            blasterHit = true;
            direction = (this.transform.position - hit.transform.position).normalized;
            Impact.AddImpact(direction, 5);

        }
        if (hit.gameObject.CompareTag("FlakProj"))
        {
            flakHit = true;
            direction = (this.transform.position - hit.transform.position).normalized;
            Impact.AddImpact(direction, 5);
        }
        if (hit.gameObject.CompareTag("PhotonProj"))
        {
            photonHit = true;
            direction = (this.transform.position - hit.transform.position).normalized;
            Impact.AddImpact(direction, 3);
        }
        if (hit.gameObject.CompareTag("PiercerProj"))
        {
            piercerHit = true;
            direction = (this.transform.position - hit.transform.position).normalized;
            Impact.AddImpact(direction, 1);
        }
        if (hit.gameObject.CompareTag("TitanProj"))
        {
            titanHit = true;
            direction = (this.transform.position - hit.transform.position).normalized;
            Impact.AddImpact(direction, 10);
        }
        if (hit.gameObject.CompareTag("HyperProj"))
        {
            hyperHit = true;
            direction = (this.transform.position - hit.transform.position).normalized;
            Impact.AddImpact(direction, 10);
        }
        if (hit.gameObject.CompareTag("VortexProj"))
        {
            vortexHit = true;
            direction = (this.transform.position - hit.transform.position).normalized;
            Impact.AddImpact(direction, 50);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (health < 100)
        {
            if (other.gameObject.CompareTag("Health+5"))
            {
                if (other.GetComponent<ItemRespawn>())
                {
                    if (other.GetComponent<ItemRespawn>().active == true)
                    {
                        other.GetComponent<ItemRespawn>().Obtained();
                        AddHealth(5, ItemUI.health5);
                    }
                }
            }
            if (other.gameObject.CompareTag("Health+25"))
            {
                if (other.GetComponent<ItemRespawn>())
                {
                    if (other.GetComponent<ItemRespawn>().active == true)
                    {
                        other.GetComponent<ItemRespawn>().Obtained();
                        AddHealth(25, ItemUI.health25);
                    }
                }
            }
            if (other.gameObject.CompareTag("Health+100"))
            {
                if (other.GetComponent<ItemRespawn>())
                {
                    if (other.GetComponent<ItemRespawn>().active == true)
                    {
                        other.GetComponent<ItemRespawn>().Obtained();
                        AddHealth(100, ItemUI.health100);
                    }
                }
            }
        }
        if (armor < 200)
        {
            if (other.gameObject.CompareTag("Armor+1"))
            {
                if (other.GetComponent<ItemRespawn>())
                {
                    if (other.GetComponent<ItemRespawn>().active == true)
                    {
                        other.GetComponent<ItemRespawn>().Obtained();
                        AddArmor(1, ItemUI.Armor1);
                    }
                }
            }
            if (other.gameObject.CompareTag("Armor+50"))
            {
                if (other.GetComponent<ItemRespawn>())
                {
                    if (other.GetComponent<ItemRespawn>().active == true)
                    {
                        other.GetComponent<ItemRespawn>().Obtained();
                        AddArmor(50, ItemUI.Armor50);
                    }
                }
            }
            if (other.gameObject.CompareTag("Armor+200"))
            {
                if (other.GetComponent<ItemRespawn>())
                {
                    if (other.GetComponent<ItemRespawn>().active == true)
                    {
                        other.GetComponent<ItemRespawn>().Obtained();
                        AddArmor(200, ItemUI.Armor200);
                    }
                }
            }
        }
    }
}
