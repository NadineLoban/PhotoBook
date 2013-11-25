using System;
using System.IO;
using DomainModels.Models;

namespace ServicesContracts.Contracts
{
    public interface IPhotoUploadService
    {
        String SetEffects(string publicId, string effectName, Int32 value);
        String SetEffects(string publicId, string effectName);
        ImageData UploadPhoto(String fileName, Stream originalPhoto);
        void DeletePhoto(String publicId);
        ImageData GetImageDataById(int imageDataId);
    }
}
