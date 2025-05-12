using PayPalCheckoutSdk.Core;

namespace ECommerceAPI.Helpers
{
    public class PayPalClientFactory
    {
        public static PayPalHttpClient CreatePayPalClient(IConfiguration confg)
        {
            var ClientId = confg["PayPal:ClientId"];
            var SecretKey = confg["PayPal:SecretKey"];
            var env = new SandboxEnvironment(ClientId, SecretKey);
            return new PayPalHttpClient(env);
        }
    }
}