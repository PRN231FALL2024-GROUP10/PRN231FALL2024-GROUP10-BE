using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JobScial.DAL.Models;

public partial class Report
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int? ReportId { get; set; }

    public string? Reason { get; set; }

    public int? ReportType { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public virtual Post? Report1 { get; set; }

    public virtual Comment? ReportNavigation { get; set; }
}
