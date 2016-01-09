using System;

namespace Core
{
    public class BankClientException : Exception
    {
        public int Code { get; set; }
        protected BankClientException(int code, string message)
            : base(message)
        {
            Code = code;
        }

        public static BankClientException ThrowWrongTokenPartsCount()
        {
            return new BankClientException(2, "Неверный формат токена.");
        }

        public static BankClientException ThrowInvalidToken()
        {
            return new BankClientException(3, "Невалидный токен.");
        }

        public static BankClientException ThrowEmployeeIsNull()
        {
            return new BankClientException(4, "Сотрудника не существует.");
        }

        public static BankClientException ThrowInvalidCredentials()
        {
            return new BankClientException(5, "Неверное имя пользователя и/или пароль.");
        }

        public static BankClientException ThrowEmailNotConfirmed()
        {
            return new BankClientException(6, "Электронный адрес не подтвержден.");
        }

        public static BankClientException ThrowAutofacError(string message)
        {
            return new BankClientException(7, message);
        }
        public static BankClientException ThrowAuthorizationError()
        {
            return new BankClientException(8, "Отказано в доступе.");
        }

        public static BankClientException ThrowUserNotRegistered()
        {
            return new BankClientException(9, "Пользователь не зарегистрирован.");
        }

        public static BankClientException ThrowUserLockout()
        {
            return new BankClientException(10, "Пользователь заблокирован. Попробуйте позднее.");
        }

        public static BankClientException ThrowCannotSetStatus()
        {
            return new BankClientException(11, "Невозможно установить статус для этой заявки.");
        }

        public static BankClientException ThrowNotPayment()
        {
            return new BankClientException(12, "Срок приема платежа еще не наступил или платежей не существует.");
        }

        public static BankClientException ThrowUserCreditNotFound()
        {
            return new BankClientException(13, "Кредита с таким номером договора не существует.");
        }

        public static BankClientException ThrowNotHaveMoney()
        {
            return new BankClientException(14, "Недостаточно средств банка для проведения операции.");
        }

        public static BankClientException ThrowSumLessThanMin()
        {
            return new BankClientException(15, "Сумма не может быть меньше минимальной.");
        }

        public static BankClientException ThrowSumMoreThanMax()
        {
            return new BankClientException(16, "Сумма не может быть больше максимальной.");
        }

        public static BankClientException ThrowMonthLessThanMin()
        {
            return new BankClientException(17, "Количество месяцев не может быть меньше минимального.");
        }

        public static BankClientException ThrowMonthMoreThanMax()
        {
            return new BankClientException(18, "Количество месяцев не может быть больше максимального.");
        }
    }
}