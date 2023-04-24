namespace AuthTest;

public partial class MainPage : BasePage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        Task.Run(async () =>
        {
            try
            {
                WebAuthenticatorResult authResult = await WebAuthenticator.Default.AuthenticateAsync(
                    new Uri("https://mysite.com/mobileauth/Microsoft"),
                    new Uri("authtest://"));

                string accessToken = authResult?.AccessToken;

                // Do something with the token
            }
            catch (TaskCanceledException e)
            {
                // Use stopped auth
            }
        });

    }
}

