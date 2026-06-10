namespace Quiz_Web.Models.MoMoPayment
{
    public class MoMoPaymentRequest
    {
        public string partnerCode { get; set; } = string.Empty;
        public string accessKey { get; set; } = string.Empty; // NEW: required by MoMo
        public string partnerName { get; set; } = "Quiz Web";
        public string storeId { get; set; } = "QuizWeb";
        public string requestId { get; set; } = string.Empty;
        public long amount { get; set; }
        public string orderId { get; set; } = string.Empty;
        public string orderInfo { get; set; } = string.Empty;
        public string redirectUrl { get; set; } = string.Empty;
        public string ipnUrl { get; set; } = string.Empty;
        public string requestType { get; set; } = "captureWallet";
        public string extraData { get; set; } = string.Empty;
        public string lang { get; set; } = "vi";
        public string signature { get; set; } = string.Empty;
    }

    public class MoMoPaymentResponse
    {
        public string partnerCode { get; set; } = string.Empty;
        public string orderId { get; set; } = string.Empty;
        public string requestId { get; set; } = string.Empty;
        public long amount { get; set; }
        public long responseTime { get; set; }
        public string message { get; set; } = string.Empty;
        public int resultCode { get; set; }
        public string payUrl { get; set; } = string.Empty;
        public string deeplink { get; set; } = string.Empty;
        public string qrCodeUrl { get; set; } = string.Empty;
    }

    public class MoMoIpnRequest
    {
        public string partnerCode { get; set; } = string.Empty;
        public string orderId { get; set; } = string.Empty;
        public string requestId { get; set; } = string.Empty;
        public long amount { get; set; }
        public string orderInfo { get; set; } = string.Empty;
        public string orderType { get; set; } = string.Empty;
        public long transId { get; set; }
        public int resultCode { get; set; }
        public string message { get; set; } = string.Empty;
        public string payType { get; set; } = string.Empty;
        public long responseTime { get; set; }
        public string extraData { get; set; } = string.Empty;
        public string signature { get; set; } = string.Empty;
    }
}
