using UnityEngine;
using UnityEngine.UI;

public enum WeaponSelect{ Unarmed, Blaster, FlakShotgun, PhotonRifle, ThePiercer, TitanLauncher, HyperBeam, VortexCannon }

public class PlayerWeapon : MonoBehaviour
{

    public enum PlayerSelect { PlayerOne, PlayerTwo, PlayerThree, PlayerFour }
 
    [Header("Player Select")]
    public PlayerSelect SelectPlayer;
    [Space]
    [Header("Gun Settings")]
    public WeaponSelect weapSelect;
    private Vector3 gunCurPos = new Vector3(0f, 0f, 0f);
    private Vector3 gunNewPos;
    private Vector3 rocketCurPos = new Vector3(0f, 0.14f, -0.24f);
    private Vector3 rocketNewPos;
    private Vector3 rocketLoadPos;
    Animator anim;
    public Image ammoFillAmount;
    float slowDownFactor = 0.01f;
    float slowDownLength= 2f;
    public GameObject weapSelectBanner;
    float gunLerpTime;
    Transform gunTransformRight;
    Transform gunTransformLeft;
    Transform gunTransform;
    bool Recoiled;
    public bool right;
    public AudioSource audioSrc;
    AudioClip shoot;
    AudioClip gunEquip;
    public AudioClip noClip;
    GameObject EmitterRight;
    GameObject EmitterLeft;
    GameObject Emitter;
    GameObject Projectile;
    float ProjectileForce;
    public int ammo;
    public int ammoMax;
    public Text weaponName;
    public Text textAmmo;
    Color textAmmoColor;
    public bool gunEnabled;
    float fireRate;
    public float recoilReturnRate;
    private float nextFire;
    private Recoil recoilComponent;
    public Camera CameraPosition;
    public GameObject[] muzzlesRight;
    public GameObject[] muzzlesLeft;
    GameObject muzzleFlashObject;
    public float muzzleFlashTimer = 0.1f;
    private float muzzleFlashTimerStart;
    public bool muzzleFlashEnabled = false;
    public bool isFiring;
    public Transform BottomCannon;
    public Transform TopCannon;
    public Transform CenterCannon;
    public bool GunSpin;
    public float SpinUpTime = 2;
    public float SpinUpTimer;
    public float MaxSpinRate = 360;
    public float SpinSpeed;
    public bool changeWeap;
    bool returnTime;
    bool BarrelSpin;
    int BarrelCounter;
    float Axisz;
    bool GotAmmo;
    public float strength;
    bool startSpin = true;
    bool stopSpin = false;
    public GameObject recoilComp;
    float Fire;
    float select;
    public int weapNum;
    bool weapSelected;
    bool WeapLoad;
    bool MouseEnabled = false;

    [Space]

    [Header("Unarmed")]
    public GameObject unarmedWeaponObj;
    public bool unarmedEquipped;
    public AudioClip unarmedPriSwipe;
    public Image crosshairUnarmed;
    [Space]

    [Header("Blaster")]
    public GameObject blastWeaponObj;
    public bool blasterEquipped;
    public bool blasterPickedUp;
    public int blasterAmmo;
    public int blastMaxAmmo;
    public AudioClip blastPriShot;
    public Transform blastTransformRight;
    public Transform blastTransformLeft;
    //public AudioClip blastSecShot;
    public AudioClip blastEquipSfx;
    public GameObject blastPriProjectile;
    //public GameObject blastSecProjectile;
    public Image crosshairBlast;
    public float blastFireRate;
    public float blasterRecoilReturnRate;
    public float blasterProjForce;
    public GameObject[] blasterEmitters;

    [Space]

    [Header("FlakShotgun")]
    public GameObject flakWeaponObj;
    public bool flakShotgunEquipped;
    public bool flakShotgunPickedUp;
    public int flakShotgunAmmo;
    public int flakMaxAmmo;
    public AudioClip flakPriShot;
    //public AudioClip flakSecShot;
    public AudioClip flakEquipSfx;
    public GameObject flakPriProjectile;
    //public GameObject flakSecProjectile;
    public Transform flakTransform;
    public Image crosshairFlak;
    public float flakFireRate;
    public float flakRecoilReturnRate;
    public GameObject[] flakEmitters;
    [Space]

    [Header("PhotonRifle")]
    public GameObject photonWeaponObj;
    public bool photonRifleEquipped;
    public bool photonRiflePickedUp;
    public int photonRifleAmmo;
    public int photonMaxAmmo;
    public AudioClip photonPriShot;
    public Transform photonTransform;
    //public AudioClip photonSecShot;
    public AudioClip photonEquipSfx;
    public GameObject photonPriProjectile;
    //public GameObject photonSecProjectile;
    public Image crosshairPhoton;
    public float photonFireRate;
    public float photonRecoilReturnRate;
    public float photonProjForce;
    public GameObject[] photonEmitters;
    [Space]

    [Header("ThePiercer")]
    public GameObject piercerWeaponObj;
    public bool thePiercerEquipped;
    public bool thePiercerPickedUp;
    public int thePiercerAmmo;
    public int piercerMaxAmmo;
    public AudioClip piercerPriShot;
    public AudioClip spinUp;
    public AudioClip spinning;
    public AudioClip spinDown;
    public Transform piercerTransform;
    //public AudioClip piercerSecShot;
    public AudioClip piercerEquipSfx;
    public GameObject piercerPriProjectile;
    //public GameObject piercerSecProjectile;
    public Image crosshairPiercer;
    public float piercerFireRate;
    public float piercerRecoilReturnRate;
    public float piercerProjForce;
    public GameObject piercerEmitter;
    Vector3 originalPos;
    [Space]


    [Header("TitanLauncher")]
    public GameObject titanWeaponObj;
    public GameObject titanBarrel;
    public bool titanLauncherEquipped;
    public bool titanLauncherPickedUp;
    public int titanLauncherAmmo;
    public int titanMaxAmmo;
    public AudioClip titanPriShot;
    public AudioClip titanReload;
    //public AudioClip titanSecShot;
    public Transform titanTransform;
    public AudioClip titanEquipSfx;
    public GameObject titanPriProjectile;
    //public GameObject titanSecProjectile;
    public Image crosshairTitan;
    public float titanFireRate;
    public float titanRecoilReturnRate;
    public float rocketReturnRate;
    public float titanProjForce;
    public GameObject titanEmitter;
    bool barrelReload = true;
    public GameObject rocket;
    Transform rocketTransform;
    bool loadingRocket;
    float rocketLerpTime;


    [Space]

    [Header("HyperBeam")]
    public GameObject hyperWeaponObj;
    public bool hyperBeamEquipped;
    public bool hyperBeamPickedUp;
    public int hyperBeamAmmo;
    public int hyperMaxAmmo;
    public AudioClip hyperPriShot;
    //public AudioClip hyperSecShot;
    public Transform hyperTransform;
    public AudioClip hyperEquipSfx;
    public GameObject hyperPriProjectile;
    //public GameObject hyperSecProjectile;
    public Image crosshairHyper;
    public float hyperFireRate;
    public float hyperRecoilReturnRate;
    public float hyperProjForce;
    public GameObject hyperEmitter;
    [Space]

