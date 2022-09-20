using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using matcrm.api.SignalR;
using matcrm.data.Models.Dto;
using matcrm.data.Models.Request;
using matcrm.data.Models.Response;
using matcrm.data.Models.Tables;
using matcrm.data.Models.ViewModels;
using matcrm.service.BusinessLogic;
using matcrm.service.Common;
using matcrm.service.Services;
using matcrm.data.Context;
using matcrm.service.Utility;

namespace Employee.api.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class EmployeeSubTaskController : Controller
    {
        private readonly IEmployeeTaskService _employeeTaskService;
        private readonly IEmployeeSubTaskService _employeeSubTaskService;
        private readonly IEmployeeChildTaskService _employeeChildTaskService;
        private readonly IEmployeeTaskUserSerivce _employeeTaskUserSerivce;
        private readonly IEmployeeSubTaskUserService _employeeSubTaskUserService;
        private readonly IEmployeeChildTaskUserService _employeeChildTaskUserService;
        private readonly IEmployeeTaskTimeRecordService _employeeTaskTimeRecordService;
        private readonly IEmployeeSubTaskTimeRecordService _employeeSubTaskTimeRecordService;
        private readonly IEmployeeChildTaskTimeRecordService _employeeChildTaskTimeRecordService;
        private readonly IWeClappService _weClappService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IEmployeeTaskAttachmentService _employeeTaskAttachmentService;
        private readonly IEmployeeSubTaskAttachmentService _employeeSubTaskAttachmentService;
        private readonly IEmployeeChildTaskAttachmentService _employeeChildTaskAttachmentService;
        private readonly IEmployeeTaskActivityService _employeeTaskActivityService;
        private readonly IEmployeeSubTaskActivityService _employeeSubTaskActivityService;
        private readonly IEmployeeChildTaskActivityService _employeeChildTaskActivityService;
        private readonly IEmployeeTaskCommentService _employeeTaskCommentService;
        private readonly IEmployeeSubTaskCommentService _employeeSubTaskCommentService;
        private readonly IEmployeeChildTaskCommentService _employeeChildTaskCommentService;
        private readonly IUserService _userService;
        private readonly IEmployeeTaskStatusService _employeeTaskStatusService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IEmailLogService _emailLogService;
        private readonly IEmailConfigService _emailConfigService;
        private readonly IEmailProviderService _emailProvider;
        private readonly IHubContext<BroadcastHub, IHubClient> _hubContext;
        private readonly OneClappContext _context;
        private IMapper _mapper;
        private SendEmail sendEmail;
        private int UserId = 0;
        private int TenantId = 0;

        public EmployeeSubTaskController(IEmployeeTaskService employeeTaskService,
            IEmployeeSubTaskService employeeSubTaskService,
            IEmployeeChildTaskService employeeChildTaskService,
            IEmployeeTaskUserSerivce employeeTaskUserSerivce,
            IEmployeeSubTaskUserService employeeSubTaskUserService,
            IEmployeeChildTaskUserService employeeChildTaskUserService,
            IEmployeeTaskTimeRecordService employeeTaskTimeRecordService,
            IEmployeeSubTaskTimeRecordService employeeSubTaskTimeRecordService,
            IEmployeeChildTaskTimeRecordService employeeChildTaskTimeRecordService,
            IWeClappService weClappService,
            IHostingEnvironment hostingEnvironment,
            IEmployeeTaskAttachmentService employeeTaskAttachmentService,
            IEmployeeSubTaskAttachmentService employeeSubTaskAttachmentService,
            IEmployeeChildTaskAttachmentService employeeChildTaskAttachmentService,
            IEmployeeTaskActivityService employeeTaskActivityService,
            IEmployeeSubTaskActivityService employeeSubTaskActivityService,
            IEmployeeChildTaskActivityService employeeChildTaskActivityService,
            IUserService userService,
            IEmployeeTaskCommentService employeeTaskCommentService,
            IEmployeeSubTaskCommentService employeeSubTaskCommentService,
            IEmployeeChildTaskCommentService employeeChildTaskCommentService,
            IEmployeeTaskStatusService employeeTaskStatusService,
            IEmailTemplateService emailTemplateService,
            IEmailLogService emailLogService,
            IEmailConfigService emailConfigService,
            IEmailProviderService emailProvider,
            IHubContext<BroadcastHub, IHubClient> hubContext,
            OneClappContext context,
            IMapper mapper)
        {
            _employeeTaskService = employeeTaskService;
            _employeeSubTaskService = employeeSubTaskService;
            _employeeChildTaskService = employeeChildTaskService;
            _employeeTaskUserSerivce = employeeTaskUserSerivce;
            _employeeSubTaskUserService = employeeSubTaskUserService;
            _employeeChildTaskUserService = employeeChildTaskUserService;
            _employeeTaskTimeRecordService = employeeTaskTimeRecordService;
            _employeeSubTaskTimeRecordService = employeeSubTaskTimeRecordService;
            _employeeChildTaskTimeRecordService = employeeChildTaskTimeRecordService;
            _weClappService = weClappService;
            _hostingEnvironment = hostingEnvironment;
            _employeeTaskAttachmentService = employeeTaskAttachmentService;
            _employeeSubTaskAttachmentService = employeeSubTaskAttachmentService;
            _employeeChildTaskAttachmentService = employeeChildTaskAttachmentService;
            _employeeTaskActivityService = employeeTaskActivityService;
            _employeeSubTaskActivityService = employeeSubTaskActivityService;
            _employeeChildTaskActivityService = employeeChildTaskActivityService;
            _userService = userService;
            _employeeTaskCommentService = employeeTaskCommentService;
            _employeeSubTaskCommentService = employeeSubTaskCommentService;
            _employeeChildTaskCommentService = employeeChildTaskCommentService;
            _employeeTaskStatusService = employeeTaskStatusService;
            _emailTemplateService = emailTemplateService;
            _emailLogService = emailLogService;
            _emailProvider = emailProvider;
            _emailConfigService = emailConfigService;
            _hubContext = hubContext;
            _context = context;
            _mapper = mapper;
            sendEmail = new SendEmail(emailTemplateService, emailLogService, emailConfigService, emailProvider, mapper);
        }

        // Save Time Record [Task]
        [Authorize(Roles = "Admin, TenantAdmin, TenantManager, TenantUser, ExternalUser")]
        [HttpPost]
        public async Task<OperationResult<EmployeeSubTaskAddUpdateResponse>> Add([FromBody] AddUpdateEmployeeSubTaskRequest requestModel)
        {
            ClaimsPrincipal user = this.User as ClaimsPrincipal;
            UserId = Convert.ToInt32(user.FindFirst(JwtRegisteredClaimNames.Sid).Value);
            TenantId = Convert.ToInt32(user.FindFirst("TenantId").Value);
            EmployeeSubTaskDto model = _mapper.Map<EmployeeSubTaskDto>(requestModel);
            // EmployeeSubTaskDto employeeSubTaskObj = new EmployeeSubTaskDto();
            // employeeSubTaskObj.IsActive = true;
            // employeeSubTaskObj.StatusId = employeeSubTask.StatusId;
            // employeeSubTaskObj.Description = employeeSubTask.Description;
            // employeeSubTaskObj.StartDate = employeeSubTask.StartDate;
            // employeeSubTaskObj.EndDate = employeeSubTask.EndDate;
            // employeeSubTaskObj.EmployeeTaskId = employeeSubTask.EmployeeTaskId;
            // // employeeTaskObj.Priority = employeeTask.Priority;

            // if (employeeSubTask.Id == null)
            // {
            //     employeeSubTaskObj.CreatedBy = UserId;
            // }
            // else
            // {
            //     employeeSubTaskObj.Id = employeeSubTaskObj.Id;
            //     employeeSubTaskObj.UpdatedBy = UserId;
            // }

            // For all subtask with completed status then main task automatic completed status
            // if (employeeTask.Id != null) {
            //     var taskId = employeeTask.Id.Value;
            //     var subTasks = _subTaskService.GetAllSubTaskByTask (taskId);
            //     if (employeeTask.UserId != null && subTasks.Count () > 0) {
            //         var userId = employeeTask.UserId.Value;
            //         var employeeTaskStatusList = _employeeTaskService.GetTaskByUser (userId);
            //         var finalStatus = employeeTaskStatusList.Where (t => t.IsFinalize == true).FirstOrDefault ();
            //         if (finalStatus != null) {
            //             var completedSubTaskCount = subTasks.Where (t => t.StatusId == finalStatus.Id).Count ();
            //             if (subTasks.Count () == completedSubTaskCount) {
            //                 taskObj.StatusId = finalStatus.Id;
            //             }
            //         }
            //     }
            // }
            if (model.Id == null)
            {
                model.CreatedBy = UserId;
            }
            else
            {
                model.UpdatedBy = UserId;
            }
            model.IsActive = true;

            var subTaskResult = await _employeeSubTaskService.CheckInsertOrUpdate(model);
            if (subTaskResult != null)
            {
                model.Id = subTaskResult.Id;
            }

            EmployeeSubTaskActivity employeeSubTaskActivityObj = new EmployeeSubTaskActivity();
            employeeSubTaskActivityObj.UserId = UserId;
            if (model.Id == null)
            {
                employeeSubTaskActivityObj.EmployeeSubTaskId = subTaskResult.Id;
                employeeSubTaskActivityObj.Activity = Enums.EmployeeTaskActivityEnum.Task_Created.ToString().Replace("_", " ");
            }
            else
            {
                employeeSubTaskActivityObj.EmployeeSubTaskId = subTaskResult.Id;
                employeeSubTaskActivityObj.Activity = Enums.EmployeeTaskActivityEnum.Task_Updated.ToString().Replace("_", " ");
            }
            var AddUpdate = await _employeeSubTaskActivityService.CheckInsertOrUpdate(employeeSubTaskActivityObj);

            if (model.AssignedUsers != null && model.AssignedUsers.Count() > 0)
            {
                foreach (var userObj in model.AssignedUsers)
                {
                    EmployeeSubTaskUserDto employeeSubTaskUserDto = new EmployeeSubTaskUserDto();
                    employeeSubTaskUserDto.EmployeeSubTaskId = subTaskResult.Id;
                    employeeSubTaskUserDto.UserId = UserId;
                    var isExist = _employeeSubTaskUserService.IsExistOrNot(employeeSubTaskUserDto);
                    var employeeSubTaskUserObj = await _employeeSubTaskUserService.CheckInsertOrUpdate(employeeSubTaskUserDto);
                    if (employeeSubTaskUserObj != null)
                    {
                        userObj.Id = employeeSubTaskUserObj.Id;
                    }

                    if (!isExist)
                    {
                        var userAssignDetails = _userService.GetUserById(UserId);
                        if (userAssignDetails != null)
                            await sendEmail.SendEmailEmployeeTaskUserAssignNotification(userAssignDetails.Email, userAssignDetails.FirstName + ' ' + userAssignDetails.LastName, model.Description, TenantId);
                        EmployeeSubTaskActivity employeeSubTaskActivityObj1 = new EmployeeSubTaskActivity();
                        employeeSubTaskActivityObj1.EmployeeSubTaskId = subTaskResult.Id;
                        employeeSubTaskActivityObj1.UserId = UserId;
                        employeeSubTaskActivityObj1.Activity = Enums.EmployeeTaskActivityEnum.Task_assigned_to_user.ToString().Replace("_", " ");
                        var AddUpdate1 = await _employeeSubTaskActivityService.CheckInsertOrUpdate(employeeSubTaskActivityObj1);
                    }
                }
                model.AssignedUsers = new List<EmployeeSubTaskUser>();
            }

            var response = _mapper.Map<EmployeeSubTaskAddUpdateResponse>(model);
            response.AssignedUsers = requestModel.AssignedUsers;
            return new OperationResult<EmployeeSubTaskAddUpdateResponse>(true, System.Net.HttpStatusCode.OK, "Task saved successfully.", response);
        }

        // Save Time Record [Task]
        [Authorize(Roles = "Admin, TenantAdmin, TenantManager, TenantUser, ExternalUser")]
        [HttpPut]
        public async Task<OperationResult<EmployeeSubTaskAddUpdateResponse>> Update([FromBody] AddUpdateEmployeeSubTaskRequest requestModel)
        {
            ClaimsPrincipal user = this.User as ClaimsPrincipal;
            UserId = Convert.ToInt32(user.FindFirst(JwtRegisteredClaimNames.Sid).Value);
            TenantId = Convert.ToInt32(user.FindFirst("TenantId").Value);
            EmployeeSubTaskDto model = _mapper.Map<EmployeeSubTaskDto>(requestModel);

            // For all subtask with completed status then main task automatic completed status
            // if (employeeTask.Id != null) {
            //     var taskId = employeeTask.Id.Value;
            //     var subTasks = _subTaskService.GetAllSubTaskByTask (taskId);
            //     if (employeeTask.UserId != null && subTasks.Count () > 0) {
            //         var userId = employeeTask.UserId.Value;
            //         var employeeTaskStatusList = _employeeTaskService.GetTaskByUser (userId);
            //         var finalStatus = employeeTaskStatusList.Where (t => t.IsFinalize == true).FirstOrDefault ();
            //         if (finalStatus != null) {
            //             var completedSubTaskCount = subTasks.Where (t => t.StatusId == finalStatus.Id).Count ();
            //             if (subTasks.Count () == completedSubTaskCount) {
            //                 taskObj.StatusId = finalStatus.Id;
            //             }
            //         }
            //     }
            // }
            if (model.Id == null)
            {
                model.CreatedBy = UserId;
            }
            else
            {
                model.UpdatedBy = UserId;
            }
            model.IsActive = true;

            var subTaskResult = await _employeeSubTaskService.CheckInsertOrUpdate(model);
            if (subTaskResult != null)
            {
                model.Id = subTaskResult.Id;
            }

            EmployeeSubTaskActivity employeeSubTaskActivityObj = new EmployeeSubTaskActivity();
            employeeSubTaskActivityObj.UserId = UserId;
            if (model.Id == null)
            {
                employeeSubTaskActivityObj.EmployeeSubTaskId = subTaskResult.Id;
                employeeSubTaskActivityObj.Activity = Enums.EmployeeTaskActivityEnum.Task_Created.ToString().Replace("_", " ");
            }
            else
            {
                employeeSubTaskActivityObj.EmployeeSubTaskId = subTaskResult.Id;
                employeeSubTaskActivityObj.Activity = Enums.EmployeeTaskActivityEnum.Task_Updated.ToString().Replace("_", " ");
            }
            var AddUpdate = await _employeeSubTaskActivityService.CheckInsertOrUpdate(employeeSubTaskActivityObj);

            if (model.AssignedUsers != null && model.AssignedUsers.Count() > 0)
            {
                foreach (var userObj in model.AssignedUsers)
                {
                    EmployeeSubTaskUserDto employeeSubTaskUserDto = new EmployeeSubTaskUserDto();
                    employeeSubTaskUserDto.EmployeeSubTaskId = subTaskResult.Id;
                    employeeSubTaskUserDto.UserId = UserId;
                    var isExist = _employeeSubTaskUserService.IsExistOrNot(employeeSubTaskUserDto);
                    var employeeSubTaskUserObj = await _employeeSubTaskUserService.CheckInsertOrUpdate(employeeSubTaskUserDto);
                    if (employeeSubTaskUserObj != null)
                    {
                        userObj.Id = employeeSubTaskUserObj.Id;
                    }

                    if (!isExist)
                    {
                        var userAssignDetails = _userService.GetUserById(UserId);
                        if (userAssignDetails != null)
                            await sendEmail.SendEmailEmployeeTaskUserAssignNotification(userAssignDetails.Email, userAssignDetails.FirstName + ' ' + userAssignDetails.LastName, model.Description, TenantId);
                        EmployeeSubTaskActivity employeeSubTaskActivityObj1 = new EmployeeSubTaskActivity();
                        employeeSubTaskActivityObj1.EmployeeSubTaskId = subTaskResult.Id;
                        employeeSubTaskActivityObj1.UserId = UserId;
                        employeeSubTaskActivityObj1.Activity = Enums.EmployeeTaskActivityEnum.Task_assigned_to_user.ToString().Replace("_", " ");
                        var AddUpdate1 = await _employeeSubTaskActivityService.CheckInsertOrUpdate(employeeSubTaskActivityObj1);
                    }
                }
                model.AssignedUsers = new List<EmployeeSubTaskUser>();
            }

            var response = _mapper.Map<EmployeeSubTaskAddUpdateResponse>(model);
            response.AssignedUsers = requestModel.AssignedUsers;
            return new OperationResult<EmployeeSubTaskAddUpdateResponse>(true, System.Net.HttpStatusCode.OK, "Task saved successfully.", response);
        }

        // Assign task to users
        [Authorize(Roles = "Admin, TenantAdmin, TenantManager, TenantUser, ExternalUser")]
        [HttpPost]
        public async Task<OperationResult<EmployeeSubTaskUser>> AssignToUser([FromBody] EmployeeSubTaskUserDto subTaskUser)
        {
            var assignSubTaskUserObj = await _employeeSubTaskUserService.CheckInsertOrUpdate(subTaskUser);
            return new OperationResult<EmployeeSubTaskUser>(true, System.Net.HttpStatusCode.OK, "", assignSubTaskUserObj);
        }

        [Authorize(Roles = "Admin, TenantAdmin, TenantManager, TenantUser, ExternalUser")]
        [HttpPost]
        public async Task<OperationResult<EmployeeSubTaskTimeRecordResponse>> TimeRecord([FromBody] AddUpdateEmployeeSubTaskTimeRecordRequest requestModel)
        {
            ClaimsPrincipal user = this.User as ClaimsPrincipal;
            UserId = Convert.ToInt32(user.FindFirst(JwtRegisteredClaimNames.Sid).Value);
            EmployeeSubTaskTimeRecordResponse response = new EmployeeSubTaskTimeRecordResponse();
            EmployeeSubTaskTimeRecordDto employeeSubTaskTimeRecordDto = new EmployeeSubTaskTimeRecordDto();
            employeeSubTaskTimeRecordDto = _mapper.Map<EmployeeSubTaskTimeRecordDto>(requestModel);
            EmployeeSubTaskTimeRecord employeeSubTaskTimeRecordObj = new EmployeeSubTaskTimeRecord();
            if (employeeSubTaskTimeRecordDto.Duration != null && employeeSubTaskTimeRecordDto.SubTaskId != null)
            {
                var subTaskTotalDuration = _employeeSubTaskTimeRecordService.GetTotalEmployeeSubTaskTimeRecord(employeeSubTaskTimeRecordDto.SubTaskId.Value);
                if (subTaskTotalDuration >= 0)
                {
                    employeeSubTaskTimeRecordDto.Duration = employeeSubTaskTimeRecordDto.Duration - subTaskTotalDuration;
                }
                employeeSubTaskTimeRecordDto.UserId = UserId;
                employeeSubTaskTimeRecordObj = await _employeeSubTaskTimeRecordService.CheckInsertOrUpdate(employeeSubTaskTimeRecordDto);
                EmployeeSubTaskActivity employeeSubTaskActivityObj = new EmployeeSubTaskActivity();
                employeeSubTaskActivityObj.EmployeeSubTaskId = employeeSubTaskTimeRecordObj.SubTaskId;
                employeeSubTaskActivityObj.UserId = UserId;
                employeeSubTaskActivityObj.Activity = Enums.EmployeeTaskActivityEnum.Time_record_added.ToString().Replace("_", " ");
                var AddUpdate1 = await _employeeSubTaskActivityService.CheckInsertOrUpdate(employeeSubTaskActivityObj);
                response = _mapper.Map<EmployeeSubTaskTimeRecordResponse>(employeeSubTaskTimeRecordObj);
                return new OperationResult<EmployeeSubTaskTimeRecordResponse>(true, System.Net.HttpStatusCode.OK, "", response);
            }
            else
            {
                var message = "EmployeeSubTaskId can not be null";
                if (requestModel.Duration == null)
                {
                    message = "Duration can not be null";
                }
                return new OperationResult<EmployeeSubTaskTimeRecordResponse>(false, System.Net.HttpStatusCode.OK, message, response);
            }

        }

        [Authorize(Roles = "Admin, TenantAdmin, TenantManager, TenantUser, ExternalUser")]
        [HttpPost]
        public async Task<OperationResult<List<EmployeeSubTaskAttachment>>> UploadFiles([FromForm] EmployeeSubTaskAttachmentRequest model)
        {
            ClaimsPrincipal user = this.User as ClaimsPrincipal;
            UserId = Convert.ToInt32(user.FindFirst(JwtRegisteredClaimNames.Sid).Value);
            List<EmployeeSubTaskAttachment> employeeSubTaskAttachmentList = new List<EmployeeSubTaskAttachment>();

            if (model.FileList == null) throw new Exception("File is null");
            if (model.FileList.Length == 0) throw new Exception("File is empty");

            EmployeeSubTaskActivity employeeSubTaskActivityObj = new EmployeeSubTaskActivity();
            employeeSubTaskActivityObj.EmployeeSubTaskId = model.EmployeeSubTaskId;
            employeeSubTaskActivityObj.UserId = UserId;
            employeeSubTaskActivityObj.Activity = Enums.EmployeeTaskActivityEnum.Document_uploaded.ToString().Replace("_", " ");

            foreach (IFormFile file in model.FileList)
            {
                // full path to file in temp location
                //var dirPath = _hostingEnvironment.WebRootPath + "\\EmployeeSubTaskUpload";
                var dirPath = _hostingEnvironment.WebRootPath + OneClappContext.EmployeeSubTaskUploadDirPath;

                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                var fileName = string.Concat(
                    Path.GetFileNameWithoutExtension(file.FileName + "_" + model.EmployeeSubTaskId),
                    DateTime.Now.ToString("yyyyMMdd_HHmmss"),
                    Path.GetExtension(file.FileName)
                );
                var filePath = dirPath + "\\" + fileName;

                if (OneClappContext.ClamAVServerIsLive)
                {
                    ScanDocument scanDocumentObj = new ScanDocument();
                    bool fileStatus = await scanDocumentObj.ScanDocumentWithClam(file);
                    if (fileStatus)
                    {
                        return new OperationResult<List<EmployeeSubTaskAttachment>>(false, System.Net.HttpStatusCode.OK, "Virus Found!");
                    }
                }

                using (var oStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    await file.CopyToAsync(oStream);
                }

                EmployeeSubTaskAttachmentDto employeeSubTaskAttchmentObj = new EmployeeSubTaskAttachmentDto();
                employeeSubTaskAttchmentObj.Name = fileName;
                employeeSubTaskAttchmentObj.EmployeeSubTaskId = model.EmployeeSubTaskId;
                employeeSubTaskAttchmentObj.UserId = UserId;
                var addedItem = await _employeeSubTaskAttachmentService.CheckInsertOrUpdate(employeeSubTaskAttchmentObj);
                employeeSubTaskAttachmentList.Add(addedItem);
            }
            var AddUpdate1 = await _employeeSubTaskActivityService.CheckInsertOrUpdate(employeeSubTaskActivityObj);

            await _hubContext.Clients.All.OnUploadEmployeeTaskDocumentEventEmit(model.EmployeeSubTaskId);

            return new OperationResult<List<EmployeeSubTaskAttachment>>(true, System.Net.HttpStatusCode.OK, "", employeeSubTaskAttachmentList);
        }

        [Authorize(Roles = "Admin, TenantAdmin, TenantManager, TenantUser, ExternalUser")]
        // [AllowAnonymous]
        [HttpGet("{EmployeeSubTaskId}")]
        public async Task<OperationResult<List<EmployeeSubTaskAttachment>>> Documents(long EmployeeSubTaskId)
        {

            List<EmployeeSubTaskAttachment> employeeSubTaskAttachmentList = new List<EmployeeSubTaskAttachment>();
            employeeSubTaskAttachmentList = _employeeSubTaskAttachmentService.GetAllByEmployeeSubTaskId(EmployeeSubTaskId);

            return new OperationResult<List<EmployeeSubTaskAttachment>>(true, System.Net.HttpStatusCode.OK, "", employeeSubTaskAttachmentList);
        }

        [AllowAnonymous]
        [HttpGet("{SubTaskAttachmentId}")]
        public async Task<OperationResult<string>> Document(long SubTaskAttachmentId)
        {
            var taskAttachmentObj = _employeeSubTaskAttachmentService.GetEmployeeSubTaskAttachmentById(SubTaskAttachmentId);

            //var dirPath = _hostingEnvironment.WebRootPath + "\\EmployeeSubTaskUpload";
            var dirPath = _hostingEnvironment.WebRootPath + OneClappContext.EmployeeSubTaskUploadDirPath;
            var filePath = dirPath + "\\" + taskAttachmentObj.Name;
            Byte[] newBytes = System.IO.File.ReadAllBytes(filePath);
            String file = Convert.ToBase64String(newBytes);
            if (file != "")
            {
                return new OperationResult<string>(true, System.Net.HttpStatusCode.OK, "File received successfully", file);
            }
            else
            {
                return new OperationResult<string>(false, System.Net.HttpStatusCode.OK, "Issue in downloading file.");
            }

        }

        // Get Task Detail by Id
        [Authorize(Roles = "Admin, TenantAdmin, TenantManager, TenantUser, ExternalUser")]
        [HttpGet("{EmployeeSubTaskId}")]
        public async Task<OperationResult<EmployeeSubTaskVM>> Detail(long EmployeeSubTaskId)
        {
            EmployeeSubTaskVM employeeSubTaskVMObj = new EmployeeSubTaskVM();

            // var AllCustomers = _customerService.GetAllCustomer();
            var AllUsers = _userService.GetAll();
            var subTaskObj = _employeeSubTaskService.GetSubTaskById(EmployeeSubTaskId);
            employeeSubTaskVMObj = _mapper.Map<EmployeeSubTaskVM>(subTaskObj);

            var taskTotalDuration = _employeeSubTaskTimeRecordService.GetTotalEmployeeSubTaskTimeRecord(EmployeeSubTaskId);
            //employeeSubTaskVMObj.Duration = taskTotalDuration;

            // For task assign users
            var assignUsers = _employeeSubTaskUserService.GetAssignUsersBySubTask(EmployeeSubTaskId);
            if (assignUsers != null && assignUsers.Count > 0)
            {
                var assignTaskUserVMList = _mapper.Map<List<EmployeeSubTaskUserDto>>(assignUsers);
                if (employeeSubTaskVMObj.AssignedUsers == null)
                {
                    employeeSubTaskVMObj.AssignedUsers = new List<EmployeeSubTaskUserDto>();
                }
                if (assignTaskUserVMList != null && assignTaskUserVMList.Count() > 0)
                {
                    foreach (var assignUser in assignTaskUserVMList)
                    {
                        var customerObj = AllUsers.Where(t => t.Id == assignUser.UserId).FirstOrDefault();
                        if (customerObj != null)
                        {
                            assignUser.Name = customerObj.UserName;
                            // assignUser.LastName = userObj.LastName;
                        }
                    }
                }
                employeeSubTaskVMObj.AssignedUsers = assignTaskUserVMList;
            }

            // For task documents
            var employeeSubTaskDocuments = _employeeSubTaskAttachmentService.GetAllByEmployeeSubTaskId(EmployeeSubTaskId);
            if (employeeSubTaskDocuments != null && employeeSubTaskDocuments.Count > 0)
            {
                if (employeeSubTaskVMObj.Documents == null)
                {
                    employeeSubTaskVMObj.Documents = new List<EmployeeSubTaskAttachment>();
                }
                employeeSubTaskVMObj.Documents = employeeSubTaskDocuments;
            }

            // For task commnets
            var employeeSubTaskComments = _employeeSubTaskCommentService.GetAllByEmployeeSubTaskId(EmployeeSubTaskId);
            if (employeeSubTaskComments != null && employeeSubTaskComments.Count > 0)
            {
                var employeeSubTaskCommentsVM = _mapper.Map<List<EmployeeSubTaskCommentDto>>(employeeSubTaskComments);
                if (employeeSubTaskCommentsVM != null && employeeSubTaskCommentsVM.Count() > 0)
                {
                    foreach (var employeeSubTaskCommentObj in employeeSubTaskCommentsVM)
                    {
                        var userObjCom = AllUsers.Where(t => t.Id == employeeSubTaskCommentObj.UserId).FirstOrDefault();
                        if (userObjCom != null)
                        {
                            employeeSubTaskCommentObj.FirstName = userObjCom.FirstName;
                            employeeSubTaskCommentObj.LastName = userObjCom.LastName;
                        }
                    }
                }
                if (employeeSubTaskVMObj.Comments == null)
                {
                    employeeSubTaskVMObj.Comments = new List<EmployeeSubTaskCommentDto>();
                }
                employeeSubTaskVMObj.Comments = employeeSubTaskCommentsVM;
            }

            // For task activities
            var employeeSubTaskActivities = _employeeSubTaskActivityService.GetAllByEmployeeSubTaskId(EmployeeSubTaskId);
            if (employeeSubTaskActivities != null && employeeSubTaskActivities.Count > 0)
            {
                var employeeSubTaskActivityDtoList = _mapper.Map<List<EmployeeSubTaskActivityDto>>(employeeSubTaskActivities);
                if (employeeSubTaskActivityDtoList != null && employeeSubTaskActivityDtoList.Count() > 0)
                {
                    foreach (var EmployeeSubTaskActivityObj in employeeSubTaskActivityDtoList)
                    {
                        var userObjAct = AllUsers.Where(t => t.Id == EmployeeSubTaskActivityObj.UserId).FirstOrDefault();
                        if (userObjAct != null)
                        {
                            EmployeeSubTaskActivityObj.FirstName = userObjAct.FirstName;
                            EmployeeSubTaskActivityObj.LastName = userObjAct.LastName;
                        }
                    }
                }
                if (employeeSubTaskVMObj.Activities == null)
                {
                    employeeSubTaskVMObj.Activities = new List<EmployeeSubTaskActivityDto>();
                }
                employeeSubTaskVMObj.Activities = employeeSubTaskActivityDtoList;
            }

            return new OperationResult<EmployeeSubTaskVM>(true, System.Net.HttpStatusCode.OK, "", employeeSubTaskVMObj);
        }

        [Authorize(Roles = "Admin, TenantAdmin, TenantManager, TenantUser, ExternalUser")]
        [HttpDelete("{EmployeeSubTaskId}")]
        public async Task<OperationResult<RemoveEmployeeSubTaskResponse>> Remove(long EmployeeSubTaskId)
        {
            ClaimsPrincipal user = this.User as ClaimsPrincipal;
            UserId = Convert.ToInt32(user.FindFirst(JwtRegisteredClaimNames.Sid).Value);
            TenantId = Convert.ToInt32(user.FindFirst("TenantId").Value);
            var childTasks = _employeeChildTaskService.GetAllChildTaskBySubTask(EmployeeSubTaskId);

            foreach (var item in childTasks)
            {
                var childTaskId = item.Id;

                var childDocuments = await _employeeChildTaskAttachmentService.DeleteAttachmentByChildTaskId(childTaskId);

                // Remove child task documents from folder

                foreach (var childTaskDoc in childDocuments)
                {

                    //var dirPath = _hostingEnvironment.WebRootPath + "\\ChildTaskUpload";
                    var dirPath = _hostingEnvironment.WebRootPath + OneClappContext.ChildTaskUploadDirPath;
                    var filePath = dirPath + "\\" + childTaskDoc.Name;

                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(Path.Combine(filePath));
                    }
                }

                var childComments = await _employeeChildTaskCommentService.DeleteCommentByChildTaskId(childTaskId);

                var childTimeRecords = await _employeeChildTaskTimeRecordService.DeleteTimeRecordByEmployeeChildTaskId(childTaskId);

                var childTaskUsers = await _employeeChildTaskUserService.DeleteByChildTaskId(childTaskId);

                var childTaskActivities = await _employeeChildTaskActivityService.DeleteByEmployeeChildTaskId(childTaskId);

                var childTaskToDelete = await _employeeChildTaskService.Delete(childTaskId);

                EmployeeChildTaskActivity employeeChildTaskActivityObj = new EmployeeChildTaskActivity();
                employeeChildTaskActivityObj.EmployeeChildTaskId = childTaskId;
                employeeChildTaskActivityObj.UserId = UserId;
                employeeChildTaskActivityObj.Activity = Enums.EmployeeTaskActivityEnum.Task_Removed.ToString().Replace("_", " ");
                var AddUpdate1 = await _employeeChildTaskActivityService.CheckInsertOrUpdate(employeeChildTaskActivityObj);
            }

            var subDocuments = await _employeeSubTaskAttachmentService.DeleteAttachmentByEmployeeSubTaskId(EmployeeSubTaskId);

            // Remove sub task documents from folder

            foreach (var subTaskDoc in subDocuments)
            {

                //var dirPath = _hostingEnvironment.WebRootPath + "\\SubTaskUpload";
                var dirPath = _hostingEnvironment.WebRootPath + OneClappContext.SubTaskUploadDirPath;
                var filePath = dirPath + "\\" + subTaskDoc.Name;

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(Path.Combine(filePath));
                }
            }

            var subComments = await _employeeSubTaskCommentService.DeleteCommentByEmployeeSubTaskId(EmployeeSubTaskId);

            var subTimeRecords = await _employeeSubTaskTimeRecordService.DeleteTimeRecordBySubTaskId(EmployeeSubTaskId);

            var subTaskUsers = await _employeeSubTaskUserService.DeleteBySubTaskId(EmployeeSubTaskId);

            var subTaskActivities = await _employeeSubTaskActivityService.DeleteByEmployeeSubTaskId(EmployeeSubTaskId);

            var subTaskToDelete = await _employeeSubTaskService.Delete(EmployeeSubTaskId);

            EmployeeSubTaskActivity employeeSubTaskActivityObj = new EmployeeSubTaskActivity();
            employeeSubTaskActivityObj.EmployeeSubTaskId = EmployeeSubTaskId;
            employeeSubTaskActivityObj.UserId = UserId;
            employeeSubTaskActivityObj.Activity = Enums.EmployeeTaskActivityEnum.Task_Removed.ToString().Replace("_", " ");
            var AddUpdate2 = await _employeeSubTaskActivityService.CheckInsertOrUpdate(employeeSubTaskActivityObj);

            var response = _mapper.Map<RemoveEmployeeSubTaskResponse>(subTaskToDelete);

            return new OperationResult<RemoveEmployeeSubTaskResponse>(true, System.Net.HttpStatusCode.OK, "Task deleted successfully", response);
        }

        private string GetMimeTypes(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }

        [Authorize(Roles = "Admin, TenantAdmin, TenantManager, TenantUser, ExternalUser")]
        [HttpGet("{EmployeeSubTaskId}")]
        public async Task<OperationResult<List<EmployeeSubTaskHistoryResponse>>> History(long EmployeeSubTaskId)
        {
            List<EmployeeSubTaskActivityDto> employeeSubTaskActivityList = new List<EmployeeSubTaskActivityDto>();
            var AllUsers = _userService.GetAll();
            var activities = _employeeSubTaskActivityService.GetAllByEmployeeSubTaskId(EmployeeSubTaskId);
            employeeSubTaskActivityList = _mapper.Map<List<EmployeeSubTaskActivityDto>>(activities);

            if (employeeSubTaskActivityList != null && employeeSubTaskActivityList.Count() > 0)
            {
                foreach (var item in employeeSubTaskActivityList)
                {
                    if (AllUsers != null)
                    {
                        var userObj = AllUsers.Where(t => t.Id == item.UserId).FirstOrDefault();
                        if (userObj != null)
                        {
                            item.FirstName = userObj.FirstName;
                            item.LastName = userObj.LastName;
                            item.Email = userObj.Email;
                            if (item.FirstName != null)
                            {
                                item.ShortName = item.FirstName.Substring(0, 1);
                            }
                            if (item.LastName != null)
                            {
                                item.ShortName = item.ShortName + item.LastName.Substring(0, 1);
                            }
                        }
                    }
                }
            }

            var response = _mapper.Map<List<EmployeeSubTaskHistoryResponse>>(employeeSubTaskActivityList);
            return new OperationResult<List<EmployeeSubTaskHistoryResponse>>(true, System.Net.HttpStatusCode.OK, "", response);
        }

        // Document delete method - Shakti
        [Authorize(Roles = "Admin, TenantAdmin, TenantManager, TenantUser, ExternalUser")]
        [HttpDelete("{Id}")]
        public async Task<OperationResult<RemoveEmployeeSubTaskAttachmentResponse>> RemoveDocument(long Id)
        {
            ClaimsPrincipal user = this.User as ClaimsPrincipal;
            UserId = Convert.ToInt32(user.FindFirst(JwtRegisteredClaimNames.Sid).Value);

            var deletedDocument = await _employeeSubTaskAttachmentService.DeleteEmployeeSubTaskAttachmentById(Id);

            await _hubContext.Clients.All.OnUploadEmployeeSubTaskDocumentEventEmit(deletedDocument.EmployeeSubTaskId);

            // Remove task documents from folder
            if (deletedDocument != null)
            {
                //var dirPath = _hostingEnvironment.WebRootPath + "\\EmployeeSubTaskUpload";
                var dirPath = _hostingEnvironment.WebRootPath + OneClappContext.EmployeeSubTaskUploadDirPath;
                var filePath = dirPath + "\\" + deletedDocument.Name;

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(Path.Combine(filePath));
                }
                EmployeeSubTaskActivity employeeSubTaskActivityObj = new EmployeeSubTaskActivity();
                employeeSubTaskActivityObj.EmployeeSubTaskId = deletedDocument.EmployeeSubTaskId;
                employeeSubTaskActivityObj.UserId = UserId;
                employeeSubTaskActivityObj.Activity = Enums.EmployeeTaskActivityEnum.Document_removed.ToString().Replace("_", " ");
                var AddUpdate = await _employeeSubTaskActivityService.CheckInsertOrUpdate(employeeSubTaskActivityObj);
                return new OperationResult<RemoveEmployeeSubTaskAttachmentResponse>(true, System.Net.HttpStatusCode.OK, "Doccument removed successfully", new RemoveEmployeeSubTaskAttachmentResponse());
            }
            else
            {
                return new OperationResult<RemoveEmployeeSubTaskAttachmentResponse>(false, System.Net.HttpStatusCode.OK, "Doccument not found", new RemoveEmployeeSubTaskAttachmentResponse());
            }
        }

        // Assigned task user delete method - Shakti
        [Authorize(Roles = "Admin, TenantAdmin, TenantManager, TenantUser, ExternalUser")]
        [HttpDelete("{SubTaskAssignUserId}")]
        public async Task<OperationResult<EmployeeTaskUserDto>> AssignUser(long SubTaskAssignUserId)
        {
            ClaimsPrincipal user = this.User as ClaimsPrincipal;
            UserId = Convert.ToInt32(user.FindFirst(JwtRegisteredClaimNames.Sid).Value);

                var assignUserRemove = await _employeeSubTaskUserService.DeleteAssignedSubTaskUser(SubTaskAssignUserId);

                if (assignUserRemove != null)
                {
                    EmployeeSubTaskActivity employeeSubTaskActivityObj = new EmployeeSubTaskActivity();
                    employeeSubTaskActivityObj.EmployeeSubTaskId = assignUserRemove.EmployeeSubTaskId;
                    employeeSubTaskActivityObj.UserId = UserId;
                    employeeSubTaskActivityObj.Activity = Enums.EmployeeTaskActivityEnum.Assign_user_removed.ToString().Replace("_", " ");
                    var AddUpdate = await _employeeSubTaskActivityService.CheckInsertOrUpdate(employeeSubTaskActivityObj);

                    if (assignUserRemove.UserId != null)
                    {
                        var removeTaskUserDetails = _userService.GetUserById(assignUserRemove.UserId.Value);
                        var subTaskUserdetails = _employeeSubTaskService.GetSubTaskById(assignUserRemove.EmployeeSubTaskId.Value);
                        if (removeTaskUserDetails != null && subTaskUserdetails != null)
                        {
                            await sendEmail.SendEmailRemoveEmployeeTaskUserAssignNotification(removeTaskUserDetails.Email, removeTaskUserDetails.FirstName + ' ' + removeTaskUserDetails.LastName, subTaskUserdetails.Description, TenantId);
                            return new OperationResult<EmployeeTaskUserDto>(true, System.Net.HttpStatusCode.OK, "User removed successfully from task.");
                        }
                        else
                        {
                            return new OperationResult<EmployeeTaskUserDto>(false, System.Net.HttpStatusCode.OK, "Something went wrong");
                        }
                    }
                }
            return new OperationResult<EmployeeTaskUserDto>(false, System.Net.HttpStatusCode.OK, "Remove user not found");
        }

    }
}