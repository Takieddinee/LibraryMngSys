using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.ViewModels
{
    public class AssetIndexModel
    {
        public int Id { get; set; }
        public String ImageUrl { get; set; }
        public String Title { get; set; }
        public String AuthorOrDirector { get; set; }
        public String Type { get; set; }
        public String DeweyCallNumber { get; set; }
        public int NumberOfCopies { get; set; }
    }
}
