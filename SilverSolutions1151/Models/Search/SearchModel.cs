using System.ComponentModel.DataAnnotations;

namespace SilverSolutions1151.Models.Search
{
    public class SearchModel
    {
        public string Action
        {
            get;
            set;
        }

        public string text
        {
            get;
            set;
        }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd'/'MM'/'yyyy}")]
        public DateTime? from
        {
            get;
            set;
        }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd'/'MM'/'yyyy}")]
        public DateTime? to
        {
            get;
            set;
        }
    }
}
