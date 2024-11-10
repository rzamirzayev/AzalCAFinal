namespace AzalCAFinal.AppCode.Services
{
    public interface ICryptoService
    {
        string Encrypt(string value,bool appliedUrlEncode);
        string Decrypt(string chiperText);
    }
}
