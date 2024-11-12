namespace Services.Common
{
    public interface ICryptoService
    {
        string Encrypt(string value, bool appliedUrlEncode);
        string Decrypt(string chiperText);
    }
}
