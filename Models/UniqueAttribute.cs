using System;

namespace demo_project.Models
{
    internal class UniqueAttribute : Attribute
    {
        public string ErrorMessage { get; set; }
    }
}