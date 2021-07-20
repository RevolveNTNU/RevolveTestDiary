using Avalonia.Controls;
using ReactiveUI;
using RevolveTestDiaryXf.Interfaces;
using RevolveTestDiaryXf.Models;
using RevolveTestDiaryXf.Views;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace RevolveTestDiaryXf.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private SynchronizationContext _syncContext = SynchronizationContext.Current;

        private ObservableCollection<ITestDay> testDays;

        public ObservableCollection<ITestDay> TestDays
        {
            get
            {
                return testDays;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref testDays, value);
            }
        }

        public MainWindowViewModel()
        {
            TestDays = new ObservableCollection<ITestDay>{
                new TestDay(new TestLocation("NONE"), new Person("ESO/ASR"))
            };
        }

        public async void SaveTestDayCommand()
        {
            var file = await GetJsonFileName();

            if (file == null || TestDays.Count == 0)
                return;

            var testDay = TestDays.First() as TestDay;

            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(testDay, options);

            await File.WriteAllTextAsync(file, jsonString);
        }

        private async Task<string> GetJsonFileName()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filters.Add(new FileDialogFilter() { Name = "JSON", Extensions = { "json" } });
            return await saveFileDialog.ShowAsync(MainWindow.Instance).ConfigureAwait(false);
        }
    }
}
