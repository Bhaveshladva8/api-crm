using matcrm.data.Models.MollieModel.Onboarding.Request;
using matcrm.data.Models.MollieModel.Onboarding.Response;
using System.Threading.Tasks;

namespace matcrm.service.Services.Mollie.Client.Abstract {
    public interface IOnboardingClient {
        /// <summary>
        /// Get the status of onboarding of the authenticated organization.
        /// </summary>
        Task<OnboardingStatusResponse> GetOnboardingStatusAsync();

        /// <summary>
        /// Submit data that will be prefilled in the merchant’s onboarding. Please note that the data
        /// you submit will only be processed when the onboarding status is needs-data.
        /// </summary>
        Task SubmitOnboardingDataAsync(SubmitOnboardingDataRequest request);
    }
}
