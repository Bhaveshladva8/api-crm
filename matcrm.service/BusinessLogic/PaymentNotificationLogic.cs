using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using matcrm.data.Models.Dto;
using matcrm.service.Services;
using matcrm.service.Services.Mollie.Subscription;

namespace matcrm.service.BusinessLogic
{
    public class PaymentNotificationLogic
    {
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly ISubscriptionTypeService _subscriptionTypeService;
        private readonly IEmailProviderService _emailProviderService;
        private readonly IEmailConfigService _emailConfigService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IEmailLogService _emailLogService;
        private readonly IMollieSubscriptionService _mollieSubscriptionService;
        private readonly IMollieCustomerService _mollieCustomerService;
        private readonly ISubscriptionStorageClient _subscriptionStorageClient;
        private IMapper _mapper;
        private SendEmail sendEmail;

        public PaymentNotificationLogic(
            IUserSubscriptionService userSubscriptionService,
            ISubscriptionTypeService subscriptionTypeService,
            IEmailTemplateService emailTemplateService,
            IEmailLogService emailLogService,
            IEmailProviderService emailProviderService,
            IEmailConfigService emailConfigService,
            IMollieSubscriptionService mollieSubscriptionService,
            IMollieCustomerService mollieCustomerService,
            ISubscriptionStorageClient subscriptionStorageClient,
            IMapper mapper
            )
        {
            _userSubscriptionService = userSubscriptionService;
            _subscriptionTypeService = subscriptionTypeService;
            _mapper = mapper;
            _emailTemplateService = emailTemplateService;
            _emailLogService = emailLogService;
            _emailProviderService = emailProviderService;
            _emailConfigService = emailConfigService;
            _mollieSubscriptionService = mollieSubscriptionService;
            _mollieCustomerService = mollieCustomerService;
            _subscriptionStorageClient = subscriptionStorageClient;
            sendEmail = new SendEmail(emailTemplateService, emailLogService, emailConfigService, emailProviderService, mapper);
        }

