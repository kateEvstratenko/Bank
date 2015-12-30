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
    public class CreditRequestDocService : BaseDocService
    {
        private readonly string _creditContractsDocPath;
        private readonly string _templateDocPath;
        private const string DaymonthPlace = "\"__\"______";
        private const string YearPlace = "20__";
        private const string LastNamePlace = "________________";
        private const string FirstNamePlace = "________________";
        private const string PatronymicPlace = "________________";
        private const string BirthDayPlace = "_____________";
        private const string DocumentNumberPlace = "_____________________";

        private const string CityPlace = "_________________________";
        private const string StreetPlace = "_________________________";
        private const string StreetNumberPlace = "_________________________";
        private const string FlatPlace = "_________________________";
        private const string FioPlace = "________________";

        private const string SumPlace = "______________________";
        private const string CreditNamePlace = "___________________________";
        private const string MonthCountPlace = "________________________________";

        public CreditRequestDocService()
        {
            _creditContractsDocPath = AppDomain.CurrentDomain.BaseDirectory + "Content\\CreditRequestContracts\\";
            Directory.CreateDirectory(_creditContractsDocPath);
            _templateDocPath = _creditContractsDocPath + "creditRequestTemplate.docx";
        }

        public void FillConcreteContract(DomainCreditRequest creditRequest)
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
                    var dateNow = DateTime.Now;

                    //day & month
                    FindAndReplace(ref i, runs, DaymonthPlace, dateNow.ToString("dd MM"));//customerDeposit.StartDate.ToString("M"));
                    //year
                    FindAndReplace(ref i, runs, YearPlace, " " + dateNow.ToString("yyyy"));
                    //sum
                    FindAndReplace(ref i, runs, SumPlace, creditRequest.Sum.ToString());
                    //FIO
                    FindAndReplace(ref i, runs, LastNamePlace, creditRequest.Customer.Lastname);
                    FindAndReplace(ref i, runs, FirstNamePlace, creditRequest.Customer.Firstname);
                    FindAndReplace(ref i, runs, PatronymicPlace, creditRequest.Customer.Patronymic ?? "");
                    //birthday
                    FindAndReplace(ref i, runs, BirthDayPlace, creditRequest.Customer.DateOfBirth.ToString("dd.MM.yyyy") ?? "");
                    //documentNumber
                    FindAndReplace(ref i, runs, DocumentNumberPlace, creditRequest.Customer.DocumentNumber);
                    //address
                    FindAndReplace(ref i, runs, CityPlace, creditRequest.Customer.Address.City);
                    FindAndReplace(ref i, runs, StreetPlace, creditRequest.Customer.Address.Street);
                    FindAndReplace(ref i, runs, StreetNumberPlace, creditRequest.Customer.Address.House);
                    FindAndReplace(ref i, runs, FlatPlace, creditRequest.Customer.Address.Flat != null ? creditRequest.Customer.Address.Flat.ToString() : "");

                   //credit name
                    FindAndReplace(ref i, runs, CreditNamePlace, creditRequest.Credit.Name);
                    //sum
                    FindAndReplace(ref i, runs, SumPlace, creditRequest.Sum.ToString());
                    //monthCount
                    FindAndReplace(ref i, runs, MonthCountPlace, creditRequest.MonthCount.ToString());

                    //customerPlace
                    FindAndReplace(ref i, runs, FioPlace,
                        string.Format("{0} {1}{2}", creditRequest.Customer.Lastname,
                            creditRequest.Customer.Firstname,
                            creditRequest.Customer.Patronymic != null
                                ? " " + creditRequest.Customer.Patronymic
                                : ""), UnderlineValues.None);

                    //day & month
                    FindAndReplace(ref i, runs, DaymonthPlace, dateNow.ToString("dd MM"));//customerDeposit.StartDate.ToString("M"));
                    //year
                    FindAndReplace(ref i, runs, YearPlace, " " + dateNow.ToString("yyyy"));

                    wordDoc.MainDocumentPart.Document.Save();
                    //                                            wordDoc.MainDocumentPart.Document.Save(stream);
                    File.WriteAllBytes(
                        string.Format("{0}{1}.docx", _creditContractsDocPath, creditRequest.Id),
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

                //Шапка
                AddParagraph(body, JustificationValues.Center, SetBoldRunProperties(new RunProperties()), "Заявление на получение кредита");

                //Дата
                var runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), DaymonthPlace),
                    GenerateRun(new RunProperties(), YearPlace)
                };
                AddParagraph(body, JustificationValues.Right, runs);
                AddParagraph(body, JustificationValues.Left, new RunProperties(), string.Empty);

                //Сумма кредита
                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(){Bold = new Bold()}, "Сумма кредита: "),
                    GenerateRun(new RunProperties(), SumPlace)
                };
                AddParagraph(body, JustificationValues.Left, runs);
                AddParagraph(body, JustificationValues.Left, new RunProperties(), string.Empty);

                //Персональные данные
                AddParagraph(body, JustificationValues.Left, SetBoldRunProperties(new RunProperties()), "Персональные данные заявителя");
                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), "Фамилия: "),
                    GenerateRun(new RunProperties(), LastNamePlace)
                };
                AddParagraph(body, JustificationValues.Left, runs);
                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), "Имя: "),
                    GenerateRun(new RunProperties(), FirstNamePlace)
                };
                AddParagraph(body, JustificationValues.Left, runs);
                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), "Отчество: "),
                    GenerateRun(new RunProperties(), PatronymicPlace)
                };
                AddParagraph(body, JustificationValues.Left, runs);
                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), "Дата рождения: "),
                    GenerateRun(new RunProperties(), BirthDayPlace)
                };
                AddParagraph(body, JustificationValues.Left, runs);
                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), "Номер документа, удостоверяющего личность: "),
                    GenerateRun(new RunProperties(), DocumentNumberPlace)
                };
                AddParagraph(body, JustificationValues.Left, runs);
                AddParagraph(body, JustificationValues.Left, new RunProperties(), string.Empty);

                //Место жительства
                AddParagraph(body, JustificationValues.Left, SetBoldRunProperties(new RunProperties() { Underline = new Underline() }), "Место жительства");
                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), "Город: "),
                    GenerateRun(new RunProperties(), CityPlace)
                };
                AddParagraph(body, JustificationValues.Left, runs);
                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), "Улица: "),
                    GenerateRun(new RunProperties(), StreetPlace)
                };
                AddParagraph(body, JustificationValues.Left, runs);
                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), "Дом: "),
                    GenerateRun(new RunProperties(), StreetNumberPlace)
                };
                AddParagraph(body, JustificationValues.Left, runs);
                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), "Квартира: "),
                    GenerateRun(new RunProperties(), FlatPlace)
                };
                AddParagraph(body, JustificationValues.Left, runs);
                AddParagraph(body, JustificationValues.Left, new RunProperties(), string.Empty);

                //Информация о кредите

                //Место жительства
                AddParagraph(body, JustificationValues.Left, SetBoldRunProperties(new RunProperties() { Underline = new Underline() }), "Информация о кредите");
                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), "Название: "),
                    GenerateRun(new RunProperties(), CreditNamePlace)
                };
                AddParagraph(body, JustificationValues.Left, runs);
                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), "Сумма: "),
                    GenerateRun(new RunProperties(), SumPlace)
                };
                AddParagraph(body, JustificationValues.Left, runs);
                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), "Срок, мес: "),
                    GenerateRun(new RunProperties(), MonthCountPlace)
                };
                AddParagraph(body, JustificationValues.Left, runs);
                AddParagraph(body, JustificationValues.Left, new RunProperties(), string.Empty);
                //
                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), "Я, "),
                    GenerateRun(new RunProperties(), FioPlace),
                    GenerateRun(new RunProperties(), ":")
                };
                AddParagraph(body, JustificationValues.Left, runs);

                AddParagraph(body, JustificationValues.Both, new RunProperties(), "1) подтверждаю, что информация, содержащаяся в заявлении, достоверна;");
                AddParagraph(body, JustificationValues.Both, new RunProperties(), "2) предоставляю Банку право проверки предоставленной мною информации;");
                AddParagraph(body, JustificationValues.Both, new RunProperties(), "3) соглашаюсь, что кредит будет предоставлен в случае прохождения соответствующих проверок, " +
                                                                                  "проводимых Банком в соответствии с его локальными нормативными правовыми актами, при этом, Банк " +
                                                                                  "вправе отказать мне в предоставлении кредита без указания причин;");
                AddParagraph(body, JustificationValues.Both, new RunProperties(), "4) соглашаюсь, что Банк вправе отказать мне в заключении кредитного договора и в предоставлении кредита, если " +
                                                                                  "в случае проведения после принятия решения о выдаче кредита сверки оригиналов документов на " +
                                                                                  "предоставление кредита и их электронных копий в Банке будет выявлено несоответствие, " +
                                                                                  "препятствующее выдаче кредита;");
                AddParagraph(body, JustificationValues.Both, new RunProperties(), "5) соглашаюсь, что при отказе в предоставлении кредита оригинал заявления на получение " +
                                                                                  "кредита не возвращается;");

                AddParagraph(body, JustificationValues.Left, new RunProperties(), string.Empty);
                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), DaymonthPlace),
                    GenerateRun(new RunProperties(), YearPlace)
                };
                AddParagraph(body, JustificationValues.Right, runs);

                doc.AppendChild(body);
                mainPart.Document = doc;
            }
        }
    }
}