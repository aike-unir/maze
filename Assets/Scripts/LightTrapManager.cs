using System.Linq;
using UnityEngine;

public class LightTrapManager : MonoBehaviour
{
    
    [SerializeField] private GameObject[] lights;
    [SerializeField] private float timeToChange = 5f;

    int selectedLight = 0;
    private void Start()
    {
        LightTrapController lightController = lights[0].GetComponent<LightTrapController>();
        lightController.ActivateLightTrap();
    }
    
    // Update is called once per frame
    float timer = 0f;
    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= timeToChange)
        {
            int[] numbers = {0, 1, 2};
            numbers = numbers.Except(new int[]{selectedLight}).ToArray();
            selectedLight= numbers[Random.Range(0, numbers.Length)];
            
            Debug.Log(selectedLight);
            for (int i = 0; i < lights.Length; i++)
            {
                
                LightTrapController lightController = lights[i].GetComponent<LightTrapController>();

                if (i == selectedLight)
                {
                    Debug.Log($" === Activate {selectedLight}");
                    lightController.ActivateLightTrap();
                }
                else
                {
                    lightController.DeactivateLightTrap();
                }
            }
            
            timer = 0f;
        }
    }
}
