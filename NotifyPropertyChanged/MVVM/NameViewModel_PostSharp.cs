using System.ComponentModel;
using NotifyPropertyChanged.Aspects;

namespace NotifyPropertyChanged.MVVM
{
    public class NameViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedAspect("FullName")]
        public string FirstName { get; set; }
        
        [NotifyPropertyChangedAspect("FullName")]
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return string.Format("(PostSharp) {0} {1}", FirstName, LastName);
            }
        }
    }


//    public class NameViewModel
//    {
//        public string FirstName { get; set; }
//        public string LastName { get; set; }
//        public string FullName
//        {
//            get
//            {
//                return string.Format("(PostSharp Patterns) {0} {1}", FirstName, LastName);
//            }
//        }
//    }
}