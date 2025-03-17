using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebAPI.Domain.Models;
public class Company
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }

    public ICollection<Contact>? Contacts { get; set; }


    public void SetCompanyId(int companyId)
    {
        CompanyId = companyId;
    }
}
