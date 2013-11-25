using System;
using System.IO;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DomainModels.Models;
using DomainRepositories;
using ServicesContracts.Contracts;

namespace PhotoUploadService
{
    public class PhotoUploadService : IPhotoUploadService
    {
        private static Api api;
        private static Cloudinary cloudinary;
        private readonly ImageDataRepository imageDataRepository = new ImageDataRepository();

        public PhotoUploadService(Account account)
        {
            cloudinary = new Cloudinary(account);
            api = new Api(account);
        }

        protected Cloudinary Cloudinary
        {
            get { return cloudinary; }
        }

        public String SetEffects(string publicId, string effectName, Int32 value)
        {

            Transformation transformation = new Transformation().Effect(effectName, value);
            GetResourceResult result = Cloudinary.GetResource(new GetResourceParams(publicId));
            String url = api.UrlImgUp.Transform(transformation).BuildUrl("v" + result.Version + "/" + publicId + "." +  result.Format);
            return url;
        }

        public string SetEffects(string publicId, string effectName)
        {
            Transformation transformation = new Transformation().Effect(effectName);
            GetResourceResult result = Cloudinary.GetResource(new GetResourceParams(publicId));
            String url = api.UrlImgUp.Transform(transformation).BuildUrl("v" + result.Version + "/" + publicId + "." + result.Format);
            return url;
        }

        public ImageData UploadPhoto(string fileName, Stream stream)
        {
            var result = Cloudinary.Upload(new ImageUploadParams
            {
                File = new FileDescription(fileName,
                    stream),
                Tags = "PhotoBookApplication",
                Transformation = new Transformation().Width(612).Height(612).Crop("pad").Background("#000000")
            });

            var imageData = new ImageData
                {
                Bytes = (int)result.Length,
                CreatedAt = DateTime.Now,
                Format = result.Format,
                Height = result.Height,
                Path = result.Uri.AbsolutePath,
                PublicId = result.PublicId,
                ResourceType = result.ResourceType,
                SecureUrl = result.SecureUri.AbsoluteUri,
                Signature = result.Signature,
                Type = result.JsonObj["type"].ToString(),
                Url = result.Uri.AbsoluteUri,
                Version = Int32.Parse(result.Version),
                Width = result.Width,
            };

            var res = Cloudinary.Tag(new TagParams { Tag = "PhotoBookApplication" });
            imageDataRepository.Create(imageData);
            return imageData;
        }

        public void DeletePhoto(String publicId)
        {
            var deleteParam = new DeletionParams(publicId);
            Cloudinary.Destroy(deleteParam);
        }

        public ImageData GetImageDataById(int imageDataId)
        {
            return imageDataRepository.GetById(imageDataId);
        }
    }
}
