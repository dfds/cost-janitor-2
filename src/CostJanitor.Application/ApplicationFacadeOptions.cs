using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace CostJanitor.Application
{
    public sealed class ApplicationFacadeOptions
    {
        [Required]
        public IConfigurationSection ConnectionStrings { get; set; }

        public bool EnableAutoMigrations { get; set; } = false;
    }
}