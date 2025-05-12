using ECommerceAPI.Models;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;

namespace ECommerceAPI.Data.Services.Implementation
{
    public class PayPalService
    {
        private readonly PayPalHttpClient _client; 
        public PayPalService(PayPalHttpClient client)
        {
            _client = client;
        }
        public async Task<(string OrderId,string ApprovalLink)> CreateOrder(decimal amount, string currency, string returnUrl, string cancelUrl)
        {
            var orderRequest = new OrderRequest
            {
                CheckoutPaymentIntent = "CAPTURE",
                PurchaseUnits = new List<PurchaseUnitRequest>
            {
                new PurchaseUnitRequest
                {
                    AmountWithBreakdown = new AmountWithBreakdown
                    {
                        CurrencyCode = currency,
                        Value = amount.ToString("F2")
                    }
                }
            },
                ApplicationContext = new ApplicationContext
                {
                    ReturnUrl = returnUrl,
                    CancelUrl = cancelUrl
                }
            };

            var request = new OrdersCreateRequest();
            request.Prefer("return=representation");
            request.RequestBody(orderRequest);

            var response = await _client.Execute(request);
            var result = response.Result<PayPalCheckoutSdk.Orders.Order>();

            var approvalLink = result.Links.FirstOrDefault(link => link.Rel == "approve")?.Href;

            return (result.Id, approvalLink);
        }
        public async Task<bool> CaptureOrder(string orderId)
        {
            var request = new OrdersCaptureRequest(orderId);
            request.RequestBody(new OrderActionRequest());

            var response = await _client.Execute(request);
            var result = response.Result<PayPalCheckoutSdk.Orders.Order>();

            return result.Status == "COMPLETED";
        }
    }
}
