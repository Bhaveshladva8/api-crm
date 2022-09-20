using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace matcrm.service.Common
{
    public class Enums
    {
        public enum PlaceType
        {
            InSideTheTextBoxes = 1,
            AboveTheTextBoxes = 2
        }

        public enum OrientationType
        {
            LeftSideTab = 1,
            RightSideTab = 2,
            LowerLeftTab = 3,
            LowerRightTab = 4,
            LightBox = 5,
        }

        public enum ImagePlacementType
        {
            Left = 2,
            Right = 3,
            Hidden = 1,
        }

        public enum Actions
        {
            NONE = 3
        }

        public enum ProjectActivityEnum
        {
            Project_Created = 1,
            Project_Updated = 2,
            Project_Removed = 3,
            Priority_changed_for_this_project = 4
        }

        public enum EmployeeTaskActivityEnum
        {
            Task_Created = 1,
            Task_Updated = 2,
            Task_Removed = 3,
            Priority_changed_for_this_task = 4,
            Task_assigned_to_user = 5,
            Time_record_added = 6,
            Task_comment_created = 7,
            Task_comment_updated = 8,
            Task_comment_removed = 9,
            Document_uploaded = 10,
            Document_removed = 11,
            Assign_user_removed = 12
        }

        public enum EmployeeProjectActivityEnum
        {
            Project_Created = 1,
            Project_Updated = 2,
            Project_Removed = 3,
            Priority_changed_for_this_Project = 4,
            Project_assigned_to_user = 5,            
            Assign_user_removed = 6
        }
    }
}