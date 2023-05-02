using Generator.GemeratorServices;

string image1Path = "image1.jpg";
string image2Path = "face.jpg"; 

FileStream bachgroundFileStream = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, image1Path), FileMode.Open, FileAccess.Read);
FileStream mainFileStream = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, image2Path), FileMode.Open, FileAccess.Read);   

string mainText = "Халапсин Николай Викторович 16.11.1913 - 14.11.1995";
string secondaryText = "Мой родной, мой любимый сынок Артур. Отважный российский солдат, молю Господа нашего оберегать тебя. Горжусь тобой, я всегда рядом, сердцем и душой. Победа будет за нами! Ждем тебя домой, родной."; // Дополнительный текст

byte[] pdfBytes = PdfGeneratorService.GenerateGreetingCardPDF(bachgroundFileStream, mainFileStream, mainText, secondaryText);
System.IO.File.WriteAllBytes("greeting_card.pdf", pdfBytes);

Console.WriteLine("Поздравительная открытка успешно создана в файле greeting_card.pdf");