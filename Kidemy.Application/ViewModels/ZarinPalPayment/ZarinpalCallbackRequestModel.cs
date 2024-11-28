namespace Kidemy.Application.ViewModels.ZarinPalPayment
{
    public class ZarinpalCallbackRequestModel
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public string Authority { get; set; }
    }
}
