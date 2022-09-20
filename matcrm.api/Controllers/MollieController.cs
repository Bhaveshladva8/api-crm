using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using matcrm.data.Models.Dto;
using matcrm.data.Models.Dto.Mollie;
using matcrm.data.Models.MollieModel;
using matcrm.data.Models.MollieModel.Customer;
using matcrm.data.Models.MollieModel.Payment.Response;
using matcrm.data.Models.MollieModel.Subscription;
using matcrm.data.Models.Tables;
using matcrm.service.BusinessLogic;
using matcrm.service.Common;
using matcrm.service.Services;
using matcrm.service.Services.Mollie.Customer;
using matcrm.service.Services.Mollie.Payment;
using matcrm.service.Services.Mollie.Subscription;

namespace matcrm.api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class MollieController : Controller
    {
        // private readonly ICustomerClient _customerClient;
        private readonly ICustomerOverviewClient _customerOverviewClient;
        private readonly ICustomerStorageClient _customerStorageClient;
        private readonly IPaymentOverviewClient _paymentOverviewClient;
        private readonly IPaymentStorageClient _paymentStorageClient;
        private readonly ISubscriptionOverviewClient _subscriptionOverviewClient;
        private readonly ISubscriptionStorageClient _subscriptionStorageClient;

        private readonly ISubscriptionPlanService _subscriptionPlanService;
        private readonly ISubscriptionPlanDetailService _subscriptionPlanDetailService;
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly ISubscriptionTypeService _subscriptionTypeService;
        private readonly IMollieCustomerService _mollieCustomerService;
        private readonly IMollieSubscriptionService _mollieSubscriptionService;
        private readonly IUserService _userService;
        private readonly IEmailProviderService _emailProviderService;
        private readonly IEmailConfigService _emailConfigService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IEmailLogService _emailLogService;
        private IMapper _mapper;
        private SendEmail sendEmail;
        private int UserId = 0;



        public MollieController(
            // ICustomerClient customerClient,
            ICustomerOverviewClient customerOverviewClient,
            ICustomerStorageClient customerStorageClient,
            IPaymentOverviewClient paymentOverviewClient,
            IPaymentStorageClient paymentStorageClient,
            ISubscriptionOverviewClient subscriptionOverviewClient,
            ISubscriptionStorageClient subscriptionStorageClient,
            IMapper mapper,
            ISubscriptionPlanService subscriptionPlanService,
            ISubscriptionPlanDetailService subscriptionPlanDetailService,
            IUserSubscriptionService userSubscriptionService,
            ISubscriptionTypeService subscriptionTypeService,
            IUserService userService,
            IMollieCustomerService mollieCustomerService,
            IMollieSubscriptionService mollieSubscriptionService,
            IEmailTemplateService emailTemplateService,
            IEmailLogService emailLogService,
            IEmailProviderService emailProviderService,
            IEmailConfigService emailConfigService
        )
        {
            // _customerClient = customerClient;
            _customerOverviewClient = customerOverviewClient;
            _customerStorageClient = customerStorageClient;
            _paymentOverviewClient = paymentOverviewClient;
            _paymentStorageClient = paymentStorageClient;
            _subscriptionOverviewClient = subscriptionOverviewClient;
            _subscriptionStorageClient = subscriptionStorageClient;
            _subscriptionPlanService = subscriptionPlanService;
            _subscriptionPlanDetailService = subscriptionPlanDetailService;
            _userSubscriptionService = userSubscriptionService;
            _subscriptionTypeService = subscriptionTypeService;
            _userService = userService;
            _mollieCustomerService = mollieCustomerService;
            _mollieSubscriptionService = mollieSubscriptionService;
            _mapper = mapper;
            _emailTemplateService = emailTemplateService;
            _emailLogService = emailLogService;
            _emailProviderService = emailProviderService;
            _emailConfigService = emailConfigService;
            sendEmail = new SendEmail(emailTemplateService, emailLogService, emailConfigService, emailProviderService, mapper);
        }


        [HttpPost]
        public async Task<SubscriptionResponse> CreateCustomer(CreateCustomerModel model)
        {
            CustomerResponse customerResponseObj = new CustomerResponse();
            SubscriptionResponse subscriptionResponseObj = new SubscriptionResponse();
            if (string.IsNullOrEmpty(model.CustomerId) && !string.IsNullOrEmpty(model.Email) && !string.IsNullOrEmpty(model.Name))
            {
                customerResponseObj = await this._customerStorageClient.Create(model);
                model.CustomerId = customerResponseObj.Id;
            }
            else
            {

                CreatePaymentModel createPaymentModelObj = new CreatePaymentModel()
                {
                    Amount = Convert.ToDecimal(0.01),
                    Currency = PaymentCurrency.EUR,
                    CustomerId = model.CustomerId,
                    SequenceType = "first",
                    Description = "first payment from api"
                };
                await this._paymentStorageClient.Create(createPaymentModelObj);
                CreateSubscriptionModel createSubscriptionModelObj = new CreateSubscriptionModel();
                createSubscriptionModelObj.CustomerId = model.CustomerId;
                createSubscriptionModelObj.Amount.value = Convert.ToDecimal(100.00);
                createSubscriptionModelObj.Amount.Currency = PaymentCurrency.EUR;
                createSubscriptionModelObj.IntervalAmount = 1;

                createSubscriptionModelObj.Times = 10;
                createSubscriptionModelObj.IntervalPeriod = IntervalPeriod.Days;
                createSubscriptionModelObj.Description = "Test 1 subscription from api";

                subscriptionResponseObj = await this._subscriptionStorageClient.Create(createSubscriptionModelObj);
            }
            return subscriptionResponseObj;
        }

        [HttpPost]
        public async Task<PaymentResponse> CreatePayment(CreatePaymentModel model)
        {
            return await this._paymentStorageClient.Create(model);
            // return this.RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        public async Task<OperationResult<PaymentResponse>> AddUpdatePayment(UserSubscriptionDto model)
        {
            SubscriptionResponse subscriptionResponseObj = new SubscriptionResponse();
            PaymentResponse paymentResponseObj = new PaymentResponse();
            MollieCustomer? mollieCustomerObj = null;
            UserSubscription? userSubscriptionObj = null;

            ClaimsPrincipal user = this.User as ClaimsPrincipal;
            UserId = Convert.ToInt32(user.FindFirst(JwtRegisteredClaimNames.Sid).Value);

            var userObj = _userService.GetUserById(UserId);
            if (UserId != null && model.SubScriptionPlanId != null && model.SubscriptionTypeId != null)
            {

                UserSubscriptionDto userSubscriptionDto = new UserSubscriptionDto();
                userSubscriptionDto.SubscriptionTypeId = model.SubscriptionTypeId;
                userSubscriptionDto.SubScriptionPlanId = model.SubScriptionPlanId;
                userSubscriptionDto.UserId = UserId;
                userSubscriptionDto.IsSubscribed = true;
                userSubscriptionObj = await _userSubscriptionService.CheckInsertOrUpdate(userSubscriptionDto);

                CustomerResponse customerResponseObj = new CustomerResponse();
                CreateCustomerModel createCustomerModelObj = new CreateCustomerModel();
                mollieCustomerObj = _mollieCustomerService.GetByUser(UserId);
                if (mollieCustomerObj != null)
                {
                    createCustomerModelObj.CustomerId = mollieCustomerObj.CustomerId;
                    createCustomerModelObj.Email = mollieCustomerObj.User.Email;
                    createCustomerModelObj.Name = mollieCustomerObj.User.FirstName;
                }
                else
                {
                    if (userObj != null)
                    {
                        createCustomerModelObj.Email = userObj.Email;
                        createCustomerModelObj.Name = userObj.FirstName;
                        customerResponseObj = await this._customerStorageClient.Create(createCustomerModelObj);
                        MollieCustomerDto mollieCustomerDto = new MollieCustomerDto();
                        mollieCustomerDto.UserId = UserId;
                        mollieCustomerDto.CustomerId = customerResponseObj.Id;
                        mollieCustomerObj = await _mollieCustomerService.CheckInsertOrUpdate(mollieCustomerDto);
                    }
                }
                if (model.SubscriptionTypeId != null)
                {
                    var subscriptionTypeObj = _subscriptionTypeService.GetById(model.SubscriptionTypeId.Value);
                    if (model.SubScriptionPlanId != null)
                    {
                        var subscriptionPlanObj = _subscriptionPlanService.GetById(model.SubScriptionPlanId.Value);
                        if (subscriptionTypeObj.Name == "Yearly")
                        {
                            model.Price = (subscriptionPlanObj.YearlyPrice * 12);
                        }

                        decimal DEBITAMT = Convert.ToDecimal(string.Format("{0:F2}", model.Price));
                        CreatePaymentModel createPaymentModelObj = new CreatePaymentModel()
                        {
                            Amount = DEBITAMT,
                            Currency = PaymentCurrency.EUR,
                            CustomerId = mollieCustomerObj.CustomerId,
                            SequenceType = "first",
                            Description = "first payment from api"
                        };
                        paymentResponseObj = await this._paymentStorageClient.Create(createPaymentModelObj);

                        CreateSubscriptionModel createSubscriptionModelObj = new CreateSubscriptionModel();
                        createSubscriptionModelObj.CustomerId = mollieCustomerObj.CustomerId;
                        createSubscriptionModelObj.Amount.value = DEBITAMT;
                        createSubscriptionModelObj.Amount.Currency = PaymentCurrency.EUR;
                        // subscriptionModel.IntervalAmount = 1;

                        // subscriptionModel.Times = 12;
                        if (subscriptionTypeObj.Name == "Monthly")
                        {
                            createSubscriptionModelObj.IntervalPeriod = IntervalPeriod.Months;
                            createSubscriptionModelObj.IntervalAmount = 1;
                            // subscriptionModel.Times = 12;
                        }
                        else
                        {
                            createSubscriptionModelObj.IntervalPeriod = IntervalPeriod.Months;
                            createSubscriptionModelObj.IntervalAmount = 12;
                            // subscriptionModel.Times = 1;
                        }

                        Random generator = new Random();
                        String r = generator.Next(0, 1000000).ToString("D6");
                        // subscriptionModel.IntervalPeriod = IntervalPeriod.Days;
                        createSubscriptionModelObj.Description = "Test 1 subscription from api " + r;
                        var bbb = DateTime.Now.ToString("YYYY-MM-DD");
                        createSubscriptionModelObj.startDate = DateTime.Now.ToString("yyyy-MM-dd");

                        subscriptionResponseObj = await this._subscriptionStorageClient.Create(createSubscriptionModelObj);
                    }
                }
                MollieSubscriptionDto mollieSubscriptionDto = new MollieSubscriptionDto();
                mollieSubscriptionDto.SubscriptionId = subscriptionResponseObj.Id;
                mollieSubscriptionDto.PaymentId = paymentResponseObj.Id;
                mollieSubscriptionDto.UserId = UserId;
                var mollieSubscriptionObj = await _mollieSubscriptionService.CheckInsertOrUpdate(mollieSubscriptionDto);

                if (userObj != null)
                {
                    var userName = userObj.FirstName + " " + userObj.LastName;
                    await sendEmail.SendEmailForUserSubscription(userObj.Email, userName, null);
                }


            }

            return new OperationResult<PaymentResponse>(true, System.Net.HttpStatusCode.OK, "", paymentResponseObj);
        }

        [HttpGet]
        [Authorize]
        public async Task<OperationResult<PaymentResponse>> Payment()
        {
            PaymentResponse paymentResponseObj = new PaymentResponse();
            MollieCustomer? mollieCustomer = null;
            UserSubscription? userSubscription = null;

            ClaimsPrincipal user = this.User as ClaimsPrincipal;
            UserId = Convert.ToInt32(user.FindFirst(JwtRegisteredClaimNames.Sid).Value);

            MollieSubscription mollieSubscriptionObj = _mollieSubscriptionService.GetByUser(UserId);

            var userObj = _userService.GetUserById(UserId);
            if (UserId != null)
            {
                paymentResponseObj = await this._paymentStorageClient.GetPayment(mollieSubscriptionObj.PaymentId);

                if (paymentResponseObj.Status == "paid")
                {
                    var userSubscriptionObj = _userSubscriptionService.GetByUser(UserId);
                    var userSubscriptionDto = _mapper.Map<UserSubscriptionDto>(userSubscriptionObj);
                    userSubscriptionDto.IsSubscribed = true;
                    var data = await _userSubscriptionService.CheckInsertOrUpdate(userSubscriptionDto);
                    return new OperationResult<PaymentResponse>(true, System.Net.HttpStatusCode.OK, "", paymentResponseObj);
                }

            }

            return new OperationResult<PaymentResponse>(false, System.Net.HttpStatusCode.OK, "", paymentResponseObj);
        }

        [HttpDelete]
        public async Task<OperationResult<UserSubscriptionDto>> CancelSubscription()
        {
            UserSubscriptionDto? userSubscriptionDto = null;

            ClaimsPrincipal user = this.User as ClaimsPrincipal;
            UserId = Convert.ToInt32(user.FindFirst(JwtRegisteredClaimNames.Sid).Value);

            var userSubscriptionObj = _userSubscriptionService.GetByUser(UserId);
            var mollieSubscriptionObj = _mollieSubscriptionService.GetByUser(UserId);
            var mollieCustomerObj = _mollieCustomerService.GetByUser(UserId);
            if (mollieCustomerObj != null && mollieSubscriptionObj != null)
            {
                await this._subscriptionStorageClient.Cancel(mollieCustomerObj.CustomerId, mollieSubscriptionObj.SubscriptionId);
                var mollieSubscriptionDelete = _mollieSubscriptionService.DeleteMollieSubscription(mollieSubscriptionObj.Id);
                var userSubscriptionDelete = _userSubscriptionService.DeleteUserSubscription(userSubscriptionObj.Id);
                userSubscriptionDto = _mapper.Map<UserSubscriptionDto>(userSubscriptionObj);
                return new OperationResult<UserSubscriptionDto>(true, System.Net.HttpStatusCode.OK, "", userSubscriptionDto);
            }

            return new OperationResult<UserSubscriptionDto>(false, System.Net.HttpStatusCode.OK, "", userSubscriptionDto);
        }


        [HttpPost]
        public async Task<OperationResult<UserSubscriptionDto>> ContinueSubscription()
        {
            UserSubscriptionDto? userSubscriptionDto = null;

            ClaimsPrincipal user = this.User as ClaimsPrincipal;
            UserId = Convert.ToInt32(user.FindFirst(JwtRegisteredClaimNames.Sid).Value);

            var userSubscriptionObj = _userSubscriptionService.GetByUser(UserId);
            userSubscriptionObj.IsSubscribed = true;
            if (userSubscriptionObj.SubscribedOn != null)
            {
                userSubscriptionObj.SubscribedOn = userSubscriptionObj.SubscribedOn.Value.AddDays(30);
            }
            else
            {
                userSubscriptionObj.SubscribedOn = DateTime.UtcNow;
            }

            userSubscriptionDto = _mapper.Map<UserSubscriptionDto>(userSubscriptionObj);
            var AddUpdate = await _userSubscriptionService.CheckInsertOrUpdate(userSubscriptionDto);
            return new OperationResult<UserSubscriptionDto>(false, System.Net.HttpStatusCode.OK, "", userSubscriptionDto);
        }

    }
}