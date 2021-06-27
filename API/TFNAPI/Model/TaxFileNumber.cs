using System.ComponentModel.DataAnnotations;

namespace TFNAPI.Model
{
    public class TaxFileNumber 
    {
        [Required]
        [StringLength(9, ErrorMessage = "TFN must be of length 8 or 9.", MinimumLength = 8)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "TFN must be numeric containing charachters [0 - 9] only.")]
        public string TFN { get; set; }
       
    }
}
