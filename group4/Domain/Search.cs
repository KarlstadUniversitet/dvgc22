using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Search
    {
        [StringLength(30, ErrorMessage="Maximum search length is 30 characters"),Required,Display(Name="Search word", Prompt="Example 'DVGC22'"), RegularExpression(@"^[ \w\d\/å/ä/ö/Å/Ä/Ö]+$", ErrorMessage="Search value can only contain alphanumeric characters")]
        public string SearchWord { get; set; }

    }
}
