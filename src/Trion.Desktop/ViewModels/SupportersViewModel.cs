using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Trion.Desktop.Models;
using Trion.Desktop.Services;

namespace Trion.Desktop.ViewModels;

public partial class SupportersViewModel : ObservableObject
{
    private readonly ISupportersService  _supporters;
    private readonly ILocalizationService _loc;

    [ObservableProperty] private bool   _isLoading    = true;
    [ObservableProperty] private bool   _hasError;
    [ObservableProperty] private string _errorMessage = "";
    [ObservableProperty] private int    _totalCount;

    public ObservableCollection<SupporterTierGroup> TierGroups { get; } = [];
    public ILocalizationService Loc => _loc;

    public bool HasNoSupporters => !IsLoading && !HasError && TierGroups.Count == 0;

    partial void OnIsLoadingChanged(bool value)   => OnPropertyChanged(nameof(HasNoSupporters));
    partial void OnHasErrorChanged(bool value)    => OnPropertyChanged(nameof(HasNoSupporters));

    private static readonly string[] TierOrder = ["legend", "champion", "guardian", "support"];

    public SupportersViewModel(ISupportersService supporters, ILocalizationService loc)
    {
        _supporters = supporters;
        _loc        = loc;
        _ = LoadAsync();
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        HasError  = false;
        IsLoading = true;
        await LoadAsync();
    }

    private async Task LoadAsync()
    {
        try
        {
            var all = await _supporters.GetSupportersAsync();

            TierGroups.Clear();
            TotalCount = all.Count;

            foreach (var tier in TierOrder)
            {
                var members = all.Where(s => s.Tier == tier).ToList();
                if (members.Count == 0) continue;

                var group = new SupporterTierGroup(tier);
                foreach (var m in members)
                    group.Members.Add(m);

                TierGroups.Add(group);
            }
        }
        catch
        {
            HasError     = true;
            ErrorMessage = "Could not load supporters. Check your connection.";
        }
        finally
        {
            IsLoading = false;
        }
    }
}
