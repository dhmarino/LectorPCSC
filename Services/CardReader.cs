using PCSC;

namespace LectorPCSC.Services
{
    public class CardReader
    {
        public string GetATR()
        {
            using (var context = ContextFactory.Instance.Establish(SCardScope.System))
            {
                var readerNames = context.GetReaders();
                if (readerNames == null || readerNames.Length == 0)
                {
                    throw new Exception("No card readers found.");
                }

                using (var reader = context.ConnectReader(readerNames[0], SCardShareMode.Shared, SCardProtocol.Any))
                {
                    var atr = reader.GetAttrib(SCardAttribute.AtrString);
                    return BitConverter.ToString(atr).Replace("-", " ");
                }
            }
        }
    }
}
