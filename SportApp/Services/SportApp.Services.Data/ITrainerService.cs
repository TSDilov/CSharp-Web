﻿namespace SportApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using SportApp.Web.ViewModels.Trainers;

    public interface ITrainerService
    {
        Task<IEnumerable<T>> GetAll<T>();

        Task CreateAsync(CreateTrainerInputModel input, string userId, string imagePath);

        T GetById<T>(int id);
    }
}