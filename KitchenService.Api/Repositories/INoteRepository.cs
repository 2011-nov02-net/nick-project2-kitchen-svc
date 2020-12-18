using System;
using System.Collections.Generic;
using KitchenService.Api.Model;

namespace KitchenService.Api.Repositories
{
    public interface INoteRepository
    {
        void Add(Note note);

        IEnumerable<Note> GetAll(DateTime? since = null);

        IEnumerable<Note> GetAllByUser(int userId);

        IEnumerable<User> GetAllUsers();

        IEnumerable<Tag> GetAllTags();

        Note GetById(int id);

        void Remove(Note note);
    }
}
