using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using LectorPCSC.Services;

public class CardReaderHub : Hub
{
    private readonly CardReader _cardReader;

    public CardReaderHub(CardReader cardReader)
    {
        _cardReader = cardReader;
    }

    public async Task SendATR()
    {
        var atr = _cardReader.GetATR();
        await Clients.All.SendAsync("ReceiveATR", atr);
    }
}
