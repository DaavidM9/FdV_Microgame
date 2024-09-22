using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static int SCORE = 0;
    public float thrustForce = 100f;
    public float rotationSpeed = 120f;

    public GameObject gun, bulletPrefab;
    private Rigidbody _rigid;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float rotation = Input.GetAxis("Rotate") * Time.deltaTime;
        float thrust = Input.GetAxis("Thrust") * Time.deltaTime; 
        Vector3 thrustDirection = transform.right;
       

        _rigid.AddForce(thrustDirection * thrust * thrustForce);
        transform.Rotate(Vector3.forward, -rotation * rotationSpeed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);
            Bullet balaScript = bullet.GetComponent<Bullet>();
            balaScript.targetVector = transform.right;
        }
    }

    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SCORE = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Lo mismo que "SampleScene"
        }
       
    }   
}
