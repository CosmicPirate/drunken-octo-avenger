using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AspTestTask.Models
{
    public class SiteMetadata
    {
        [Required(ErrorMessage = "Нужно ввести имя сайта")]
        public string SiteName { get; set; }
    }

    [MetadataType(typeof(SiteMetadata))]
    public partial class Site
    {
    }
}