using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject bomba;
    [SerializeField] GameObject player;
    [SerializeField] GameObject contador;
    [SerializeField] GameObject sanguche;
    public GameObject[] CosasARotar;
    [Tooltip("Lista de los objetos que van a ser rotados con el Gyro")]


    public Playercontroller Playercontroller;

    private int Diametro = 5;
    private float spawnratio = 1f;
    private int tiempo = 0;
    private float speedR = 25f;



    void Start()
    {
        StartCoroutine(Minar());
        StartCoroutine(Sanguchear());
        StartCoroutine(Contador());
    }
    void Update()
    {
        Gyro();
    }
    public IEnumerator Minar()
    {
        if (Playercontroller.Perder == false)
        {
            Instantiate(bomba, transform);
            bomba.transform.localScale = Vector3.zero;
            bomba.transform.position = Posicion();
            bomba.transform.Rotate(0, 0, Random.Range(0, 360));
            bomba.transform.DOScale(Vector3.one / (Random.Range(2, 4)), 0.2f);

            yield return new WaitForSeconds(spawnratio);
            spawnratio -= 0.01f;

            StartCoroutine(Minar());
        }
    }

    public IEnumerator Sanguchear()
    {
        if (Playercontroller.Perder == false)
        {
            Instantiate(sanguche, transform);
            sanguche.transform.position = Posicion();
            sanguche.transform.Rotate(0, 0, Random.Range(0, 360));

            yield return new WaitForSeconds(2);

            StartCoroutine(Sanguchear());
        }
    }

    Vector3 Posicion()
    {
        Vector3 Pos;
        Vector3 Posicion;
        int Magnitud = 15;

        //Inicializo el vector con la posicion del Player
        Pos = player.transform.position;

        //Inicializo el vector para que me de un punto aleatorio (A modificar)
        Posicion = new Vector3(Random.Range(-Diametro, Diametro), Random.Range(-Diametro, Diametro), 2);

        //Hago que el Vector Posicion este normalizado para que apunte en una direccion con longitud 1
        Posicion = Posicion.normalized * Magnitud;

        //Multiplico al vector unitario x magnitud para poder sacarlo de la vista del jugador
        //Y lo sumo a la posicion del jugador para generarlo entre los radios deseados 
        Pos = Pos + Posicion;
        Pos.z = 2;
        return Pos;
    }
    IEnumerator Contador()
    {
        if (Playercontroller.Perder == false)
        {
            tiempo += 1;
            contador.GetComponent<TextMeshProUGUI>().text = "Tiempo Vivo: " + tiempo.ToString();
            yield return new WaitForSeconds(1.0f);
            StartCoroutine(Contador());
        }
    }

    public void Pausa()
    {
        SceneManager.LoadScene("GameLoop");
    }

    public void Gyro()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            foreach (GameObject objet in CosasARotar)
            {
                //Debug.Log(Input.gyro.attitude.z);
                objet.transform.DORotate((Vector3.back * speedR) + objet.transform.rotation.eulerAngles, 1.0f);
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            foreach (GameObject objet in CosasARotar)
            {
                //Debug.Log(Input.gyro.attitude.z);
                objet.transform.DORotate((Vector3.forward * speedR) + objet.transform.rotation.eulerAngles, 1.0f);
            }
        }
    }
    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
    public void BotonI()
    {
        foreach (GameObject objet in CosasARotar)
        {
            //Debug.Log(Input.gyro.attitude.z);
            objet.transform.DORotate((Vector3.back * speedR) + objet.transform.rotation.eulerAngles, 1.0f);
        }
    }
    public void BotonD()
    {
        foreach (GameObject objet in CosasARotar)
        {
            //Debug.Log(Input.gyro.attitude.z);
            objet.transform.DORotate((Vector3.forward * speedR) + objet.transform.rotation.eulerAngles, 1.0f);
        }
    }

}
