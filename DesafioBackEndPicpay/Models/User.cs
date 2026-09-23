using System.ComponentModel.DataAnnotations;
using DesafioBackEndPicpay.Enum;
using Microsoft.EntityFrameworkCore;

namespace DesafioBackEndPicpay.Models;

[Index(nameof(Email), IsUnique = true)]
[Index(nameof(Documento), IsUnique = true)]
public class User
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(50, ErrorMessage = "O nome deve conter entre 5 a 50 caracteres", MinimumLength = 5)]
    public string Name { get; set; }
    
    [Required]
    [StringLength(11, ErrorMessage = "O nome deve conter 11 caracteres"), Display(Name = "CPF ou CNPJ")]
    public string Documento { get; set; }     
    
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string PassWord { get; set; }

    [Required]
    public decimal Balance { get; set; }

    public TypeUser TypeUser { get; set; }
}