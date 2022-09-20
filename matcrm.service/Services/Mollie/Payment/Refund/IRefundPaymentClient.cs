﻿using System.Threading.Tasks;
using matcrm.data.Models.MollieModel;
using matcrm.data.Models.MollieModel.Payment.Response;
using matcrm.data.Models.MollieModel.Refund;
using matcrm.service.Services.Mollie.Client.Abstract;

namespace matcrm.service.Services.Mollie.Payment.Refund {
    public interface IRefundPaymentClient {
        Task Refund(string paymentId);
    }

    public class RefundPaymentClient : IRefundPaymentClient {
        private readonly IRefundClient _refundClient;
        private readonly IPaymentClient _paymentClient;

        public RefundPaymentClient(IRefundClient refundClient, IPaymentClient paymentClient) {
            this._refundClient = refundClient;
            this._paymentClient = paymentClient;
        }

        public async Task Refund(string paymentId) {
            PaymentResponse paymentToRefund = await this.GetPaymentDetails(paymentId);
            RefundRequest refundRequest = new RefundRequest() {
                Amount = new Amount(paymentToRefund.Amount.Currency, paymentToRefund.Amount.Value)
            };

            await this._refundClient.CreateRefundAsync(paymentId, refundRequest);
        }

        private async Task<PaymentResponse> GetPaymentDetails(string paymentId) {
            return await this._paymentClient.GetPaymentAsync(paymentId);
        }
    }
}