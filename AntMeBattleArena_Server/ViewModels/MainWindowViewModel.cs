using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntMeBattleArena_Server.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public string TimeString
        {
            get
            {
                string timer = TimeRemaining.ToString();
                if ((int)TimeRemaining.TotalMinutes >= 1)
                {
                    DateTime until1 = DateTime.Now + TimeRemaining;
                    string until2 = until1.ToString("HH:mm");
                    timer = $"{timer} (bis {until2})";
                }
                return timer;
            }
        }

        public TimeSpan TimeRemaining { get => timeRemaining; set { SetProperty(ref timeRemaining, value, "TimeRemaining"); OnPropertyChanged("TimeString"); } }
        private TimeSpan timeRemaining = new TimeSpan();

        public ObservableCollection<PlayerViewModel> Players { get => players; set => SetProperty(ref players, value, "Players"); }
        private ObservableCollection<PlayerViewModel> players;

        public List<System.IO.FileInfo> IncomingFiles { get => incomingFiles; set => SetProperty(ref incomingFiles, value, "IncomingFiles"); }
        private List<System.IO.FileInfo> incomingFiles = new List<System.IO.FileInfo>();

    }
}
