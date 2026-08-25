using ClosedXML.Excel;
using HaitikBackend.Application.Abstractions;
using HaitikBackend.Application.Common.Models.BulkOrdersModel;

namespace HaitikBackend.Infrastructure.Implementaions;

public class DocumentImporter : IDocumentImporter
{
    public BulkUploadResult Parse(Stream file)
    {
        const string CountryKey = "+20";
        using var workbook = new XLWorkbook(file);

        var worksheet = workbook.Worksheet(1);

        var models = new List<BulkOrderModel>();

        int rejectedRows = 0;

        foreach (var row in worksheet.RowsUsed().Skip(1))
        {

            try
            {
                var phoneNumber = CountryKey + row.Cell(1).GetValue<string>();
                var latitude = row.Cell(2).GetValue<double>();
                var longitude = row.Cell(3).GetValue<double>();

                


                models.Add(new BulkOrderModel(latitude, longitude, phoneNumber));

                Console.WriteLine(
                    $" {phoneNumber} | {latitude} | {longitude}"
                );

            }
            catch (Exception ex)
            {
                rejectedRows++;

                Console.WriteLine("Rejected Row");
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }


        return new BulkUploadResult(models, rejectedRows);
    }
}
