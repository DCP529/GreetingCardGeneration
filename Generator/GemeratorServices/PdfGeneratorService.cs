using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using Image = iText.Layout.Element.Image;
using iText.Kernel.Geom;
using iText.Kernel.Font;
using iText.IO.Font;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.IO.Image;

namespace Generator.GemeratorServices
{
    public class PdfGeneratorService
    {
        public static byte[] GenerateGreetingCardPDF(FileStream backgroundFileStream, FileStream mainImageFileStream, string mainText, string additionalText)
        {
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = new PdfWriter(memoryStream);
            PageSize pageSize = new PageSize(PageSize.A6.GetWidth(), PageSize.A6.GetHeight());
            PdfDocument pdf = new PdfDocument(writer);
            pdf.SetDefaultPageSize(pageSize);
            iText.Layout.Document document = new Document(pdf);

            // Загрузка пользовательского шрифта
            string fontPath = System.IO.Path.Combine(Environment.CurrentDirectory, "shrift.ttf");
            PdfFont font = PdfFontFactory.CreateFont(fontPath);
            PdfFont boldFont = PdfFontFactory.CreateFont(System.IO.Path.Combine(Environment.CurrentDirectory, "Bold.ttf"), PdfEncodings.IDENTITY_H);
            document.SetFont(font);
            document.SetBold().SetFont(boldFont);

            Image victoryImage = new Image(iText.IO.Image.ImageDataFactory.Create("Victory.jpg")); //победили в 45-м
            victoryImage
                .SetWidth(pageSize.GetWidth() / 1.4f)
                .SetHeight(pageSize.GetHeight() / 20)
                .SetFixedPosition(40, 370);
            victoryImage.ScaleToFit(victoryImage.GetImageWidth(), victoryImage.GetImageHeight());
            document.Add(victoryImage);

            Image frameImage = new(iText.IO.Image.ImageDataFactory.Create("frame.jpg"));   // рамка
            frameImage
                .SetWidth(pageSize.GetWidth() / 1.4f)
                .SetHeight(pageSize.GetHeight() / 1.3f)
                .SetFixedPosition(40, 35);
            frameImage.ScaleToFit(frameImage.GetImageWidth(), frameImage.GetImageHeight());
            document.Add(frameImage);

            Image backgroundImage = new Image(ImageDataFactory.Create(ReadFully(backgroundFileStream)));   //фото челика 
            backgroundImage
                .SetWidth(pageSize.GetWidth() / 1.8f)
                .SetHeight(pageSize.GetHeight() / 1.9f)
                .SetFixedPosition(62, 115);
            backgroundImage.ScaleToFit(backgroundImage.GetImageWidth(), backgroundImage.GetImageHeight());
            document.Add(backgroundImage);

            Image rectangleImage = new(ImageDataFactory.Create("Rectangle.png")); // лента
            rectangleImage
                .SetWidth(pageSize.GetWidth() / 1.8f)
                .SetHeight(pageSize.GetHeight() / 7f)
                .SetFixedPosition(62, 105);
            rectangleImage.ScaleToFit(rectangleImage.GetImageWidth(), rectangleImage.GetImageHeight());
            document.Add(rectangleImage);

            var text = mainText.Split(" ");

            Paragraph mainParagraph = new Paragraph(text[0]).SetBold()
            .SetTextAlignment(TextAlignment.LEFT)
            .SetFontSize(16f)
            .SetMaxWidth(pageSize.GetWidth() - 20)
            .SetMarginLeft(25f)
            .SetMarginTop(275f)
            .SetMarginBottom(5f);

            document.Add(mainParagraph);

            mainParagraph = new Paragraph(text[1] + " " + text[2]).SetMaxWidth(1)
           .SetTextAlignment(TextAlignment.LEFT)
           .SetFontSize(15f)
           .SetMaxWidth(pageSize.GetWidth() - 20)
           .SetMarginLeft(25f)
           .SetMarginTop(-15f).SetBold();

            document.Add(mainParagraph);

            mainParagraph = new Paragraph(text[3] + " " + text[4] + " " + text[5])
           .SetTextAlignment(TextAlignment.LEFT)
           .SetFontSize(9f)
           .SetMaxWidth(pageSize.GetWidth() - 20)
           .SetMarginLeft(25f)
           .SetMarginTop(-10f)
           .SetMarginBottom(5f)
           .SetFont(font);

            document.Add(mainParagraph);


            Canvas canvas = new Canvas(new PdfCanvas(pdf.AddNewPage(pageSize)), new Rectangle(pageSize));

            Image openerImage = new Image(iText.IO.Image.ImageDataFactory.Create("image2.png"));
            openerImage.SetWidth(pageSize.GetHeight())
                       .SetHeight(pageSize.GetWidth())
                       .SetRotationAngle(Math.PI / 2);

            canvas.Add(openerImage);

            Image mainImage = new Image(ImageDataFactory.Create(ReadFully(mainImageFileStream)));    //фото кому посвящается открытка
            mainImage.SetWidth(pageSize.GetWidth() / 5)
                     .SetHeight(pageSize.GetHeight() / 6)
                     .SetRotationAngle(Math.PI / 2)
                     .SetFixedPosition(20, 340);

            canvas.Add(mainImage);

            Paragraph secondaryParagraph = new Paragraph(additionalText)         //текст открытки 
                .SetFontSize(10)
                .SetRotationAngle(Math.PI / 2)
                .SetFont(font);

            secondaryParagraph.SetMaxWidth(pageSize.GetHeight()).SetMarginTop(60f).SetMarginBottom(10f).SetMarginLeft(200);
            document.Add(secondaryParagraph);

            canvas.Add(secondaryParagraph);

            document.Close();

            return memoryStream.ToArray();
        }

        private static byte[] ReadFully(FileStream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}