namespace Kidemy.Application.ViewModels.ZarinPalPayment
{
    public class RequestZarinPalPaymentViewModel
    {
        #region Propeties

        public string MerchantId { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }

        public string CallbackUrl { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string OrderId { get; set; }

        #endregion
    }
}
