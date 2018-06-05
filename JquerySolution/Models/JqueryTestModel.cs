using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JquerySolution.Models
{
    public class JqueryTestModel
    {

        [Required(ErrorMessage = "Enter Rno"), RegularExpression(@"^[0-9]+$", ErrorMessage = "Rno in numerically")]
        public int Rno { get; set; }

        [Required(ErrorMessage = "Enter Student Name"), RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Name in alphabetically")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter MoileNo"), RegularExpression(@"^[0-9]+$", ErrorMessage = "Rno in numerically")]
        public double MobileNo { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Enter EmailID")]
        public string EmailId { get; set; }

    }
}