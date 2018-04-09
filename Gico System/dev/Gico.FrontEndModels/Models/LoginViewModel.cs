namespace Gico.FrontEndModels.Models
{
    public class LoginViewModel : PageModel
    {
        public LoginViewModel()
        {
        }

        public LoginViewModel(PageModel model) : base(model)
        {
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Remember { get; set; }
        
    }
}