using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8
{
    class Student : INotifyPropertyChanged
    {
        private string fName;
        private string lName;
        private int sID;
        private double GPA;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public string firstName
        {
            get { return firstName; }
            set { firstName = fName;
                OnPropertyChanged("firstName");
            }
        }

        public string lastName
        {
            get { return lastName; }
            set { lastName = lName;
                OnPropertyChanged("lastName");
            }
        }

        public int studentID
        {
            get { return studentID; }
            set { studentID = sID;
                OnPropertyChanged("studentID");
            }
        }

        public double sGPA
        {
            get { return GPA; }
            set { GPA = sGPA;
                OnPropertyChanged("GPA");
            }
        }

        public Student()
        {
            fName = "John";
            lName = "Scott";
            sID = 18530236;
            sGPA = 3.5;
        }
    }
}
