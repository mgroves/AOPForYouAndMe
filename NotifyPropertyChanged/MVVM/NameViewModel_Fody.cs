using PropertyChanged;

namespace NotifyPropertyChanged.MVVM
{
    [ImplementPropertyChanged]
    public class NameViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return string.Format("(Fody) {0} {1}", FirstName, LastName);
            }
        }
    }
}