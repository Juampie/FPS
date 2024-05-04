using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab; // ������ ����
    public Transform firePoint; // ����� ��������
    public Camera mainCamera; // ������ �� �������� ������
    public float bulletSpeed = 50f; // �������� ����
    public int maxAmmo = 12; // ������������ ���������� ��������
    public float reloadTime = 1f; // ����� �����������
    public float fireRate = 0.1f; // ������� ��������

    private int currentAmmo; // ������� ���������� �������� � ��������
    private int remainingAmmo; // ���������� ���������� ��������
    private bool isReloading = false; // ���� �����������
    private bool canShoot = true; // ����� �� ��������
    private Recoil recoilController; // ������ �� ��������� ���������� �������
    private Animation reloadAnimation; // �������� �����������
    private AudioSource audio; // ���� ��������

    public Text ammoText; // ��������� ���� ��� ����������� �������� ���������� ��������
    public Text remainingAmmoText; // ��������� ���� ��� ����������� ����������� ���������� ��������
    public ParticleSystem muzzleFlash; // ������ ����

    void Start()
    {
        currentAmmo = maxAmmo; // ������������� ��������� ���������� ��������
        remainingAmmo = maxAmmo * 5; // ������������� ���������� ���������� ��������
        UpdateAmmoUI(); // ��������� UI

        recoilController = GetComponentInChildren<Recoil>(); // �������� ��������� ���������� �������
        reloadAnimation = GetComponent<Animation>(); // �������� ��������� �������� �����������
        audio = GetComponent<AudioSource>();

        if (recoilController == null)
        {
            Debug.LogError("Recoil component not found in children."); // ������� ������, ���� ��������� ���������� ������� �� ������
        }

        if (mainCamera == null)
        {
            mainCamera = Camera.main; // ���� ������ �� �������� ������ �� �����������, �������� ����� �� �������������
            if (mainCamera == null)
            {
                Debug.LogError("Main camera not found. Please assign the main camera in the inspector."); // ������� ������, ���� �������� ������ �� �������
            }
        }
    }

    void Update()
    {
        if (isReloading)
            return;

        if (currentAmmo <= 0 && remainingAmmo != 0)
        {
            StartCoroutine(Reload()); // ��������� �����������, ���� ����������� ������� � �������� � ���� ��������
            return;
        }

        if (canShoot && Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(ShootWithDelay()); // ��������� �������� � ��������� ����� ����������
        }

        if (Input.GetKeyDown(KeyCode.R) && remainingAmmo != 0)
        {
            StartCoroutine(Reload()); // ��������� ����������� ��� ������� �� ������ R, ���� ���� �������� �������
        }

        // ��������� ����� ������ � ���������� firePoint � ��� �����
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            firePoint.LookAt(hit.point);
        }
        else
        {
            firePoint.LookAt(ray.GetPoint(1000)); // ���� ��� �� ������� �������, ���������� firePoint �� ���������� 1000 ������
        }
    }

    // ������� ��������
    void Shoot()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--; // ��������� ���������� �������� � ��������
            muzzleFlash.Play(); // ������������� ������ ��������
            audio.Play(); // ������������� ���� ��������

            // ������� ���� � ������ �� ��������
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.velocity = firePoint.forward * bulletSpeed;
            }

            recoilController.ApplyRecoil(); // ��������� ������
            UpdateAmmoUI(); // ��������� UI
        }
        else
        {
            Debug.Log("Out of ammo!"); // ������� ���������, ���� ����������� ������� � ��������
        }
    }



    // �������� ��� �����������
    IEnumerator Reload()
    {
        reloadAnimation.Play(); // ������������� �������� �����������
        isReloading = true; // ������������� ���� �����������

        Debug.Log("Reloading..."); // ������� ��������� � �����������

        yield return new WaitForSeconds(reloadTime); // ���� ����� �����������

        // ��������� ��������� ���������� �������� ��� �����������
        int availableAmmo = maxAmmo - currentAmmo;
        // ���������� � �������� ���������� �������� ������� �� ���������� � ��������� ��� �����������
        currentAmmo += Mathf.Min(remainingAmmo, availableAmmo);
        // ��������� ���������� ������� �� ���������� �������������� ��� �����������
        remainingAmmo = Mathf.Max(0, remainingAmmo - availableAmmo);

        isReloading = false; // ������� ���� �����������
        UpdateAmmoUI(); // ��������� UI
    }

    // ���������� ���������� � �������� �� UI
    void UpdateAmmoUI()
    {
        ammoText.text = currentAmmo.ToString(); // ��������� ���������� �������� � ��������
        remainingAmmoText.text = remainingAmmo.ToString(); // ��������� ���������� ���������� ��������
    }

    // �������� ��� �������� � ���������
    IEnumerator ShootWithDelay()
    {
        canShoot = false; // ������������� ����, ����������� ��������
        Shoot(); // ��������� ��������

        yield return new WaitForSeconds(fireRate); // ���� �������� ����� ����� ����������

        canShoot = true; // ��������� ����� ��������
    }
}
