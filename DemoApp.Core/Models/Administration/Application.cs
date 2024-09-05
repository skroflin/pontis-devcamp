﻿namespace DemoApp.Core.Models.Administration
{
    public class Application
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserCreated { get; set; }
        public DateTime DateCreated { get; set; }
        public string? UserModified { get; set; }
        public string? DateModified { get; set; }
    }
}
