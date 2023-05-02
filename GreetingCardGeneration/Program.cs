using Generator.GemeratorServices;

string image1Path = "image1.jpg";

FileStream bachgroundFileStream = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, image1Path), FileMode.Open, FileAccess.Read);
string secondaryText = "Мой родной, мой любимый сынок Артур. Отважный российский солдат, молю Господа нашего оберегать тебя. Горжусь тобой, я всегда рядом, сердцем и душой. Победа будет за нами! Ждем тебя домой, родной."; // Дополнительный текст

byte[] pdfBytes = PdfGeneratorService.GenerateGreetingCardPDF(bachgroundFileStream, secondaryText);
File.WriteAllBytes("greeting_card.pdf", pdfBytes);

Console.WriteLine("Поздравительная открытка успешно создана в файле greeting_card.pdf");