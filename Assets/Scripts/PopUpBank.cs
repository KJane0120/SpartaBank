using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpBank : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI userNameText;
    [SerializeField] private TextMeshProUGUI cashText;
    [SerializeField] private TextMeshProUGUI balanceText;

    public UserData data;
    private void Start()
    {
        data = new UserData("염예찬", 100000, 50000);
    }

    private void FixedUpdate()
    {
        Refresh();
    }

    public void Refresh()
    {
        userNameText.text = data.userName;
        cashText.text = string.Format("{0:N0}", data.cash);
        balanceText.text = string.Format("{0:N0}", data.balance);
    }
}