        public async Task<List<UserSubscriptionDto>> PaymentNotification()
        {
            var subscriptionTypes = _subscriptionTypeService.GetAll();
            var userSubscriptionList = _userSubscriptionService.GetAll();

            var subscriptionMonthlyTypeObj = _subscriptionTypeService.GetByName("Monthly");
            var subscriptionYearlyTypeObj = _subscriptionTypeService.GetByName("Yearly");
            if (subscriptionMonthlyTypeObj != null && subscriptionYearlyTypeObj != null)
            {
                foreach (var userSubscriptionObj in userSubscriptionList)
                {
                    if (userSubscriptionObj.SubscriptionTypeId == subscriptionMonthlyTypeObj.Id)
                    {
                        if (userSubscriptionObj.SubscribedOn != null)
                        {
                            var subscribedOn = userSubscriptionObj.SubscribedOn.Value;
                            var Today = DateTime.Today;
                            TimeSpan diff = Today.Date - subscribedOn.Date;

                            if (diff.Days % 25 == 0)
                            {
                                var userName = userSubscriptionObj.User.FirstName + " " + userSubscriptionObj.User.LastName;
                                userSubscriptionObj.IsSubscribed = false;
                                var userSubscriptionDto = _mapper.Map<UserSubscriptionDto>(userSubscriptionObj);
                                var AddUpdate = await _userSubscriptionService.CheckInsertOrUpdate(userSubscriptionDto);
                                // var AddUpdate = await _userSubscriptionService.UpdateIsSubscribed(userSubscriptionObj, userSubscriptionObj.Id);
                                await sendEmail.SendEmailForExpireNotification(userSubscriptionObj.User.Email, userName, null);
                            }
                            else if (diff.Days == 29)
                            {
                                var userName = userSubscriptionObj.User.FirstName + " " + userSubscriptionObj.User.LastName;
                                await sendEmail.SendEmailForExpireNotification(userSubscriptionObj.User.Email, userName, null);
                            }

                            // Add logic for cancel subscription after 30 days
                            else if (diff.Days == 30)
                            {

                                UserSubscriptionDto? userSubscriptionDto = null;
                                    
                                var mollieSubscriptionObj = _mollieSubscriptionService.GetByUser(userSubscriptionObj.UserId.Value);
                                var mollieCustomerObj = _mollieCustomerService.GetByUser(userSubscriptionObj.UserId.Value);
                                if (mollieCustomerObj != null && mollieSubscriptionObj != null)
                                {
                                    await this._subscriptionStorageClient.Cancel(mollieCustomerObj.CustomerId, mollieSubscriptionObj.SubscriptionId);
                                    var mollieSubscriptionDelete = _mollieSubscriptionService.DeleteMollieSubscription(mollieSubscriptionObj.Id);
                                    var userSubscriptionDelete = _userSubscriptionService.DeleteUserSubscription(userSubscriptionObj.Id);
                                }

                                var userName = userSubscriptionObj.User.FirstName + " " + userSubscriptionObj.User.LastName;
                                await sendEmail.SendEmailForRemoveUserSubscription(userSubscriptionObj.User.Email, userName, null);
                            }
                        }
                        // else
                        // {
                        //     var UpdatedOn = userSubscriptionObj.UpdatedOn.Value;
                        //     var Today = DateTime.Today;
                        //     TimeSpan diff = Today.Date - UpdatedOn.Date;

                        //     if (diff.Days % 25 == 0)
                        //     {
                        //         var userName = userSubscriptionObj.User.FirstName + " " + userSubscriptionObj.User.LastName;
                        //         userSubscriptionObj.IsSubscribed = false;
                        //         var userSubscriptionDto = _mapper.Map<UserSubscriptionDto>(userSubscriptionObj);
                        //         var AddUpdate = await _userSubscriptionService.UpdateIsSubscribed(userSubscriptionObj, userSubscriptionObj.Id);
                        //         await sendEmail.SendEmailForExpireNotification(userSubscriptionObj.User.Email, userName, null);
                        //     }
                        //     else if (diff.Days == 29)
                        //     {
                        //         var userName = userSubscriptionObj.User.FirstName + " " + userSubscriptionObj.User.LastName;
                        //         await sendEmail.SendEmailForExpireNotification(userSubscriptionObj.User.Email, userName, null);
                        //     }

                        //     // Add logic for cancel subscription after 30 days
                        //     else if (diff.Days == 30)
                        //     {

                        //         UserSubscriptionDto? userSubscriptionDto = null;

                        //         var mollieSubscriptionObj = _mollieSubscriptionService.GetByUser(userSubscriptionObj.UserId.Value);
                        //         var mollieCustomerObj = _mollieCustomerService.GetByUser(userSubscriptionObj.UserId.Value);
                        //         if (mollieCustomerObj != null && mollieSubscriptionObj != null)
                        //         {
                        //             await this._subscriptionStorageClient.Cancel(mollieCustomerObj.CustomerId, mollieSubscriptionObj.SubscriptionId);
                        //             var mollieSubscriptionDelete = _mollieSubscriptionService.DeleteMollieSubscription(mollieSubscriptionObj.Id);
                        //             var userSubscriptionDelete = _userSubscriptionService.DeleteUserSubscription(userSubscriptionObj.Id);
                        //         }

                        //         var userName = userSubscriptionObj.User.FirstName + " " + userSubscriptionObj.User.LastName;
                        //         await sendEmail.SendEmailForRemoveUserSubscription(userSubscriptionObj.User.Email, userName, null);
                        //     }
                        // }
                    }
                    else if (userSubscriptionObj.SubscriptionTypeId == subscriptionYearlyTypeObj.Id)
                    {
                        if (userSubscriptionObj.SubscribedOn != null)
                        {
                            var subscribedOn = userSubscriptionObj.SubscribedOn.Value;
                            var Today = DateTime.Today;
                            TimeSpan diff = Today.Date - subscribedOn.Date;

                            if (diff.Days == 360)
                            {
                                var userName = userSubscriptionObj.User.FirstName + " " + userSubscriptionObj.User.LastName;
                                userSubscriptionObj.IsSubscribed = false;
                                var userSubscriptionDtoObj = _mapper.Map<UserSubscriptionDto>(userSubscriptionObj);
                                var AddUpdate = await _userSubscriptionService.CheckInsertOrUpdate(userSubscriptionDtoObj);
                                await sendEmail.SendEmailForExpireNotification(userSubscriptionObj.User.Email, userName, null);
                            }

                            // Add logic for cancel subscription after 1 year
                            else if (diff.Days == 365)
                            {

                                UserSubscriptionDto? userSubscriptionDto = null;

                                var mollieSubscriptionObj = _mollieSubscriptionService.GetByUser(userSubscriptionObj.UserId.Value);
                                var mollieCustomerObj = _mollieCustomerService.GetByUser(userSubscriptionObj.UserId.Value);
                                if (mollieCustomerObj != null && mollieSubscriptionObj != null)
                                {
                                    await this._subscriptionStorageClient.Cancel(mollieCustomerObj.CustomerId, mollieSubscriptionObj.SubscriptionId);
                                    var mollieSubscriptionDelete = _mollieSubscriptionService.DeleteMollieSubscription(mollieSubscriptionObj.Id);
                                    var userSubscriptionDelete = _userSubscriptionService.DeleteUserSubscription(userSubscriptionObj.Id);
                                }

                                var userName = userSubscriptionObj.User.FirstName + " " + userSubscriptionObj.User.LastName;
                                await sendEmail.SendEmailForRemoveUserSubscription(userSubscriptionObj.User.Email, userName, null);
                            }
                        }
                    }
                }
            }

            List<UserSubscriptionDto> userSubscritionDtoList = new List<UserSubscriptionDto>();

            userSubscritionDtoList = _mapper.Map<List<UserSubscriptionDto>>(userSubscriptionList);

            return new List<UserSubscriptionDto>(userSubscritionDtoList);

        }

    }
}