using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using MahApps.Metro.Controls;
using System.IO;

namespace AntMeBattleArena_Server
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            timer.Tick += Timer_Tick;
            fsw.Created += Fsw_Created;
            this.DataContext = vm;
        }

        private ViewModels.MainWindowViewModel vm = new ViewModels.MainWindowViewModel();
        private DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };
        private int currentTimer = 0;
        private int maxTimer = 1;
        private FileSystemWatcher fsw = new FileSystemWatcher();
        private DateTime fswTick = DateTime.Now;

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            vm.Players = new System.Collections.ObjectModel.ObservableCollection<ViewModels.PlayerViewModel>();
            PlayerView.ItemsSource = vm.Players;
            /*
            string assemblyFile = @"C:\Program Files (x86)\AntMe 1.7\AntMe.DemoAmeisen.dll";
            var ants = Tech.GetAntsFromAssembly(assemblyFile);
            foreach (var ant in ants)
            {
                vm.Players.Add(new ViewModels.PlayerViewModel(ant, new string[] { assemblyFile }));
            }
            vm.Players[2].LastUpdated = (DateTime.Now + new TimeSpan(4, 0, 0)).ToString();
            */
            //string sharePath = Path.Combine(Directory.GetCurrentDirectory(), "Players", "Share");
            string sharePath = @"C:\Users\mmertens\Desktop\Players\Share";
            fsw.Path = sharePath;
            fsw.Filter = "*.dll";
        }

        private void NumberOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private void NumberOnly_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextNumeric(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private bool IsTextNumeric(string text)
        {
            Regex regex = new Regex("^[0-9]+$"); //regex that matches disallowed text
            return regex.IsMatch(text);
        }

        private void TimerButton_Click(object sender, RoutedEventArgs e)
        {
            if(timer.IsEnabled == false)
            {
                string toParse = TimerTextBox.Text.Trim();
                if (String.IsNullOrEmpty(toParse))
                    toParse = "1";
                maxTimer = int.Parse(toParse) * 60;
                if(maxTimer == 0)
                    maxTimer = 1;
                timer.Start();

                fsw.EnableRaisingEvents = true;

                TimerButton.Content = "Timer stoppen";
                vm.TimeRemaining = new TimeSpan(0, 0, maxTimer - Math.Min(currentTimer, maxTimer));
            }
            else
            {
                timer.Stop();
                timer.IsEnabled = false;
                TimerButton.Content = "Timer starten";
                currentTimer = 0;

                fsw.EnableRaisingEvents = false;
            }
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            int t = ++currentTimer;
            vm.TimeRemaining = new TimeSpan(0, 0, maxTimer - Math.Min(currentTimer, maxTimer));

            if(t >= maxTimer)
            {
                currentTimer = 0;
                timer.Stop();
                timer.IsEnabled = false;
                TimerButton.Content = "Timer starten";

                fsw.EnableRaisingEvents = false;
            }
            await HandleFiles();
        }

        private async void Fsw_Created(object sender, FileSystemEventArgs e) // Registrierte neue Datei in Uploadordner
        {
            try
            {
                if (DateTime.Now.Subtract(fswTick).TotalMilliseconds > 1000) // Mindestwartezeit für Created-Event
                {
                    fswTick = DateTime.Now;

                    string fileName = e.FullPath;
                    FileInfo createdFile = new FileInfo(fileName);
                    vm.IncomingFiles.Add(createdFile); // Hinzufügen zur Abarbeitungsliste

                    System.Threading.Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
            }
        }

        protected bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            return false;
        }

        private async Task HandleFiles() // Asynchrones Abarbeiten der erkannten Dateien
        {
            for(int i = 0; i < vm.IncomingFiles.Count; i++) // Alle Dateien iterieren, die seit dem letzten Durchlauf erkannt wurden
            {
                FileInfo file = vm.IncomingFiles[i];
                if (!IsFileLocked(file))
                {
                    vm.IncomingFiles.RemoveAt(i);
                    var path = file.Directory.Parent;
                    string new_location = System.IO.Path.Combine(path.FullName, file.Name);
                    try
                    {
                        var content = Tech.GetAntsFromAssembly(file.FullName); // DLL Informationen abrufen
                        if(content.Count > 0)
                        {
                            if (File.Exists(new_location))
                            {
                                File.SetAttributes(new_location, FileAttributes.Normal);
                                if(new FileInfo(new_location).IsReadOnly)
                                {

                                }
                                File.Delete(new_location);
                            }

                            File.Move(file.FullName, new_location); // An interne Position verschieben
                            File.SetAttributes(new_location, FileAttributes.Normal);

                            foreach (var new_ in content) // Alle Völker aus der DLL iterieren
                            {
                                try
                                {
                                    bool found = false; // Falls bestehendes Volk -> Update
                                    foreach (var player in vm.Players)
                                    {
                                        if (new_.Guid == player.Guid) // Duplikat-Check
                                        {
                                            player.Apply(new_, new string[] { new_location });
                                            found = true;
                                            break;
                                        }
                                    }
                                    if (!found) // Falls nicht bestehendes Volk -> neu anlegen
                                    {
                                        vm.Players.Add(new ViewModels.PlayerViewModel(new_, new string[] { new_location }));
                                        vm.RaisePropertyChanged("Players"); // UI-Update
                                    }
                                    PlayerView.Items.Refresh();
                                } catch { }
                            }
                        }
                    }
                    catch
                    {

                    }
                }
            }
        }
    }
}
