namespace TrionControlPanel.Classes.Models
{
    public interface IUserModelWeb
    {
        int Access { get; set; }
        string CurrentIP { get; set; }
        string Email { get; set; }
        int Expansion { get; set; }
        int Id { get; set; }
        bool IsDonor { get; set; }
        bool IsLogediIn { get; set; }
        string LastIP { get; set; }
        string Name { get; set; }
    }
}