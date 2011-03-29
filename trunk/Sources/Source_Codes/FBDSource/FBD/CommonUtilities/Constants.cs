using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBD.CommonUtilities
{
    public class Constants
    {
        /** NUMERIC VALUE */

        public const int NUMBER_OF_RANKING_STRUCTURE = 4;


        /*****************CLASS NAMES *******************/

        //CUSTOMER
        public const string CUSTOMER_BUSINESS_SCALE = "Customer business Scale";
        public const string CUSTOMER_BUSINESS = "business Customer";
        public const string CUSTOMER_BUSINESS_RANKING = "Ranked business Customer";
        public const string CUSTOMER_LOANTERM = "Loan term";
        public const string CUSTOMER_BUSINESS_FINANCIAL_INDEX = "Customer business financial index";

        //BUSINESS
        public const string BUSINESS_INDUSTRY = "business Industry";
        public const string BUSINESS_LINE = "business Line";
        public const string BUSINESS_RANK = "business Rank";
        public const string BUSINESS_RANKING_STRUCTURE = "business Ranking Structure";
        public const string BUSINESS_TYPE = "business Type";
        public const string BUSINESS_SCALE = "business Scale";
        public const string BUSINESS_SCALECRITERIA = "business Scale Criteria";

        public const string BUSINESS_FINANCIAL_INDEX = "business Financial Index";
        public const string BUSINESS_FINANCIAL_INDEX_LEVEL = "business Financial Index Level";
        public const string BUSINESS_FINANCIAL_INDEX_PROPORTION = "business Financial Index Proportion";
        public const string BUSINESS_FINANCIAL_INDEX_SCORE = "business Financial Index ScoreList";

        public const string BUSINESS_NON_FINANCIAL_INDEX = "business Non-Financial Index";
        public const string BUSINESS_NON_FINANCIAL_INDEX_LEVEL = "business Non-Financial Index Level";
        public const string BUSINESS_NON_FINANCIAL_INDEX_PROPORTION = "business Non-Financial Index Proportion";
        public const string BUSINESS_NON_FINANCIAL_INDEX_SCORE = "business Non-Financial Index ScoreList";

        //SYSTEM
        public const string SYSTEM_USER_GROUP = "System User Group";
        public const string SYSTEM_RIGHT = "System Right";
        public const string SYSTEM_USER_GROUP_RIGHT = "User Groups and Rights";
        public const string SYSTEM_REPORTING_PERIOD = "System Reporting Period";
        public const string SYSTEM_USER = "System User";
        public const string SYSTEM_BRANCH = "System Branch";
        public const string SYSTEM_LIST_RIGHTS = "list of Right for this User Group";

        //INV
        public const string BORROWING_PURPOSE = "Borrowing purpose";
        public const string INV_BASIC_INDEX = "Individual Basic Index";
        public const string INV_COLLATERAL_INDEX = "Individual collateral index";
        public const string INV_BASIC_RANK = "Individual Basic rank";
        public const string INV_SUMMARY_RANK = "Individual summary rank";
        public const string INV_BASIC_INDEX_SCORE = "Individual Basic Index score";
        public const string INV_BASIC_LEVEL_INDEX = "Individual Basic level Index";
        public const string INV_COLL_LEVEL_INDEX = "Individual collateral level Index";
        public const string INV_PROPORTION = "Individual Proportion";
        public const string INV_COLL_RANK = "Individual Collateral Rank";
        


        /****************** SYSTEM RIGHTS ******************/

        public const string RIGHT_PARAMETERS_VIEW = "001";
        public const string RIGHT_PARAMETERS_UPDATE = "002";
        public const string RIGHT_RANKING = "003";


        /***************** MESSAGE TYPES *****************/

        public const string SCC_MESSAGE = "SuccessfulMessage";
        public const string ERR_MESSAGE = "ErrorMessage";



        /************** ERROR MESSAGES ****************/

        // COMMON ERRORS
        public const string ERR_INDEX = "Error when displaying the list of {0}";
        public const string ERR_ADD_POST = "Error when adding new {0} due to duplicated ID or invalid data input. Please check again";
        public const string ERR_EDIT = "Error when select the {0}. Please try again";
        public const string ERR_EDIT_POST = "Error when editting {0}. Please recheck your input ";
        public const string ERR_DELETE = "Error when deleting the {0}. Try again later";
        public const string ERR_KEY_EXIST = "The ID already exists. Please try again!";
        public const string ERR_UNABLE_CHECK = "Error when checking ID dupplication. Please try again!";
        public const string ERR_CONTROLLER_PARSING = "Error when parsing information at server. Try again later";
        public const string ERR_INVALID_INDEX_ID = "The Index ID must be digits number!";
        
        // PROPORTION ERRORS
        public const string ERR_UPDATE_PROPORTION = "Error when updating to the index {0}. Try again later";
        public const string ERR_UPDATE_PROPORTION_CALCULATED = "Proportion changes were updated successfully but "
                                                                + "the summary proportion got errors. Please check "
                                                                + "the logic of proportion again";
        public const string ERR_UPDATE_PROPORTION_COMMON = "Error when updating propotion. Try again later";
        // SCORE ERRORS
        public const string ERR_UPDATE_SCORE = "Error when updating to the level {0}. Try again later";
        public const string ERR_UPDATE_SCORE_COMMON = "Error when updating score. Try again later";

        // AUTHENTICATION ERRORS
        public const string ERR_LOGIN_MATCH = "The username and password do not match. Please input again";
        public const string ERR_LOGIN_INPUT = "Unable to login at this time. Please come back a bit later";
        public const string ERR_CHANGE_PASS_MATCH = "The old password is invalid or confirm password does not match. Please input again";
        public const string ERR_CHANGE_PASS_INPUT = "Unable to change pass at this time. Please come back a bit later";
        public const string ERR_RESET_PASS = "Cannot reset this password. Plz come back later";

        //COMMON
        public const string ERR_TO_DATE_LESS_THAN_FROM_DATE = "To date must be greater than from date. Please input again";

        /************** SUCCESSFUL MESSAGES ****************/

        public const string SCC_ADD = "A new {0} has been added successfully";
        public const string SCC_EDIT_POST = "The {0} with ID {1} has been editted successfully";
        public const string SCC_DELETE = "A {0} has been deleted successfully";


        public const string SCC_UPDATE_PROPORTION = "Update proportion successfully";
        public const string SCC_UPDATE_SCORE = "Update score successfully";

        // AUTHENTICATION SUCCESS
        public const string SCC_CHANGE_PASS = "Your password has been changed successfully. You need to login with new password";
        public const string SCC_RESET_PASS = "The password has been reset successfully";

        /// <summary>
        /// Used for validating index rankingID
        /// </summary>
        public const string INDEX_FORMAT_STRING = "0123456789";

        public const string FORM_USER_ID = "UserID";
        public const string FORM_PASSWORD = "Password";
        public const string FORM_OLD_PASSWORD = "OldPassword";
        public const string FORM_NEW_PASSWORD = "NewPassword";
        public const string FORM_CONFIRM_NEW_Password = "ConfirmNewPassword";
        public const string FORM_USER_GROUP_ID = "UserGroupID";

        public const string SESSION_USER_ID = "UserID";
    }
}
