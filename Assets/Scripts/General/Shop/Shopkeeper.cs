using UnityEngine;

public class Shopkeeper : MonoBehaviour
{
    [SerializeField] GameObject buyKeyImage;
    [SerializeField] GameObject buyMenu;

    private bool canShop = false;
    private bool isShop = false;

    private void Start()
    {
        buyKeyImage.SetActive(false);
        buyMenu.SetActive(false);
    }

    private void Update()
    {
        CheckInput();

        buyMenu.SetActive(isShop);
    }

    private void CheckInput()
    {
        if (canShop && !isShop && Input.GetKeyDown(KeyCode.P))
        {
            isShop = true;
        }
        else if (isShop && Input.GetKeyDown(KeyCode.P))
        {
            isShop = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out var _))
        {
            canShop = true;
            buyKeyImage.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out var _))
        {
            canShop = false;
            isShop = false;
            buyKeyImage.SetActive(false);
        }
    }
}
