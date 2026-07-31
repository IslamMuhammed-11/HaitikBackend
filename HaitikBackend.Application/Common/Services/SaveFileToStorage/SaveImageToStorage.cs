using HaitikBackend.Application.Common.FileModels;
using HaitikBackend.Application.Common.Interfaces.FileUpload;

namespace HaitikBackend.Application.Common.Services.SaveFileToStorage;

public class SaveImageToStorage
{
    private readonly IFileStorage _fileStorage;
    private readonly FileUpload _fileUpload;

    public SaveImageToStorage(IFileStorage fileStorage, FileUpload fileUpload)
    {
        _fileStorage = fileStorage;
        _fileUpload = fileUpload;
    }


    

}
