using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using TourPlanner.Models;
using System.Threading.Tasks;
using TourPlanner.Enums;

namespace TourPlanner.Helpers
{
    public static class ReportGenerator
    {
        public static async Task GeneratePDFReportAsync(Tour tour, string filePath, string mapImagePath)
        {
            try
            {

                Document doc = new Document(PageSize.A4, 50, 50, 50, 50);
                PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
                doc.Open();

                // Title
                var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.DARK_GRAY);
                var headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, BaseColor.DARK_GRAY);
                var bodyFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.DARK_GRAY);
                var tableHeaderFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.WHITE);

                // Add title
                var titleParagraph = new Paragraph("Tour Report", titleFont)
                {
                    Alignment = Element.ALIGN_CENTER
                };
                doc.Add(titleParagraph);
                doc.Add(new Paragraph("\n")); // Adding a space

                // Add tour details section
                PdfPTable detailsTable = new PdfPTable(2) { WidthPercentage = 100 };
                detailsTable.SetWidths(new float[] { 1, 2 });

                AddCellToDetailsTable(detailsTable, "Tour Name:", tour.Name, headerFont, bodyFont);
                AddCellToDetailsTable(detailsTable, "Description:", tour.Description, headerFont, bodyFont);
                AddCellToDetailsTable(detailsTable, "Start Location:", tour.StartLocation, headerFont, bodyFont);
                AddCellToDetailsTable(detailsTable, "End Location:", tour.EndLocation, headerFont, bodyFont);
                AddCellToDetailsTable(detailsTable, "Transport Type:", tour.Type.ToString(), headerFont, bodyFont);
                AddCellToDetailsTable(detailsTable, "Distance:", $"{tour.Distance} km", headerFont, bodyFont);
                AddCellToDetailsTable(detailsTable, "Time:", $"{tour.Time} hours", headerFont, bodyFont);

                doc.Add(detailsTable);
                doc.Add(new Paragraph("\n")); // Adding a space

                // Add the captured map image
                if (!string.IsNullOrEmpty(mapImagePath))
                {
                    Image mapImage = Image.GetInstance(mapImagePath);
                    mapImage.Alignment = Element.ALIGN_CENTER;
                    mapImage.ScaleToFit(500f, 500f); // Adjust the scale as needed
                    doc.Add(mapImage);
                    doc.Add(new Paragraph("\n")); // Adding a space
                }

                // Tour Logs Table
                doc.Add(new Paragraph("Tour Logs", headerFont));
                doc.Add(new Paragraph("\n")); // Adding a space

                PdfPTable logsTable = new PdfPTable(6) { WidthPercentage = 100 };
                logsTable.SetWidths(new float[] { 2, 2, 3, 2, 2, 2 });

                // Table headers
                AddCellToHeader(logsTable, "Date", tableHeaderFont);
                AddCellToHeader(logsTable, "Total Time (hours)", tableHeaderFont);
                AddCellToHeader(logsTable, "Comment", tableHeaderFont);
                AddCellToHeader(logsTable, "Difficulty", tableHeaderFont);
                AddCellToHeader(logsTable, "Total Distance (km)", tableHeaderFont);
                AddCellToHeader(logsTable, "Rating", tableHeaderFont);

                // Table rows
                foreach (var log in tour.Logs)
                {
                    AddCellToBody(logsTable, log.Date, bodyFont);
                    AddCellToBody(logsTable, log.TotalTime.ToString(), bodyFont);
                    AddCellToBody(logsTable, log.Comment, bodyFont);
                    AddCellToBody(logsTable, log.Difficulty.ToString(), bodyFont);
                    AddCellToBody(logsTable, log.TotalDistance.ToString(), bodyFont);
                    AddCellToBody(logsTable, log.Rating.ToString(), bodyFont);
                }

                doc.Add(logsTable);
                doc.Close();
            }
            catch
            {
                
            }
        }
        public static async Task GenerateAveragePDFReportAsync(Tour tour, string filePath)
        {
            try
            {
                // Check if the file is already in use
                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    Document doc = new Document(PageSize.A4, 50, 50, 50, 50);
                    PdfWriter.GetInstance(doc, fs);
                    doc.Open();

                    // Title
                    var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.DARK_GRAY);
                    var headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, BaseColor.DARK_GRAY);
                    var bodyFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.DARK_GRAY);
                    var highlightFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.RED);

                    // Add title
                    var titleParagraph = new Paragraph("Tour Average Report", titleFont)
                    {
                        Alignment = Element.ALIGN_CENTER
                    };
                    doc.Add(titleParagraph);
                    doc.Add(new Paragraph("\n")); // Adding a space

                    // Add tour details section
                    PdfPTable detailsTable = new PdfPTable(2) { WidthPercentage = 100 };
                    detailsTable.SetWidths(new float[] { 1, 2 });

                    AddCellToDetailsTable(detailsTable, "Tour Name:", tour.Name, headerFont, bodyFont);
                    AddCellToDetailsTable(detailsTable, "Description:", tour.Description, headerFont, bodyFont);
                    AddCellToDetailsTable(detailsTable, "Start Location:", tour.StartLocation, headerFont, bodyFont);
                    AddCellToDetailsTable(detailsTable, "End Location:", tour.EndLocation, headerFont, bodyFont);
                    AddCellToDetailsTable(detailsTable, "Transport Type:", tour.Type.ToString(), headerFont, bodyFont);
                    AddCellToDetailsTable(detailsTable, "Average Distance:", $"{tour.Logs.Average(log => log.TotalDistance):F2} km", headerFont, highlightFont);
                    AddCellToDetailsTable(detailsTable, "Average Time:", $"{tour.Logs.Average(log => log.TotalTime):F2} hours", headerFont, highlightFont);
                    AddCellToDetailsTable(detailsTable, "Average Rating:", $"{tour.Logs.Average(log => RatingToNumber(log.Rating)):F2}", headerFont, highlightFont);

                    doc.Add(detailsTable);
                    doc.Add(new Paragraph("\n")); // Adding a space

                    // Add explanation of ratings in a table
                    PdfPTable ratingTable = new PdfPTable(5) { WidthPercentage = 100, HorizontalAlignment = Element.ALIGN_LEFT };
                    ratingTable.SetWidths(new float[] { 2, 2, 2, 2, 2 });

                    AddCellToRatingTable(ratingTable, "Rating", headerFont, BaseColor.GRAY);
                    AddCellToRatingTable(ratingTable, "Bad", headerFont, BaseColor.GRAY);
                    AddCellToRatingTable(ratingTable, "Good", headerFont, BaseColor.GRAY);
                    AddCellToRatingTable(ratingTable, "VeryGood", headerFont, BaseColor.GRAY);
                    AddCellToRatingTable(ratingTable, "Excellent", headerFont, BaseColor.GRAY);

                    AddCellToRatingTable(ratingTable, "Value", headerFont, BaseColor.GRAY);
                    AddCellToRatingTable(ratingTable, "4", bodyFont, BaseColor.WHITE);
                    AddCellToRatingTable(ratingTable, "3", bodyFont, BaseColor.WHITE);
                    AddCellToRatingTable(ratingTable, "2", bodyFont, BaseColor.WHITE);
                    AddCellToRatingTable(ratingTable, "1", bodyFont, BaseColor.WHITE);

                    doc.Add(ratingTable);
                    doc.Add(new Paragraph("\n")); // Adding a space

                    doc.Close();
                }
            }
            catch (IOException)
            {
                return;
            }
        }

        private static void AddCellToDetailsTable(PdfPTable table, string header, string value, Font headerFont, Font bodyFont)
        {
            PdfPCell headerCell = new PdfPCell(new Phrase(header, headerFont))
            {
                Border = Rectangle.NO_BORDER,
                Padding = 5,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };
            table.AddCell(headerCell);

            PdfPCell valueCell = new PdfPCell(new Phrase(value, bodyFont))
            {
                Border = Rectangle.NO_BORDER,
                Padding = 5,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };
            table.AddCell(valueCell);
        }

        private static void AddCellToHeader(PdfPTable table, string text, Font font)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, font))
            {
                BackgroundColor = BaseColor.GRAY,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5
            };
            table.AddCell(cell);
        }

        private static void AddCellToBody(PdfPTable table, string text, Font font)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, font))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5
            };
            table.AddCell(cell);
        }
        
        private static void AddCellToRatingTable(PdfPTable table, string text, Font font, BaseColor backgroundColor)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, font))
            {
                BackgroundColor = backgroundColor,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5,
                Border = Rectangle.BOX
            };
            table.AddCell(cell);
        }

        private static float RatingToNumber(Rating rating)
        {
            return rating switch
            {
                Rating.Bad => 4,
                Rating.Good => 3,
                Rating.VeryGood => 2,
                Rating.Excellent => 1,
                _ => 0
            };
        }
    }
}
