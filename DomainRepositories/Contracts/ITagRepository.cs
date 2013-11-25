using System;
using DomainModels.Models;

namespace DomainRepositories.Contracts
{
    public interface ITagRepository
    {
        Tag GetByName(String name);
    }
}
