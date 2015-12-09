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
    public class CreditDocService : BaseDocService
    {
        private readonly string _creditContractsDocPath;
        private readonly string _templateDocPath;
        private const string ContractNumberPlace = "________________";
        private const string DaymonthPlace = "\"__\"______";
        private const string YearPlace = "20__";
        private const string CustomerPlace = "_________________________";
        private const string CreditPlace = "_________";
        private const string CreditSumPlace = "____________";
        private const string SincePlace = "____________";
        private const string UntilPlace = "____________";
        private const string PercentRatePlace = "_________";
        private const string PaymentDayPlace = "___________";

        public CreditDocService()
        {
            _creditContractsDocPath = AppDomain.CurrentDomain.BaseDirectory + "Content\\CreditContracts\\";
            _templateDocPath = _creditContractsDocPath + "creditTemplate.docx";
        }

        public void FillConcreteCreditContract(DomainCustomerCredit customerCredit)
        {
            if (!File.Exists(_templateDocPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_templateDocPath));
                GenerateCreditContractTemplate();
            }

            var byteArray = File.ReadAllBytes(_templateDocPath);
            //            using (var fs = new FileStream(string.Format("{0}{1}.docx", CreditContractsDocPath, customerCredit.ContractNumber),FileMode.Create))
            {
                using (var stream = new MemoryStream())
                {
                    stream.Write(byteArray, 0, byteArray.Length);
                    using (var wordDoc = WordprocessingDocument.Open(stream, true))
                    {
                        var body = wordDoc.MainDocumentPart.Document.Body;
                        var runs = body.Descendants<Run>().ToList();
                        var i = 0;

                        //contract number
                        FindAndReplace(ref i, runs, ContractNumberPlace, " " + customerCredit.ContractNumber);

                        //day & month
                        FindAndReplace(ref i, runs, DaymonthPlace, customerCredit.StartDate.ToString("M"));

                        //year
                        FindAndReplace(ref i, runs, YearPlace, " " + customerCredit.StartDate.ToString("yyyy"));

                        //customerPlace
                        FindAndReplace(ref i, runs, CustomerPlace,
                            string.Format("{0} {1}{2}", customerCredit.Customer.Lastname,
                                customerCredit.Customer.Firstname,
                                customerCredit.Customer.Patronymic != null
                                    ? " " + customerCredit.Customer.Patronymic
                                    : ""), UnderlineValues.None);

                        //credit name
                        FindAndReplace(ref i, runs, CreditPlace, customerCredit.Credit.Name);

                        //sum
                        FindAndReplace(ref i, runs, CreditSumPlace, customerCredit.CreditSum.ToString());

                        //since
                        FindAndReplace(ref i, runs, SincePlace, customerCredit.StartDate.ToString("dd.MM.yyyy"));

                        //end date
                        FindAndReplace(ref i, runs, UntilPlace, customerCredit.EndDate.ToString("dd.MM.yyyy"));

                        //percent rate
                        FindAndReplace(ref i, runs, PercentRatePlace, customerCredit.Credit.PercentRate.ToString());

                        //payment day
                        FindAndReplace(ref i, runs, PaymentDayPlace, customerCredit.StartDate.ToString("dd"));

                        wordDoc.MainDocumentPart.Document.Save();
                        //                        wordDoc.MainDocumentPart.Document.Save(fs);
                        File.WriteAllBytes(
                            string.Format("{0}{1}.docx", _creditContractsDocPath, customerCredit.ContractNumber),
                            stream.ToArray());
                    }
                }
            }
        }

        private void GenerateCreditContractTemplate()
        {
            using (var document = WordprocessingDocument.Create(_templateDocPath, WordprocessingDocumentType.Document, true))
            {
                var mainPart = document.AddMainDocumentPart();
                var doc = new Document();
                var body = new Body();
                AddParagraph(body, JustificationValues.Center, SetBoldRunProperties(new RunProperties()), "Кредитный договор");
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
                    GenerateRun(new RunProperties(), "Банк \"Три толстяка\", далее именуемый \"Кредитор\", в лице Чугаинова Кирилла, действующего(-ей) на основании Устава, с одной стороны, и "),
                    GenerateRun(new RunProperties(), CustomerPlace),
                    GenerateRun(new RunProperties(), ", далее именуемый(-ая) \"Заемщик\", с другой стороны, заключили настоящий договор о следующем.")
                };
                AddParagraph(body, JustificationValues.Both, runs);

                //1. Предмет договора
                AddParagraph(body, JustificationValues.Left, new RunProperties(), string.Empty);
                AddParagraph(body, JustificationValues.Center, new RunProperties() { Italic = new Italic(), Bold = new Bold() }, "1. Предмет договора");

                //1.1
                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), "1.1. Кредитор предоставляет Заемщику  в  порядке  и  на  условиях,  предусмотренных  Договором, кредит "),
                    GenerateRun(new RunProperties(), "_________"),
                    GenerateRun(new RunProperties(), " (далее  –  кредит) "),
                    GenerateRun(new RunProperties(), "в сумме "),
                    GenerateRun(new RunProperties(), "____________"),
                    GenerateRun(new RunProperties(), " рублей"),
                    GenerateRun(new RunProperties(), " с "),
                    GenerateRun(new RunProperties(), "____________"),
                    GenerateRun(new RunProperties(), " по "),
                    GenerateRun(new RunProperties(), "____________"),
                    GenerateRun(new RunProperties(), "."),
//                    GenerateRun(new RunProperties(), "на _____________________________________________________________. (целевое использование кредита)")
                };
                AddParagraph(body, JustificationValues.Both, runs);

                //1.2
                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), "1.2.  Процентная ставка за пользование кредитом устанавливается в размере "),
                    GenerateRun(new RunProperties(), "_________"),
                    GenerateRun(new RunProperties(),  " процентов годовых.")
                };
                AddParagraph(body, JustificationValues.Both, runs);
                //1.3
                AddParagraph(body, JustificationValues.Both, new RunProperties(), "1.3.  Под «задолженностью по Договору» понимаются возникшие в связи " +
                                                                                  "с исполнением Договора обязательства  Заемщика(-ов)  по  уплате  Банку: " +
                                                                                  " основного  долга  (суммы  кредита),  процентов, штрафов, осуществленных в связи " +
                                                                                  "с исполнением/неисполнением Договора. ");
                //2. Порядок и сроки погашения кредита
                AddParagraph(body, JustificationValues.Left, new RunProperties(), string.Empty);
                AddParagraph(body, JustificationValues.Center, new RunProperties() { Italic = new Italic(), Bold = new Bold() }, "2. Порядок и сроки погашения кредита");

                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), "2.1. Заемщик обязан погасить полученный им кредит путем совершения ежемесячных платежей. " +
                                                                                  "При этом платеж должен быть произведен "),
                    GenerateRun(new RunProperties(), "___________"),
                    GenerateRun(new RunProperties(),   " числа либо ранее, но не более, чем за 10 дней до даты платежа.")
                };
                AddParagraph(body, JustificationValues.Both, runs);

                AddParagraph(body, JustificationValues.Both, new RunProperties(), "2.2. Сумма произведенного платежа, недостаточная для полного погашения всей " +
                                                                                  "задолженности Заемщика, погашает прежде всего издержки Кредитора по " +
                                                                                  "принятию исполнения и принудительному взысканию, затем - проценты за " +
                                                                                  "пользование кредитными ресурсами, " +
                                                                                  "а в оставшейся части - основную сумму долга.");

                doc.AppendChild(body);
                mainPart.Document = doc;
            }
        }
    }
}