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

                //�����
                AddParagraph(body, JustificationValues.Center, SetBoldRunProperties(new RunProperties()), "��������� �� ��������� �������");

                //����
                var runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), DaymonthPlace),
                    GenerateRun(new RunProperties(), YearPlace)
                };
                AddParagraph(body, JustificationValues.Right, runs);
                AddParagraph(body, JustificationValues.Left, new RunProperties(), string.Empty);

                //����� �������
                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(){Bold = new Bold()}, "����� �������: "),
                    GenerateRun(new RunProperties(), SumPlace)
                };
                AddParagraph(body, JustificationValues.Left, runs);
                AddParagraph(body, JustificationValues.Left, new RunProperties(), string.Empty);

                //������������ ������
                AddParagraph(body, JustificationValues.Left, SetBoldRunProperties(new RunProperties()), "������������ ������ ���������");
                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), "�������: "),
                    GenerateRun(new RunProperties(), LastNamePlace)
                };
                AddParagraph(body, JustificationValues.Left, runs);
                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), "���: "),
                    GenerateRun(new RunProperties(), FirstNamePlace)
                };
                AddParagraph(body, JustificationValues.Left, runs);
                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), "��������: "),
                    GenerateRun(new RunProperties(), PatronymicPlace)
                };
                AddParagraph(body, JustificationValues.Left, runs);
                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), "���� ��������: "),
                    GenerateRun(new RunProperties(), BirthDayPlace)
                };
                AddParagraph(body, JustificationValues.Left, runs);
                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), "����� ���������, ��������������� ��������: "),
                    GenerateRun(new RunProperties(), DocumentNumberPlace)
                };
                AddParagraph(body, JustificationValues.Left, runs);
                AddParagraph(body, JustificationValues.Left, new RunProperties(), string.Empty);

                //����� ����������
                AddParagraph(body, JustificationValues.Left, SetBoldRunProperties(new RunProperties() { Underline = new Underline() }), "����� ����������");
                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), "�����: "),
                    GenerateRun(new RunProperties(), CityPlace)
                };
                AddParagraph(body, JustificationValues.Left, runs);
                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), "�����: "),
                    GenerateRun(new RunProperties(), StreetPlace)
                };
                AddParagraph(body, JustificationValues.Left, runs);
                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), "���: "),
                    GenerateRun(new RunProperties(), StreetNumberPlace)
                };
                AddParagraph(body, JustificationValues.Left, runs);
                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), "��������: "),
                    GenerateRun(new RunProperties(), FlatPlace)
                };
                AddParagraph(body, JustificationValues.Left, runs);
                AddParagraph(body, JustificationValues.Left, new RunProperties(), string.Empty);

                //���������� � �������

                //����� ����������
                AddParagraph(body, JustificationValues.Left, SetBoldRunProperties(new RunProperties() { Underline = new Underline() }), "���������� � �������");
                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), "��������: "),
                    GenerateRun(new RunProperties(), CreditNamePlace)
                };
                AddParagraph(body, JustificationValues.Left, runs);
                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), "�����: "),
                    GenerateRun(new RunProperties(), SumPlace)
                };
                AddParagraph(body, JustificationValues.Left, runs);
                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), "����, ���: "),
                    GenerateRun(new RunProperties(), MonthCountPlace)
                };
                AddParagraph(body, JustificationValues.Left, runs);
                AddParagraph(body, JustificationValues.Left, new RunProperties(), string.Empty);
                //
                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), "�, "),
                    GenerateRun(new RunProperties(), FioPlace),
                    GenerateRun(new RunProperties(), ":")
                };
                AddParagraph(body, JustificationValues.Left, runs);

                AddParagraph(body, JustificationValues.Both, new RunProperties(), "1) �����������, ��� ����������, ������������ � ���������, ����������;");
                AddParagraph(body, JustificationValues.Both, new RunProperties(), "2) ������������ ����� ����� �������� ��������������� ���� ����������;");
                AddParagraph(body, JustificationValues.Both, new RunProperties(), "3) ����������, ��� ������ ����� ������������ � ������ ����������� ��������������� ��������, " +
                                                                                  "���������� ������ � ������������ � ��� ���������� ������������ ��������� ������, ��� ����, ���� " +
                                                                                  "������ �������� ��� � �������������� ������� ��� �������� ������;");
                AddParagraph(body, JustificationValues.Both, new RunProperties(), "4) ����������, ��� ���� ������ �������� ��� � ���������� ���������� �������� � � �������������� �������, ���� " +
                                                                                  "� ������ ���������� ����� �������� ������� � ������ ������� ������ ���������� ���������� �� " +
                                                                                  "�������������� ������� � �� ����������� ����� � ����� ����� �������� ��������������, " +
                                                                                  "�������������� ������ �������;");
                AddParagraph(body, JustificationValues.Both, new RunProperties(), "5) ����������, ��� ��� ������ � �������������� ������� �������� ��������� �� ��������� " +
                                                                                  "������� �� ������������;");

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