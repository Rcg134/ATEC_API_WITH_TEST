using System;
using System.Collections.Generic;

namespace ATEC_API.DomainModels.HRISDomainModel;

public partial class TblCert
{
    public int Id { get; set; }

    public string? EmpNo { get; set; }

    public string? Name { get; set; }

    public string? TrainingTitle { get; set; }

    public string? Location { get; set; }

    public string? Operation { get; set; }

    public string? Department { get; set; }

    public string? CustomerId { get; set; }

    public int? RecipeCode { get; set; }

    public string? RecipeId { get; set; }

    public string? RecipeDescription { get; set; }

    public DateTime? QualDate { get; set; }

    public DateTime? RequalDate { get; set; }

    public bool Certification { get; set; }

    public DateTime? CertQualDate { get; set; }

    public DateTime? CertRequalDate { get; set; }

    public string? TrainingFee { get; set; }

    public bool Contract { get; set; }

    public DateTime? ContractQualDate { get; set; }

    public DateTime? ContractRequalDate { get; set; }

    public int? Numberofhours { get; set; }

    public DateTime? DateHired { get; set; }

    public string? Employer { get; set; }

    public string? IntialQualDate { get; set; }

    public string? Destination { get; set; }

    public string? Superior { get; set; }

    public string? Trainer { get; set; }

    public string? Exam { get; set; }

    public string? Quality { get; set; }

    public string? SuplementalTraining { get; set; }

    public bool? Active { get; set; }
}
