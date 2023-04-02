namespace BtcTurkAPI.Proxies.BtcTurkApi.Models;

public class UserBalance
{
    public List<UserBalanceData> Data { get; set; }
}

public class UserBalanceData
{
    public string Asset { get; set; }

    public string AssetName { get; set; }

    public string Balance { get; set; }

    public string Locked { get; set; }

    public string Free { get; set; }

    public string OrderFund { get; set; }

    public string RequestFund { get; set; }

    public int Precision { get; set; }
}