using System.ComponentModel.DataAnnotations;

public class ProfileViewModel
{
    public string UserName { get; set; }
    public string Email { get; set; }

    [Required(ErrorMessage = "Введите текущий пароль")]
    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; }

    [Required(ErrorMessage = "Введите новый пароль")]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "Повторите новый пароль")]
    [Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
    [DataType(DataType.Password)]
    public string ConfirmNewPassword { get; set; }
}