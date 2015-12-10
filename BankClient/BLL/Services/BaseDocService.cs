using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Wordprocessing;

namespace BLL.Services
{
    public class BaseDocService
    {
        protected void AddParagraph(Body body, JustificationValues justification, RunProperties runProperties, string text)
        {
            SetFontRunProperties(runProperties);
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

        protected void AddParagraph(Body body, JustificationValues justification, List<Run> runs)
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

        protected Run GenerateRun(RunProperties runProperties, string text)
        {
            SetFontRunProperties(runProperties);
            var run = new Run() { RunProperties = runProperties };
            run.AppendChild(new Text(text));
            return run;
        }

        protected void SetFontRunProperties(RunProperties runProperties)
        {
            runProperties.RunFonts = new RunFonts()
            {
                Ascii = "Times New Roman"
            };
            runProperties.FontSize = new FontSize()
            {
                Val = "24"
            };
        }

        protected RunProperties SetBoldRunProperties(RunProperties runProperties)
        {
            runProperties.Bold = new Bold();
            return runProperties;
        }

        protected void FindAndReplace(ref int i, List<Run> runs, string textToReplace, string newText, UnderlineValues underlineValue = UnderlineValues.Single)
        {
            for (; i < runs.Count(); i++)
            {
                var run = runs[i];
                var itemOld = FindByInnerText(run, textToReplace);
                if (itemOld == null)
                {
                    continue;
                }
                run.RunProperties.Underline = new Underline() { Val = underlineValue };
                Replace(itemOld, textToReplace, newText);
                return;
            }
        }

        protected static Text FindByInnerText(Run run, string innerText)
        {
            return run.Descendants<Text>().FirstOrDefault(x => x.Text.Contains(innerText));
        }

        protected static void Replace(Text text, string oldText, string newText)
        {
            var regex = new Regex(oldText);
            text.Text = regex.Replace(text.Text, newText, 1);
        }
    }
}