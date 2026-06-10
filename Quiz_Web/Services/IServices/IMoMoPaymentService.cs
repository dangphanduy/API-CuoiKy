using Quiz_Web.Models.MoMoPayment;

namespace Quiz_Web.Services.IServices
{
    public interface IMoMoPaymentService
    {
        Task<MoMoPaymentResponse> CreatePaymentAsync(decimal amount, string orderInfo, string orderId);
        bool ValidateSignature(MoMoIpnRequest ipnRequest);
        string GenerateSignature(string rawData);
        Task<bool> ProcessPaymentCallbackAsync(MoMoIpnRequest ipnRequest);
        public Task<MoMoQueryResponse> QueryTransactionAsync(string orderId);

	}
}
