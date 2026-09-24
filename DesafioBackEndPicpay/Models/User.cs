using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DesafioBackEndPicpay.Enum;
using Microsoft.EntityFrameworkCore;

namespace DesafioBackEndPicpay.Models;

[Index(nameof(Email), IsUnique = true)]
[Index(nameof(Documento), IsUnique = true)]
public class User
{
    [Key]
    [JsonIgnore]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "O campo é obrigatorio!")]
    [StringLength(50, ErrorMessage = "O nome deve conter entre 5 a 50 caracteres", MinimumLength = 5)]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "O campo é obrigatorio!")]
    [Length(11,11, ErrorMessage = "O documento deve conter 11 caracteres")]
    public string Documento { get; set; }     
    
    [Required(ErrorMessage = "O campo é obrigatorio!")]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "O campo é obrigatorio!")]
    [DataType(DataType.Password)]
    public string PassWord { get; set; }

    [Required(ErrorMessage = "O campo é obrigatorio!")]
    public decimal Balance { get; set; }

    public TypeUser TypeUser { get; set; }
}