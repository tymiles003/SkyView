﻿using System.Collections.Generic;
using SkyView.Image;
using System;
using System.ComponentModel;
using System.Windows;

namespace SkyView.Nodes {

    [Serializable]
    public class LogicalOutputPin : INotifyPropertyChanged {
        public LogicalOutputPin(LogicalNode parent, string name, Filter filter) {
            Parent = parent;
            Filter = filter;
            Name = name;
            TargetPins = new List<LogicalLink>();
        }

        public LogicalNode Parent;

        public Filter Filter {
            get { return _Filter; }
            set { _Filter = value; RaisePropertyChanged("Filter"); }
        }
        public string Shader {
            get { return _Shader; }
            set { _Shader = value; RaisePropertyChanged("Shader"); }
        }
        public List<LogicalLink> TargetPins {
            get { return _TargetPins; }
            set { _TargetPins = value; RaisePropertyChanged("TargetPins"); }
        }
        public string Name {
            get { return _Name; }
            set { _Name = value; RaisePropertyChanged("Name"); }
        }
        public Point Coordinates {
            get { return _Coordinates; }
            set { _Coordinates = value; RaisePropertyChanged("Coordinates"); }
        }

        private Filter _Filter;
        private string _Shader;
        private List<LogicalLink> _TargetPins;
        private string _Name;
        private Point _Coordinates;

        #region INotifyPropertyChanged
        protected void RaisePropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
