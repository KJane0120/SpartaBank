using Unity.VisualScripting;

[System.Serializable]
public class UserData //: MonoBehaviour
{
    public string ID;
    public string PW;
    public string Name;
    public int cash;
    public int balance;

    public UserData(string ID, string PW, string Name, int cash, int balance) //생성자
    {
        this.ID = ID;
        this.PW = PW;
        this.Name = Name;
        this.cash = cash;
        this.balance = balance;
    }
}
