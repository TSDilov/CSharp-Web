﻿namespace ReceptionApp.Services.Data
{
    using System.Collections.Generic;

    public interface ICategoriesService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllCategoriesAsKeyValuePairs();
    }
}
