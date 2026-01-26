using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ColiderTriggerDetector : MonoBehaviour
{
    [SerializeField] private int initialLife = 3;
    [SerializeField] private float secondsAfterDamage = 3f;
    
    [SerializeField] private float radius = 1.1f;
    [SerializeField] private Canvas canvasTrapDetected;
    [SerializeField] private Canvas canvasWin;
    [SerializeField] private LayerMask layerAllowed;
    
    [SerializeField] public Image[] heartsImages;
    // estas layer mask es para comprobar solo una layer, dicha layer la podemos poner en
    // ProjectSettings / physics / Layer Collision Matrix / (desactivar la layer para no tener que calcularla constantemente)

    private int life;

    private void Awake()
    {
        life = initialLife;
    }
    
    float timer = 0f;
    private void Update()
    {
        timer += Time.deltaTime;
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layerAllowed);
        bool anyTrapDetected = false;
        foreach (Collider col in colliders)
        {
            if (timer >= secondsAfterDamage)
            {
                if (col.tag == "Trap")
                {
                    anyTrapDetected = true;

                    life--;
                    timer = 0f;
                    Debug.Log($"Vida disminuye a {life}");
                    AdjustLife(life);
                    StartCoroutine(ShowDamage());
                } else if (col.tag == "Win")
                {
                    canvasWin.gameObject.SetActive(true);
                }
            }
        }

        //canvasTrapDetected.gameObject.SetActive(anyTrapDetected);

    }
    
    IEnumerator ShowDamage()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        canvasTrapDetected.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        canvasTrapDetected.gameObject.SetActive(false);
    }

    private void AdjustLife(int life)
    {
        if (life <= 0)
        {
            RestartGame();
        }
        else
        {
            for (int i = 0; i < heartsImages.Length; i++)
            {
                heartsImages[i].enabled = life > i;
            }
        }

    }
    
    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
}
