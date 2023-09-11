using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _ammoLeftText;
    [SerializeField] private TMP_Text _reloadText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateAmmoAmount(int newAmount, int maxAmount)
    {
        _ammoLeftText.text = $"{newAmount.ToString()}/{maxAmount.ToString()}";
    }

    public void ShowReloadText()
    {
        _reloadText.gameObject.SetActive(true);
    }

    public void HideReloadText()
    {
        _reloadText.gameObject.SetActive(false);
    }
}
