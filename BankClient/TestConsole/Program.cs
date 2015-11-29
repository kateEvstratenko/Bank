using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BLL.Models;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            GenerateDefault();
            FillConcreteContract(new DomainCustomerCredit()
            {
                ContractNumber = "67836458457899",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now + TimeSpan.FromDays(365*5),
                CreditSum = 50000000,
                Customer = new DomainCustomer()
                {
                    Firstname = "Екатерина",
                    Lastname = "Евстратенко"
                },
                Credit = new DomainCredit()
                {
                    Name = "Прозрачный"
                }
            });
        }

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

        private static void GenerateDefault()
        {
            string filepath = "D:\\test.docx";
            using (var document = WordprocessingDocument.Create(filepath, WordprocessingDocumentType.Document, true))
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
                    GenerateRun(new RunProperties(), CustomerPlace + ", далее именуемый(-ая) \"Заемщик\", с другой стороны, заключили настоящий договор о следующем.")
                };
                AddParagraph(body, JustificationValues.Both, runs);

                //1. Предмет договора
                AddParagraph(body, JustificationValues.Left, new RunProperties(), string.Empty);
                AddParagraph(body, JustificationValues.Center, new RunProperties() { Italic = new Italic(), Bold = new Bold() }, "1. Предмет договора");

                //1.1
                runs = new List<Run>
                {
                    GenerateRun(new RunProperties(), "1.1. Кредитор предоставляет Заемщику  в  порядке  и  на  условиях,  предусмотренных  Договором, кредит _________  (далее  –  кредит) "),
                    GenerateRun(new RunProperties(), "в сумме ____________ рублей "),
                    GenerateRun(new RunProperties(), "с ____________ "),
                    GenerateRun(new RunProperties(), "по ____________ "),
                    GenerateRun(new RunProperties(), "."),
//                    GenerateRun(new RunProperties(), "на _____________________________________________________________. (целевое использование кредита)")
                };
                AddParagraph(body, JustificationValues.Both, runs);

                //1.2
                AddParagraph(body, JustificationValues.Both, new RunProperties(), "1.2.  Процентная ставка за пользование кредитом устанавливается в размере _________ процентов годовых.");
                //1.3
                AddParagraph(body, JustificationValues.Both, new RunProperties(), "1.3.  Под «задолженностью по Договору» понимаются возникшие в связи " +
                                                                                  "с исполнением Договора обязательства  Заемщика(-ов)  по  уплате  Банку: " +
                                                                                  " основного  долга  (суммы  кредита),  процентов, штрафов, осуществленных в связи " +
                                                                                  "с исполнением/неисполнением Договора. ");
                //2. Порядок и сроки погашения кредита
                AddParagraph(body, JustificationValues.Left, new RunProperties(), string.Empty);
                AddParagraph(body, JustificationValues.Center, new RunProperties() { Italic = new Italic(), Bold = new Bold() }, "2. Порядок и сроки погашения кредита");
                AddParagraph(body, JustificationValues.Both, new RunProperties(), "2.1. Заемщик обязан погасить полученный им кредит путем совершения ежемесячных платежей. " +
                                                                                  "При этом платеж должен быть произведен  ___________ числа либо ранее, " +
                                                                                  "но не более, чем за 10 дней до даты платежа.");
                AddParagraph(body, JustificationValues.Both, new RunProperties(), "2.2. Сумма произведенного платежа, недостаточная для полного погашения всей " +
                                                                                  "задолженности Заемщика, погашает прежде всего издержки Кредитора по " +
                                                                                  "принятию исполнения и принудительному взысканию, затем - проценты за " +
                                                                                  "пользование кредитными ресурсами, " +
                                                                                  "а в оставшейся части - основную сумму долга.");

                doc.AppendChild(body);
                mainPart.Document = doc;
            }
        }
        private static void AddParagraph(Body body, JustificationValues justification, RunProperties runProperties, string text)
        {
            var paragraph = body.AppendChild(new Paragraph()
            {
                ParagraphProperties = new ParagraphProperties()
                {
                    Justification = new Justification() { Val = justification }
                }
            });

            var run = paragraph.AppendChild(new Run() { RunProperties = runProperties });
            run.AppendChild(new Text(text));
        }

        private static void AddParagraph(Body body, JustificationValues justification, List<Run> runs)
        {
            var paragraph = body.AppendChild(new Paragraph()
            {
                ParagraphProperties = new ParagraphProperties()
                {
                    Justification = new Justification() { Val = justification }
                }
            });

            foreach (var run in runs)
            {
                paragraph.AppendChild(run);
            }
        }

        private static Run GenerateRun(RunProperties runProperties, string text)
        {
            var run = new Run() { RunProperties = runProperties };
            run.AppendChild(new Text(text));
            return run;
        }

        private static RunProperties SetBoldRunProperties(RunProperties runProperties)
        {
            runProperties.Bold = new Bold();
            return runProperties;
        }

        private static void FillConcreteContract(DomainCustomerCredit credit)
        {
            using (var wordDoc = WordprocessingDocument.Open("D:/test.docx", true))
            {
                var body = wordDoc.MainDocumentPart.Document.Body;

                var runs = body.Descendants<Run>().ToList();
                var i = 0;
                var len = runs.Count();

                for (; i < len; i++)
                {
                    var run = runs[i];
                    var contractNumOld = FindByInnerText(run, ContractNumberPlace);
                    if (contractNumOld != null)
                    {
                        run.RunProperties.Underline = new Underline() { Val = UnderlineValues.Single };
                        Replace(contractNumOld, ContractNumberPlace, " " + credit.ContractNumber);
                        break;
                    }
                }

                for (; i < runs.Count(); i++)
                {
                    var run = runs[i];
                    var monthOld = FindByInnerText(run, DaymonthPlace);
                    if (monthOld != null)
                    {
                        run.RunProperties.Underline = new Underline() { Val = UnderlineValues.Single };
                        Replace(monthOld, DaymonthPlace, credit.StartDate.ToString("M"));
                        break;
                    }
                }

                for (; i < runs.Count(); i++)
                {
                    var run = runs[i];
                    var yearOld = FindByInnerText(run, YearPlace);
                    if (yearOld != null)
                    {
                        run.RunProperties.Underline = new Underline() { Val = UnderlineValues.Single };
                        Replace(yearOld, YearPlace, " " + credit.StartDate.ToString("yyyy"));
                        break;
                    }
                }

                //customerPlace
                for (; i < runs.Count(); i++)
                {
                    var run = runs[i];
                    var customerOld = FindByInnerText(run, CustomerPlace);
                    if (customerOld != null)
                    {
                        run.RunProperties.Underline = new Underline() { Val = UnderlineValues.Single };
                        Replace(customerOld, CustomerPlace, string.Format("{0} {1}{2}", credit.Customer.Lastname, credit.Customer.Firstname, 
                            credit.Customer.Patronymic != null ? " " + credit.Customer.Patronymic : ""));
                        break;
                    }
                }
                //credit name
                for (; i < runs.Count(); i++)
                {
                    var run = runs[i];
                    var itemOld = FindByInnerText(run, CreditPlace);
                    if (itemOld != null)
                    {
                        run.RunProperties.Underline = new Underline() { Val = UnderlineValues.Single };
                        Replace(itemOld, CreditPlace, credit.Credit.Name);
                        break;
                    }
                }
                //sum
                FindAndReplace(ref i, runs, SincePlace, credit.StartDate.ToString("dd.MM.yyyy"));
                for (; i < runs.Count(); i++)
                {
                    var run = runs[i];
                    var itemOld = FindByInnerText(run, CreditSumPlace);
                    if (itemOld != null)
                    {
                        run.RunProperties.Underline = new Underline() { Val = UnderlineValues.Single };
                        Replace(itemOld, CreditSumPlace, credit.CreditSum.ToString());
                        break;
                    }
                }
                //since
                FindAndReplace(ref i, runs, SincePlace, credit.StartDate.ToString("dd.MM.yyyy"));
                
                //end date
                FindAndReplace(ref i, runs, UntilPlace, credit.EndDate.ToString("dd.MM.yyyy"));

                //percent rate
                FindAndReplace(ref i, runs, PercentRatePlace, credit.Credit.PercentRate.ToString());

                //payment day
                FindAndReplace(ref i, runs, PaymentDayPlace, credit.StartDate.ToString("dd"));
            }
        }

        private static void FindAndReplace(ref int i, List<Run> runs, string textToReplace, string newText, UnderlineValues underlineValue = UnderlineValues.Single)
        {
            for (; i < runs.Count(); i++)
            {
                var run = runs[i];
                var itemOld = FindByInnerText(run, textToReplace);
                if (itemOld == null)
                {
                    continue;
                }
                run.RunProperties.Underline = new Underline() { Val = underlineValue};
                Replace(itemOld, textToReplace, newText);
                return;
            }
        }


        private static Text FindByInnerText(Run run, string innerText)
        {
            return run.Descendants<Text>().FirstOrDefault(x => x.Text.Contains(innerText));
        }

        private static void Replace(Text text, string oldText, string newText)
        {
            var regex = new Regex(oldText);
            text.Text = regex.Replace(text.Text, newText, 1);
        }
    }
}
