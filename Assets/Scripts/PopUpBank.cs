using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpBank : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI userNameText;
    [SerializeField] private TextMeshProUGUI cashText;
    [SerializeField] private TextMeshProUGUI balanceText;
    [SerializeField] private GameObject ATM;
    [SerializeField] private GameObject depositWindow;
    [SerializeField] private GameObject withdrawWindow;
    [SerializeField] private Button depositBtn;
    [SerializeField] private Button withdrawBtn;

    public UserData data { get => GameManager.Instance.data; }
    private void Start()
    {
        GameManager.Instance.data = new UserData("염예찬", 100000, 50000);
    }

    private void Update()
    {
        Refresh();
    }

    public void Refresh()
    {
        data.userName = GameManager.Instance.data.userName;
        data.cash = GameManager.Instance.data.cash;
        data.balance = GameManager.Instance.data.balance;

        userNameText.text = data.userName;
        cashText.text = string.Format("{0:N0}", data.cash);
        balanceText.text = string.Format("{0:N0}", data.balance);
    }

    public void OnClickDepositButton() //ButtonEvent에서 처리했음, 안 쓰는 코드 
    {
        ATM.SetActive(false);
        depositWindow.SetActive(true);
    }
    public void OnClickWithdrawButton()
    {
        ATM.SetActive(false);
        withdrawWindow.SetActive(true);
    }
}
