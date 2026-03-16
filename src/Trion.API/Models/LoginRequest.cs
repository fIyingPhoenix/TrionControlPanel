using System.ComponentModel.DataAnnotations;

namespace Trion.API.Models;

public sealed record LoginRequest(
    [Required, EmailAddress] string Email,
    [Required, MinLength(1)] string Password);
