namespace LectorPCSC.Services
{
    using PCSC;
    using PCSC.Utils;
    
    public class SmartCardServiceold
    {
        public string GetAtr()
        {
            using (var context = ContextFactory.Instance.Establish(SCardScope.System))
            {
                var readerNames = context.GetReaders();

                if (readerNames == null || readerNames.Length == 0)
                {
                    try
                    {
                        throw new Exception("No PC/SC readers connected.");
                    }
                    catch (Exception)
                    {
                        return "No hay un lector de tarjetas conectado al equipo";
                    }
                }

                using (var reader = new SCardReader(context))
                {
                    var readerName = readerNames[0]; // Asume que hay al menos un lector

                    var rc = reader.Connect(readerName, SCardShareMode.Shared, SCardProtocol.Any);
                    if (rc != SCardError.Success)
                    {
                        try
                        {
                            throw new Exception($"Could not connect to reader {readerName}. Error: {SCardHelper.StringifyError(rc)}");
                        }
                        catch (Exception)
                        {
                            return "No hay un tarjetas insertada en el lector";
                        }
                    }
                    
                    // Crear un buffer para el ATR
                    byte[] atrBuffer = new byte[64];
                    int bufferLength = atrBuffer.Length;

                    // Obtener el ATR
                    rc = reader.GetAttrib(SCardAttribute.AtrString, atrBuffer, out bufferLength);
                    if (rc != SCardError.Success)
                    {
                        try
                        {
                            throw new Exception($"Error al obtener el ATR. Error: {SCardHelper.StringifyError(rc)}");
                        }
                        catch (Exception ex)
                        {
                            return "No se pudo obtener el ATR" + ex.ToString();
                        }
                    }

                    // Convertir el ATR a cadena
                    var atr = BitConverter.ToString(atrBuffer, 0, bufferLength).Replace("-", " ");
                    return atr;
                }
            }
        }
    }
}