    [Header("VortexCannon")]
    public GameObject vortexWeaponObj;
    public bool vortexCannonEquipped;
    public bool vortexCannonPickedUp;
    public int vortexCannonAmmo;
    public int vortexMaxAmmo;
    public AudioClip vortextPriShot;
    //public AudioClip vortextSecShot;
    public Transform vortexTransform;
    public AudioClip vortexEquipSfx;
    public GameObject vortexPriProjectile;
    //public GameObject vortexSecProjectile;
    public Image crosshairVortex;
    public float vortexFireRate;
    public float vortexRecoilReturnRate;
    public float vortexProjForce;
    public GameObject vortexEmitter;
    void Start()
    {
        SwitchWeapon(weapSelect);
        
        originalPos = piercerEmitter.transform.localPosition;
        muzzleFlashTimerStart = muzzleFlashTimer;
        //audioSrc = GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        recoilComponent = recoilComp.GetComponent<Recoil>();
        gunEnabled = true;
        textAmmoColor = textAmmo.color;
        BarrelCounter = 0;
        GotAmmo = false;
        anim = GetComponent<Animator>();
        SetCountAmmo();
        weapNum = 1;
    }
    public void Update()
    {
        SwitchPlayer(SelectPlayer);
        Recoiling();
        Handler();
        if (titanLauncherEquipped)
            ActiveRocketBarrel();
        if (Input.GetMouseButton(2))
        {
            weapSelectBanner.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SlowMotion();
        }
        else
        {
            weapSelectBanner.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            returnTime = true;
        }

        if (returnTime)
        {
            AudioSource musicSrc = GameObject.Find("Master/Audio/MainGame").GetComponent<AudioSource>();
            if (Time.timeScale < 1)
            {
                Time.timeScale += (1f / slowDownLength) * Time.unscaledDeltaTime;
                Time.timeScale = Mathf.Clamp(Time.timeScale, 0, 1);
                musicSrc.pitch = Time.timeScale;
                if (Time.timeScale > 0.8f)
                {
                    musicSrc.pitch = 1;
                    Time.timeScale = 1;
                }
            }
        }
        if (!changeWeap)
        {
            if (select == 1 && !weapSelected)
            {
                weapNum++;
                if (weapNum > 7)
                    weapNum = 7;
                if (!WeapLoad)
                {
                    if (weapNum == 0)
                        anim.SetTrigger("SelectUnarmed");
                    else if (weapNum == 1 && blasterPickedUp)
                        anim.SetTrigger("SelectBlaster");
                    else if (weapNum == 2 && flakShotgunPickedUp)
                        anim.SetTrigger("SelectFlak");
                    else if (weapNum == 3 && photonRiflePickedUp)
                        anim.SetTrigger("SelectPhoton");
                    else if (weapNum == 4 && thePiercerPickedUp)
                        anim.SetTrigger("SelectPiercer");
                    else if (weapNum == 5 && titanLauncherPickedUp)
                        anim.SetTrigger("SelectTitan");
                    else if (weapNum == 6 && hyperBeamPickedUp)
                        anim.SetTrigger("SelectHyper");
                    else if (weapNum == 7 && vortexCannonPickedUp)
                        anim.SetTrigger("SelectVortex");
                    WeapLoad = true;
                }
                weapSelected = true;
            }
            else if (select == -1 && !weapSelected)
            {
                weapNum--;
                if (weapNum < 1)
                    weapNum = 1;
                if (!WeapLoad)
                {
                    if (weapNum == 0)
                        anim.SetTrigger("SelectUnarmed");
                    else if (weapNum == 1 && blasterPickedUp)
                        anim.SetTrigger("SelectBlaster");
                    else if (weapNum == 2 && flakShotgunPickedUp)
                        anim.SetTrigger("SelectFlak");
                    else if (weapNum == 3 && photonRiflePickedUp)
                        anim.SetTrigger("SelectPhoton");
                    else if (weapNum == 4 && thePiercerPickedUp)
                        anim.SetTrigger("SelectPiercer");
                    else if (weapNum == 5 && titanLauncherPickedUp)
                        anim.SetTrigger("SelectTitan");
                    else if (weapNum == 6 && hyperBeamPickedUp)
                        anim.SetTrigger("SelectHyper");
                    else if (weapNum == 7 && vortexCannonPickedUp)
                        anim.SetTrigger("SelectVortex");
                    WeapLoad = true;
                }
                weapSelected = true;
            }
            else if(select == 0)
            {
                WeapLoad = false;
                weapSelected = false;
            }
        }
        if (MouseEnabled)
        {
            if (Fire == -1 && !changeWeap && ammo > 0 || Input.GetButtonDown("Fire") && !changeWeap && ammo > 0)
            {
                isFiring = true;
                if (unarmedEquipped)
                    EquipWeapState(WeaponEquipped.Unarmed);
                else if (blasterEquipped)
                    EquipWeapState(WeaponEquipped.Blaster);
                else if (flakShotgunEquipped)
                    EquipWeapState(WeaponEquipped.FlakShotgun);
                else if (photonRifleEquipped)
                    EquipWeapState(WeaponEquipped.PhotonRifle);
                else if (thePiercerEquipped)
                    EquipWeapState(WeaponEquipped.ThePiercer);
                else if (titanLauncherEquipped)
                    EquipWeapState(WeaponEquipped.TitanLauncher);
                else if (hyperBeamEquipped)
                    EquipWeapState(WeaponEquipped.HyperBeam);
                else if (vortexCannonEquipped)
                    EquipWeapState(WeaponEquipped.VortexCannon);

            }
            else if (Fire == -1 && Time.time > nextFire && !changeWeap && ammo <= 0 || Input.GetButtonDown("Fire") && Time.time > nextFire && !changeWeap && ammo <= 0)
            {
                BarrelSpin = true;
                fireRate = 0.5f;
                recoilComponent.StartRecoil(0.1f, Random.Range(-2f, -4f), Random.Range(5, 10f));
                shoot = noClip;
                audioSrc.PlayOneShot(shoot);
                nextFire = Time.time + fireRate;
            }
            else
            {
                SpinUpTimer = Mathf.Clamp(SpinUpTimer - Time.deltaTime, 0, SpinUpTime);
                isFiring = false;
                barrelReload = true;
                ActivePiercerBullet(strength, false);
                if (stopSpin)
                {
                    AudioSource gunAudioSrc = CenterCannon.GetComponent<AudioSource>();
                    gunAudioSrc.clip = spinDown;
                    gunAudioSrc.Play();
                    gunAudioSrc.loop = false;
                    stopSpin = false;
                    startSpin = true;
                }

            }
        }
        else
        {
            if (Fire == -1 && !changeWeap && ammo > 0)
            {
                isFiring = true;
                if (unarmedEquipped)
                    EquipWeapState(WeaponEquipped.Unarmed);
                else if (blasterEquipped)
                    EquipWeapState(WeaponEquipped.Blaster);
                else if (flakShotgunEquipped)
                    EquipWeapState(WeaponEquipped.FlakShotgun);
                else if (photonRifleEquipped)
                    EquipWeapState(WeaponEquipped.PhotonRifle);
                else if (thePiercerEquipped)
                    EquipWeapState(WeaponEquipped.ThePiercer);
                else if (titanLauncherEquipped)
                    EquipWeapState(WeaponEquipped.TitanLauncher);
                else if (hyperBeamEquipped)
                    EquipWeapState(WeaponEquipped.HyperBeam);
                else if (vortexCannonEquipped)
                    EquipWeapState(WeaponEquipped.VortexCannon);

            }
            else if (Fire == -1 && Time.time > nextFire && !changeWeap && ammo <= 0)
            {
                BarrelSpin = true;
                fireRate = 0.5f;
                recoilComponent.StartRecoil(0.1f, Random.Range(-2f, -4f), Random.Range(5, 10f));
                shoot = noClip;
                audioSrc.PlayOneShot(shoot);
                nextFire = Time.time + fireRate;
            }
            else
            {
                SpinUpTimer = Mathf.Clamp(SpinUpTimer - Time.deltaTime, 0, SpinUpTime);
                isFiring = false;
                barrelReload = true;
                ActivePiercerBullet(strength, false);
                if (stopSpin)
                {
                    AudioSource gunAudioSrc = CenterCannon.GetComponent<AudioSource>();
                    gunAudioSrc.clip = spinDown;
                    gunAudioSrc.Play();
                    gunAudioSrc.loop = false;
                    stopSpin = false;
                    startSpin = true;
                }

            }
        }
        
        float theta = (SpinUpTimer / SpinUpTime) * MaxSpinRate * Time.deltaTime * SpinSpeed;
        TopCannon.transform.Rotate(Vector3.forward, theta);
        BottomCannon.transform.Rotate(Vector3.forward, -theta);
        CenterCannon.transform.Rotate(Vector3.forward, -theta);

        if (ammo <= 25)
            textAmmo.color = new Color(Mathf.Sin(Time.time * 10), 0f, 0f, 1.0f);
        else if (ammo >= 26)
            textAmmo.color = textAmmoColor;

        if (muzzleFlashEnabled == true)
        {
            if (blasterEquipped)
            {
                if (right)
                    muzzleFlashObject = muzzlesRight[0];
                else
                    muzzleFlashObject = muzzlesLeft[0];
            }
            else if (flakShotgunEquipped)
            {
                muzzleFlashObject = muzzlesRight[1];
            }
            else if (photonRifleEquipped)
            {
                if (right)
                    muzzleFlashObject = muzzlesRight[2];
                else
                    muzzleFlashObject = muzzlesLeft[2];
            }
            else if (thePiercerEquipped)
            {
                muzzleFlashObject = muzzlesRight[3];
            }
            else if (titanLauncherEquipped)
            {
                muzzleFlashObject = muzzlesRight[4];
            }
            else if (hyperBeamEquipped)
            {
                muzzleFlashObject = muzzlesRight[5];
            }
            else if (vortexCannonEquipped)
            {
                muzzleFlashObject = muzzlesRight[6];
            }
            muzzleFlashObject.transform.Rotate(Random.Range(0, 90), Random.Range(0, 90), Random.Range(0, 90));
            muzzleFlashObject.SetActive(true);
            muzzleFlashTimer -= Time.deltaTime;
        }
        if (muzzleFlashTimer <= 0)
        {
            muzzleFlashObject.SetActive(false);
            muzzleFlashEnabled = false;
            muzzleFlashTimer = muzzleFlashTimerStart;
        }
    }
    void SetCountAmmo()
    {
        if (blasterEquipped)
            ammo = blasterAmmo;
        else if (flakShotgunEquipped)
            ammo = flakShotgunAmmo;
        else if (photonRifleEquipped)
            ammo = photonRifleAmmo;
        else if (thePiercerEquipped)
            ammo = thePiercerAmmo;
        else if (titanLauncherEquipped)
            ammo = titanLauncherAmmo;
        else if (hyperBeamEquipped)
            ammo = hyperBeamAmmo;
        else if (vortexCannonEquipped)
            ammo = vortexCannonAmmo;
        else
            ammo = 0;
        if (!unarmedEquipped)
            textAmmo.text = ammo.ToString();
        else
            textAmmo.text = "";
    }
    public void Shot()
    {
        if (flakShotgunEquipped)
        {
            ActiveFlakBullet();
        }
        else
        {
            if (blasterEquipped)
            {
                EmitterRight = blasterEmitters[0];
                EmitterLeft = blasterEmitters[1];
                if (right)
                    Emitter = EmitterRight;
                else
                    Emitter = EmitterLeft;
            }
            else if (photonRifleEquipped)
            {
                EmitterRight = photonEmitters[0];
                EmitterLeft = photonEmitters[1];
                if (right)
                    Emitter = EmitterRight;
                else
                    Emitter = EmitterLeft;
            }
            else if (thePiercerEquipped)
                Emitter = piercerEmitter;
            else if (titanLauncherEquipped)
                Emitter = titanEmitter;
            else if (hyperBeamEquipped)
                Emitter = hyperEmitter;
            else if (vortexCannonEquipped)
                Emitter = vortexEmitter;
            Rigidbody Temporary_RigidBody;
            GameObject Temporary_Bullet_Handler;
            Temporary_Bullet_Handler = Instantiate(Projectile, Emitter.transform.position, Emitter.transform.rotation) as GameObject;
            Temporary_Bullet_Handler.transform.Rotate(0, 0, 0);
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();
            Temporary_RigidBody.AddForce(Emitter.transform.forward * ProjectileForce);
            Destroy(Temporary_Bullet_Handler, 5.0f);
        }
    }
    void Handler()
    {
        if (blasterEquipped)
            ammoMax = blastMaxAmmo;
        else if (flakShotgunEquipped)
            ammoMax = flakMaxAmmo;
        else if (photonRifleEquipped)
            ammoMax = photonMaxAmmo;
        else if (thePiercerEquipped)
            ammoMax = piercerMaxAmmo;
        else if (titanLauncherEquipped)
            ammoMax = titanMaxAmmo;
        else if (hyperBeamEquipped)
            ammoMax = hyperMaxAmmo;
        else if (vortexCannonEquipped)
            ammoMax = vortexMaxAmmo;
        else
            ammoMax = 0;
        ammoFillAmount.fillAmount = Map(ammo, 0, ammoMax, 0, 1);
    }
    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
    public void Recoiling()
    {
        if (Recoiled)
        {

            if (right)
            {
                if (blasterEquipped)
                {
                    gunNewPos = new Vector3(0f, 0f, -0.1f);
                    gunTransformRight = blastTransformRight;
                    recoilReturnRate = blasterRecoilReturnRate;
                }
                else if (flakShotgunEquipped)
                {
                    gunNewPos = new Vector3(0f, 0f, -1f);
                    gunTransformRight = flakTransform;
                    recoilReturnRate = flakRecoilReturnRate;
                }
                else if (photonRifleEquipped)
                {
                    gunNewPos = new Vector3(0f, 0f, -0.5f);
                    gunTransformRight = photonTransform;
                    recoilReturnRate = photonRecoilReturnRate;
                }
                else if (thePiercerEquipped)
                {
                    gunNewPos = new Vector3(0f, 0f, -0.1f);
                    gunTransformRight = piercerTransform;
                    recoilReturnRate = piercerRecoilReturnRate;
                }
                else if (titanLauncherEquipped)
                {
                    gunNewPos = new Vector3(0f, 0f, -0.3f);
                    gunTransformRight = titanTransform;
                    recoilReturnRate = titanRecoilReturnRate;
                }
                else if (hyperBeamEquipped)
                {
                    gunNewPos = new Vector3(0f, 0f, -1f);
                    gunTransformRight = hyperTransform;
                }
                else if (vortexCannonEquipped)
                    gunTransformRight = vortexTransform;
                gunTransform = gunTransformRight;
            }
            else if (!right)
            {
                if (blasterEquipped)
                {
                    gunNewPos = new Vector3(0f, 0f, -0.1f);
                    gunTransformLeft = blastTransformLeft;
                    recoilReturnRate = blasterRecoilReturnRate;
                }
                else if (flakShotgunEquipped)
                {
                    gunNewPos = new Vector3(0f, 0f, -1f);
                    gunTransformLeft = flakTransform;
                    recoilReturnRate = flakRecoilReturnRate;
                }
                else if (photonRifleEquipped)
                {
                    gunNewPos = new Vector3(0f, 0f, -0.5f);
                    gunTransformLeft = photonTransform;
                    recoilReturnRate = photonRecoilReturnRate;
                }
                else if (thePiercerEquipped)
                {
                    gunNewPos = new Vector3(0f, 0f, -0.1f);
                    gunTransformLeft = piercerTransform;
                    recoilReturnRate = piercerRecoilReturnRate;
                }
                else if (titanLauncherEquipped)
                {
                    gunNewPos = new Vector3(0f, 0f, -0.3f);
                    gunTransformLeft = titanTransform;
                    recoilReturnRate = titanRecoilReturnRate;
                }
                gunTransform = gunTransformLeft;
            }

            gunLerpTime += Time.deltaTime;
            float perc = gunLerpTime / recoilReturnRate;
            //==========================ShootGunBack================================//
            if (gunLerpTime >= recoilReturnRate)
                gunLerpTime = recoilReturnRate;
            //==========================ReturnGunForward================================//
            if (gunTransform.transform.localPosition == gunNewPos)
            {
                gunTransform.transform.localPosition = Vector3.Lerp(gunNewPos, gunCurPos, perc);
                //-------------------TurnOffGun-------------------//
                if (gunTransform.transform.localPosition == gunCurPos)
                {
                    Recoiled = false;
                    gunLerpTime = 0;
                }
            }
            else
            {
                gunTransform.transform.localPosition = Vector3.Lerp(gunCurPos, gunNewPos, perc);
            }
        }
    }
    public void SwitchWeapon(WeaponSelect gun)
    {
        switch (gun)
        {
            case WeaponSelect.Unarmed:
                {
                    weapNum = 0;
                    weaponName.text = "Unarmed";
                    unarmedEquipped = true;
                    blasterEquipped = false;
                    flakShotgunEquipped = false;
                    photonRifleEquipped = false;
                    thePiercerEquipped = false;
                    titanLauncherEquipped = false;
                    hyperBeamEquipped = false;
                    vortexCannonEquipped = false;

                    unarmedWeaponObj.SetActive(true);
                    blastWeaponObj.SetActive(false);
                    flakWeaponObj.SetActive(false);
                    photonWeaponObj.SetActive(false);
                    piercerWeaponObj.SetActive(false);
                    titanWeaponObj.SetActive(false);
                    hyperWeaponObj.SetActive(false);
                    vortexWeaponObj.SetActive(false);

                    crosshairUnarmed.enabled = true;
                    crosshairBlast.enabled = false;
                    crosshairFlak.enabled = false;
                    crosshairPhoton.enabled = false;
                    crosshairPiercer.enabled = false;
                    crosshairTitan.enabled = false;
                    crosshairHyper.enabled = false;
                    crosshairVortex.enabled = false;
                    break;
                }
            case WeaponSelect.Blaster:
                {
                    if (blasterPickedUp)
                    {
                        weapNum = 1;
                        weaponName.text = "Blaster";
                        ProjectileForce = blasterProjForce;
                        unarmedEquipped = false;
                        blasterEquipped = true;
                        flakShotgunEquipped = false;
                        photonRifleEquipped = false;
                        thePiercerEquipped = false;
                        titanLauncherEquipped = false;
                        hyperBeamEquipped = false;
                        vortexCannonEquipped = false;

                        //unarmedWeaponObj.SetActive(false);
                        blastWeaponObj.SetActive(true);
                        flakWeaponObj.SetActive(false);
                        photonWeaponObj.SetActive(false);
                        piercerWeaponObj.SetActive(false);
                        titanWeaponObj.SetActive(false);
                        //hyperWeaponObj.SetActive(false);
                        //vortexWeaponObj.SetActive(false);

                        //crosshairUnarmed.enabled = false;
                        crosshairBlast.enabled = true;
                        crosshairFlak.enabled = false;
                        crosshairPhoton.enabled = false;
                        crosshairPiercer.enabled = false;
                        crosshairTitan.enabled = false;
                        //crosshairHyper.enabled = false;
                        //crosshairVortex.enabled = false;

                        gunEquip = blastEquipSfx;
                        audioSrc.PlayOneShot(gunEquip);
                    }

                    break;
                }
            case WeaponSelect.FlakShotgun:
                {
                    if (flakShotgunPickedUp)
                    {
                        right = true;
                        weapNum = 2;
                        weaponName.text = "Flak Shotgun";
                        unarmedEquipped = false;
                        blasterEquipped = false;
                        flakShotgunEquipped = true;
                        photonRifleEquipped = false;
                        thePiercerEquipped = false;
                        titanLauncherEquipped = false;
                        hyperBeamEquipped = false;
                        vortexCannonEquipped = false;

                        //unarmedWeaponObj.SetActive(false);
                        blastWeaponObj.SetActive(false);
                        flakWeaponObj.SetActive(true);
                        photonWeaponObj.SetActive(false);
                        piercerWeaponObj.SetActive(false);
                        titanWeaponObj.SetActive(false);
                        //hyperWeaponObj.SetActive(false);
                        //vortexWeaponObj.SetActive(false);

                        //crosshairUnarmed.enabled = false;
                        crosshairBlast.enabled = false;
                        crosshairFlak.enabled = true;
                        crosshairPhoton.enabled = false;
                        crosshairPiercer.enabled = false;
                        crosshairTitan.enabled = false;
                        //crosshairHyper.enabled = false;
                        //crosshairVortex.enabled = false;

                        gunEquip = flakEquipSfx;
                        audioSrc.PlayOneShot(gunEquip);
                    }
                    break;
                }
            case WeaponSelect.PhotonRifle:
                {
                    if (photonRiflePickedUp)
                    {
                        SpinSpeed = 20;
                        right = true;
                        weapNum = 3;
                        weaponName.text = "Photon Rifle";
                        ProjectileForce = photonProjForce;

                        unarmedEquipped = false;
                        blasterEquipped = false;
                        flakShotgunEquipped = false;
                        photonRifleEquipped = true;
                        thePiercerEquipped = false;
                        titanLauncherEquipped = false;
                        hyperBeamEquipped = false;
                        vortexCannonEquipped = false;

                        //unarmedWeaponObj.SetActive(false);
                        blastWeaponObj.SetActive(false);
                        flakWeaponObj.SetActive(false);
                        photonWeaponObj.SetActive(true);
                        piercerWeaponObj.SetActive(false);
                        titanWeaponObj.SetActive(false);
                        //hyperWeaponObj.SetActive(false);
                        //vortexWeaponObj.SetActive(false);

                        //crosshairUnarmed.enabled = false;
                        crosshairBlast.enabled = false;
                        crosshairFlak.enabled = false;
                        crosshairPhoton.enabled = true;
                        crosshairPiercer.enabled = false;
                        crosshairTitan.enabled = false;
                        //crosshairHyper.enabled = false;
                        //crosshairVortex.enabled = false;

                        gunEquip = photonEquipSfx;
                        audioSrc.PlayOneShot(gunEquip);
                    }
                    break;
                }
            case WeaponSelect.ThePiercer:
                {
                    if (thePiercerPickedUp)
                    {
                        SpinSpeed = 10;
                        right = true;
                        weapNum = 4;
                        weaponName.text = "The Piercer";
                        ProjectileForce = piercerProjForce;

                        unarmedEquipped = false;
                        blasterEquipped = false;
                        flakShotgunEquipped = false;
                        photonRifleEquipped = false;
                        thePiercerEquipped = true;
                        titanLauncherEquipped = false;
                        hyperBeamEquipped = false;
                        vortexCannonEquipped = false;

                        //unarmedWeaponObj.SetActive(false);
                        blastWeaponObj.SetActive(false);
                        flakWeaponObj.SetActive(false);
                        photonWeaponObj.SetActive(false);
                        piercerWeaponObj.SetActive(true);
                        titanWeaponObj.SetActive(false);
                        //hyperWeaponObj.SetActive(false);
                        //vortexWeaponObj.SetActive(false);

                        //crosshairUnarmed.enabled = false;
                        crosshairBlast.enabled = false;
                        crosshairFlak.enabled = false;
                        crosshairPhoton.enabled = false;
                        crosshairPiercer.enabled = true;
                        crosshairTitan.enabled = false;
                        //crosshairHyper.enabled = false;
                        //crosshairVortex.enabled = false;

                        gunEquip = piercerEquipSfx;
                        audioSrc.PlayOneShot(gunEquip);
                    }
                    break;
                }
            case WeaponSelect.TitanLauncher:
                {
                    if (titanLauncherPickedUp)
                    {
                        SpinSpeed = 200;
                        right = true;
                        weapNum = 5;
                        weaponName.text = "Titan Launcher";
                        ProjectileForce = titanProjForce;

                        unarmedEquipped = false;
                        blasterEquipped = false;
                        flakShotgunEquipped = false;
                        photonRifleEquipped = false;
                        thePiercerEquipped = false;
                        titanLauncherEquipped = true;
                        hyperBeamEquipped = false;
                        vortexCannonEquipped = false;

                        //unarmedWeaponObj.SetActive(false);
                        blastWeaponObj.SetActive(false);
                        flakWeaponObj.SetActive(false);
                        photonWeaponObj.SetActive(false);
                        piercerWeaponObj.SetActive(false);
                        titanWeaponObj.SetActive(true);
                        //hyperWeaponObj.SetActive(false);
                        //vortexWeaponObj.SetActive(false);

                        //crosshairUnarmed.enabled = false;
                        crosshairBlast.enabled = false;
                        crosshairFlak.enabled = false;
                        crosshairPhoton.enabled = false;
                        crosshairPiercer.enabled = false;
                        crosshairTitan.enabled = true;
                        //crosshairHyper.enabled = false;
                        //crosshairVortex.enabled = false;

                        gunEquip = titanEquipSfx;
                        audioSrc.PlayOneShot(gunEquip);
                    }
                    break;
                }
            case WeaponSelect.HyperBeam:
                {
                    if (hyperBeamPickedUp)
                    {
                        right = true;
                        weapNum = 6;
                        weaponName.text = "Hyper Beam";
                        ProjectileForce = hyperProjForce;
                        recoilReturnRate = hyperRecoilReturnRate;

                        unarmedEquipped = false;
                        blasterEquipped = false;
                        flakShotgunEquipped = false;
                        photonRifleEquipped = false;
                        thePiercerEquipped = false;
                        titanLauncherEquipped = false;
                        hyperBeamEquipped = true;
                        vortexCannonEquipped = false;

                        //unarmedWeaponObj.SetActive(false);
                        blastWeaponObj.SetActive(false);
                        flakWeaponObj.SetActive(false);
                        photonWeaponObj.SetActive(false);
                        piercerWeaponObj.SetActive(false);
                        titanWeaponObj.SetActive(false);
                        //hyperWeaponObj.SetActive(true);
                        //vortexWeaponObj.SetActive(false);

                        //crosshairUnarmed.enabled = false;
                        crosshairBlast.enabled = false;
                        crosshairFlak.enabled = false;
                        crosshairPhoton.enabled = false;
                        crosshairPiercer.enabled = false;
                        //crosshairTitan.enabled = false;
                        crosshairHyper.enabled = true;
                        //crosshairVortex.enabled = false;

                        gunEquip = hyperEquipSfx;
                        audioSrc.PlayOneShot(gunEquip);
                    }
                    break;
                }
            case WeaponSelect.VortexCannon:
                {
                    if (vortexCannonPickedUp)
                    {
                        right = true;
                        weapNum = 7;
                        weaponName.text = "Vortex Cannon";
                        ProjectileForce = vortexProjForce;
                        recoilReturnRate = vortexRecoilReturnRate;

                        unarmedEquipped = false;
                        blasterEquipped = false;
                        flakShotgunEquipped = false;
                        photonRifleEquipped = false;
                        thePiercerEquipped = false;
                        titanLauncherEquipped = false;
                        hyperBeamEquipped = false;
                        vortexCannonEquipped = true;

                        unarmedWeaponObj.SetActive(false);
                        blastWeaponObj.SetActive(false);
                        flakWeaponObj.SetActive(false);
                        photonWeaponObj.SetActive(false);
                        piercerWeaponObj.SetActive(false);
                        titanWeaponObj.SetActive(false);
                        hyperWeaponObj.SetActive(false);
                        vortexWeaponObj.SetActive(true);

                        crosshairUnarmed.enabled = false;
                        crosshairBlast.enabled = false;
                        crosshairFlak.enabled = false;
                        crosshairPhoton.enabled = false;
                        crosshairPiercer.enabled = false;
                        crosshairTitan.enabled = false;
                        crosshairHyper.enabled = false;
                        crosshairVortex.enabled = true;

                        gunEquip = vortexEquipSfx;
                        audioSrc.PlayOneShot(gunEquip);
                    }
                    break;
                }
        }
        SetCountAmmo();
    }
    public void SelectWeapon(int num)
    {
        if (num == 0)
            anim.SetTrigger("SelectUnarmed");
        else if (num == 1)
            anim.SetTrigger("SelectBlaster");
        else if (num == 2)
            anim.SetTrigger("SelectFlak");
        else if (num == 3)
            anim.SetTrigger("SelectPhoton");
        else if (num == 4)
            anim.SetTrigger("SelectPiercer");
        else if (num == 5)
            anim.SetTrigger("SelectTitan");
        else if (num == 6)
            anim.SetTrigger("SelectHyper");
        else if (num == 7)
            anim.SetTrigger("SelectVortex");
    }
    public enum GunAmmo {Blaster, FlakShotgun, ProtonRifle, ThePiercer, TitanLauncher, HyperBeam, VortexCannon}
    public void GainAmmo(GunAmmo type, int amount)
    {
        switch (type)
        {
            case GunAmmo.Blaster:
                {
                    audioSrc.PlayOneShot(noClip);
                    blasterAmmo += amount;
                    if (blasterAmmo >= blastMaxAmmo)
                        blasterAmmo = blastMaxAmmo;
                    SetCountAmmo();
                    break;
                }
            case GunAmmo.FlakShotgun:
                {
                    audioSrc.PlayOneShot(noClip);
                    flakShotgunAmmo += amount;
                    if (flakShotgunAmmo >= flakMaxAmmo)
                        flakShotgunAmmo = flakMaxAmmo;
                    SetCountAmmo();
                    break;
                }
            case GunAmmo.ProtonRifle:
                {
                    audioSrc.PlayOneShot(noClip);
                    photonRifleAmmo += amount;
                    if (photonRifleAmmo >= photonMaxAmmo)
                        photonRifleAmmo = photonMaxAmmo;
                    SetCountAmmo();
                    break;
                }
            case GunAmmo.ThePiercer:
                {
                    audioSrc.PlayOneShot(noClip);
                    thePiercerAmmo += amount;
                    if (thePiercerAmmo >= piercerMaxAmmo)
                        thePiercerAmmo = piercerMaxAmmo;
                    SetCountAmmo();
                    break;
                }
            case GunAmmo.TitanLauncher:
                {
                    GotAmmo = true;
                    audioSrc.PlayOneShot(noClip);
                    titanLauncherAmmo += amount;
                    if (titanLauncherAmmo >= titanMaxAmmo)
                        titanLauncherAmmo = titanMaxAmmo;
                    SetCountAmmo();
                    break;
                }
        }


    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FlakGun"))
        {
            flakShotgunPickedUp = true;
            GainAmmo(GunAmmo.FlakShotgun, 30);
            anim.SetTrigger("SelectFlak");
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("PhotonGun"))
        {
            photonRiflePickedUp = true;
            GainAmmo(GunAmmo.ProtonRifle, 100);
            anim.SetTrigger("SelectPhoton");
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("PiercerGun"))
        {
            thePiercerPickedUp = true;
            GainAmmo(GunAmmo.ThePiercer, 250);
            anim.SetTrigger("SelectPiercer");
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("TitanGun"))
        {
            titanLauncherPickedUp = true;
            GainAmmo(GunAmmo.TitanLauncher, 15);
            anim.SetTrigger("SelectTitan");
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("BlasterAmmo"))
        {
            audioSrc.PlayOneShot(noClip);
            GainAmmo(GunAmmo.Blaster, 50);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("FlakAmmo"))
        {
            audioSrc.PlayOneShot(noClip);
            GainAmmo(GunAmmo.FlakShotgun, 15);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("PhotonAmmo"))
        {
            audioSrc.PlayOneShot(noClip);
            GainAmmo(GunAmmo.ProtonRifle, 50);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("PiercerAmmo"))
        {
            audioSrc.PlayOneShot(noClip);
            GainAmmo(GunAmmo.ThePiercer, 100);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("TitanAmmo"))
        {
            audioSrc.PlayOneShot(noClip);
            GainAmmo(GunAmmo.TitanLauncher, 5);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("HyperAmmo"))
        {
            audioSrc.PlayOneShot(noClip);
            GainAmmo(GunAmmo.HyperBeam, 10);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("VortexAmmo"))
        {
            audioSrc.PlayOneShot(noClip);
            GainAmmo(GunAmmo.VortexCannon, 10);
            Destroy(other.gameObject);
        }
        SetCountAmmo();
    }
    public void ChangingWeapon(int num)
    {
        if (num == 1)
            changeWeap = true;
        else
            changeWeap = false;
    }
    public void SlowMotion()
    {
        returnTime = false;
        AudioSource musicSrc = GameObject.Find("Master/Audio/MainGame").GetComponent<AudioSource>();
        Time.timeScale -= slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0.2f, 1);
        musicSrc.pitch = Time.timeScale;
    }
    public enum WeaponEquipped { Unarmed, Blaster, FlakShotgun, PhotonRifle, ThePiercer, TitanLauncher, HyperBeam, VortexCannon }
    public void EquipWeapState(WeaponEquipped state)
    {
        switch (state)
        {
            case WeaponEquipped.Unarmed:
                {
                    break;
                }
            case WeaponEquipped.Blaster:
                {
                    if (Time.time > nextFire)
                    {
                        fireRate = blastFireRate;
                        if (blasterAmmo > 0)
                        {
                            Recoiled = true;
                            Projectile = blastPriProjectile;
                            shoot = blastPriShot;
                            blasterAmmo -= 1;
                            if (right)
                                right = false;
                            else
                                right = true;
                        }
                        recoilComponent.StartRecoil(0.1f, Random.Range(-2f, -4f), Random.Range(5, 10f));
                        muzzleFlashEnabled = true;
                        SetCountAmmo();
                        audioSrc.PlayOneShot(shoot);
                        Shot();
                        nextFire = Time.time + fireRate;
                    }
                    break;
                }
            case WeaponEquipped.FlakShotgun:
                {
                    if (Time.time > nextFire)
                    {
                        fireRate = flakFireRate;
                        if (flakShotgunAmmo > 0)
                        {
                            Recoiled = true;
                            Projectile = flakPriProjectile;
                            shoot = flakPriShot;
                            flakShotgunAmmo -= 1;
                        }
                        recoilComponent.StartRecoil(0.1f, Random.Range(-5f, -10f), Random.Range(9, 15f));
                        muzzleFlashEnabled = true;
                        SetCountAmmo();
                        audioSrc.PlayOneShot(shoot);
                        Shot();
                        nextFire = Time.time + fireRate;
                    }
                    break;
                }
            case WeaponEquipped.PhotonRifle:
                {
                    SpinUpTimer = Mathf.Clamp(SpinUpTimer + Time.deltaTime, 0, SpinUpTime);
                    if (Time.time > nextFire && SpinUpTimer >= SpinUpTime)
                    {
                        Projectile = photonPriProjectile;
                        fireRate = photonFireRate;
                        Recoiled = true;
                        shoot = photonPriShot;
                        photonRifleAmmo -= 1;
                        if (right)
                            right = false;
                        else
                            right = true;
                        recoilComponent.StartRecoil(0.1f, Random.Range(-2f, -4f), Random.Range(5, 10f));
                        muzzleFlashEnabled = true;
                        SetCountAmmo();
                        audioSrc.PlayOneShot(shoot);
                        Shot();
                        nextFire = Time.time + fireRate;
                    }
                    return;
                }

            case WeaponEquipped.ThePiercer:
                {
                    AudioSource gunAudioSrc = CenterCannon.GetComponent<AudioSource>();
                    if (startSpin)
                    {
                        gunAudioSrc.clip = spinUp;
                        gunAudioSrc.Play();
                        gunAudioSrc.loop = false;
                        startSpin = false;
                        stopSpin = true;
                    }
                    SpinUpTimer = Mathf.Clamp(SpinUpTimer + Time.deltaTime, 0, SpinUpTime);
                    if (Time.time > nextFire && SpinUpTimer >= SpinUpTime)
                    {
                        gunAudioSrc.clip = spinning;
                        gunAudioSrc.Play();
                        gunAudioSrc.loop = true;
                        ActivePiercerBullet(strength, true);
                        Projectile = piercerPriProjectile;
                        fireRate = piercerFireRate;
                        Recoiled = true;
                        shoot = piercerPriShot;
                        thePiercerAmmo -= 1;
                        if (right)
                            right = false;
                        else
                            right = true;
                        recoilComponent.StartRecoil(0.1f, Random.Range(-2f, -4f), Random.Range(5, 10f));
                        muzzleFlashEnabled = true;
                        SetCountAmmo();
                        audioSrc.PlayOneShot(shoot);
                        Shot();
                        nextFire = Time.time + fireRate;
                    }
                    break;
                }
            case WeaponEquipped.TitanLauncher:
                {
                    if (Time.time > nextFire)
                    {
                        if (barrelReload)
                        {
                            AudioSource gunAudioSrc = titanBarrel.GetComponent<AudioSource>();
                            gunAudioSrc.PlayOneShot(titanReload);
                            barrelReload = false;
                        }
                        BarrelSpin = true;
                        fireRate = titanFireRate;
                        if (titanLauncherAmmo > 0)
                        {
                            Recoiled = true;
                            loadingRocket = true;
                            Projectile = titanPriProjectile;
                            shoot = titanPriShot;
                            titanLauncherAmmo -= 1;
                        }
                        recoilComponent.StartRecoil(0.3f, Random.Range(-5f, -10f), Random.Range(9, 15f));
                        muzzleFlashEnabled = true;
                        SetCountAmmo();
                        audioSrc.PlayOneShot(shoot);
                        Shot();
                        nextFire = Time.time + fireRate;
                    }
                    break;
                }
            case WeaponEquipped.HyperBeam:
                {
                    if (Time.time > nextFire)
                    {
                        fireRate = hyperFireRate;
                        if (hyperBeamAmmo > 0)
                        {
                            Recoiled = true;
                            Projectile = hyperPriProjectile;
                            shoot = hyperPriShot;
                            hyperBeamAmmo -= 1;
                        }
                        recoilComponent.StartRecoil(0.1f, Random.Range(-5f, -10f), Random.Range(9, 15f));
                        muzzleFlashEnabled = true;
                        SetCountAmmo();
                        audioSrc.PlayOneShot(shoot);
                        Shot();
                        nextFire = Time.time + fireRate;
                    }
                    break;
                }
            case WeaponEquipped.VortexCannon:
                {
                    if (Time.time > nextFire)
                    {
                        fireRate = vortexFireRate;
                        if (vortexCannonAmmo > 0)
                        {
                            Recoiled = true;
                            Projectile = vortexPriProjectile;
                            shoot = vortextPriShot;
                            vortexCannonAmmo -= 1;
                        }
                        recoilComponent.StartRecoil(0.1f, Random.Range(-5f, -10f), Random.Range(9, 15f));
                        muzzleFlashEnabled = true;
                        SetCountAmmo();
                        audioSrc.PlayOneShot(shoot);
                        Shot();
                        
                        nextFire = Time.time + fireRate;
                    }
                    break;
                }
        }
    }
    void ActiveRocketBarrel()
    {
        Axisz = titanBarrel.transform.rotation.eulerAngles.z;
        if (GotAmmo)
        {
            rocket.SetActive(true);
            SpinSpeed = 200;
            float theta = Time.deltaTime * SpinSpeed;
            titanBarrel.transform.Rotate(Vector3.forward, theta);
            if (ammo > 0)
            {
                if (BarrelCounter == 0)
                {
                    if (BarrelSpin)
                    {

                        titanBarrel.transform.Rotate(Vector3.forward, theta);
                        Mathf.Clamp(Axisz, 120f, 120f);
                    }
                    if (Axisz > 117f)
                    {
                        GotAmmo = false;
                        BarrelSpin = false;

                        BarrelCounter = 1;
                    }
                }
                else if (BarrelCounter == 1)
                {
                    if (BarrelSpin)
                    {
                        titanBarrel.transform.Rotate(Vector3.forward, theta);
                        Mathf.Clamp(Axisz, 240f, 240f);
                    }
                    if (Axisz > 237f)
                    {
                        GotAmmo = false;
                        BarrelSpin = false;
                        
                        BarrelCounter = 2;
                    }
                }
                else if (BarrelCounter == 2)
                {
                    if (BarrelSpin)
                    {
                        titanBarrel.transform.Rotate(Vector3.forward, theta);
                        Mathf.Clamp(Axisz, 0f, 0f);
                    }
                    if (Axisz > 0f && Axisz < 6f)
                    {
                        GotAmmo = false;
                        BarrelSpin = false;
                        BarrelCounter = 0;
                    }
                }

            }

        }
        if (!GotAmmo)
        {
            if (ammo > 0)
            {
                if (BarrelCounter == 0)
                {
                    if (BarrelSpin)
                    {
                        float theta = Time.deltaTime * SpinSpeed;
                        titanBarrel.transform.Rotate(Vector3.forward, theta);
                        Mathf.Clamp(Axisz, 120, 120);
                    }
                    if (Axisz > 117f)
                    {
                        BarrelSpin = false;
                        BarrelCounter = 1;
                    }
                }
                else if (BarrelCounter == 1)
                {
                    if (BarrelSpin)
                    {

                        float theta = Time.deltaTime * SpinSpeed;
                        titanBarrel.transform.Rotate(Vector3.forward, theta);
                        Mathf.Clamp(Axisz, 240, 240);
                    }
                    if (Axisz > 237f)
                    {
                        BarrelSpin = false;
                        BarrelCounter = 2;
                    }
                }
                else if (BarrelCounter == 2)
                {
                    if (BarrelSpin)
                    {

                        float theta = Time.deltaTime * SpinSpeed;
                        titanBarrel.transform.Rotate(Vector3.forward, theta);
                        Mathf.Clamp(Axisz, 0, 0);
                    }
                    if (Axisz > 0f && Axisz < 6f)
                    {
                        BarrelSpin = false;
                        BarrelCounter = 0;
                    }
                }
            }
        }
        if (BarrelSpin && ammo <= 0)
        {
            rocket.SetActive(false);
            SpinSpeed = 50;
            float theta = Time.deltaTime * SpinSpeed;
            titanBarrel.transform.Rotate(Vector3.forward, theta);
        }
        if (loadingRocket)
        {
            rocketTransform = rocket.transform;
            rocketNewPos = new Vector3(0f, 0.09f, 0.2f);
            rocketLoadPos = new Vector3(0, 0, 0);
            rocketLerpTime += Time.deltaTime;
            float perc = rocketLerpTime / rocketReturnRate;
            if (rocketLerpTime >= rocketReturnRate)
                rocketLerpTime = rocketReturnRate;
            if (rocketTransform.transform.localPosition == rocketNewPos)
            {
                rocketTransform.transform.localPosition = Vector3.Lerp(rocketNewPos, rocketLoadPos, perc);
                if (rocketTransform.transform.localPosition == rocketLoadPos)
                {
                    rocketTransform.transform.localPosition = Vector3.Lerp(rocketLoadPos, rocketCurPos, perc);
                    if (rocketTransform.transform.localPosition == rocketCurPos)
                    {
                        loadingRocket = false;
                        rocketLerpTime = 0;
                    }
                }
            }
            else
                rocketTransform.transform.localPosition = Vector3.Lerp(rocketCurPos, rocketNewPos, perc);


        }
    }
    void ActivePiercerBullet(float Strength, bool True)
    {
       
        Transform emitTrans = piercerEmitter.transform;
        if (True)
        {
            emitTrans.localPosition = originalPos + Random.insideUnitSphere * Strength;
        }
        else
        {
            emitTrans.localPosition = originalPos;
        }
    }
    void ActiveFlakBullet()
    {
        Rigidbody Temporary_RigidBody0;
        GameObject Temporary_Bullet_Handler0;
        Temporary_Bullet_Handler0 = Instantiate(Projectile, flakEmitters[0].transform.position, flakEmitters[0].transform.rotation) as GameObject;
        Temporary_Bullet_Handler0.transform.Rotate(0, 0, 0);
        Temporary_RigidBody0 = Temporary_Bullet_Handler0.GetComponent<Rigidbody>();
        Temporary_RigidBody0.AddForce(flakEmitters[0].transform.forward * Random.Range(4000, 5500));
        Temporary_RigidBody0.AddForce(flakEmitters[0].transform.right * Random.Range(-500, 500));
        Temporary_RigidBody0.AddForce(flakEmitters[0].transform.up * Random.Range(-500, 500));
        Destroy(Temporary_Bullet_Handler0, 5.0f);

        Rigidbody Temporary_RigidBody1;
        GameObject Temporary_Bullet_Handler1;
        Temporary_Bullet_Handler1 = Instantiate(Projectile, flakEmitters[1].transform.position, flakEmitters[1].transform.rotation) as GameObject;
        Temporary_Bullet_Handler1.transform.Rotate(0, 0, 0);
        Temporary_RigidBody1 = Temporary_Bullet_Handler1.GetComponent<Rigidbody>();
        Temporary_RigidBody1.AddForce(flakEmitters[1].transform.forward * Random.Range(4000, 5500));
        Temporary_RigidBody1.AddForce(flakEmitters[1].transform.right * Random.Range(-500, 500));
        Temporary_RigidBody1.AddForce(flakEmitters[1].transform.up * Random.Range(-500, 500));
        Destroy(Temporary_Bullet_Handler1, 5.0f);

        Rigidbody Temporary_RigidBody2;
        GameObject Temporary_Bullet_Handler2;
        Temporary_Bullet_Handler2 = Instantiate(Projectile, flakEmitters[2].transform.position, flakEmitters[2].transform.rotation) as GameObject;
        Temporary_Bullet_Handler2.transform.Rotate(0, 0, 0);
        Temporary_RigidBody2 = Temporary_Bullet_Handler2.GetComponent<Rigidbody>();
        Temporary_RigidBody2.AddForce(flakEmitters[2].transform.forward * Random.Range(4000, 5500));
        Temporary_RigidBody2.AddForce(flakEmitters[2].transform.right * Random.Range(-500, 500));
        Temporary_RigidBody2.AddForce(flakEmitters[2].transform.up * Random.Range(-500, 500));
        Destroy(Temporary_Bullet_Handler2, 5.0f);

        Rigidbody Temporary_RigidBody3;
        GameObject Temporary_Bullet_Handler3;
        Temporary_Bullet_Handler3 = Instantiate(Projectile, flakEmitters[3].transform.position, flakEmitters[3].transform.rotation) as GameObject;
        Temporary_Bullet_Handler3.transform.Rotate(0, 0, 0);
        Temporary_RigidBody3 = Temporary_Bullet_Handler3.GetComponent<Rigidbody>();
        Temporary_RigidBody3.AddForce(flakEmitters[3].transform.forward * Random.Range(4000, 5500));
        Temporary_RigidBody3.AddForce(flakEmitters[3].transform.right * Random.Range(-500, 500));
        Temporary_RigidBody3.AddForce(flakEmitters[3].transform.up * Random.Range(-500, 500));
        Destroy(Temporary_Bullet_Handler3, 5.0f);

        Rigidbody Temporary_RigidBody4;
        GameObject Temporary_Bullet_Handler4;
        Temporary_Bullet_Handler4 = Instantiate(Projectile, flakEmitters[4].transform.position, flakEmitters[4].transform.rotation) as GameObject;
        Temporary_Bullet_Handler4.transform.Rotate(0, 0, 0);
        Temporary_RigidBody4 = Temporary_Bullet_Handler4.GetComponent<Rigidbody>();
        Temporary_RigidBody4.AddForce(flakEmitters[4].transform.forward * Random.Range(4000, 5500));
        Temporary_RigidBody4.AddForce(flakEmitters[4].transform.right * Random.Range(-500, 500));
        Temporary_RigidBody4.AddForce(flakEmitters[4].transform.up * Random.Range(-500, 500));
        Destroy(Temporary_Bullet_Handler4, 5.0f);

        Rigidbody Temporary_RigidBody5;
        GameObject Temporary_Bullet_Handler5;
        Temporary_Bullet_Handler5 = Instantiate(Projectile, flakEmitters[5].transform.position, flakEmitters[5].transform.rotation) as GameObject;
        Temporary_Bullet_Handler5.transform.Rotate(0, 0, 0);
        Temporary_RigidBody5 = Temporary_Bullet_Handler5.GetComponent<Rigidbody>();
        Temporary_RigidBody5.AddForce(flakEmitters[5].transform.forward * Random.Range(4000, 5500));
        Temporary_RigidBody5.AddForce(flakEmitters[5].transform.right * Random.Range(-500, 500));
        Temporary_RigidBody5.AddForce(flakEmitters[5].transform.up * Random.Range(-500, 500));
        Destroy(Temporary_Bullet_Handler5, 5.0f);
    }
    public void SwitchPlayer(PlayerSelect number)
    {
        switch (number)
        {
            case PlayerSelect.PlayerOne:
                {
                    MouseEnabled = true;
                    Fire = Input.GetAxisRaw("Fire");
                    select = Input.GetAxisRaw("Select");
                    break;
                }
            case PlayerSelect.PlayerTwo:
                {
                    Fire = Input.GetAxisRaw("Fire2");
                    select = Input.GetAxisRaw("Select2");
                    break;
                }
            case PlayerSelect.PlayerThree:
                {
                    Fire = Input.GetAxisRaw("Fire3");
                    select = Input.GetAxisRaw("Select3");
                    break;
                }
            case PlayerSelect.PlayerFour:
                {
                    Fire = Input.GetAxisRaw("Fire4");
                    select = Input.GetAxisRaw("Select4");
                    break;
                }
        }
    }
}




