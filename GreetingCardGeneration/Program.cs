using Generator.GemeratorServices;

string image1Path = "image1.png";

FileStream bachgroundFileStream = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, image1Path), FileMode.Open, FileAccess.Read);
string secondaryText = "День Победы –  особый праздник для всех. В годы испытаний страна в едином порыве поднялась на борьбу с врагом. Одна на всех беда сроднила людей, пробудила патриотизм, героизм и стойкость. Народ одержал в войне Великую Победу, отстояв независимость Родины. День Победы – наполняет сердца гордостью."; // Дополнительный текст

byte[] pdfBytes = PdfGeneratorService.GenerateGreetingCardPDF(bachgroundFileStream, secondaryText);
File.WriteAllBytes("greeting_card.pdf", pdfBytes);

Console.WriteLine("Поздравительная открытка успешно создана в файле greeting_card.pdf");