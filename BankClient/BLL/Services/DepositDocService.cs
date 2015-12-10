using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BLL.Models;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace BLL.Services
{
    public class DepositDocService : BaseDocService
    {
        private readonly string _creditContractsDocPath;
        private readonly string _templateDocPath;
        private const string ContractNumberPlace = "________________";
        private const string DaymonthPlace = "\"__\"______";
        private const string YearPlace = "20__";
        private const string CustomerPlace = "_________________________";
        private const string SumPlace = "____________";
        private const string SincePlace = "____________";
        private const string UntilPlace = "____________";
        private const string PercentRatePlace = "_________";
        private const string PaymentDayPlace = "___________";

        public DepositDocService()
        {
            _creditContractsDocPath = AppDomain.CurrentDomain.BaseDirectory + "Content\\DepositContracts\\";
            _templateDocPath = _creditContractsDocPath + "depositTemplate.docx";
        }

        public void FillConcreteContract(DomainCustomerDeposit customerDeposit)
        {
            if (!File.Exists(_templateDocPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_templateDocPath));
                GenerateContractTemplate();
            }

            var byteArray = File.ReadAllBytes(_templateDocPath);

            using (var stream = new MemoryStream())
            {
                stream.Write(byteArray, 0, byteArray.Length);
                using (var wordDoc = WordprocessingDocument.Open(stream, true))
                {
                    var body = wordDoc.MainDocumentPart.Document.Body;
                    var runs = body.Descendants<Run>().ToList();
                    var i = 0;

                    //contract number
                    FindAndReplace(ref i, runs, ContractNumberPlace, " " + customerDeposit.ContractNumber);

                    //day & month
                    FindAndReplace(ref i, runs, DaymonthPlace, customerDeposit.StartDate.ToString("dd MM"));//customerDeposit.StartDate.ToString("M"));

                    //year
                    FindAndReplace(ref i, runs, YearPlace, " " + customerDeposit.StartDate.ToString("yyyy"));

                    //customerPlace
                    FindAndReplace(ref i, runs, CustomerPlace,
                        string.Format("{0} {1}{2}", customerDeposit.Customer.Lastname,
                            customerDeposit.Customer.Firstname,
                            customerDeposit.Customer.Patronymic != null
                                ? " " + customerDeposit.Customer.Patronymic
                                : ""), UnderlineValues.None);

                    //credit name
//                    FindAndReplace(ref i, runs, CreditPlace, customerDeposit.Deposit.Name);

                    //sum
                    FindAndReplace(ref i, runs, SumPlace, customerDeposit.InitialSum.ToString());

                    //since
                    FindAndReplace(ref i, runs, SincePlace, customerDeposit.StartDate.ToString("dd.MM.yyyy"));

                    //end date
                    FindAndReplace(ref i, runs, UntilPlace, customerDeposit.EndDate.ToString("dd.MM.yyyy"));

                    //percent rate
                    FindAndReplace(ref i, runs, PercentRatePlace, customerDeposit.Deposit.InterestRate.ToString());

                    //payment day
                    FindAndReplace(ref i, runs, PaymentDayPlace, customerDeposit.StartDate.ToString("dd"));

                    wordDoc.MainDocumentPart.Document.Save();
                    //                        wordDoc.MainDocumentPart.Document.Save(fs);
                    File.WriteAllBytes(
                        string.Format("{0}{1}.docx", _creditContractsDocPath, customerDeposit.ContractNumber),
                        stream.ToArray());
                }
            }
        }

        private void GenerateContractTemplate()
        {
            using (var document = WordprocessingDocument.Create(_templateDocPath, WordprocessingDocumentType.Document, true))
            {
                var mainPart = document.AddMainDocumentPart();
                var doc = new Document();
                var body = new Body();
                AddParagraph(body, JustificationValues.Center, SetBoldRunProperties(new RunProperties()), "Депозитный договор");
                AddParagraph(body, JustificationValues.Left, new RunProperties(), string.Empty);

                var runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), "№"),
                    GenerateRun(new RunProperties(), ContractNumberPlace)
                };
                AddParagraph(body, JustificationValues.Left, runs);

                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), DaymonthPlace),
                    GenerateRun(new RunProperties(), YearPlace)
                };
                AddParagraph(body, JustificationValues.Left, runs);
                AddParagraph(body, JustificationValues.Left, new RunProperties(), string.Empty);

                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), "Банк \"Три толстяка\", далее именуемый \"Банк\", в лице Чугаинова Кирилла, действующего(-ей) на основании Устава, с одной стороны, и "),
                    GenerateRun(new RunProperties(), CustomerPlace),
                    GenerateRun(new RunProperties(), ", далее именуемый(-ая) \"Вкладчик\", с другой стороны, заключили настоящий договор о следующем.")
                };
                AddParagraph(body, JustificationValues.Both, runs);

                //1. Предмет договора
                AddParagraph(body, JustificationValues.Left, new RunProperties(), string.Empty);
                AddParagraph(body, JustificationValues.Center, new RunProperties() { Italic = new Italic(), Bold = new Bold() }, "1. Предмет договора");

                //1.1
                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), "1.1. Вкладчик вносит, а Банк принимает в  порядке и на условиях, предусмотренных Договором, денежные средства в размере "),
                    GenerateRun(new RunProperties(), SumPlace),
                    GenerateRun(new RunProperties(), " рублей"),
                    GenerateRun(new RunProperties(), " с "),
                    GenerateRun(new RunProperties(), SincePlace),
                    GenerateRun(new RunProperties(), " по "),
                    GenerateRun(new RunProperties(), UntilPlace),
                    GenerateRun(new RunProperties(), ".")
                };
                AddParagraph(body, JustificationValues.Both, runs);

                //1.2
                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), "1.2. Банк начисляет Вкладчику проценты на сумму Депозита, внесенного в соответствии с условиями Договора, по ставке "),
                    GenerateRun(new RunProperties(), PercentRatePlace),
                    GenerateRun(new RunProperties(),  " процентов годовых.")
                };
                AddParagraph(body, JustificationValues.Both, runs);
                AddParagraph(body, JustificationValues.Both, new RunProperties(), "Проценты на сумму Депозита начисляются со дня, следующего за днем внесения Депозита на " +
                                                                                  "депозитный счет, по день возврата суммы Депозита Вкладчику включительно. При исчислении " +
                                                                                  "процентов и Срока депозита количество дней в месяце и году принимается равным календарному " +
                                                                                  "числу дней (365 дней или 366 дней соответственно).");
                //1.3
                AddParagraph(body, JustificationValues.Both, new RunProperties(), "1.3. Выплата Банком процентов по вкладу осуществляется по истечении Срока депозита.");
//                AddParagraph(body, JustificationValues.Both, new RunProperties(), "1.4. Сумма Депозита и начисленные проценты в день окончания Срока депозита перечисляются на " +

                doc.AppendChild(body);
                mainPart.Document = doc;
            }
        }
    }
}