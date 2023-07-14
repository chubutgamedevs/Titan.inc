using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Playercontroller : MonoBehaviour
{
    private Vector3 Gravedad = Vector2.down;
    public float Speed = 2;
    [Space(15)]
    [SerializeField] GameObject Fondo;
    [SerializeField] GameObject ButtonReiniciar;
    public GameObject Score;
    [Space(15)]
    public bool Perder = false;
    private float m_Saturation;
    private float m_Value;
    public float alpha;
    private Color color = new Color(0.1F, 0.1F, 0.1F, 1F);
    private SpriteRenderer opacidad;
    private int Puntaje = 0;
    [SerializeField] GameObject Implosion;

    void Start()
    {

        m_Saturation = 0.0f;
        m_Value = 1.0f;

        opacidad = Fondo.GetComponent<SpriteRenderer>();
        StartCoroutine(Contador());
    }
    void Update()
    {
        transform.Translate(Gravedad * Speed * Time.deltaTime);
        Radar();
    }

    private void HSVPanel()
    {
        color = Color.HSVToRGB(224f / 360f, m_Saturation, m_Value, false);
        color.a = alpha;
        opacidad.color = color;
        if (Perder == true)
        {
            opacidad.DOColor(Color.black, 0.5f);
        }
    }

    public void Colliders(Collider2D other)
    {
        if (other.CompareTag("mina"))
        {
            Gravedad = Vector2.zero;
            Perder = true;
            ButtonReiniciar.SetActive(true);
            Implosion.SetActive(true);
            transform.DOShakePosition(0.3f, new Vector3(1, 0, 0), 9, 40);
            this.GetComponent<SpriteRenderer>().sprite = null;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("sanguche"))
        {
            Puntaje += 1;
            Score.GetComponent<TextMeshProUGUI>().text = "Sanguchitos: " + Puntaje.ToString();
            other.transform.DOScale(Vector3.zero, 0.3f);
            other.transform.DOLocalRotate(new Vector3(0, 0, 360), 0.3f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
        }

    }

    IEnumerator Contador()
    {
        if (Perder == !true)
        {
            Speed += 0.02f;
            m_Saturation += 0.005f;
            m_Value -= 0.005f;
            alpha += 0.005f;
            // Color.Lerp(Color.white, Color.black, 0.5f);
            yield return new WaitForSeconds(0.5f);
            HSVPanel();
            StartCoroutine(Contador());
        }
        else
        {
            HSVPanel();
        }

    }
    void Radar()
    {
        GameObject[] bombacercana = GameObject.FindGameObjectsWithTag("mina");
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        Vector3[] positions = { transform.position, FindClosestEnemy().transform.position};
        lineRenderer.SetPositions(positions);
    }
    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;                                       //Array que va a tener a las minas
        gos = GameObject.FindGameObjectsWithTag("mina");        //Guarda las minas en el array
        GameObject closest = null;                              //El gameobject que tendra la mina mas cercana y sera devuelta
        float distance = Mathf.Infinity;                        //Mientras mas largo mej....
        Vector3 position = transform.position;                  //La posicion de este gameobject
        foreach (GameObject go in gos)                          //Para cada gameobject GO del array gos:
        {
            Vector3 diff = go.transform.position - position;    //Vecto de gos[] al Gameobject
            float curDistance = diff.sqrMagnitude;              //returns the squared distance between two Vector3 objects segun san google
            if (curDistance < distance)                         
            {                                                   
                closest = go;
                distance = curDistance;
            }
        }
        return closest;                                         //Devolver la mas cercana
    }
}


