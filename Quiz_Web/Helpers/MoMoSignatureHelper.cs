using System.Security.Cryptography;
using System.Text;

namespace Quiz_Web.Helpers
{
    public static class MoMoSignatureHelper
    {
        public static string CreateSignature(string secretKey, string rawData)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var messageBytes = Encoding.UTF8.GetBytes(rawData);

            using var hmac = new HMACSHA256(keyBytes);
            var hashBytes = hmac.ComputeHash(messageBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }

        public static string CreateRawDataForCreatePayment(
            string accessKey,
            long amount,
            string extraData,
            string ipnUrl,
            string orderId,
            string orderInfo,
            string partnerCode,
            string redirectUrl,
            string requestId,
            string requestType)
        {
            // Th? t? alphabet: accessKey, amount, extraData, ipnUrl, orderId, orderInfo, partnerCode, redirectUrl, requestId, requestType
            return $"accessKey={accessKey}" +
                   $"&amount={amount}" +
                   $"&extraData={extraData}" +
                   $"&ipnUrl={ipnUrl}" +
                   $"&orderId={orderId}" +
                   $"&orderInfo={orderInfo}" +
                   $"&partnerCode={partnerCode}" +
                   $"&redirectUrl={redirectUrl}" +
                   $"&requestId={requestId}" +
                   $"&requestType={requestType}";
        }
    }
}
