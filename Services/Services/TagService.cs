using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DomainModels.Models;
using DomainRepositories;
using DomainViewModels.ViewModels;
using ServicesContracts.Contracts;

namespace Services.Services
{
    public class TagService : ITagService
    {
        private readonly TagRepository tagRepository = new TagRepository();

        public List<Tag> FindTagByName(String term) 
        {
            return tagRepository.GetBy(tag => tag.Name.StartsWith(term)).ToList();
        }

        public List<PhotoCommonInfoViewModel> GetPhotosByTag(String name, String login)
        {
            var photoService = new PhotoService();
            var listOfPhotos = new List<PhotoCommonInfoViewModel>();
            var tag = tagRepository.GetByName(name);
            var photos = tag.Photos.ToList();

            foreach (var photo in photos)
            {
                if (login != null)
                {
                    listOfPhotos.Add(new PhotoCommonInfoViewModel(photo, login, photoService.IsLiked(photo.Id, login),
                                                                  photoService.IsDisliked(photo.Id, login)));
                }
            }
            return listOfPhotos;
        }

        public List<TagViewModel> GetAllTags()
        {
            var tagViewModels = new List<TagViewModel>();
            var tags = tagRepository.GetAll();

            foreach (var tag in tags)
            {
                tagViewModels.Add(new TagViewModel(tag));
            }
            return tagViewModels;
        }

        public void AddTag(TagAddViewModel tagAddViewModel)
        {
            var tag = new Tag {Name = tagAddViewModel.Name};
            tagRepository.Create(tag);
        }

        public IEnumerable<Tag> GetTopTags(int count)
        {
            return tagRepository.GetAll().OrderByDescending(tag => tag.Count).Take(count);
        }

        public Tag CheckTag(String name)
        {
            Tag tag = tagRepository.GetByName(name);
            if (tag != null)
            {
                tag.Count++;
                tagRepository.Update(tag);
                return tag;
            }
            return tagRepository.Create(new Tag()
                {
                    Name = name,
                    Count = 1
                });
        }

    }
}
