﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CombatManager
{
	/// <summary>
	/// Interaction logic for DieRollEditWindow.xaml
	/// </summary>
	public partial class DieRollEditWindow : Window
	{
        DieRoll _Roll;
        NotifyValue<int> _Mod;
        ObservableCollection<DieStep> _Steps;
        

		public DieRollEditWindow()
		{
			this.InitializeComponent();
            _Steps = new ObservableCollection<DieStep>();
            _Mod = new NotifyValue<int>();
            _Mod.PropertyChanged += new PropertyChangedEventHandler(Mod_PropertyChanged);
            DieRollBonusText.DataContext = _Mod;
			UpdateUI();
		}

        public DieRoll Roll
        {
            get
            {
                return _Roll;
            }
            set
            {
                _Roll = value;
                if (_Roll != null)
                {
                    _Steps = new ObservableCollection<DieStep>();
                    _Steps.Add(new DieStep(_Roll.Step.Count, _Roll.Step.Die));

                    if (_Roll.extraRolls != null)
                    {
                        foreach (DieStep s in _Roll.extraRolls)
                        {
                            _Steps.Add(s);
                        }
                    }

                    foreach (DieStep s in _Steps)
                    {
                        s.PropertyChanged += new PropertyChangedEventHandler(DieStep_PropertyChanged);
                    }

                    _Mod.Value = _Roll.mod;

                    DieStepList.DataContext = _Steps;
                    UpdateUI();
                }
                
            }
        }

        private DieRoll MakeRoll()
        {
            DieRoll roll = new DieRoll();

            if (_Steps.Count > 0)
            {
                roll.die = _Steps[0].Die;
                roll.count = _Steps[0].Count;
                

                if (_Steps.Count > 1)
                {
                    roll.extraRolls = new List<DieStep>();
                    for (int i = 1; i < _Steps.Count; i++)
                    {
                        roll.extraRolls.Add(_Steps[i]);
                    }

                }
            }

            roll.mod = _Mod.Value;

            return roll;
        }

		private void UpdateUI()
		{
			UpdateDieText();
			UpdateOK();
		}
		
        private void UpdateDieText()
        {
            DieRollText.Text = MakeRoll().Text;
        }

        private void UpdateOK()
        {
            OKButton.IsEnabled = MakeRoll().TotalCount > 0;
        }



        void Mod_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateUI();
        }


        void DieStep_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateUI();
        }

        private void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DieStep d = (DieStep)((FrameworkElement)sender).DataContext;

            _Steps.Remove(d);

            d.PropertyChanged -= new PropertyChangedEventHandler(DieStep_PropertyChanged);

            UpdateUI();

        }

        private void AddDieButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	DieStep d = new DieStep();
			d.PropertyChanged += new PropertyChangedEventHandler(DieStep_PropertyChanged);
			_Steps.Add(d);
			
			UpdateUI();
        }

        private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	Roll = MakeRoll();
			DialogResult = true;
			Close();
        }

	}
}