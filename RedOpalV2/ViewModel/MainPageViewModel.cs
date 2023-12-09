using RedOpalV2.Model;//Gets data
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace RedOpalV2.ViewModel
{
    /*   internal class MainPageViewModel
       {
           //MODAL
           // People Person { get; set; } - just a test

           //Observable
           //[ObservableObject]
           //List<People> Person { get; set; }
       }
   }
    */
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Person>? _people;
        private readonly PersonRepository _personRepository;

        public ObservableCollection<Person> People
        {
            get
            {
                if (_people == null)
                {
                    _people = new ObservableCollection<Person>();
                    LoadPeopleAsync(); // Trigger async loading
                }
                return _people;
            }
        }

        public ICommand ViewProfileCommand { get; private set; }

        public MainPageViewModel()
        {
            _personRepository = new PersonRepository();
            ViewProfileCommand = new Command<Person>(async (person) => await ViewProfile(person));
        }

        private async void LoadPeopleAsync()
        {
            try
            {
                var peopleFromDb = await Task.Run(() => _personRepository.GetAllPeople());
                _people ??= new ObservableCollection<Person>();
                foreach (var person in peopleFromDb)
                {
                    _people.Add(person);
                }
            }
            catch (Exception)
            {
                // Handle any exceptions (e.g., display an error message)
            }
        }

        private async Task ViewProfile(Person person)
        {
            // Navigate to the profile page with the person's details
            // This will likely involve using Shell.Current.GoToAsync
            if (person != null)
            {
                await Shell.Current.GoToAsync($"profilepage?personId={person.Id}");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}