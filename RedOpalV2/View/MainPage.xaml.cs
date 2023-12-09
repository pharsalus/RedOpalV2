using RedOpalV2.ViewModel;
namespace RedOpalV2
{
    public partial class MainPage : ContentPage
    {
    
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new MainPageViewModel();
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            // Button click event logic
        }
    }
}

    


