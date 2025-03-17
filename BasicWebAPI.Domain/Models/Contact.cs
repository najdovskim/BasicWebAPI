using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebAPI.Domain.Models;
public class Contact
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int ContactId { get; set; }
    public string ContactName { get; set; }

    [ForeignKey("CompanyId")]
    public int CompanyId { get; set; }
    public Company? Company { get; set; } 

    [ForeignKey("CountryId")]
    public int CountryId { get; set; }
    public Country? Country { get; set; }

    public void SetContactId(int contactId)
    {
        ContactId = contactId;
    }
}

