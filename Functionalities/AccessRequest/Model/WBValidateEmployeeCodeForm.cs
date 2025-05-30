using System.ComponentModel.DataAnnotations;

namespace GestorCorrespondencia.Frontend.Functionalities.AccessRequest.Model;
public class WBValidateEmployeeCodeForm
{
    [Required(ErrorMessage = "Codigo requerido")]
    public int? EmployeeCode { get; set; }
}