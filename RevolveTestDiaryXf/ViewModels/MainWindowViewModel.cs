﻿using Avalonia.Controls;
using Microsoft.VisualBasic.FileIO;
using ReactiveUI;
using RevolveTestDiaryXf.Models;
using RevolveTestDiaryXf.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Text.Json;

namespace RevolveTestDiaryXf.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private readonly HttpClient client = new HttpClient();

        private ObservableCollection<TestDay> testDays;

        public ObservableCollection<TestDay> TestDays
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

        private TestDay _selectedTestDay;

        public TestDay SelectedTestDay
        {
            get { return _selectedTestDay; }
            set { _selectedTestDay = value; }
        }

        public int TestPhaseId { get; set; }
        public MainWindowViewModel()
        {
            var testDay = new TestDay(new TestLocation("NONE"), new Person("ESO/ASR"));
            testDay.TriggerAutoSaveEvent += SaveTestDay;
            testDay.CloseTestDayEvent += CloseTestDay;
            TestDays = new ObservableCollection<TestDay>
            {
                testDay
            };
        }

        public async void SaveTestDay(object sender, TestDay testDay)
        {
            var folder = Path.Combine(SpecialDirectories.MyDocuments, "RevolveTestDiary");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            var file = testDay.Timestamp.ToString("d-MM-yy_HH-mm-ss") + ".json";

            if (file == null || testDay == null)
                return;

            var options = new JsonSerializerOptions { WriteIndented = true, };
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

        public async void LoadTestDayFromFileCommand()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            var files = await openFileDialog.ShowAsync(MainWindow.Instance);

            foreach (var file in files)
            {
                if (File.Exists(file))
                {
                    using (var stream = File.OpenRead(file))
                    {
                        if (stream != null)
                        {
                            try
                            {
                                TestDay testDay = await JsonSerializer.DeserializeAsync(stream, typeof(TestDay)) as TestDay;
                                testDay.TriggerAutoSaveEvent += SaveTestDay;
                                testDay.CloseTestDayEvent += CloseTestDay;

                                foreach (var session in testDay.Sessions)
                                {
                                    session.TriggerAutoSaveEvent += testDay.TriggerAutoSaveFromSession;
                                    foreach (var entry in session.SessionEntries)
                                    {
                                        entry.TriggerAutoSaveEvent += testDay.TriggerAutoSaveFromEntry;
                                    }
                                }
                                foreach (var goal in testDay.Goals)
                                {
                                    goal.TriggerAutoSaveEvent += testDay.TriggerAutoSaveFromGoal;
                                }

                                testDay.Debrief.TriggerAutoSaveEvent += testDay.TriggerAutoSaveFromDebrief;


                                if (testDay != null)
                                    TestDays.Add(testDay);
                            }
                            catch (JsonException e)
                            {
                                // Invalid JSON file, carry on I guess
                            }
                        }
                    }
                }
            }
        }

        public void NewDayCommand()
        {
            var testDay = new TestDay(new TestLocation("NONE"), new Person("ESO/ASR"));
            testDay.TriggerAutoSaveEvent += SaveTestDay;
            TestDays.Add(testDay);
        }

        public void CloseTestDay(object sender, TestDay testDay)
        {
            if (testDay != null && TestDays.Contains(testDay))
            {
                TestDays.Remove(testDay);
            }
        }

        public void ExportTestDayCommand()
        {
            SelectedTestDay?.ExportToMarkdown();
        }

        public async void UploadTestDayToTestLog()
        {
            if (SelectedTestDay == null)
                return;
            var testDay = SelectedTestDay;
            var options = new JsonSerializerOptions { WriteIndented = true, };
            var jsonString = JsonSerializer.Serialize(testDay, options);

            var data = new Dictionary<string, string>()
            {
                {"testday",  jsonString},
                {"phaseId", TestPhaseId.ToString() }
            };

            var content = new FormUrlEncodedContent(data);
            try
            {
                var response = await client.PostAsync("http://127.0.0.1:8000/testlog/register/exterallog/", content);
                var responseString = await response.Content.ReadAsStringAsync();
            }
            catch (Exception)
            {

            }
        }
    }
}
