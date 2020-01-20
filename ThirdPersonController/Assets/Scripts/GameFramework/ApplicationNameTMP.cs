using TMPro;
using UnityEngine;

public class ApplicationNameTMP : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<TMP_Text>().text = Application.productName.ToString();
    }
}
