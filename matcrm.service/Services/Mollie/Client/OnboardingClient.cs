using matcrm.service.Services.Mollie.Client.Abstract;
using matcrm.data.Models.MollieModel.Onboarding.Request;
using matcrm.data.Models.MollieModel.Onboarding.Response;
using System.Net.Http;
using System.Threading.Tasks;
using matcrm.service.Services.Client;

namespace matcrm.service.Services.Mollie.Client {
    public class OnboardingClient : BaseMollieClient, IOnboardingClient {
        public OnboardingClient(string apiKey, HttpClient httpClient = null) : base(apiKey, httpClient) {
        }

        public async Task<OnboardingStatusResponse> GetOnboardingStatusAsync() {
            return await this.GetAsync<OnboardingStatusResponse>("onboarding/me").ConfigureAwait(false);
        }

        public async Task SubmitOnboardingDataAsync(SubmitOnboardingDataRequest request) {
            await this.PostAsync<object>("onboarding/me", request).ConfigureAwait(false);
        }
    }
}
