using AntMe.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntMeBattleArena_Server.ViewModels
{
    public class PlayerViewModel : BaseViewModel
    {
        public string Name { get { return name; } set { SetProperty(ref name, value, "Name"); } }
        private string name;

        public string PlayerName { get => playerName; set => SetProperty(ref playerName, value, "PlayerName"); }
        private string playerName;

        public string LastUpdated { get => lastUpdated.ToString("HH:mm"); set => lastUpdated = DateTime.Parse(value); }
        private DateTime lastUpdated;

        public string[] Files { get => files; set => SetProperty(ref files, value, "Files"); }
        private string[] files;

        public bool Static { get => static_; set => SetProperty(ref static_, value, "Static"); }
        private bool static_;

        public Guid Guid { get => guid; set => SetProperty(ref guid, value, "Guid"); }
        private Guid guid;

        public PlayerViewModel(string _name, string _playername, string _lastUpdated, string[] _files, bool _static, Guid _guid)
        {
            Name = _name;
            PlayerName = _playername;
            LastUpdated = _lastUpdated;
            Files = _files;
            Static = _static;
            Guid = _guid;
        }

        public PlayerViewModel(PlayerInfo p, string[] _files)
        {
            Apply(p, _files);
        }

        public void Apply(PlayerInfo p, string[] _files)
        {
            Name = p.ColonyName;
            PlayerName = $"{p.FirstName} {p.LastName}";
            LastUpdated = DateTime.Now.ToString();
            Files = _files;
            Static = p.Static;
            Guid = p.Guid;
        }
    }
}
