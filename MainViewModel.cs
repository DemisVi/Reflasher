using System;
using System.Buffers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Reflasher;

internal class MainViewModel : INotifyPropertyChanged
{
    private readonly object _ku1lock = new();
    private readonly object _ku2lock = new();
    private readonly object _ku3lock = new();
    private readonly object _ku4lock = new();
    private readonly static object _syncLock = new();

    public ObservableCollection<string> CU1Log { get; } = new();
    public ObservableCollection<string> CU2Log { get; } = new();
    public ObservableCollection<string> CU3Log { get; } = new();
    public ObservableCollection<string> CU4Log { get; } = new();

    private readonly CUCollection ContactUnits = CUCollection.Instance;

    public string? CU1Label => ContactUnits.CU1;
    public string? CU2Label => ContactUnits.CU2;
    public string? CU3Label => ContactUnits.CU3;
    public string? CU4Label => ContactUnits.CU4;

    private Brush _cu1Color = Brushes.White;
    private Brush _cu2Color = Brushes.White;
    private Brush _cu3Color = Brushes.White;
    private Brush _cu4Color = Brushes.White;

    private readonly Writer _writer1;
    private readonly Writer _writer2;
    private readonly Writer _writer3;
    private readonly Writer _writer4;

    public event PropertyChangedEventHandler? PropertyChanged;

    public MainViewModel()
    {
        BindingOperations.EnableCollectionSynchronization(CU1Log, _ku1lock);
        BindingOperations.EnableCollectionSynchronization(CU2Log, _ku2lock);
        BindingOperations.EnableCollectionSynchronization(CU3Log, _ku3lock);
        BindingOperations.EnableCollectionSynchronization(CU4Log, _ku4lock);

        _writer1 = new(new StatusReporter(CU1Log.Add, SetCU1Color, CU1Log.Clear), CU1Label, _syncLock);
        _writer2 = new(new StatusReporter(CU2Log.Add, SetCU2Color, CU2Log.Clear), CU2Label, _syncLock);
        _writer3 = new(new StatusReporter(CU3Log.Add, SetCU3Color, CU3Log.Clear), CU3Label, _syncLock);
        _writer4 = new(new StatusReporter(CU4Log.Add, SetCU4Color, CU4Log.Clear), CU4Label, _syncLock);

        _writer1.Run();
        _writer2.Run();
        _writer3.Run();
        _writer4.Run();
    }

    protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string? propertyName = null)
    {
        if (!Equals(field, newValue))
        {
            field = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return true;
        }
        return false;
    }

    protected void OnPropertyChanged([CallerMemberName] string? property = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

    public Brush CU1Color { get => _cu1Color; set => SetProperty(ref _cu1Color, value); }
    public Brush CU2Color { get => _cu2Color; set => SetProperty(ref _cu2Color, value); }
    public Brush CU3Color { get => _cu3Color; set => SetProperty(ref _cu3Color, value); }
    public Brush CU4Color { get => _cu4Color; set => SetProperty(ref _cu4Color, value); }

    protected void SetCU1Color(Brush color) => CU1Color = color;
    protected void SetCU2Color(Brush color) => CU2Color = color;
    protected void SetCU3Color(Brush color) => CU3Color = color;
    protected void SetCU4Color(Brush color) => CU4Color = color;
}