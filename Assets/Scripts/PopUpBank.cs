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
    [SerializeField] private GameObject popupErrorPanel;
    [SerializeField] private Button depositBtn;
    [SerializeField] private Button withdrawBtn;
    [SerializeField] private Button customMoney_deposit;
    [SerializeField] private Button customMoney_withdraw;
    [SerializeField] private TMP_InputField inputFieldText_deposit;
    [SerializeField] private TMP_InputField inputFieldText_withdraw;


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

    public void Deposit(int cash) //입금시
    {
        if (data.cash - cash < 0)
        {
            popupErrorPanel.SetActive(true);
            return;
        }

        data.cash -= cash;
        data.balance += cash;
        Refresh();
    }

    public void Deposit_Input(TMP_InputField input)
    {
        int customMoney = int.Parse(inputFieldText_deposit.text.Trim());
        Deposit(customMoney);
    }

    public void Withdrawal(int balance) // 출금시 
    {
        if (data.balance - balance < 0)
        {
            popupErrorPanel.SetActive(true);
            return;
        }

        data.balance -= balance;
        data.cash += balance;
        Refresh();
    }

    public void Withdrawal_Input(TMP_InputField input)
    {
        int customMoney = int.Parse(inputFieldText_withdraw.text.Trim());
        Withdrawal(customMoney);
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

    public void OnClickBackButton_Deposit()
    {
        inputFieldText_deposit.text = string.Empty;
        depositWindow.SetActive(false);
        ATM.SetActive(true);
    }
    public void OnClickBackButton_Withdraw()
    {
        inputFieldText_withdraw.text = string.Empty;
        withdrawWindow.SetActive(false);
        ATM.SetActive(true);
    }
}
