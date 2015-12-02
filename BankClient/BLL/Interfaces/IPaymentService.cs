namespace BLL.Interfaces
{
    public interface IPaymentService
    {
        void Add(string contractNumber, double sum);
    }
}