using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpBank : MonoBehaviour
{
    [Header("TextMeshProUGUI")]
    [SerializeField] private TextMeshProUGUI userNameText;
    [SerializeField] private TextMeshProUGUI cashText;
    [SerializeField] private TextMeshProUGUI balanceText;

    [Header("GameObject")]
    [SerializeField] private GameObject ATM;
    [SerializeField] private GameObject depositWindow;
    [SerializeField] private GameObject withdrawWindow;
    [SerializeField] private GameObject transferWindow;
    [SerializeField] private GameObject popupErrorPanel;

    [Header("Button")]
    [SerializeField] private Button depositBtn;
    [SerializeField] private Button withdrawBtn;
    [SerializeField] private Button customMoney_deposit;
    [SerializeField] private Button customMoney_withdraw;
    [SerializeField] private Button transferBtn;

    [Header("TMP_InputField")]
    [SerializeField] private TMP_InputField inputFieldText_deposit;
    [SerializeField] private TMP_InputField inputFieldText_withdraw;
    [SerializeField] private TMP_InputField transfer_Name;
    [SerializeField] private TMP_InputField transfer_Money;


    public UserData data { get => GameManager.Instance.data; }
    private void Start()
    {
        Refresh();
        //Debug.Log($"IsSuccesLoad() 결과: {GameManager.Instance.IsSuccesLoad()}");
        //if (!GameManager.Instance.IsSuccesLoad())
        //{
        //GameManager.Instance.data = new UserData("염예찬", 100000, 50000);
        //GameManager.Instance.SaveUserData(GameManager.Instance.data);
        //    Refresh();
        //}
        //else Refresh();

    }

    public void Refresh()
    {
        if (GameManager.Instance.data == null)
        {
            Debug.LogError("GameManager.Instance.data가 null입니다");
            return;
        }
        
        data.Name = GameManager.Instance.data.Name;
        data.cash = GameManager.Instance.data.cash;
        data.balance = GameManager.Instance.data.balance;

        Debug.Log($"{data.ID}, {data.PW}, {data.Name}, {data.cash}, {data.balance}");
        GameManager.Instance.SaveUserData(data.ID, data.PW, data.Name, data.cash, data.balance);
        Debug.Log($"{data.ID}, {data.PW}, {data.Name}, {data.cash}, {data.balance}");

        userNameText.text = data.Name;
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
        //GameManager.Instance.SaveUserData(data);
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
        //GameManager.Instance.SaveUserData(data);
        Refresh();
    }

    public void Withdrawal_Input(TMP_InputField input)
    {
        int customMoney = int.Parse(inputFieldText_withdraw.text.Trim());
        Withdrawal(customMoney);
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
