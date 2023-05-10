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
using iText.Layout.Layout;
using iText.Layout.Renderer;

namespace Generator.GemeratorServices
{
    public class PdfGeneratorService
    {
        public static byte[] GenerateGreetingCardPDF(FileStream backgroundFileStream, string additionalText)
        {
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = new PdfWriter(memoryStream);
            PageSize pageSize = new PageSize(PageSize.A6.GetWidth(), PageSize.A6.GetHeight());
            PdfDocument pdf = new PdfDocument(writer);
            pdf.SetDefaultPageSize(pageSize);
            Document document = new Document(pdf);

            // Загрузка пользовательского шрифта
            string fontPath = System.IO.Path.Combine(Environment.CurrentDirectory, "shrift.ttf");
            PdfFont font = PdfFontFactory.CreateFont(fontPath);
            document.SetFont(font);

            Image victoryImage = new Image(ImageDataFactory.Create("Victory.png")); //победили в 45-м
            victoryImage
                .SetWidth(pageSize.GetWidth() / 1.4f)
                .SetHeight(pageSize.GetHeight() / 20)
                .SetFixedPosition(40, 370);
            victoryImage.ScaleToFit(victoryImage.GetImageWidth(), victoryImage.GetImageHeight());
            document.Add(victoryImage);

            Image frameImage = new(ImageDataFactory.Create("frame.png"));   // рамка
            frameImage
                .SetWidth(pageSize.GetWidth() / 1.4f)
                .SetHeight(pageSize.GetHeight() / 1.3f)
                .SetFixedPosition(40, 35);
            frameImage.ScaleToFit(frameImage.GetImageWidth(), frameImage.GetImageHeight());
            document.Add(frameImage);

            Image backgroundImage = new Image(ImageDataFactory.Create(ReadFully(backgroundFileStream)));   //фото челика 
            backgroundImage
                .SetWidth(pageSize.GetWidth() / 1.6f)
                .SetHeight(pageSize.GetHeight() / 1.7f)
                .SetFixedPosition(53, 90);
            backgroundImage.ScaleToFit(backgroundImage.GetImageWidth(), backgroundImage.GetImageHeight());
            document.Add(backgroundImage);

            Image rectangleImage = new(ImageDataFactory.Create("Rectangle.png")); // лента
            rectangleImage
                .SetWidth(pageSize.GetWidth() / 1.6f)
                .SetHeight(pageSize.GetHeight() / 7f)
                .SetFixedPosition(53, 80);
            rectangleImage.ScaleToFit(rectangleImage.GetImageWidth(), rectangleImage.GetImageHeight());
            document.Add(rectangleImage);

            Image downText = new(ImageDataFactory.Create("Group.png"));
            downText
                .SetWidth(pageSize.GetWidth() / 1.6f)
                .SetHeight(pageSize.GetHeight() / 14f)
                .SetFixedPosition(53, 50);
            downText.ScaleToFit(downText.GetImageWidth(), downText.GetImageHeight());
            document.Add(downText);//Group.png

            Canvas canvas = new Canvas(new PdfCanvas(pdf.AddNewPage(pageSize)), new Rectangle(pageSize));

            Image openerImage = new Image(ImageDataFactory.Create("image2.png"));
            openerImage.SetWidth(pageSize.GetHeight())
                       .SetHeight(pageSize.GetWidth())
                       .SetRotationAngle(Math.PI / 2);

            canvas.Add(openerImage);

            //Помещаем текст на вторую страницу
            document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
            Paragraph additionalParagraph = new Paragraph(additionalText)
                .SetRotationAngle(Math.PI / 2)
                .SetFont(font)
                .SetFontSize(10)
                .SetWidth(pageSize.GetHeight() - 260)
                .SetMarginTop(60f)
                .SetMarginBottom(10f)
                .SetMarginLeft(200);

            if (additionalText.Length > 240)
            {
                additionalParagraph.SetFontSize(8);
            }

            document.Add(additionalParagraph);
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