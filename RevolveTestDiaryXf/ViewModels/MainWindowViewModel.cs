using Avalonia.Controls;
using Microsoft.VisualBasic.FileIO;
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
            var testDay = new TestDay(new TestLocation("NONE"), new Person("ESO/ASR"));
            testDay.TriggerAutoSaveEvent += SaveTestDay;
            TestDays = new ObservableCollection<ITestDay>
            {
                testDay
            };
        }

        public async void SaveTestDay(object sender, ITestDay testDay)
        {
            var folder = Path.Combine(SpecialDirectories.MyDocuments, "RevolveTestDiary");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            var file = testDay.Timestamp.ToString("d-MM-yy_HH-mm-ss") + ".json";

            if (file == null || testDay == null)
                return;

            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(testDay, options);

            await File.WriteAllTextAsync(Path.Combine(folder, file), jsonString);
        }

        public void SaveAllTestDaysCommand()
        {
            foreach (var testDay in TestDays)
            {
                SaveTestDay(this, testDay);
            }
        }

        public void AddDebriefCommand()
        {

        }

        public void NewDayCommand()
        {
            var testDay = new TestDay(new TestLocation("NONE"), new Person("ESO/ASR"));
            testDay.TriggerAutoSaveEvent += SaveTestDay;
            TestDays.Add(testDay);
        }
    }
}
