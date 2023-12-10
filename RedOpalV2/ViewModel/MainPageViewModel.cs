using RedOpalV2.Model; // Gets data
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls; // Required for ICommand

namespace RedOpalV2.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly PersonRepository _personRepository;
        public ObservableCollection<Person> People { get; private set; }

        public ICommand ViewProfileCommand { get; private set; }

        public MainPageViewModel()
        {
            _personRepository = new PersonRepository();
            People = new ObservableCollection<Person>();
            LoadPeopleAsync();
            ViewProfileCommand = new Command(async () =>
            {
                // Place a breakpoint on the next line
                await Shell.Current.GoToAsync("///StaffProfile");
            });
        }

        private async void LoadPeopleAsync()
        {
            var peopleFromDb = await _personRepository.GetAllPeople();
            foreach (var person in peopleFromDb)
            {
                People.Add(person);
            }
        }

        // Implement INotifyPropertyChanged interface
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

