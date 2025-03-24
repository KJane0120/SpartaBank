using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VInspector.Libs;

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

        GameManager.Instance.SaveUserData(data.ID, data.PW, data.Name, data.cash, data.balance);

        userNameText.text = data.Name;
        cashText.text = string.Format("{0:N0}", data.cash);
        balanceText.text = string.Format("{0:N0}", data.balance);
    }

    public void Deposit(int cash) //입금시
    {
        if (data.cash - cash < 0)
        {
            ErrorMsg("잔액이 부족합니다.");
            return;
        }

        data.cash -= cash;
        data.balance += cash;
        Refresh();
    }

    public void Deposit_Input(TMP_InputField input)
    {
        if (input.text.IsNullOrEmpty())
            ErrorMsg("입금하실 금액을 입력하세요.");

        int customMoney = int.Parse(input.text.Trim());
        Deposit(customMoney);
    }

    public void Withdrawal(int balance) // 출금시 
    {
        if (data.balance - balance < 0)
        {
            ErrorMsg("잔액이 부족합니다.");
            return;
        }

        data.balance -= balance;
        data.cash += balance;
        Refresh();
    }

    public void Withdrawal_Input(TMP_InputField input)
    {
        if (input.text.IsNullOrEmpty())
            ErrorMsg("출금하실 금액을 입력하세요.");

        int customMoney = int.Parse(input.text.Trim());
        Withdrawal(customMoney);
    }

    public void OnClickTransferButton(TMP_InputField input_Name)
    {
        //송금대상이 비어있을 때 
        if (input_Name.text.IsNullOrEmpty())
        {
            ErrorMsg("입력 정보를 확인해주세요.");
            return;
        }
        //ID나 이름이 존재하지 않을 때 
        if (!PlayerPrefs.HasKey(input_Name.text + GameManager.PW))
        {
            ErrorMsg("대상이 존재하지 않습니다.");
            return;
        }
        
        
        //입력한 송금 대상의 ID나 이름이 데이터에 있을 때
        if (PlayerPrefs.HasKey(input_Name.text + GameManager.PW))
        {
            //금액란이 비어있지 않을 때 
            if (!transfer_Money.text.IsNullOrEmpty())
            {
                int customMoney = int.Parse(transfer_Money.text.Trim());
                //잔액이 충분할 때
                if (customMoney <= data.balance)
                {
                    Transfer(customMoney,input_Name.text);
                }
                //잔액이 충분하지 않을 때
                else
                {
                    ErrorMsg("잔액이 부족합니다.");
                }
            }
            //금액란이 비었을 때 (에러)
            else
            {
                ErrorMsg("입력 정보를 확인해주세요.");
            }
        }
    }

    private void Transfer(int balance, string id)
    {
        int savedMoney = PlayerPrefs.GetInt(id + GameManager.Balance);
        data.balance -= balance; //내 통장에서 돈 빠져나감
        Debug.Log(PlayerPrefs.GetInt(id + GameManager.Balance)); //송금대상의 통장잔액
        PlayerPrefs.SetInt(id + GameManager.Balance, savedMoney + balance); // 송금대상에게 보낸돈만큼 더해주기
        Debug.Log(PlayerPrefs.GetInt(id + GameManager.Balance));// 반영된 송금대상의 통장잔액
        Refresh();
    }

    private void ErrorMsg(string message)
    {
        popupErrorPanel.GetComponentInChildren<TextMeshProUGUI>(true).text = message;
        popupErrorPanel.SetActive(true);
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

    public void OnClickBackButton_Transfer()
    {
        transfer_Name.text = string.Empty;
        transfer_Money.text = string.Empty;
        transferWindow.SetActive(false);
        ATM.SetActive(true);
    }
}
