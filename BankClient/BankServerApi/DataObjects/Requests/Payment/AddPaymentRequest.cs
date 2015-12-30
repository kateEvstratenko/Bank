namespace BankServerApi.DataObjects.Requests.Payment
{
    public class AddPaymentRequest
    {
        public string ContractNumber { get; set; }
        public double Sum { get; set; }
    }
}