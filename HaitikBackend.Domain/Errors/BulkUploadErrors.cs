using HaitikBackend.Domain.Enums;

namespace HaitikBackend.Domain.Errors;

public static class BulkUploadErrors
{
    public static Error BatchNotFound(int id) => Error.Create("BulkUpload.BatchNotFound", $"Bulk upload batch with id {id} was not found.", enErrorTypes.NotFound);

    public static Error RejectedRowNotFound(int id) => Error.Create("BulkUpload.RejectedRowNotFound", $"Rejected row with id {id} was not found.", enErrorTypes.NotFound);

    public static Error InvalidFileFormat => Error.Create("BulkUpload.InvalidFormat", "The uploaded file format is invalid.", enErrorTypes.Validation);

    public static Error UploadFailed => Error.Create("BulkUpload.Failed", "Bulk upload processing failed.", enErrorTypes.Conflict);
}
