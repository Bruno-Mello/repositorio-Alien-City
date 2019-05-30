using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	private Animator anim;
	private Rigidbody2D rb2d;

	public Transform posPe;
	public bool tocaChao = true;


	public float Velocidade;
	public float ForcaPulo = 1000f;
	[HideInInspector] public bool viradoDireita = true;

	public Image vida;
    public Image arma;
    public Sprite arma_01;
    public Sprite arma_02;
	private MensagemControle MC;
    private int armaNum = 1;
    public GameObject projectilePrefab;
    public GameObject projectilePrefab_02;
    public GameObject tiro_origem;

	void Start () {
        
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
        
        
       

		GameObject mensagemControleObject = GameObject.FindWithTag ("MensagemControle");
		if (mensagemControleObject != null) {
			MC = mensagemControleObject.GetComponent<MensagemControle> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
        //Implementar Pulo Aqui! 
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameObject projectile = null;
            if ( armaNum == 1)
            {
                anim.SetTrigger("shoot");
                projectile = GameObject.Instantiate(projectilePrefab, tiro_origem.transform.position, Quaternion.identity);
            }
            else if (armaNum == 2)
            {
                anim.SetTrigger("shoot_02");
                projectile = GameObject.Instantiate(projectilePrefab_02, tiro_origem.transform.position, Quaternion.identity);
            }
            
            
            if (viradoDireita == true)
            {


                projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 0);
            }
            else
                    {
                projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);
                projectile.transform.localScale = new Vector3(-1, 1, 1);

            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            
            armaNum += 1;
            if(armaNum >= 3)
            {
                armaNum = 1;
            }
            if (armaNum == 1)
            {
                arma.sprite = arma_01;
            }
            else
            {
                arma.sprite = arma_02;
            }
           
        }
        if (Input.GetKeyDown(KeyCode.Space) && tocaChao == true)
        {
            anim.SetBool("noChao", false);
            tocaChao = false;
            rb2d.AddForce(new Vector2(0, 1000));
        }
	}

	void FixedUpdate()
	{
		float translationY = 0;
		float translationX = Input.GetAxis ("Horizontal") * Velocidade;
		transform.Translate (translationX, translationY, 0);
		transform.Rotate (0, 0, 0);
		if (translationX != 0) {
			anim.SetTrigger ("corre");
            
		} else {
			anim.SetTrigger("parado");
            
        }
        
		//Programar o pulo Aqui! 

		if (translationX > 0 && !viradoDireita) {
			Flip ();
		} else if (translationX < 0 && viradoDireita) {
			Flip();
		}

	}
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Chao"))
        {
            anim.SetBool("noChao", true);
            tocaChao = true;
            
        }
    }

    void Flip()
	{
		viradoDireita = !viradoDireita;
		Vector3 escala = transform.localScale;
		escala.x *= -1;
		transform.localScale = escala;
	}

	public void SubtraiVida()
	{
		vida.fillAmount-=0.1f;
		if (vida.fillAmount <= 0) {
			MC.GameOver();
			Destroy(gameObject);
		}
	}
    
}
